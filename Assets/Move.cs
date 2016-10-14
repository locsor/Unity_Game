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
    private Transform[] map;
    public List<Transform> path;
    public int step= 0;
    private float a;
    private float b;
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
        map = GetComponent<PathFinding>().cameFrom;
        for(int i = 0; i < map.Length; i++)
        {
            if(map[i] != null)
            {
                path.Add(map[i]);
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
        }
        else if (!can_move)
        {
            target = transform.position;
        }
        x1 = container.GetComponent<VariableStoreage>().target_arrx[unit_name - 1];
        y1 = container.GetComponent<VariableStoreage>().target_arry[unit_name - 1];
        if (Mathf.Round(transform.position.x*10f)/10f < Mathf.Round((x1*10f)/10f)) {
            transform.position = new Vector3(x2 + x3, y2, z2);
            x2 = transform.position.x;
        }
        if (Mathf.Round(transform.position.x * 10f) / 10f > Mathf.Round((x1 * 10f) / 10f))
        {
            transform.position = new Vector3(x2 - x3, y2, z2);
            x2 = transform.position.x;
        }
        if (Mathf.Round(transform.position.y * 10f) / 10f < Mathf.Round((y1 * 10f) / 10f))
        {
            
            transform.position = new Vector3(x2, y2 + y3, z2);
            y2 = transform.position.y;
        }
        if (Mathf.Round(transform.position.y * 10f) / 10f > Mathf.Round((y1 * 10f) / 10f))
        {
            transform.position = new Vector3(x2, y2 - y3, z2);
            //Debug.Log('!');
            y2 = transform.position.y;
        }
        //Debug.Log(Mathf.Round(transform.position.y/10f)*10f);
        //Debug.Log(Mathf.Round(y1*10f)/10f);
        if (transform.position.x < target.x + 0.11f && transform.position.x > target.x - 0.11f && transform.position.y < target.y + 0.11f && transform.position.y > target.y - 0.11f)
        {
            step++;
        }
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
