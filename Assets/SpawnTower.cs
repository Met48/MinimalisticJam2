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
    RaycastHit2D detectFloor;
    Vector2 detectFloorVector;
    Vector3 silhouettePos;
    Vector3 mousePos;

    private void Start()
    {
        invalidTower.SetActive(false);
        validTower.SetActive(false);
        canPlace = false;
        invalidTowerSilhouetteVisible = false;
    }

    // Update is called once per frame
    void Update () {
        // To ignore player collision with invalidTower.
        Physics2D.IgnoreCollision(player.GetComponent<BoxCollider2D>(), invalidTower.GetComponent<BoxCollider2D>());
        Physics2D.IgnoreCollision(player.GetComponent<BoxCollider2D>(), invalidTower.GetComponent<BoxCollider2D>());
        // To convert the mouse position relative to camera to Unity's transform matrix.
        silhouettePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
        mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
        // To turn the invalidTower's GameObject position to the mouse position.
        invalidTower.transform.position = silhouettePos;
        validTower.transform.position = silhouettePos;
        // To find the y value of the bottom of the invalidTower's BoxCollider2D.
        invalidTowerColliderY = invalidTower.transform.position.y - (invalidTower.GetComponent<BoxCollider2D>().size.y / 2);
        // To delegate the location of the bottom of the box collider to a Vector2.
        detectFloorVector = new Vector2(invalidTower.transform.position.x, invalidTowerColliderY);
        detectFloor = Physics2D.Raycast(detectFloorVector, -Vector2.up, 10f);
        // To find the exact location of the surface beneath the object.
        UpdateY();
        invalidTower.transform.position = silhouettePos;
        validTower.transform.position = silhouettePos;

        if (Input.anyKeyDown)
            ShowInvalidTowerSilhouette();

        if (invalidTowerSilhouetteVisible == false)
            invalidTower.SetActive(false);

        if (canPlace == true)
        {
            validTower.SetActive(true);
        }

        else
        {
            validTower.SetActive(false);
        }
    }

    private void UpdateY()
    {
        if (invalidTowerColliderY != invalidTowerColliderY - detectFloor.distance)
            floorTransformPos = invalidTowerColliderY - detectFloor.distance;

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
                {
                    invalidTowerSilhouetteVisible = false;
                }
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    invalidTowerSilhouetteVisible = false;
                }
            }
        }
    }

    private void SpawnTheTower()
    {
        Instantiate(tower, new Vector3(silhouettePos.x, silhouettePos.y, 0.0f), Quaternion.identity);
        invalidTower.SetActive(false);
    }
}
