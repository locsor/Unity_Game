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
    private Transform[] all;
    public List<Transform> neighbor;
    private List<Transform> added_neighbor;
    private bool already_added;
    private bool found = false;
    private Node[] node_list;
    private int abc = 0;
    public List<Transform> result;
    public Transform[] cameFrom = new Transform[27];
    public Transform[] check_cameFrom = new Transform[27];
    void Start() {
        all = new Transform[27];
        neighbor = new List<Transform>();
        added_neighbor = new List<Transform>();
        result = new List<Transform>();
        for (int i = 0; i < 27; i++)
        {
            all[i] = NodeMap.gameObject.transform.GetChild(i);
            //Debug.Log(all[i].name);
        }
        node_list = new Node[27];
        for (int i = 0; i < 27; i++)
        {
            node_list[i].node = all[i];
            node_list[i].index = i;
        }
        already_added = false;
    }

    void Update() {
        if(abc == 0)
        {
            found = Astar(node_list[0].node, node_list[21], check_cameFrom);
            for(int i = 0; i < 27; i++)
            {
                node_list[i].fScore = 0;
                node_list[i].gScore = 0;
            }
            found = Astar(node_list[21].node, node_list[0], cameFrom);
            for(int i = 0; i < cameFrom.Length; i++)
            {
                for(int j = 0; j < check_cameFrom.Length; j++)
                {
                    if(cameFrom[i] == check_cameFrom[j] && cameFrom[i] != null)
                    {
                        Debug.Log(check_cameFrom[j]);
                        result.Add(cameFrom[i]);
                    }
                }
            }
            for(int i = 0; i < result.Count; i++)
            {
                if(result[i] == null)
                {
                    result.Remove(result[i]);
                }
            }
            abc++;
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
            for (int j = 0; j < 27; j++)
            {
                if (node_list[j].node == openSet[0])
                {
                     min_fScore = node_list[j].fScore;
                }
            }
            for (int i = 0; i < openSet.Count; i++)
            {
                for(int j = 0; j < 27; j++)
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
            //Debug.Log(current);
            if (current.node == goal)
            {
                //cameFrom = smooth(cameFrom);
                Debug.Log("Route Found");
                //Debug.Log(current.index);
                //path[ind] = current.node;
                //result = reconstruct(cameFrom, current);
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
                for(int j = 0; j < 27; j++)
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
        for (int i = 0; i < 27; i++)
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
    Transform[] smooth(Transform[] map)
    {
        neighbor.Clear();
        for(int i = 0; i < map.Length; i++)
        {
            if(i < map.Length - 1 )
            {
                neighbors(map[i]);
                if(neighbor.Contains(map[i+1]))
                {
                    map[i] = null;
                }
            }
        }
        return map;
    }
    //List<Transform> reconstruct(Transform[] came, Node cur)
    //{
    //    int a = 0;
    //    List<Transform> TotalPath = new List<Transform>();
    //    List<Transform> came_list = new List<Transform>();
    //    TotalPath.Add(cur.node);
    //    for(int i = 0; i < 27; i++)
    //    {
    //        came_list.Add(came[i]);
    //        Debug.Log(came_list[i]);
    //    }
    //    while (came_list.Contains(cur.node)||a<20)
    //    {
    //        a++;
    //        Debug.Log("reconstruction");
    //        for (int i = 0; i < 27; i++)
    //        {
    //            if (came_list[cur.index] == node_list[i].node )
    //            {
    //                cur = node_list[i];
    //                Debug.Log('!');
    //                TotalPath.Add(cur.node);
    //            }
    //        }
    //    }
    //    return TotalPath;
    //}
    bool check(Transform[] came, Transform cur)
    {
        for(int i = 0; i < came.Length; i++)
        {
            if(came[i] == cur)
            {
                return true;
            }
        }
        return false;
    }
    int check_num(Transform[] came, Transform cur)
    {
        for (int i = 0; i < came.Length; i++)
        {
            if (came[i] == cur)
            {
                return i;
            }
        }
        return 100;
    }
}
