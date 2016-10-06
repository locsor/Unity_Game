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
		transform.position = new Vector3(position.x, position.y, -1);
		click_pos = transform.position;
		if ((Input.GetMouseButtonDown (0))&&(click_pos1.x == -1000f )&&(click_pos1.y == -1000f)) {
			click_pos1 = click_pos;
		}
		if (Input.GetMouseButton (0)){
			transform.position = new Vector3 ((position.x + click_pos1.x)/2, (position.y + click_pos1.y)/2, -1);
			transform.localScale = new Vector3 (Mathf.Abs (click_pos.x - click_pos1.x), Mathf.Abs (click_pos.y - click_pos1.y), 0);
            col.size = new Vector3(1.0f,1.0f);
        }
        if (Input.GetMouseButtonUp(0))
        {
            click_pos1.x = -1000f;
            click_pos1.y = -1000f;
            transform.localScale = new Vector3(0, 0, 0);
        }
    }
}
