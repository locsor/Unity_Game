using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public struct points
{
    public Vector2 point1;
    public float angle;
    public Collider2D coll;
}
public class Cone : MonoBehaviour {
    public List<Transform> inVision = new List<Transform>();
    private List<Vector2[]> gl_ep = new List<Vector2[]>();
    private List<points> pointsL = new List<points>();
    private List<float> anglesss = new List<float>();
    private GameObject coneM;
    private Vector2 first;
    private points hitpoint;
    private points hitpoint1;
    private points hitpoint2;
    private points origin;
    private List<Vector2> debugL;
    private Vector3 position;
    public GameObject Walls;
    private Transform[] wall_arr;
    private Mesh mesh;
    // Use this for initialization
    void Start () {
        position = transform.position;
        wall_arr = new Transform[Walls.transform.childCount];
        for(int i = 0; i < Walls.transform.childCount; i++)
        {
            inVision.Add(Walls.transform.GetChild(i));
        }
        for (int i = 0; i < inVision.Count; i++)
        {
            vertices(inVision[i]);
        }
        mesh = new Mesh();
    }
    void Update()
    {
        int layerMask = 1 << 8;
        Vector2 temp;
        Vector2 tempPos = transform.position;
<<<<<<< HEAD
        Quaternion spreadAngle1 = Quaternion.AngleAxis(0.01f, Vector3.forward);
        Quaternion spreadAngle2 = Quaternion.AngleAxis(-0.01f, Vector3.forward);
=======
        Quaternion spreadAngle1 = Quaternion.AngleAxis(0.1f, Vector3.forward);
        Quaternion spreadAngle2 = Quaternion.AngleAxis(-0.1f, Vector3.forward);
>>>>>>> 245d8fa0eb5068e123c9dcb270502275dffc82b9
        Vector2[] holder1 = new Vector2[5];
        Vector2[] holder2 = new Vector2[5];
        if (position != transform.position)
        {
            position = transform.position;
        }
        //for (int i = 0; i < gl_ep.Count; i++)
        //{
        //    Vector2[] holder = new Vector2[5];
        //    holder = gl_ep[i];
        //    if (!GetComponent<CircleCollider2D>().bounds.Contains(holder[4]))
        //    {
        //        gl_ep.Remove(gl_ep[i]);
        //    }
        //}
        for(int i = 0; i < pointsL.Count; i++)
        {
            RaycastHit2D hit = Physics2D.Linecast(transform.position, pointsL[i].point1, layerMask);
            if(hit.point != pointsL[i].point1)
            {
                pointsL.RemoveAt(i);
            }
        }
        if(position == transform.position)
        {
            pointsL.Clear();
        }
        for (int i = 0; i < gl_ep.Count; i++)
        {
            holder1 = gl_ep[i];
            for (int j = 0; j < holder1.Length - 1; j++)
            {
                RaycastHit2D hit = Physics2D.Linecast(transform.position, holder1[j], layerMask);
                hitpoint.point1 = hit.point;
                if (hit.point == holder1[j] && !pointsL.Contains(hitpoint))
                {
                    temp = spreadAngle1 * holder1[j];
                    RaycastHit2D hit1 = Physics2D.Raycast(transform.position, temp - tempPos, Mathf.Infinity, layerMask);
                    temp = spreadAngle2 * holder1[j];
                    RaycastHit2D hit2 = Physics2D.Raycast(transform.position, temp - tempPos, Mathf.Infinity, layerMask);
                    hitpoint1.point1 = hit1.point;
                    hitpoint2.point1 = hit2.point;
                    pointsL.Add(hitpoint);
                    pointsL.Add(hitpoint1);
                    pointsL.Add(hitpoint2);
                }
            }
        }
        pointsL = angles(pointsL);
        BuildMesh(pointsL);
    }
    List<points> angles(List<points> abc)
    {
        Vector2 tempPos = transform.position;
        points holder2;
        points holder3;
        for (int i = 0; i < pointsL.Count; i++)
        {
            holder2 = abc[i];
            holder2.angle = Vector2.Angle(tempPos - abc[i].point1, Vector2.up);
            Vector3 cross = Vector3.Cross(tempPos - abc[i].point1, Vector2.up);
            if(cross.z > 0)
            {
                holder2.angle = 360 - holder2.angle;
            }
            abc[i] = holder2;
        }
        for (int i = 0; i < abc.Count; i++)
        {
            for (int j = 0; j < abc.Count; j++)
            {
                if (abc[j].angle < abc[i].angle)
                {
                    holder3 = abc[i];
                    abc[i] = abc[j];
                    abc[j] = holder3;
                }
            }
        }
<<<<<<< HEAD
        //for(int i = 0; i < abc.Count; i++)
        //{
        //    Debug.DrawLine(transform.position, abc[i].point1, Color.red);
        //}
=======
>>>>>>> 245d8fa0eb5068e123c9dcb270502275dffc82b9
        origin.point1 = transform.position;
        abc.Add(origin);
        return abc;
    }
    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.CompareTag("Wall"))
    //    {
    //        inVision.Add(other.transform);
    //    }
    //    for (int i = 0; i < inVision.Count; i++)
    //    {
    //        vertices(inVision[i]);
    //    }
    //}
    //void OnTriggerExit2D(Collider2D other)
    //{
    //    if (other.CompareTag("Wall"))
    //    {
    //        inVision.Remove(other.transform);
    //    }
    //}
    void vertices(Transform tile)
    {
            Vector2[] endpoints = new Vector2[5];
            Vector2 center = tile.position;
            endpoints[0] = new Vector2(center.x + tile.localScale.x / 2, center.y + tile.localScale.y / 2);
            endpoints[1] = new Vector2(center.x + tile.localScale.x / 2, center.y - tile.localScale.y / 2);
            endpoints[2] = new Vector2(center.x - tile.localScale.x / 2, center.y + tile.localScale.y / 2);
            endpoints[3] = new Vector2(center.x - tile.localScale.x / 2, center.y - tile.localScale.y / 2);
            endpoints[4] = new Vector2(center.x, center.y);
            gl_ep.Add(endpoints);
    }
    void BuildMesh(List<points> abc)
    {
        int count = 0;
        Vector3[] vertic = new Vector3[abc.Count];
        int[] triangles = new int[abc.Count * 3];
        Vector3[] normals = new Vector3[abc.Count];
        Color[] colors = new Color[abc.Count];
        for (int i = 0; i < abc.Count; i++)
        {
<<<<<<< HEAD
            vertic[i] = abc[i].point1 * 40;
=======
            vertic[i] = abc[i].point1 * 20;
>>>>>>> 245d8fa0eb5068e123c9dcb270502275dffc82b9
            colors[i] = Color.black;
        }
        for(int i = 0; i < abc.Count - 2; i++)
        {
            triangles[count * 3] = abc.Count - 1;
            triangles[1 + count * 3] = i;
            triangles[2 + count * 3] = i + 1;
            count++;
        }
        triangles[abc.Count * 3 - 3] = abc.Count - 2;
        triangles[abc.Count * 3 - 2] = 0;
        triangles[abc.Count * 3 - 1] = abc.Count - 1;
        for (int i = 0; i < abc.Count; i++)
        {
            normals[i] = Vector3.up;
        }
<<<<<<< HEAD
=======
        Debug.Log(vertic.Length);
        Debug.Log(normals.Length);
>>>>>>> 245d8fa0eb5068e123c9dcb270502275dffc82b9
        mesh.vertices = vertic;
        mesh.triangles = triangles;
        mesh.normals = normals;
        MeshFilter mf = transform.GetChild(1).GetComponent<MeshFilter>();
        MeshRenderer mr = transform.GetChild(1).GetComponent<MeshRenderer>();
        mf.mesh = mesh;
        mf.transform.position = Vector3.zero;
    }
 }
