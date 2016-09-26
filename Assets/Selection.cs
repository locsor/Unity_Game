using UnityEngine;
using System.Collections;

public class Selection : MonoBehaviour {
	public int unit;
	public GameObject container;
	public Camera cam;
	public GameObject spawn;
	private Vector3 target;
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		target = cam.ScreenToWorldPoint (Input.mousePosition);
		if (Input.GetKeyDown (KeyCode.Space)) {
			select_func (unit);
		}
	}
	void select_func(int i)
	{
		spawn.GetComponent<Spawning> ().new_unit [i].GetComponent<Move> ().can_move = true;
		for ( int j = 1; j < 20; j++){
			if (j != i) {
				spawn.GetComponent<Spawning> ().new_unit [j].GetComponent<Move> ().can_move = false;
			}
		}
	}
}
