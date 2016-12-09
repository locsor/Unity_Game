using UnityEngine;
using System.Collections;

public class CorCor : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), Mathf.RoundToInt(transform.position.z));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
