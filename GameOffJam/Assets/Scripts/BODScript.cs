using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BODScript : MonoBehaviour {

    //Build or Destroy Objects
    public GameObject wall;
    public GameObject tower;
    public GameObject tree;

    public GameObject toBuild;

    public string currentBODMode;

    public tileInfo tileInfoScript;

	// Use this for initialization
	void Start () {
        toBuild = null;
	}
	
	// Update is called once per frame
	void Update () 
    {

        if (Input.GetKeyDown(KeyCode.Q)) 
        {
            currentBODMode = "Tower";
        }
        if (Input.GetKeyDown(KeyCode.W)) 
        {
            currentBODMode = "Wall";
        }
        if (Input.GetKeyDown(KeyCode.E)) 
        {
            currentBODMode = "Tree";
        }

		
	}

    public void WhatGotHit(GameObject hitGO) 
    {
        if (hitGO.tag == "Ground") 
        {
            tileInfoScript = hitGO.GetComponentInParent<tileInfo>();
            BuildWall(hitGO.transform, tileInfoScript);
        }
        if(hitGO.tag != "Ground") 
        {
            print("To Destroy / notGround");
            print(hitGO.name);
            DestroyOnTile(hitGO.GetComponentInParent<ToDestroy>().gameObject);
        }
    }

    void BuildWall(Transform whereToBuild, tileInfo tileInfo) 
    {
        switch (currentBODMode) 
        {
            case "Tower":
                toBuild = tower;
                break;

            case "Wall":
                toBuild = wall;
                break;

            case "Tree":
                toBuild = tree;
                break;

            default:
                break;
        }
        if (toBuild != null)
        {
            //Instantiate(toBuild, whereToBuild.position, Quaternion.identity);
            tileInfo.onMe = Instantiate(toBuild, whereToBuild.position, Quaternion.identity);
        }
    }

    void DestroyOnTile(GameObject toDestroy) 
    {
        Destroy(toDestroy);
    }
}
