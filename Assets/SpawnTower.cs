using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTower : MonoBehaviour { 
    public GameObject tower;
    public GameObject invalidTower;
    public GameObject player;
    public GameObject tile;
    public GameObject validTower;
    float floorPosY;
    float invalidTowerColliderY;
    float floorTransformPos;
    bool canPlace;
    bool invalidTowerSilhouetteVisible;
    Vector2 detectFloorVector;
    Vector2 silhouettePos;
    Vector2 mousePos;
    RaycastHit2D detectFloor;

    private void Start()
    {
        // Ensuring that the silhouettes are hidden when starting app.
        invalidTower.SetActive(false);
        validTower.SetActive(false);
        canPlace = false;
        invalidTowerSilhouetteVisible = false;
    }

    void Update () {
        // To ignore player collision with the tower silhouettes..
        Physics2D.IgnoreCollision(player.GetComponent<BoxCollider2D>(), invalidTower.GetComponent<BoxCollider2D>());
        Physics2D.IgnoreCollision(player.GetComponent<BoxCollider2D>(), invalidTower.GetComponent<BoxCollider2D>());
        // To convert the mouse position relative to camera to Unity's transform matrix.
        // We need two of these -- one that keeps track of the silhouette's position. This one will change depending on distance to the ground.
        // The mouse position will always keep track of the mouse position, no matter what.
        silhouettePos = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        mousePos = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        // To find the y value of the bottom of the invalidTower's BoxCollider2D.
        invalidTowerColliderY = invalidTower.transform.position.y - (invalidTower.GetComponent<BoxCollider2D>().size.y / 2);
        // To delegate the location of the bottom of the box collider to a Vector2.
        detectFloorVector = new Vector2(invalidTower.transform.position.x, invalidTowerColliderY);
        // This Raycast sets the origin from the bottom of the invalid tower collider, sends a ray straight down. A max ray length is not specified.
        // The max ray length should be "infinity", ensuring that it is always searching for a surface, preventing any issues with capping out if no
        // surface is found. This should be specified eventually.
        detectFloor = Physics2D.Raycast(detectFloorVector, -Vector2.up);
        // This method updates the y-value of the silhouettePos Vector2 based on distance from the ground. 
        UpdateY();
        // To keep the silhouettes lined up at all times.
        invalidTower.transform.position = silhouettePos;
        validTower.transform.position = silhouettePos;

        // When any key is pressed, this method will determine if a silhouette of a tower needs to appear.
        if (Input.anyKeyDown)
            ShowInvalidTowerSilhouette();

        // To determine if a silhouette should be visible or not. 
        if (invalidTowerSilhouetteVisible == false)
        { 
            canPlace = false;
            invalidTower.SetActive(false);
        }

        // To hide or show the tower based on the canPlace bool.
        if (canPlace == true)
            validTower.SetActive(true);

        else
            validTower.SetActive(false);
    }

    private void UpdateY()
    {
        if (invalidTowerColliderY != invalidTowerColliderY - detectFloor.distance)
            floorTransformPos = invalidTowerColliderY - detectFloor.distance;

        // Need this to perform snapping operation of the GameObject to the floor.
        if ((floorTransformPos >= invalidTowerColliderY || floorTransformPos < (invalidTowerColliderY + 2)) && mousePos.y < floorTransformPos + 4f)
        {
            silhouettePos.y = floorTransformPos + (invalidTower.GetComponent<BoxCollider2D>().size.y / 2);
            canPlace = true;
        }

        else
            canPlace = false;
            
    }

    private void ShowInvalidTowerSilhouette()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            invalidTowerSilhouetteVisible = true;
            invalidTower.SetActive(true);
        }

        if (invalidTowerSilhouetteVisible == true)
        {
            if (canPlace == true)
            { 
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    invalidTowerSilhouetteVisible = false;
                    canPlace = false;
                    SpawnTheTower();
                }
                if (Input.GetKeyDown(KeyCode.Mouse1)) { 
                    invalidTowerSilhouetteVisible = false;
                    canPlace = false;
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                    invalidTowerSilhouetteVisible = false;

                if (Input.GetKeyDown(KeyCode.Mouse1))
                    invalidTowerSilhouetteVisible = false;
            }
        }
    }

    private void SpawnTheTower()
    {
        // Instantiates the tower at the silhouette's position.
        Instantiate(tower, new Vector2(silhouettePos.x, silhouettePos.y), Quaternion.identity);
        invalidTower.SetActive(false);
    }
}
