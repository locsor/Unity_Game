using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    private float rechargeTime;
    private int magazineSize;
    private float range;
    private float reloadTime;
    private float bulletSpead;
    
    private float timer1, timer2;
    public GameObject bullet;
    private int patrons;
    public GameObject aim;
    void Start()
    {
        timer1 = 0.0f;
        timer2 = 0.0f;
        rechargeTime = 0.0f;
        magazineSize = 12;
        range = 5.0f;
        reloadTime = 0.1f;
        patrons = magazineSize;
        bulletSpead = 3.0f;
    }
    void FixedUpdate()
    {
        Attack(aim);
    }
    public void Attack(GameObject enemy)
    {
        if (Range(enemy) <= range)
            if (patrons > 0)
                if (timer2 >= reloadTime)
                {
                    timer2 = 0;
                    Vector2 from = transform.position;
                    Vector2 dir = (-transform.position + enemy.transform.position) * bulletSpead;
                    dir = Vector3.ProjectOnPlane(dir, new Vector3(0, 0, 1));
                    GameObject bullet1 = Instantiate(bullet, transform.position, Quaternion.identity) as GameObject;
                    BulletBehavior bul1Beh = bullet1.GetComponent<BulletBehavior>();
                    bul1Beh.dir = dir;
                    --patrons;

                }
                else
                {
                    timer2 += Time.deltaTime;
                }
            else
            {
                if (timer1 >= rechargeTime)
                {
                    timer1 = 0;
                    timer2 = 0;
                    patrons = magazineSize;

                }
                else
                {
                    timer1 += Time.deltaTime;
                }
            }
    }
    public float Range(GameObject aim)
    {
        if (aim != null)
        {
            Vector2 from = transform.position;
            Vector2 dir = -transform.position + aim.transform.position;
            if (!Physics2D.Raycast(from, dir, dir.magnitude))
                return (transform.position - aim.transform.position).magnitude;
        }
        return float.MaxValue - 10f;
    }
}
