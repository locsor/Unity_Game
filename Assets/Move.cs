using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {
	public bool can_move = false;
	public Vector3 target;
	public Camera cam;
	public static Camera cam1;
	public GameObject container;
	public int unit_name;
	public GameObject selection_script;
	void Start () {
		cam1 = cam;
		container.GetComponent<VariableStoreage>().xcord [unit_name] = transform.position.x;
		container.GetComponent<VariableStoreage>().ycord [unit_name] = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		float x1 = 0, y1 = 0;
		float x2, y2, z2;
		float y3 = 0.1f, x3 = 0.1f;
		x2 = transform.position.x;
		y2 = transform.position.y;
		z2 = transform.position.z;
		if (Input.GetMouseButtonDown (0)) {
			target = cam.ScreenToWorldPoint (Input.mousePosition);
		}
		x1 = target.x;
		y1 = target.y;
		if (can_move == true) {
			if (Mathf.Round (transform.position.x) < Mathf.Round (x1)) {
				transform.position = new Vector3 (x2 + x3, y2, z2);
				x2 = transform.position.x;
			}
			if (Mathf.Round (transform.position.x) > Mathf.Round (x1)) {
				transform.position = new Vector3 (x2 - x3, y2, z2);
				x2 = transform.position.x;
			}
			if (Mathf.Round (transform.position.y) < Mathf.Round (y1)) {
				transform.position = new Vector3 (x2, y2 + y3, z2);
				y2 = transform.position.y;
			}
			if (Mathf.Round (transform.position.y) > Mathf.Round (y1)) {
				transform.position = new Vector3 (x2, y2 - y3, z2);
				y2 = transform.position.y;
			}
		}
	}
	void OnMouseOver()
	{
		selection_script.GetComponent<Selection> ().unit = unit_name;
	}
}
