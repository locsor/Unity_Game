using UnityEngine;
using System.Collections;

public class Enemy_sense : MonoBehaviour {
    public GameObject[] enemies;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }
}
