using UnityEngine;
using System.Collections;

public class Selection : MonoBehaviour {
	private int unit;
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
			get_unit (target);
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
	void get_unit(Vector3 mouse_pos)
	{
		for (int i = 1; i < 20; i++) {
			if ((Mathf.Round(mouse_pos.x) == Mathf.Round(container.GetComponent<VariableStoreage> ().xcord [i])) && (Mathf.Round(mouse_pos.y) == Mathf.Round(container.GetComponent<VariableStoreage> ().ycord [i]))) {
				unit = i;
				Debug.Log ('*');
			} 
			else {
			}
		}
	}
}
