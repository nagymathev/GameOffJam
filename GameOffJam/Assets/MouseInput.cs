using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MouseInput : MonoBehaviour
{
	public Camera cam;
	public float clickRange = 100.0f;
	public LayerMask clickMask;// = LayerMask.NameToLayer("Default");
	public Fungus.Flowchart flowChart;


	public float mousePressTime;
	public Vector3 mousePosPress;

	public Vector3 clickedPosWorld;
	public GameObject clickedObject;
	public Damageable clickedDamageable;

    public BODScript bodScript;


	void Start()
	{
		cam = Camera.main;

		if (!flowChart)
			flowChart = this.GetComponent<Fungus.Flowchart>();

        bodScript = GetComponent<BODScript>();
	}

	void Update()
	{
		if (EventSystem.current && EventSystem.current.IsPointerOverGameObject())
		{

		} else
		{
			if (Input.GetMouseButtonDown(0))
				mousePressTime = 0;

			if (Input.GetMouseButton(0))
			{
				//ToDo: save mousepos
				// these are pixel coordinates in window space (bottom left->right&up)
				if (mousePressTime > 0 && (Input.mousePosition - mousePosPress).sqrMagnitude > 1.0f)
				{
					//Cursor.lockState = CursorLockMode.Locked;
				} else
				{//holding without moving; detect a click and pass it to onfoot movement
					if (mousePressTime >= 0.2f)
						OnMouseHold(Input.mousePosition);
				}

				mousePosPress = Input.mousePosition;

				mousePressTime += Time.deltaTime;
			} else
			{
				if (mousePressTime > 0)
				{
					//Debug.Log("MPT " + mousePressTime);
					if (mousePressTime < 0.2f)
					{//click (as in, not hold)
						OnMouseClick(Input.mousePosition);
					}
					mousePressTime = 0;
				}
			}
		}
	}

	void OnMouseClick(Vector3 mousePos)
	{
		RaycastHit hit;

		Ray ray = cam.ScreenPointToRay(mousePos);
		Vector3 p1 = ray.origin;
		Vector3 dir = ray.direction;
		Debug.DrawRay(p1, dir * clickRange, Color.yellow, 1.0f);
		if (Physics.Raycast(p1, dir, out hit, clickRange, clickMask))
		{
			clickedPosWorld = hit.point;

            //print("Hit: " + hit.collider.name);

            //Call build or Destroy Script (bodScirpt)
            bodScript.WhatGotHit(hit.collider.gameObject);
            /*if (hit.collider.tag == "Ground" || hit.collider.tag == "Wall")
            {
                bodScript.WhatGotHit(hit.collider.gameObject);
            }*/

			Damageable target = hit.collider.GetComponentInParent<Damageable>();
			if (target)
			{
				Debug.Log(target.name, target);
				clickedObject = target.gameObject;
				clickedDamageable = target;
				flowChart.SetGameObjectVariable("ClickedObj", target.gameObject);
				flowChart.SendFungusMessage("ClickedObject");
			} else
			{
				Debug.Log(hit.point, hit.collider);
				//flowChart.SetVariable<Vector3>("ClickedPos", hit.point);
				flowChart.SendFungusMessage("ClickedPos");
			}
		}
	}

	void OnMouseHold(Vector3 mousePos)
	{

	}

	public Vector3 GetClickedPos()
	{
		return clickedPosWorld;
	}

	public GameObject GetClickedObj()
	{
		return clickedObject;
	}
}
