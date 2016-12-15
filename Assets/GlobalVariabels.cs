using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GlobalVariabels : MonoBehaviour {
    public List<GameObject> spawned_blood = new List<GameObject>();
    public List<GameObject> visible_enemies = new List<GameObject>();
    public int weight, height;
	void Start () {
        weight = 100;
        height = 100;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
