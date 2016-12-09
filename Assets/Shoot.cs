using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {
	public GameObject bullet;
	public int thrust;
	private GameObject bullet1;
	public Rigidbody2D rb;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update ()
    {
		if (Input.GetKeyDown (KeyCode.A)) {
			bullet1 = Instantiate(bullet, transform.position, Quaternion.identity) as GameObject;
			rb.AddForce (transform.forward * thrust);
		}
	}
}