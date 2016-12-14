using UnityEngine;
using System.Collections;

public class BulletBehavior : MonoBehaviour {

    private float time;
    private float lifeTime = 3.0f;
    private CharacterController CC;
    public Vector2 dir;
    public float damage;
	void Start ()
    {
        //Debug.Log("|||||||||s");
        time = 0.0f;
        CC = GetComponent<CharacterController>();
        damage = Random.Range(3.0f, 4.0f);
	}
    // Update is called once per frame
    void Update()
    {
        CC.Move(dir * Time.deltaTime);
        if (time >= lifeTime)
            Destroy(gameObject);
        else
            time += Time.deltaTime;
    }
    //void OnCollisionEnter(Collision hit)
    //{
    //    Debug.Log("asdasdasd");
    //    Destroy(gameObject);
    //}
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Enemy")
        {
            NewBehaviourScript weq = hit.gameObject.GetComponent<NewBehaviourScript>();
            weq.GetBullet(this);
        }
        Destroy(gameObject);
    }
}
