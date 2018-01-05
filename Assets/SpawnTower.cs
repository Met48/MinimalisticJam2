using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTower : MonoBehaviour {

    public GameObject tower;
    public GameObject invalidTower;
    public GameObject player;
    public GameObject tile;
    // float floorPos = -2.9f;
    bool towerSilhouetteVisible;
    Vector3 p;

    private void Start()
    {
        invalidTower.SetActive(false);
        towerSilhouetteVisible = false;
    }

    // Update is called once per frame
    void Update () {
        Physics2D.IgnoreCollision(player.GetComponent<BoxCollider2D>(), invalidTower.GetComponent<BoxCollider2D>());
        p = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
        invalidTower.transform.position = p;

        if (invalidTower.GetComponent<BoxCollider2D>().IsTouching(tile.GetComponent<Collider2D>()))
        {
            print("Is touching");
        }

        if (Input.anyKeyDown)
            ShowTowerSilhouette();

        if (towerSilhouetteVisible == false)
            invalidTower.SetActive(false);
    }

    private void ShowTowerSilhouette()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            towerSilhouetteVisible = true;
            invalidTower.SetActive(true);
        }

        if (towerSilhouetteVisible == true)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                towerSilhouetteVisible = false;
                SpawnTheTower();
            }
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                towerSilhouetteVisible = false;
            }
        }
    }

    private void SpawnTheTower()
    {
        Instantiate(tower, new Vector3(p.x, p.y, 0.0f), Quaternion.identity);
        invalidTower.SetActive(false);
    }
}
