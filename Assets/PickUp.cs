using UnityEngine;
using System.Collections;

public class PickUp : MonoBehaviour {
	public GameObject container;
	private bool pickedup = true;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (pickedup == true && (Mathf.Round (transform.position.x) == Mathf.Round (container.GetComponent<VariableStoreage> ().weapon_cordx [0])) &&
		   (Mathf.Round (transform.position.y) == Mathf.Round (container.GetComponent<VariableStoreage> ().weapon_cordy [0]))) {
			container.GetComponent<VariableStoreage> ().weapons [0].transform.parent = transform;
			pickedup = false;
		}
	}
}
