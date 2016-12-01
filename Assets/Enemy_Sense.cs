using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Enemy_Sense : MonoBehaviour
{
    public GameObject[] enemies;
    public List<GameObject> EnemiesInVision;
    public GameObject bullet;
    public float bullet_speed;
    public int wait = 0;
    public bool justShot;
<<<<<<< HEAD
    public int shotsFired = 0;
    private GameObject[] memory;
    public bool reloading = false;
    public float timePassed = 0;
=======
    private GameObject[] memory;
>>>>>>> f7f9e00c37d4bf67c3bb83f2138e54e1be5d3302
    // Use this for initialization
    void Start()
    {
        justShot = false;
        EnemiesInVision = new List<GameObject>();
    }
    // Update is called once per frame
    void Update()
    {
        //EnemiesInVision.Clear();
        int layerMask1 = 1 << 8 | 1 << 9;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length; i++)
        {
            Vector2 pos2D = enemies[i].transform.position;
            RaycastHit2D hit = Physics2D.Linecast(transform.position, enemies[i].transform.position, layerMask1);
            Debug.DrawLine(transform.position, hit.point, Color.red);
            if (!EnemiesInVision.Contains(enemies[i]) && enemies[i].GetComponent<Collider2D>().bounds.Contains(hit.point))
            {
                EnemiesInVision.Add(enemies[i]);
            }
            else if(!enemies[i].GetComponent<Collider2D>().bounds.Contains(hit.point))
                EnemiesInVision.Remove(enemies[i]);

        }
        if (EnemiesInVision.ToArray() != memory)
        {
            ISU(EnemiesInVision, enemies, memory);
            memory = EnemiesInVision.ToArray();
        }
        if (EnemiesInVision.Count != 0)
        {
            if (justShot == false)
            {
<<<<<<< HEAD
                if (reloading == false)
                {
                    Shoot(EnemiesInVision[0]);
                    justShot = true;
                    Reload();
                }
                else if(reloading == true)
                {
                    timePassed += Time.deltaTime;
                    if(timePassed >= 2)
                    {
                        reloading = false;
                        shotsFired = 0;
                        timePassed = 0;
                    }
                }
=======
                Shoot(EnemiesInVision[0]);
                justShot = true;
>>>>>>> f7f9e00c37d4bf67c3bb83f2138e54e1be5d3302
            }
            if (justShot == true)
            {
                wait++;
<<<<<<< HEAD
                if (wait == 10)
=======
                if (wait == 20)
>>>>>>> f7f9e00c37d4bf67c3bb83f2138e54e1be5d3302
                {
                    justShot = false;
                    wait = 0;
                }
            }
        }
    }
    void Shoot(GameObject target)
    {
<<<<<<< HEAD
        shotsFired++;
        Vector3 shootDirection;
        float angle = Vector2.Angle(target.transform.position - transform.position, transform.position);
        Vector3 cross = Vector3.Cross(target.transform.position - transform.position, transform.position);
        shootDirection = target.transform.position - transform.position;
        shootDirection.z = 0.0f;
        if (cross.z > 0)
        {
            angle = 360 - angle;
        }
        float offset_angle = Random.Range(-1f, 1f);
        GameObject bulletInstance = Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(0, 0, angle))) as GameObject;
        bulletInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(shootDirection.x * bullet_speed + offset_angle, shootDirection.y * bullet_speed + offset_angle);
        transform.GetChild(2).transform.rotation = Quaternion.Euler(new Vector3(0, 180, -angle));
    } 
    void Reload()
    {
        if(shotsFired == 20)
        {
            reloading = true;
        }
    }
=======
        Vector3 shootDirection;
        float angle = Vector2.Angle(target.transform.position - transform.position, transform.position);
        shootDirection = target.transform.position;
        shootDirection = shootDirection - transform.position;
        shootDirection.z = 0.0f;
        GameObject bulletInstance = Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(0, 0, angle))) as GameObject;
        bulletInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(shootDirection.x * bullet_speed, shootDirection.y * bullet_speed);
    } 
>>>>>>> f7f9e00c37d4bf67c3bb83f2138e54e1be5d3302
    void ISU(List<GameObject> ThatISee, GameObject[] everything, GameObject[] mem)
    {
        List<GameObject> isee = new List<GameObject>(); ;
        for (int i = 0; i < everything.Length; i++)
        {
            if(!ThatISee.Contains(everything[i]))
            {
                everything[i].GetComponent<SpriteRenderer>().enabled = false;
            }
            else if(ThatISee.Contains(everything[i]))
                everything[i].GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}