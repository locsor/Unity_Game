using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class Enemy_Sense : MonoBehaviour
{
    public GameObject[] enemies;
    public List<GameObject> EnemiesInVision;
    public GameObject bullet;
    public float bullet_speed;
    public int wait = 0;
    public bool justShot;
    public int shotsFired = 0;
    public Text ammoCount;
    private GameObject[] memory;
    public bool reloading = false;
    public bool selectedTarg = false;
    public float timePassed = 0;
    private GameObject targ;
    private Vector3 click;
    public GameObject GV;
    private Camera cam = Camera.main;
    void Start()
    {
        justShot = false;
        EnemiesInVision = new List<GameObject>();
        int ammo = 20 - shotsFired;
        ammoCount.text = ammo.ToString() +"/ 20";
    }
    void Update()
    {
        //EnemiesInVision.Clear();
        int layerMask1 = 1 << 8 | 1 << 9;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if(EnemiesInVision.Contains(null))
        {
            EnemiesInVision.Remove(null);
        }
        for (int i = 0; i < enemies.Length; i++)
        {
            //Debug.Log("faf");
            Vector2 pos2D = enemies[i].transform.position;
            RaycastHit2D hit = Physics2D.Linecast(transform.position, enemies[i].transform.position, layerMask1);
            Debug.DrawLine(transform.position, hit.point, Color.red);
            if (!EnemiesInVision.Contains(enemies[i]) && enemies[i].GetComponent<CharacterController>().bounds.Contains(hit.point))
            {
                Debug.Log("!!!!!!");
                EnemiesInVision.Add(enemies[i]);
                GV.GetComponent<GlobalVariabels>().visible_enemies.Add(enemies[i]);
            }
            else if (!enemies[i].GetComponent<CharacterController>().bounds.Contains(hit.point))
            {
                EnemiesInVision.Remove(enemies[i]);
                //GV.GetComponent<GlobalVariabels>().visible_enemies.Remove(enemies[i]);
            }

        }
        if (EnemiesInVision.ToArray() != memory)
        {
            ISU(GV.GetComponent<GlobalVariabels>().visible_enemies, enemies, memory);
            memory = EnemiesInVision.ToArray();
        }
        if(!selectedTarg)
        {
            targ = EnemiesInVision[0];
        }
        if (Input.GetMouseButtonDown(0))
        {
            click = cam.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(click, -Vector2.up);
            if (hit.collider.CompareTag("Enemy") && EnemiesInVision.Contains(hit.collider.gameObject))
            {
                selectedTarg = true;
                targ = hit.collider.gameObject;
            }
        }
        if (EnemiesInVision.Count != 0)
        {
            if (justShot == false)
            {
                if (reloading == false)
                {
                    if (targ != null)
                    {
                        Shoot(targ);
                        justShot = true;
                        Reload();
                    }
                }
                else if(reloading == true)
                {
                    Debug.Log('5');
                    timePassed += Time.deltaTime;
                    if(timePassed >= 2)
                    {
                        reloading = false;
                        shotsFired = 0;
                        timePassed = 0;
                    }
                }
            }
            if (justShot == true)
            {
                wait++;
                if (wait == 5)
                {
                    justShot = false;
                    wait = 0;
                }
            }
        }
    }
    void Shoot(GameObject target)
    {
        shotsFired++;
        int ammo = 20 - shotsFired;
        ammoCount.text = ammo.ToString() + "/ 20";
        Vector3 shootDirection;
        float angle = Vector2.Angle(target.transform.position - transform.position, transform.position);
        Vector3 cross = Vector3.Cross(target.transform.position - transform.position, transform.position);
        shootDirection = target.transform.position - transform.position;
        shootDirection.z = 0.0f;
        if (cross.z > 0)
        {
            angle = 180 - angle;
        }
        float offset_angle = Random.Range(-0.5f, 0.5f);
        GameObject bulletInstance = Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(0, 0, angle))) as GameObject;
        bulletInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(shootDirection.x * bullet_speed + offset_angle, shootDirection.y * bullet_speed + offset_angle);
        //transform.GetChild(2).transform.rotation = Quaternion.Euler(new Vector3(0, 180, -angle+8f));
    } 
    void Reload()
    {
        if (shotsFired >= 20)
        {
            Debug.Log(6);
            reloading = true;
        }
    }
    void ISU(List<GameObject> ThatISee, GameObject[] everything, GameObject[] mem)
    {
        List<GameObject> isee = new List<GameObject>(); ;
        for (int i = 0; i < everything.Length; i++)
        {
            if (!ThatISee.Contains(everything[i]))
            {
                everything[i].GetComponent<SpriteRenderer>().enabled = false;
            }
            else if (ThatISee.Contains(everything[i]))
            {
                Debug.Log("42112");
                everything[i].GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }
}