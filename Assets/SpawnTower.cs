using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTower : MonoBehaviour {

    public GameObject tower;
    public GameObject invalidTower;
    public GameObject player;
    // float floorPos = -2.9f;
    bool towerSilhouetteVisible;
    Collision2D col;
    Vector3 p;

    private void Start()
    {
        invalidTower = GameObject.Find("InvalidTower");
        player = GameObject.Find("Player");

        invalidTower.SetActive(false);
        towerSilhouetteVisible = false;
    }

    // Update is called once per frame
    void Update () {
        p = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
        Physics2D.IgnoreCollision(player.GetComponent<BoxCollider2D>(), invalidTower.GetComponent<BoxCollider2D>());

        if (Input.anyKeyDown)
            ShowTowerSilhouette();

        if (towerSilhouetteVisible == false)
            invalidTower.SetActive(false);

        invalidTower.transform.position = p;
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
