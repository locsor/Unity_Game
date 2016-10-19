using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Move : MonoBehaviour {
    public bool can_move = false;
    public Vector3 target;
    public Camera cam;
    public static Camera cam1;
    public GameObject container;
    public int unit_name;
    public GameObject selection_script;
    private GameObject unit;
    public BoxCollider2D coll;
    public bool is_selected = false;
    private Transform circle;
    private bool selected = false;
    private List<Transform> map;
    public List<Transform> path;
    public int step= 0;
    private float a;
    private float b;
    private bool on_spot = false;
    void Start()
    {
        cam1 = cam;
        container.GetComponent<VariableStoreage>().xcord[unit_name] = transform.position.x;
        container.GetComponent<VariableStoreage>().ycord[unit_name] = transform.position.y;
        circle = gameObject.transform.FindChild("Cicle3");
        path = new List<Transform>();
        //coll = GetComponent<SelectionRecatangle>().col;
    }

    // Update is called once per frame
    void Update()
    {
        path.Clear();
        map = GetComponent<PathFinding>().TotalPath;
        for (int i = 0; i < map.Count; i++)
        {
            if (map[i] != null)
            {
                path.Add(map[step]);
            }
        }
        float x1 = 0, y1 = 0;
        float x2, y2, z2;
        float y3 = 0.1f, x3 = 0.1f;
        x2 = transform.position.x;
        y2 = transform.position.y;
        z2 = transform.position.z;
        if (Input.GetMouseButtonDown(1) && can_move) {
            target = cam.ScreenToWorldPoint(Input.mousePosition);
            container.GetComponent<VariableStoreage>().target_arrx[unit_name - 1] = target.x;
            container.GetComponent<VariableStoreage>().target_arry[unit_name - 1] = target.y;
        }
        else if (!can_move)
        {
            target = path[0].position;
            container.GetComponent<VariableStoreage>().target_arrx[unit_name - 1] = target.x;
            container.GetComponent<VariableStoreage>().target_arry[unit_name - 1] = target.y;
        }
        x1 = container.GetComponent<VariableStoreage>().target_arrx[unit_name - 1];
        y1 = container.GetComponent<VariableStoreage>().target_arry[unit_name - 1];
        if(transform.position != target)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, 3f*Time.deltaTime);
        }
        else
        {
            //Debug.Log(step);
            step++;
        }
        //if (Mathf.Round(transform.position.x) != target.x && Mathf.Round(transform.position.y) != target.y)
        //{
        //    if (transform.position.x < x1 && on_spot == false) {
        //        transform.position = new Vector3(x2 + x3, y2, z2);
        //        x2 = transform.position.x;
        //    }
        //    if (transform.position.x > x1 && on_spot == false)
        //    {
        //        transform.position = new Vector3(x2 - x3, y2, z2);
        //        x2 = transform.position.x;
        //    }
        //    if (transform.position.y < y1 && on_spot == false)
        //    {

        //        transform.position = new Vector3(x2, y2 + y3, z2);
        //        y2 = transform.position.y;
        //    }
        //    if (transform.position.y > y1 && on_spot == false)
        //    {
        //        transform.position = new Vector3(x2, y2 - y3, z2);
        //        //Debug.Log('!');
        //        y2 = transform.position.y;
        //    }
        //    if ((transform.position.x - x1 < 0.4f && transform.position.y - y1 < 0.4f) && (transform.position.x - x1 > -0.4f && transform.position.y - y1 > -0.4f))
        //    {
        //        on_spot = true;
        //        transform.position = new Vector3(x2 + transform.position.x - x1, y2 + transform.position.y - y1, z2);
        //        x2 = transform.position.x;
        //        y2 = transform.position.y;
        //        step++;
        //        on_spot = false;
        //    }
        //    Debug.Log(Mathf.Round(transform.position.x));
        //}
        //else if(Mathf.Round(transform.position.x) == target.x && Mathf.Round(transform.position.y) == target.y)
        //{
        //    Debug.Log("123456");
        //}
        //Debug.Log(Mathf.Round(transform.position.y/10f)*10f);
        //Debug.Log(Mathf.Round(y1*10f)/10f);
        if(step == path.Count)
        {
            //Debug.Log('*');
            step--;
        }
        if (selection_script.GetComponent<Selection>().unit == 0 && Input.GetMouseButtonDown(0))
        {
          can_move = false;
          is_selected = false;
          container.GetComponent<VariableStoreage>().selected_units.Clear();
        }
        if(is_selected == true)
        {
            circle.gameObject.SetActive(true);
        }
        else if(is_selected == false)
        {
            circle.gameObject.SetActive(false);
        }
    }
    void move()
    {

    }
    void OnMouseOver()
    {
         selection_script.GetComponent<Selection>().unit = unit_name;
    }
    void OnMouseExit()
    {
         selection_script.GetComponent<Selection>().unit = 0;
    }
    void OnTriggerEnter2D()
    {
        if (is_selected == false)
        {
            selection_script.GetComponent<Selection>().unit = 0;
            container.GetComponent<VariableStoreage>().selected_units.Add(gameObject);
            can_move = true;
            is_selected = true;
        }
    }
}
