using UnityEngine;
using System.Collections;

public class VariableStoreage : MonoBehaviour {
	public float[] xcord;
	public float[] ycord;
	public int NumberOfUnits;
	// Use this for initialization
	void Start () {
		xcord = new float[20];
		ycord = new float[20];
		NumberOfUnits = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
