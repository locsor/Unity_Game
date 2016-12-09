using UnityEngine;
using System.Collections;

public class BulletBehavior : MonoBehaviour {

    private float time;
    private float lifeTime = 3.0f;
	void Start ()
    {
        //Debug.Log("|||||||||s");
        time = 0.0f;
	}
    // Update is called once per frame
    void FixedUpdate ()
    {
        if (time >= lifeTime)
            Destroy(this);
        else time += Time.deltaTime;
	}
}
