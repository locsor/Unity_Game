using UnityEngine;
using System.Collections;

public class Spawning : MonoBehaviour {
	public GameObject unit;
	public GameObject[] new_unit;
	private Vector3 target;
	public Camera cam;
	public GameObject container;
	// Use this for initialization
	void Start () {
		new_unit = new GameObject[20];
	}
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (1)) {
			target = cam.ScreenToWorldPoint(Input.mousePosition);
			container.GetComponent<VariableStoreage>().NumberOfUnits = container.GetComponent<VariableStoreage>().NumberOfUnits + 1;
			new_unit[container.GetComponent<VariableStoreage>().NumberOfUnits] = Instantiate (unit, new Vector3 (target.x, target.y, -1), Quaternion.identity) as GameObject;
			new_unit[container.GetComponent<VariableStoreage>().NumberOfUnits].GetComponent<Move>().cam = cam;
			new_unit [container.GetComponent<VariableStoreage> ().NumberOfUnits].GetComponent<Move> ().container = container;
		}
	}
}
