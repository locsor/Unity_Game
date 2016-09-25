using UnityEngine;
using System.Collections;

public class VariableStoreage : MonoBehaviour {
	public float[] xcord;
	public float[] ycord;
	public float[] weapon_cordx;
	public float[] weapon_cordy;
	public GameObject[] weapons;
	public int NumberOfUnits;
	public int NumberOfWeapons;
	void Start () {
		xcord = new float[20];
		ycord = new float[20];
		weapon_cordx = new float[2];
		weapon_cordy = new float[2];
		weapons = new GameObject[2];
		NumberOfUnits = 0;
		NumberOfWeapons = 2;
	}

	void Update () {
	
	}
}
