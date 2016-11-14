using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public struct Node
{
    public Transform node;
    public float fScore;
    public float gScore;
    public int index;
}
public class PathFinding : MonoBehaviour {
    public List<Transform> closedSet = new List<Transform>();
    public List<Transform> openSet = new List<Transform>();
    public GameObject NodeMap;
    public Transform[] all;
    public List<Transform> neighbor;
    private List<Transform> added_neighbor;
    private bool already_added;
    public bool found = true;
    public Node[] node_list;
    private bool abc;
    public List<Transform> result;
    public Transform[] cameFrom;
    private int childCount;
    public List<Transform> TotalPath;
    public Transform start_node;
    public Node finish_node;
    void Start() {
        TotalPath = new List<Transform>();
        abc = false;
        childCount = NodeMap.transform.childCount;
        all = new Transform[childCount];
        neighbor = new List<Transform>();
        added_neighbor = new List<Transform>();
        result = new List<Transform>();
        for (int i = 0; i < childCount; i++)
        {
            all[i] = NodeMap.gameObject.transform.GetChild(i);
            //Debug.Log(all[i].name);
        }
        node_list = new Node[childCount];
        for (int i = 0; i < childCount; i++)
        {
            node_list[i].node = all[i];
            node_list[i].index = i;
        }
        start_node = all[0];
        finish_node = node_list[0];
        already_added = false;
    }

    void Update() {
        int count;
        count = 0;
        if (!found)
        {
            cameFrom = new Transform[childCount];
            for (int i = 0; i < childCount; i++)
            {
                node_list[i].fScore = 0;
                node_list[i].gScore = 0;
            }
            found = Astar(start_node, finish_node, cameFrom);
        }
    }
    bool Astar(Transform goal, Node start, Transform[] path)
    {
        found = true;
        int ind = 0;
        int min_num = 0;
        int c = -1;
        float min_fScore = 0;
        float tentative_gScore;
        Node current;
        current.node = null;
        current.fScore = 0;
        current.gScore = 0;
        current.index = 0;
        //goal = node_list[26].node;
        List<Node> nan = new List<Node>();
        openSet.Clear();
        closedSet.Clear();
        openSet.Add(start.node);
        //List<float> gScore = new List<float>();
        //gScore.Add(0);
        //List<float> fScore = new List<float>();
        //fScore.Add(Vector2.Distance(all[0].position, goal.position));
        start.fScore = Mathf.Pow((Vector2.Distance(start.node.position, goal.position)),2);
        start.gScore = 0;
        while(openSet.Count != 0)
        {
            //Debug.Log(openSet.Count);
            for (int j = 0; j < childCount; j++)
            {
                if (node_list[j].node == openSet[0])
                {
                     min_fScore = node_list[j].fScore;
                }
            }
            for (int i = 0; i < openSet.Count; i++)
            {
                for(int j = 0; j < childCount; j++)
                {
                    if(node_list[j].node == openSet[i])
                    {
                        if(node_list[j].fScore <= min_fScore && openSet.Contains(node_list[j].node))
                        {
                            min_fScore = node_list[j].fScore;
                            min_num = node_list[j].index;
                        }
                    }
                }
            }
            current.node = node_list[min_num].node;
            current.index = min_num;
            if (current.node.position == goal.position)
            {
                recunstructPath(current);
                return true;
            }
            openSet.Remove(current.node);
            closedSet.Add(current.node);
            neighbor.Clear();
            neighbors(current.node);
            //Debug.Log('!');
            nan.Clear();
            for (int i = 0; i < neighbor.Count; i++)
            {
                for(int j = 0; j < childCount; j++)
                {
                    if(node_list[j].node == neighbor[i])
                    {
                        nan.Add(node_list[j]);
                    }
                }
            }
            c++;
            for (int i = 0; i < neighbor.Count; i++)
            {
                if(closedSet.Contains(nan[i].node))
                {
                    continue;
                }
                tentative_gScore = node_list[min_num].gScore + Mathf.Pow(Vector2.Distance(current.node.position, goal.position),2);
                if(!openSet.Contains(nan[i].node))
                {
                    openSet.Add(nan[i].node);
                    //Debug.Log('!');
                }
                else if(tentative_gScore >= nan[i].gScore)
                {
                    continue;
                }
                //Debug.Log('!');
                path[c] = current.node;
                current.node.GetComponent<SpriteRenderer>().enabled = true;
                node_list[nan[i].index].gScore = tentative_gScore;
                node_list[nan[i].index].fScore = node_list[nan[i].index].gScore + Mathf.Pow(Vector2.Distance(nan[i].node.position, goal.position),2);
                ind = nan[i].index;
                //Debug.Log(node_list[nan[i].index].fScore);
            }
        }
        return false;
    }
    void neighbors(Transform node)
    {
        added_neighbor.Clear();
        neighbor.Clear();
        for (int i = 0; i < childCount; i++)
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
                    //Debug.Log(all[i]);
                }
            }
            already_added = false;
            //Debug.Log(node.position.x + 1);
            //Debug.Log(node.position.y + 1);
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
                    //Debug.Log(all[i]);
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
                   // Debug.Log(all[i]);
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
                    //Debug.Log(all[i]);
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
                   // Debug.Log(all[i]);
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
                   // Debug.Log(all[i]);
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
                    //Debug.Log(all[i]);
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
                    //Debug.Log(all[i]);
                    neighbor.Add(all[i]);
                    added_neighbor.Add(all[i]);
                }
            }
            already_added = false;
        }
    }
    void recunstructPath(Node current)
    {
        result.Clear();
        TotalPath.Clear();
        Transform holder;
        for (int i = 0; i < cameFrom.Length; i++)
        {
            if (cameFrom[i] != null)
            {
                result.Add(cameFrom[i]);
            }
        }
        TotalPath.Add(result[result.Count - 1]);
        current.node = result[result.Count - 1];
        result.Reverse();
        for (int i = 0; i < result.Count - 1; i++)
        {
            neighbors(current.node);
            if (neighbor.Contains(result[i + 1]))
            {
                TotalPath.Add(result[i + 1]);
                current.node = result[i + 1];
            }
            else
                continue;
        }
    }
}
