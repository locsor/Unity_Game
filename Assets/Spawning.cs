﻿using UnityEngine;
using System.Collections;

public class Spawning : MonoBehaviour {
	public GameObject unit;
	public GameObject[] new_unit;
	private Vector3 target;
	public Camera cam;
	public GameObject container;
	public GameObject selection;
	private int unit_counter;
    public GameObject mesh;
	void Start () {
		new_unit = new GameObject[20];
<<<<<<< HEAD
        //add(new Vector3(-1.5f, -0.5f, -1));
        //add(new Vector3(-1.5f, 0.5f, -1));
        //add(new Vector3(-1.5f, 1.5f, -1));
=======
        add(new Vector3(-1.5f, -0.5f, -1));
        add(new Vector3(-1.5f, 0.5f, -1));
        add(new Vector3(-1.5f, 1.5f, -1));
>>>>>>> 54196b18866191fde8018fcfcdc0d22de1d9576c
        add(new Vector3(-0.5f, -0.5f, -1));
    }
	void Update () {
		if (Input.GetKeyDown (KeyCode.E)) {
			target = cam.ScreenToWorldPoint(Input.mousePosition);
			container.GetComponent<VariableStoreage> ().NumberOfUnits++;
			unit_counter = container.GetComponent<VariableStoreage> ().NumberOfUnits;
			new_unit[unit_counter] = Instantiate (unit, new Vector3 (Mathf.Round(target.x), Mathf.Round(target.y), -1), Quaternion.identity) as GameObject;
			new_unit[unit_counter].GetComponent<Move>().cam = cam;
			new_unit [unit_counter].GetComponent<Move> ().container = container;
			new_unit [unit_counter].GetComponent<Move> ().unit_name = unit_counter;
			new_unit [unit_counter].GetComponent<PickUp> ().container = container;
			new_unit [unit_counter].GetComponent<Move> ().selection_script = selection;
            new_unit[unit_counter].name = "Unit " + unit_counter;
            container.GetComponent<VariableStoreage>().target_arrx.Add(0f);
            container.GetComponent<VariableStoreage>().target_arry.Add(0f);
        }
	}
    void add(Vector3 position)
    {
        //target = cam.ScreenToWorldPoint(Input.mousePosition);
        container.GetComponent<VariableStoreage>().NumberOfUnits++;
        unit_counter = container.GetComponent<VariableStoreage>().NumberOfUnits;
        new_unit[unit_counter] = Instantiate(unit, position, Quaternion.identity) as GameObject;
        new_unit[unit_counter].GetComponent<Move>().cam = cam;
        new_unit[unit_counter].GetComponent<Move>().container = container;
        new_unit[unit_counter].GetComponent<Move>().unit_name = unit_counter;
        new_unit[unit_counter].GetComponent<PickUp>().container = container;
        new_unit[unit_counter].GetComponent<Move>().selection_script = selection;
        new_unit[unit_counter].name = "Unit " + unit_counter;
        container.GetComponent<VariableStoreage>().target_arrx.Add(0f);
        container.GetComponent<VariableStoreage>().target_arry.Add(0f);
    }
}
