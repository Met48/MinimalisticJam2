using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTower : MonoBehaviour {

    public GameObject target;
    float floorPos = -2.9f;
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Fire1"))
        {
            Vector3 p = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
            print(p);
            print(p.x);
            print(p.y);

            Instantiate(target, new Vector3(p.x, floorPos, 0.0f), Quaternion.identity);
        }
	}
}
