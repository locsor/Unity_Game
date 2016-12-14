using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Logic : MonoBehaviour {

    
    private List<NewBehaviourScript> U;
    private List<NewBehaviourScript> E;
    private PathFinding path;
    private delegate void G(List<Vector2> P);
    private delegate List<List<Vector2>> P(List<Vector2> CurrentPoition, Vector2 PositionToMove);
    private P p;
    private delegate List<Vector2> P1(Vector2 CurrentPoition, Vector2 PositionToMove);
    private P1 p1;
    // Use this for initialization
    void Start ()
    {
        U = new List<NewBehaviourScript>();
        E = new List<NewBehaviourScript>();
        E.Add(GameObject.Find("Enemy (1)").GetComponent<NewBehaviourScript>());
        E.Add(GameObject.Find("Enemy (2)").GetComponent<NewBehaviourScript>());
        E.Add(GameObject.Find("Enemy (3)").GetComponent<NewBehaviourScript>());
        U.Add(GameObject.Find("Unit").GetComponent<NewBehaviourScript>());
        U.Add(GameObject.Find("Unit1").GetComponent<NewBehaviourScript>());
        U.Add(GameObject.Find("Unit2").GetComponent<NewBehaviourScript>());
        path = GameObject.Find("PathFindingMan").GetComponent<PathFinding>();
    }
    void Update()
    {
        //U[0].Attack(GameObject.Find("Enemy (1)"));
        if (Input.GetButtonDown("Fire1"))
        {
            Camera camera = GameObject.Find("Main Camera").GetComponent<Camera>();
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                string name = hit.collider.gameObject.tag;
                switch (name)
                {
                    case "Enemy":
                        foreach (NewBehaviourScript kek in U)
                        {
                            kek.Attack(hit.collider.gameObject);
                            kek.aim = hit.collider.gameObject;
                        }
                        break;
                    case "YourUnit":
                        if (!Input.GetButton("Left Ctrl"))
                            U = new List<NewBehaviourScript> { hit.collider.gameObject.GetComponent<NewBehaviourScript>() };
                        else
                            if (U.Contains(hit.collider.gameObject.GetComponent<NewBehaviourScript>()))
                            U.Remove(hit.collider.gameObject.GetComponent<NewBehaviourScript>());
                        else
                            U.Add(hit.collider.gameObject.GetComponent<NewBehaviourScript>());
                        break;
                }
            }
            else
            {
                Vector2 position = new Vector2(Mathf.FloorToInt(Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y)).x), Mathf.FloorToInt((Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y))).y));
                if (path.PosToMove(position))
                {
                    U.Sort(delegate (NewBehaviourScript a, NewBehaviourScript b)
                    {
                        return ((a.position() - new Vector2(position.x, position.y)).magnitude.CompareTo((b.position() - new Vector2(position.x, position.y)).magnitude));
                    });
                    List<Vector2> positions = path.ClosesedFreePoints(new Point(Mathf.FloorToInt(position.x), Mathf.FloorToInt(position.y), 0, 0, null), U.Count);
                    List<Vector2> pth = new List<Vector2> { };
                    for (int i = 0; i < U.Count; ++i)
                    {
                        pth = path.Path(U[i].transform.position, positions[i]);
                        //Debug.Log(pth.Count);
                       // for (int j = 0; j < pth.Count; ++j)
                           // Debug.Log(i + ": " + j + " " + pth[j]);
                        U[i].setPath(pth);
                    }
                }
            }
        }
    }
    public void DestoyedObject(GameObject obj)
    {
        Destroy(obj);
        foreach (NewBehaviourScript kek in U)
            if (kek.aim == obj)
                kek.aim = null;
        foreach (NewBehaviourScript kek in E)
            if (kek.aim == obj)
                kek.aim = null;
    }
}
