using UnityEngine;
using System.Collections;

public class Selection : MonoBehaviour {
	public int unit;
	public GameObject container;
	public Camera cam;
	public GameObject spawn;
	private Vector3 target;
	void Start () {
		
	}
    
	void Update () {
        target = cam.ScreenToWorldPoint (Input.mousePosition);
		if (Input.GetMouseButtonDown(0)) {
            select_func(unit);
        }
	}
	void select_func(int i)
	{
        //spawn.GetComponent<Spawning>().new_unit[i].GetComponent<Move>().is_selected = true;
        spawn.GetComponent<Spawning>().new_unit[i].GetComponent<Move>().can_move = true;
        spawn.GetComponent<Spawning>().new_unit[i].GetComponent<Move>().is_selected = true;
        for (int j = 1; j < i; j++)
        {
               spawn.GetComponent<Spawning>().new_unit[j].GetComponent<Move>().can_move = false;    
               spawn.GetComponent<Spawning>().new_unit[j].GetComponent<Move>().is_selected = false;
        }
        for (int j = i+1; j < container.GetComponent<VariableStoreage>().NumberOfUnits+1; j++)
        {
            spawn.GetComponent<Spawning>().new_unit[j].GetComponent<Move>().can_move = false;
            spawn.GetComponent<Spawning>().new_unit[j].GetComponent<Move>().is_selected = false;
        }
    }
}
