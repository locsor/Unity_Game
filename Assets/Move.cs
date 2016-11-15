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
    public List<Transform> map;
    public List<Transform> path;
    public int step = 0;
    private float a;
    private float b;
    private Transform[] Nodemap;
    private bool on_spot = false;
    private Transform potentialStartNode;
    private Rigidbody2D rb;
    private int counter;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        cam1 = cam;
        container.GetComponent<VariableStoreage>().xcord[unit_name] = transform.position.x;
        container.GetComponent<VariableStoreage>().ycord[unit_name] = transform.position.y;
        circle = gameObject.transform.FindChild("Cicle3");
        path = new List<Transform>();
        Nodemap = GetComponent<PathFinding>().all;
        //coll = GetComponent<SelectionRecatangle>().col;
    }

    // Update is called once per frame
    void Update()
    {
        map = GetComponent<PathFinding>().TotalPath;
        float x1 = 0, y1 = 0;
        float x2, y2, z2;
        float y3 = 0.1f, x3 = 0.1f;
        x2 = transform.position.x;
        y2 = transform.position.y;
        z2 = transform.position.z;
        if (Input.GetMouseButtonDown(1) && can_move) {
            target = cam.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(target, -Vector2.up);
            if (hit.collider.CompareTag("Tile"))
            {
                for (int i = 0; i < GetComponent<PathFinding>().node_list.Length; i++)
                {
                    if(GetComponent<PathFinding>().node_list[i].node.transform.position == hit.collider.transform.position)
                    {
                        GetComponent<PathFinding>().start_node = potentialStartNode;
                        GetComponent<PathFinding>().finish_node = GetComponent<PathFinding>().node_list[i];
                    }
                }
                GetComponent<PathFinding>().found = false;
                path.Clear();
                for (int i = 0; i < map.Count; i++)
                {
                    if (map[i] != null)
                    {
                        path.Add(map[i]);
                    }
                }
                step = 0;
                target = path[step].position;
                container.GetComponent<VariableStoreage>().target_arrx[unit_name - 1] = target.x;
                container.GetComponent<VariableStoreage>().target_arry[unit_name - 1] = target.y;
            }
        }
        x1 = container.GetComponent<VariableStoreage>().target_arrx[unit_name - 1];
        y1 = container.GetComponent<VariableStoreage>().target_arry[unit_name - 1];
        if(transform.position != target)
        {
<<<<<<< HEAD
            transform.position = Vector2.MoveTowards(transform.position, target, 0.5f*Time.deltaTime);
=======
            transform.position = Vector2.MoveTowards(transform.position, target, 2f*Time.deltaTime);
>>>>>>> 245d8fa0eb5068e123c9dcb270502275dffc82b9
        }
        else
        {
            step++;
            target = path[step].position;
            //Debug.Log(step);s
        }
        if(step == path.Count)
        {
            //Debug.Log('*');
            step--;
        }
        if (selection_script.GetComponent<Selection>().unit == 0 && Input.GetMouseButtonDown(0))
        {
          counter = 0;
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
    void OnMouseOver()
    {
         selection_script.GetComponent<Selection>().unit = unit_name;
    }
    void OnMouseExit()
    {
         selection_script.GetComponent<Selection>().unit = 0;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (is_selected == false && other.CompareTag("Selector") && other)
        {
            selection_script.GetComponent<Selection>().unit = 0;
            container.GetComponent<VariableStoreage>().selected_units.Add(gameObject);
            can_move = true;
            is_selected = true;
        }
        else if (other.CompareTag("Tile"))
        {
            potentialStartNode = other.transform;
        }
    }
}
