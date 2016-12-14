using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NewBehaviourScript : MonoBehaviour
{
    public float Speed = 1.0f;
    public GameObject bullet;
    public int player;
    public float range = 5.0f;
    public CharacterController CC;
    public List<Vector2> Path;
    private bool starterTimer = false;
    private float time = 0;
    private PathFinding P;
    void Start()
    {
        CC = GetComponent<CharacterController>();
        Path = new List<Vector2> {};
        P = GameObject.Find("PathFindingMan").GetComponent<PathFinding>();
        Move();
    }
    void  Update()
    {
        //RaycastHit2D hit = new RaycastHit2D();
        //for (int i = 0; i < 360; i += 36)
        //{
        //    if (Physics2D.Raycast(CC.transform.position, new Vector2(Mathf.Cos(i * 0.0174533333f), Mathf.Sin(i * 0.0174533333f)), 0.1f))
        //    {
        //        hit = Physics2D.Raycast(CC.transform.position, new Vector2(Mathf.Cos(i * 0.0174533333f), Mathf.Sin(i * 0.0174533333f)), 0.1f);
        //        CC.Move(-(new Vector2(Mathf.Cos(i * 0.0174533333f), Mathf.Sin(i * 0.0174533333f)).normalized) * Speed);
        //    }
        //}
        //if (!starterTimer)
        Move();
        //else
        //{
        //    if (time < 0.1f)
        //        time += Time.deltaTime;
        //    else
        //    {
        //        starterTimer = false;
        //        time = 0;
        //    }
        //}
        //Debug.Log(ToString() +" asd "+ Path[0]);
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
    //public void Attack(GameObject enemy)
    //{
    //    if (Range(enemy) <= range)
    //    {
    //        Vector2 from = transform.position;
    //        Vector2 dir = -transform.position + enemy.transform.position;
    //        GameObject bullet1 = Instantiate(bullet, transform.position, Quaternion.identity) as GameObject;
    //        bullet1.transform.Rotate(0,0,0);
    //        Rigidbody2D rb = bullet1.GetComponent<Rigidbody2D>();
    //        rb.velocity =  dir.normalized * 10;
            
    //    }
    //}
    public float Range(GameObject aim)
    {
        Vector2 from = transform.position;
        Vector2 dir = - transform.position + aim.transform.position;
        if (!Physics2D.Raycast(from, dir, dir.magnitude))
            return (transform.position - aim.transform.position).magnitude;
        else return float.MaxValue - 10f;
    }
    public void See()
    {

    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //Bounds b = hit.collider.bounds;
        //List<Vector2> l1 = P.Path(transform.position, b.center + b.extents);
        //List<Vector2> l2 = P.Path(b.center + b.extents, b.center + new Vector3(b.extents.x, b.extents.y));
        //List<Vector2> l3 = P.Path(new Vector3(b.extents.x, b.extents.y), b.center + b.extents);
        ////Path.InsertRange(0, );
        //CC.Move(-Path[0].normalized * Time.deltaTime);
        //starterTimer = true;
        if (hit.gameObject.tag == "StaticObjectWithCollider")
        {
            CC.Move(-CC.velocity * Time.deltaTime);
        }
    }
}



