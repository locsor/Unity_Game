using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

	public Vector3 target;
	public Camera cam;
	public static Camera cam1;
	void Start () {
		cam1 = cam;
	}
	
	// Update is called once per frame
	void Update () {
		float x1 = 0, y1 = 0, z1;
		float x2, y2, z2;
		float y3 = 0.1f, x3 = 0.1f, z3;
		x2 = transform.position.x;
		y2 = transform.position.y;
		z2 = transform.position.z;
		if (Input.GetMouseButtonDown(0))
		{
			target = cam.ScreenToWorldPoint(Input.mousePosition);
			Debug.Log(target);
		}
		x1 = target.x;
		y1 = target.y;
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
