using UnityEngine;
using System.Collections;

public class SelectionRecatangle : MonoBehaviour {
	public Vector3 position;
	public Vector3 click_pos;
	public Vector3 click_pos1;
    public BoxCollider2D col;
	void Start () {
		click_pos1.x = -1000f;
		click_pos1.y = -1000f;
        col = GetComponent<BoxCollider2D>();
	}

	void Update () {
		position = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		transform.position = new Vector3(position.x, position.y, 0);
		click_pos = transform.position;
		if ((Input.GetMouseButtonDown (0))&&(click_pos1.x == -1000f )&&(click_pos1.y == -1000f)) {
			click_pos1 = click_pos;
		}
		if (Input.GetMouseButton (0)){
			transform.position = new Vector3 ((position.x + click_pos1.x)/2, (position.y + click_pos1.y)/2, 0);
			transform.localScale = new Vector3 (Mathf.Abs (click_pos.x - click_pos1.x)*25, Mathf.Abs (click_pos.y - click_pos1.y)*25, 0);
        }
        if (Input.GetMouseButtonUp(0))
        {
            click_pos1.x = -1000f;
            click_pos1.y = -1000f;
            transform.localScale = new Vector3(0, 0, 0);
        }
    }
}
