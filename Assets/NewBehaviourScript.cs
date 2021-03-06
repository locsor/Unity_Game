﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NewBehaviourScript : MonoBehaviour
{
    public float Speed = 1.0f;
    public int player;
    public float range = 10.0f;
    public CharacterController CC;
    public List<Vector2> Path;
    private bool starterTimer = false;
    private float time = 0, timer1 = 0, timer2 = 0;
    private PathFinding P;
    public GameObject aim;
    public GameObject Weapon;
    public Weapon wep;
    public float health = 100;
    private Logic logic;

    private float rechargeTime = 0.1f;
    private int magazineSize = 12;
    private float reloadTime = 0.1f;
    private int patrons = 5;
    
    void Start()
    {
        aim = null;
        CC = GetComponent<CharacterController>();
        Path = new List<Vector2> {};
        P = GameObject.Find("PathFindingMan").GetComponent<PathFinding>();
        logic = GameObject.Find("GameLogic").GetComponent<Logic>();
        Weapon = transform.FindChild("Weapon").gameObject;
        wep = Weapon.GetComponent<Weapon>();
    }
    void FixedUpdate()
    {
        //CC.Move(Vector2.right * 0.1f);
        Move();
        if (Range(aim) <= range)
        {
            Attack(aim);
        }
        else
        {
            aim = null;
        }
        Rotate();//TO DO: make this function 
    }
    void  Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, -1);
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
    void Comands()
    {
    }
    public void Move()
    {
        Vector2 CurrentPosition = transform.position;
        if(Path.Count > 0)
        CC.Move((Path[0] - CurrentPosition).normalized * Speed * Time.deltaTime);
        if (!((Path[0] - CurrentPosition).magnitude > Speed * Time.deltaTime))
        {
            transform.position = Path[0];
                Path.RemoveAt(0);
        }
    }
    Vector2 CurPos()
    {
        return new Vector2(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
    }
    public void setPath(List<Vector2> P)
    {
        //showPath();
        Path = P;
        Path.RemoveAt(0);
        Path.Insert(0, transform.position);
    }
    public List<Vector2> getPath()
    {
        return Path;
    }
    public void showPath()
    {
        for (int i = 0; i < Path.Count; ++i)
            Debug.Log( i + ": " + Path[i] + this.ToString());
    }
    public Vector2 position()
    {
        return transform.position;
    }
    public void Attack(GameObject enemy)
    {
        wep.aim = enemy;
    }
    public float Range(GameObject aim)
    {
        if (aim != null)
        {
            Vector2 from = transform.position;
            Vector2 dir = -transform.position + aim.transform.position;
            if (!Physics2D.Raycast(from, dir, dir.magnitude - 1.0f))
                return (transform.position - aim.transform.position).magnitude;
        }
        return float.MaxValue - 10f;
    }
    public void See()
    {

    }
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "StaticObjectWithCollider")
        {
            CC.Move(-CC.velocity * Time.deltaTime);
        }
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Bullet")
            Destroy(col.gameObject);
    }
    void Rotate()
    {
        if (Path.Count >= 1)
        {
            if (Vector2.Angle(Vector2.right, Path[0] - new Vector2(transform.position.x, transform.position.y)) <= 90)
            {
                //transform.localScale = new Vector3(-1, 1, 1);
            }
            else
                transform.localScale = new Vector3(1, 1, 1);
        }
        if (aim != null)
        {
            if (Vector2.Angle(Vector2.right, new Vector2(aim.transform.position.x, aim.transform.position.y) - new Vector2(transform.position.x, transform.position.y)) <= 90)
            {
                //transform.localScale = new Vector3(-1, 1, 1);
            }
            else
                transform.localScale = new Vector3(1, 1, 1);
            var newRotation = Quaternion.LookRotation(aim.transform.position - wep.gameObject.transform.position, Vector3.forward);
            newRotation.x = 0.0f;
            newRotation.y = 0.0f;
            wep.gameObject.transform.rotation = newRotation;
        }
        }
    public void GetBullet(BulletBehavior beh)
    {
        health = health - beh.damage;
        if (health < 0)
        {
            logic.DestoyedObject(gameObject);
        }
    }
}



