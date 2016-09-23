using UnityEngine;
using System.Collections;

public class Spawning : MonoBehaviour {
	public GameObject unit;
	public GameObject new_unit;
	private Vector3 target;
	public Camera cam;
	// Use this for initialization
	void Start () {
			
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (1)) {
			target = cam.ScreenToWorldPoint(Input.mousePosition);
			new_unit = Instantiate (unit, new Vector3 (target.x, target.y, -1), Quaternion.identity) as GameObject;
			VarStorage.NumberOfUnit = VarStorage.NumberOfUnit + 1;
			new_unit.GetComponent<Move>().cam = cam;
		}
	}
}
