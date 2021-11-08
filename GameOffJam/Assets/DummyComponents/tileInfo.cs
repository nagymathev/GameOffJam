using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tileInfo : MonoBehaviour {

    public GameObject onMe;

    public MouseInput mouseInputScript;

	// Use this for initialization
	void Start () {
        onMe = null;

        RaycastHit hit;

        //Physics.Raycast(transform.position, transform.up, out hit, 2f);
        if(Physics.Raycast(transform.position, transform.up, out hit, 2f, mouseInputScript.clickMask))
        {
            print("Something onMe" + gameObject.name);
            //onMe = hit.collider.GetComponentInParent<ToDestroy>().gameObject;
        }
	}
	
	// Update is called once per frame
	void Update () {
        
	}
}
