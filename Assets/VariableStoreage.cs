using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VariableStoreage : MonoBehaviour {
	public float[] xcord;
	public float[] ycord;
	public float[] weapon_cordx;
	public float[] weapon_cordy;
    public List<float> target_arrx;
    public List<float> target_arry;
    public List<GameObject> selected_units;
    public GameObject[] weapons;
	public int NumberOfUnits;
	public int NumberOfWeapons;
	void Start ()
    {
        QualitySettings.antiAliasing = 8;
        xcord = new float[20];
		ycord = new float[20];
		weapon_cordx = new float[2];
		weapon_cordy = new float[2];
        target_arrx = new List<float>();
        target_arry = new List<float>();
        selected_units = new List<GameObject>();
        weapons = new GameObject[2];
        NumberOfUnits = 0;
		NumberOfWeapons = 2;
	}
}
