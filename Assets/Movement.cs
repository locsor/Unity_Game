using UnityEngine;
using System.Collections;
public class click_move : MonoBehaviour
{
    public Vector2 target;
    public Camera cam;
    bool check = false;
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        float x1 = 0, y1 = 0, x2, y2;
        x2 = transform.position.x;
        y2 = transform.position.y;
        if (Input.GetMouseButtonDown(0))
        {
            target = cam.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log(target);
            check = false;
        }
        x1 = target.x / 100;
        y1 = target.y / 100;
        if (!check)
        {
            transform.position = new Vector2(x2 + x1, y2 + y1);
        }
        if ((Mathf.Round(transform.position.x) == Mathf.Round(target.x)) && (Mathf.Round(transform.position.y) == Mathf.Round(target.y)))
        {
            x1 = 0;
            y1 = 0;
            check = true;
        }
    }
}
