using UnityEngine;
using System.Collections;

public class CoordinatesForStorage : MonoBehaviour {
	public GameObject container;
	public GameObject weapon;
	private int i;
	// Use this for initialization
	void Start () {
		i = 0;
	}
	// Update is called once per frame
	void Update () {
		container.GetComponent<VariableStoreage> ().weapons [i] = weapon;
		container.GetComponent<VariableStoreage> ().weapon_cordx [i] = transform.position.x;
		container.GetComponent<VariableStoreage> ().weapon_cordy [i] = transform.position.y;
		//if (container.GetComponent<VariableStoreage> ().weapons [1] != 1) {
			//Debug.Log ('!');
		//}
	}
}
