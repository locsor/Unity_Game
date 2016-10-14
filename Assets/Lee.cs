using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public struct Node1
{
    public Transform node;
    public float score;
    public int index;
}
public class Lee : MonoBehaviour {
    private int d = 0;
    public GameObject NodeMap;
    private Transform[] all;
    public Node1 goal;
    public List<Transform> neighbor;
    private List<Transform> added_neighbor;
    private bool already_added;
    private Node1 current;
    public List<Node1> nan;
    private Node1[] node_list;
    private bool comp = false;
    public List<Transform> nb_copy;
    // Use this for initialization
    void Start () {
        all = new Transform[33];
        nb_copy = new List<Transform>();
        neighbor = new List<Transform>();
        added_neighbor = new List<Transform>();
        nan = new List<Node1>();
        node_list = new Node1[33];
        for (int i = 0; i < 33; i++)
        {
            all[i] = NodeMap.gameObject.transform.GetChild(i);
        }
        for (int i = 0; i < 33; i++)
        {
            node_list[i].node = all[i];
            node_list[i].index = i;
            node_list[i].score = 0;
        }
        goal = node_list[30];
        node_list[0].score = d;
        already_added = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (comp == false)
        {
            pathfinding();
        }
	}
    void pathfinding()
    {
        Debug.Log(node_list[32]);
        current = node_list[0];
        do
        {
            if (current.node == node_list[0].node)
            {
                wave(current);
            }
            else if(nb_copy.Count == 0)
            {
                neighbor.Clear();
                neighbors(current.node);
                nb_copy = neighbor;
            }
            d++;
        } while (node_list[30].score == 0);
        //Debug.Log(node_list[2].score);
        comp = true;
    }
    void neighbors(Transform node)
    {
        added_neighbor.Clear();
        neighbor.Clear();
        for (int i = 0; i < 33; i++)
        {
            if (node.position.x + 1 == all[i].position.x && node.position.y == all[i].position.y)
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
            if (node.position.x == all[i].position.x && node.position.y + 1 == all[i].position.y)
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
    void wave(Node1 node)
    {
        neighbor.Clear();
        neighbors(current.node);
        nan.Clear();
        for (int i = 0; i < neighbor.Count; i++)
        {
            for (int j = 0; j < 33; j++)
            {
                if (node_list[j].node == neighbor[i])
                {
                    nan.Add(node_list[j]);
                }
            }
        }
        for (int i = 0; i < neighbor.Count; i++)
        {
            if (node_list[nan[i].index].score == 0 && node_list[nan[i].index].node != node_list[0].node)
            {
                node_list[nan[i].index].score = d + 1;
                Debug.Log(node_list[30].score);
            }
        }
    }
}
