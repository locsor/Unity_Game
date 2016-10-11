using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathFinding : MonoBehaviour {
    public GameObject NodeMap;
    private List<Transform> closed_set;
    private List<Transform> open_set;
    private Transform[] cameFrom;
    public List<float> gScore;
    public List<float> fScore;
    private Transform[] all;
    private Transform current;
    public List<Transform> neighbor;
    private List<Transform> added_neighbor;
    private float tentative_gScore;
    private bool already_added;
    private int current_num;
    private float min_fScore;
    private Vector2 goal;
    public Camera cam;
    private int min_num;
    void Start() {
        all = new Transform[12];
        neighbor = new List<Transform>();
        added_neighbor = new List<Transform>();
        cameFrom = new Transform[12];
        for (int i = 0; i < 12; i++)
        {
            all[i] = NodeMap.gameObject.transform.GetChild(i);
        }
        closed_set = new List<Transform>();
        open_set = new List<Transform>();
        open_set.Add(NodeMap.gameObject.transform.GetChild(0));
        gScore = new List<float>();
        fScore = new List<float>();
        gScore.Add(0);
        current = NodeMap.gameObject.transform.GetChild(0);
        already_added = false;
        current_num = 0;
    }

    void Update() {
        if (Input.GetMouseButtonDown(1))
        {
            fScore.Add(Mathf.Sqrt(Mathf.Pow((all[0].position.x - goal.x), 2) - Mathf.Pow((all[0].position.y - goal.y), 2)));
            goal = cam.ScreenToWorldPoint(Input.mousePosition);
            Astar();
            fScore.Clear();
        }
    }

    void Astar()
    {
        while (open_set.Count != 0)
        {
            min_fScore = fScore[0];
            for (int i = 0; i < fScore.Count; i++)
            {
                if (fScore[i] < min_fScore)
                {
                    min_fScore = fScore[i];
                    min_num = i;
                }
            }
            current = open_set[min_num];
            if (current.position.x == Mathf.Round(goal.x) + 0.5 && current.position.y == Mathf.Round(goal.x))
            {
                Debug.Log("Route Found");
            }
            
            neighbors(current, 0);
            open_set.Remove(current);
            closed_set.Add(current);
            for(int i = 0; i < neighbor.Count; i++)
            {
                if(!closed_set.Contains(neighbor[i]))
                {
                    break;
                }
                tentative_gScore = gScore[current_num] + Mathf.Sqrt(Mathf.Pow((neighbor[i].position.x - current.position.x), 2) - Mathf.Pow((neighbor[i].position.y - current.position.y), 2));
                if(!open_set.Contains(neighbor[i]))
                {
                    open_set.Add(neighbor[i]);
                }
                else if(tentative_gScore < gScore[i])
                {
                    break;
                }
                cameFrom[i] = current;
                gScore[i] = tentative_gScore;
                Debug.Log(gScore[i]);
                fScore[i] = gScore[i] + Mathf.Sqrt(Mathf.Pow((neighbor[i].position.x - goal.x), 2) - Mathf.Pow((neighbor[i].position.y - goal.y), 2));
            }
        }
    }
    void neighbors(Transform node, int num)
    {
        for (int i = 0; i < 12; i++)
        {
            if(node.position.x + 1 == all[i].position.x && node.position.y == all[i].position.y)
            {
                for(int j = 0; j < added_neighbor.Count; j++)
                {
                    if(all[i] == added_neighbor[j])
                    {
                        already_added = true;
                    }
                }
                if (already_added == false)
                {
                    neighbor.Add(all[i]);
                    added_neighbor.Add(all[i]);
                }
            }
            already_added = false;
            if (node.position.x + 1 == all[i].position.x && node.position.y + 1 == all[i].position.y)
            {
                for (int j = 0; j < added_neighbor.Count; j++)
                {
                    if (all[i] == added_neighbor[j])
                    {
                        already_added = true;
                    }
                }
                if (already_added == false)
                {
                    neighbor.Add(all[i]);
                    added_neighbor.Add(all[i]);
                }
            }
            already_added = false;
            if (node.position.x + 1 == all[i].position.x && node.position.y - 1 == all[i].position.y)
            {
                for (int j = 0; j < added_neighbor.Count; j++)
                {
                    if (all[i] == added_neighbor[j])
                    {
                        already_added = true;
                    }
                }
                if (already_added == false)
                {
                    neighbor.Add(all[i]);
                    added_neighbor.Add(all[i]);
                }
            }
            already_added = false;
            if (node.position.x  == all[i].position.x && node.position.y + 1 == all[i].position.y)
            {
                for (int j = 0; j < added_neighbor.Count; j++)
                {
                    if (all[i] == added_neighbor[j])
                    {
                        already_added = true;
                    }
                }
                if (already_added == false)
                {
                    neighbor.Add(all[i]);
                    added_neighbor.Add(all[i]);
                }
            }
            already_added = false;
            if (node.position.x == all[i].position.x && node.position.y - 1 == all[i].position.y)
            {
                for (int j = 0; j < added_neighbor.Count; j++)
                {
                    if (all[i] == added_neighbor[j])
                    {
                        already_added = true;
                    }
                }
                if (already_added == false)
                {
                    neighbor.Add(all[i]);
                    added_neighbor.Add(all[i]);
                }
            }
            already_added = false;
            if (node.position.x - 1 == all[i].position.x && node.position.y == all[i].position.y)
            {
                for (int j = 0; j < added_neighbor.Count; j++)
                {
                    if (all[i] == added_neighbor[j])
                    {
                        already_added = true;
                    }
                }
                if (already_added == false)
                {
                    neighbor.Add(all[i]);
                    added_neighbor.Add(all[i]);
                }
            }
            already_added = false;
            if (node.position.x - 1 == all[i].position.x && node.position.y + 1 == all[i].position.y)
            {
                for (int j = 0; j < added_neighbor.Count; j++)
                {
                    if (all[i] == added_neighbor[j])
                    {
                        already_added = true;
                    }
                }
                if (already_added == false)
                {
                    neighbor.Add(all[i]);
                    added_neighbor.Add(all[i]);
                }
            }
            already_added = false;
            if (node.position.x - 1 == all[i].position.x && node.position.y - 1 == all[i].position.y)
            {
                for (int j = 0; j < added_neighbor.Count; j++)
                {
                    if (all[i] == added_neighbor[j])
                    {
                        already_added = true;
                    }
                }
                if (already_added == false)
                {
                    neighbor.Add(all[i]);
                    added_neighbor.Add(all[i]);
                }
            }
            already_added = false;
        }
        
    }
}
