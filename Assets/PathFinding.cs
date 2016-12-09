using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Point
{
    public int x = 0, y = 0;
    public bool canMove = true;
    public int PathLenghtFromStart = 0;
    public int PathLenghtToTheEnd = 0;
    public Point cameFrom;
    public int FullLenght = 0;
    public void UpdateFullLength()
    {
        FullLenght = PathLenghtToTheEnd + PathLenghtFromStart;
    }
    public Point()
    {
        x = 0;
        y = 0;
        canMove = true;
        PathLenghtFromStart = 0;
        PathLenghtToTheEnd = 0;
        cameFrom = null;
        FullLenght = 0;
    }
    public Point(int x, int y, int PathLenghtFromStart, int PathLenghtToTheEnd, Point cameFrom)
    {
        this.x = x;
        this.y = y;
        this.PathLenghtFromStart = PathLenghtFromStart;
        this.PathLenghtToTheEnd = PathLenghtToTheEnd;
        canMove = true;
        FullLenght = PathLenghtToTheEnd + PathLenghtFromStart;
        this.cameFrom = cameFrom;
    }
    public void get()
    {
        Debug.Log("x = " + x + " y = " + y + " canMove = " + canMove + " PathLenghtFromStart = " + PathLenghtFromStart + " PathLenghtToTheEnd = " + PathLenghtToTheEnd);
    }
    public static bool operator !=(Point p, Point g)
    {
        if (p.x != g.x || p.y != g.y)
            return true;
        else
            return false;
    }
    public static bool operator ==(Point p, Point g)
    {
        if (p.x == g.x && p.y == g.y)
            return true;
        else
            return false;
    }
}
public class ray
{
    public int x;
    public int y;
    public float distance;
    public ray(int x, int y, float z)
    {
        this.x = x;
        this.y = y;
        distance = z;
    }
    public ray()
    {
        x = 0;
        y = 0;
        distance = 0;
    }
    public int count()
    {
        return y - x;
    }
}
public class PathFinding : MonoBehaviour
{
    public int mapWeight = 100, mapHeight = 100;
    private GlobalVariabels GV;
    public bool[,] Map;
    public PathFinding()
    {
        Map = new bool[1, 1];
        Map[0,0] = true;
        mapWeight = 1;
        mapHeight = 1;
    }
    public List<Vector2> Path(Vector2 s, Vector2 f)
    {
        Point start = new Point(Mathf.FloorToInt(s.x), Mathf.FloorToInt(s.y), 0, 0, null);
        Point finish = new Point(Mathf.FloorToInt(f.x), Mathf.FloorToInt(f.y), 0, 0, null);
        return Path(start, finish);
    }
    public List<Vector2> Path(Point start, Point finish)
    {
        if (start.x < mapWeight && start.y < mapHeight && finish.x < mapWeight && finish.y < mapHeight && start.x >= 0 && start.y >= 0 && finish.x >= 0 && finish.y >= 0 && Map[start.x, start.y] && Map[finish.x, finish.y])
        {
            start.PathLenghtToTheEnd = Mathf.Abs(start.x - finish.x) + Mathf.Abs(start.y - finish.y);
            start.UpdateFullLength();

            List<Point> Open = new List<Point> { start };
            List<Point> Closed = new List<Point> { };
            Point currentpoint = start;

            while (Open.Count > 0)
            {
                Closed.Add(currentpoint);
                Open.RemoveAll(match => match == currentpoint);
                if (currentpoint == finish)
                {
                    break;
                }

                foreach (Point kek in neibr(currentpoint, finish))
                {
                    if (Closed.FindIndex(match => match == kek) < 0)
                    {
                        if (Open.FindIndex(match => match == kek) < 0)
                        {
                            Open.Add(kek);
                        }
                        else
                        {
                            if (Open.Find(match => match == kek).PathLenghtFromStart > kek.PathLenghtFromStart)
                                Open[Open.FindIndex(match => match == kek)] = kek;
                        }
                    }
                }
                Point h = Open[0];
                float exp = Dist(finish, h);
                foreach (Point kek in Open)
                    if (kek.FullLenght < h.FullLenght || (kek.FullLenght == h.FullLenght && Dist(kek, finish) < exp))
                    {
                        h = kek;
                        exp = Dist(h, finish);
                    }
                currentpoint = h;
            }


            List<Point> path = new List<Point> { };
            if (currentpoint == finish)
                while (currentpoint != start)
                {
                    path.Insert(0, currentpoint);
                    currentpoint = currentpoint.cameFrom;
                }
            List<Vector2> v = new List<Vector2> { };
            for (int i = 0; i < path.Count; ++i)
            {
                v.Add(new Vector2(path[i].x, path[i].y));
                //Debug.Log(v[i] + "||");
            }

            v.Insert(0, new Vector2(start.x, start.y));



            List<ray> rays = new List<ray>();
            for (int i = 0; i < v.Count - 1; ++i)
            {
                ray w = new ray();
                int LayerMask = 0;
                LayerMask = ~LayerMask;
                for (int j = i + 1; j < v.Count; ++j)
                {
                    if (!(Physics2D.Raycast(v[i] + Vector2.up * 0.9f + Vector2.right * 0.1f, v[j] - v[i], (v[j] - v[i]).magnitude - 0.1f, LayerMask) || Physics2D.Raycast(v[i] + Vector2.right * 0.9f + Vector2.up * 0.1f, v[j] - v[i], (v[j] - v[i]).magnitude - 0.1f, LayerMask) || Physics2D.Raycast(v[i] + Vector2.up * 0.1f + Vector2.right * 0.1f, v[j] - v[i], (v[j] - v[i]).magnitude - 0.1f, LayerMask) || Physics2D.Raycast(v[i] + Vector2.right * 0.9f + Vector2.up * 0.9f, v[j] - v[i], (v[j] - v[i]).magnitude - 0.1f, LayerMask)))
                    {
                        w = new ray(i, j, Dist(v[j], v[i]));
                    }
                    else
                        break;
                }

                rays.Add(w);
                //Debug.Log(w.x + "  " + w.y);
            }

             rays = RAYS(rays);
             List<Vector2> L = new List<Vector2>();
             L.Add(new Vector2(v[rays[0].x].x + 0.5f, v[rays[0].x].y + 0.5f));
            // Debug.Log(rays[0].x);
             for (int i = 0; i < rays.Count; ++i)
             {
                L.Add(new Vector2(v[rays[i].y].x + 0.5f, v[rays[i].y].y + 0.5f));
               // L.Add(v[rays[i].y]);
                // Debug.Log(rays[i].y);
             }
            return L;
        }
        else
        {
            return new List<Vector2> {};
        }
    }
    public List<Point> neibr(Point p, Point finish)
    {
        List<Point> nei = new List<Point> {};
        if (p.x + 1 < mapWeight && Map[p.x + 1, p.y])
            nei.Add(new Point(p.x + 1, p.y, p.PathLenghtFromStart + 1, Mathf.Abs(p.x + 1 - finish.x) + Mathf.Abs(p.y - finish.y), p));
        if (p.x - 1 >= 0 && Map[p.x - 1, p.y])
            nei.Add(new Point(p.x - 1, p.y, p.PathLenghtFromStart + 1, Mathf.Abs(p.x - 1 - finish.x) + Mathf.Abs(p.y - finish.y), p));
        if (p.y + 1 < mapHeight && Map[p.x, p.y + 1])
            nei.Add(new Point(p.x, p.y + 1, p.PathLenghtFromStart + 1, Mathf.Abs(p.x  - finish.x) + Mathf.Abs(p.y + 1 - finish.y), p));
        if (p.y - 1 >= 0 && Map[p.x, p.y - 1])
            nei.Add(new Point(p.x , p.y - 1, p.PathLenghtFromStart + 1, Mathf.Abs(p.x  - finish.x) + Mathf.Abs(p.y - 1 - finish.y), p));

        return nei;
    }
    public List<Point> neibr(Point p)
    {
        List<Point> nei = new List<Point> { };
        if (p.x + 1 < mapWeight && Map[p.x + 1, p.y])
            nei.Add(new Point(p.x + 1, p.y,0, 0, null));
        if (p.x - 1 >= 0 && Map[p.x - 1, p.y])
            nei.Add(new Point(p.x - 1, p.y, 0, 0, null));
        if (p.y + 1 < mapHeight && Map[p.x, p.y + 1])
            nei.Add(new Point(p.x, p.y + 1, 0, 0, null));
        if (p.y - 1 >= 0 && Map[p.x, p.y - 1])
            nei.Add(new Point(p.x, p.y - 1, 0, 0, null));

        return nei;
    }
    void Start()
    {
        GV = GameObject.Find("Global Varibales").GetComponent<GlobalVariabels>();
        Map = new bool[GV.weight, GV.height];
        for (int i = 0; i < mapWeight; ++i)
            for (int j = 0; j < mapHeight; ++j)
                Map[i, j] = true;
        GameObject[] list = GameObject.FindGameObjectsWithTag("StaticObjectWithCollider");
        foreach (GameObject kek in list)
            if (Mathf.FloorToInt(kek.transform.position.x) < mapWeight && Mathf.FloorToInt(kek.transform.position.y) < mapHeight && Mathf.FloorToInt(kek.transform.position.x) >= 0 && Mathf.FloorToInt(kek.transform.position.y) >= 0)
                Map[Mathf.FloorToInt(kek.transform.position.x), Mathf.FloorToInt(kek.transform.position.y)] = false;
    }
    List<ray> DelLishElem(List<ray> s)
    {
        List<ray> L = new List<ray>();
        for (int i = 1; i < s.Count; ++i)
        {
            if (!((s[i - 1].y <= s[i].y)&&(s[i - 1].x == s[i].x)))
            {
                L.Add(s[i - 1]);
            }
        }
        return L;
    }
    float Dist(Point a, Point b)
    {
        return Mathf.Sqrt((b.x - a.x) * (b.x - a.x) + (b.y - a.y) * (b.y - a.y));
    }
    List<ray> RAYS(List<ray> r)
    {
        List<ray> rays = r;
        List<ray> temp2 = new List<ray>();

        do
        {
            ray temp = new ray();
            foreach (ray kek in rays)
            {
                if (kek.distance > temp.distance)
                    temp = kek;
            }
            rays.Remove(temp);
            temp2.Add(temp);
            foreach (ray kek in rays)
            {
                if (kek.x < temp.x && kek.y > temp.x)
                    kek.y = temp.x;
                else
                    if (kek.x < temp.y && kek.y > temp.y)
                    kek.x = temp.y;
            }
            rays.RemoveAll(kek => kek.x >= temp.x && kek.y <= temp.y);
        } while (rays.Count > 0);
        temp2.Sort(delegate (ray a, ray b)
        {
            return a.x.CompareTo(b.x);
        });
        return temp2;
    }
    public List<List<Vector2>> Paths(List<Vector2> s, Vector2 f)
    {
        Point fi = new Point(Mathf.FloorToInt(f.x), Mathf.FloorToInt(f.y), 0, 0, null);
        List<Point> finish = new List<Point>();
        finish.Add(fi);
        List<Point> start = new List<Point>();
        for (int h = 0; h < s.Count; ++h)
            start.Add(new Point(Mathf.FloorToInt(s[h].x), Mathf.FloorToInt(s[h].y), 0, 0, null));
        List<List<Vector2>> paths = new List<List<Vector2>>();

        paths.Add(Path(start[0], fi));
        int j = 1;
        for (int i = 0; i < s.Count - 1;)
        {
            for (int p = -j; p <= j && p >= 0 && p < mapWeight; ++p)
            {
                if (Map[fi.x + p, fi.y + j])
                {
                    paths.Add(Path(start[i], new Point(fi.x + p, fi.y + j, 0, 0, null)));
                    ++i;
                }
                if (Map[fi.x + p, fi.y - j])
                {
                    paths.Add(Path(start[i], new Point(fi.x + p, fi.y + j, 0, 0, null)));
                    ++i;
                }
            }
            for (int p = -j + 1; p < j && p >= 0 && p < mapHeight; ++p)
            {
                if (Map[fi.x + j, fi.y + p])
                {
                    paths.Add(Path(start[i], new Point(fi.x + j, fi.y + p, 0, 0, null)));
                    ++i;
                }
                if (Map[fi.x - j, fi.y - p])
                {
                    paths.Add(Path(start[i], new Point(fi.x - j, fi.y - p, 0, 0, null)));
                    ++i;
                }
            }
            ++j;
        }

        return paths;
    }
    float Dist(Vector2 a, Vector2 b)
    {
        return (b-a).magnitude;
    }
    public bool PosToMove(Vector2 to)
    {
        return (Mathf.FloorToInt(to.x) >= 0 && Mathf.FloorToInt(to.x) < mapWeight && Mathf.FloorToInt(to.y) >= 0 && Mathf.FloorToInt(to.y) < mapHeight && Map[Mathf.FloorToInt(to.x), Mathf.FloorToInt(to.y)]);
    }
    public List<Vector2> ClosesedFreePoints(Point position, int CountOfBegins)
    {
        List<Vector2> result = new List<Vector2> {};
        int h = 1, t = 1, s = 1, p = 1;
        while (result.Count < CountOfBegins)
        {
            for (int i = s; i >= -p && result.Count < CountOfBegins; --i)
            {
                if (position.x + i >= 0 && position.x + i < mapWeight && position.y + h >= 0 && position.y + h < mapHeight && Map[position.x + i, position.y + h] && !result.Contains(new Vector2(position.x + i, position.y + h)))
                {
                    result.Add(new Vector2(position.x + i, position.y + h));
                    //Debug.Log("Added new point");
                }
                if (position.x + i >= 0 && position.x + i < mapWeight && position.y - t >= 0 && position.y - t < mapHeight && Map[position.x + i, position.y - t] && !result.Contains(new Vector2(position.x + i, position.y - t)))
                {
                    result.Add(new Vector2(position.x + i, position.y - t));
                    // Debug.Log("Added new point");
                }
            }
            ++s;
            ++p;
            for (int i = h; i >= -t && result.Count < CountOfBegins; --i)
            {
                if (position.y + i >= 0 && position.y + i < mapWeight && position.x + h >= 0 && position.x + h < mapHeight && Map[position.x + h, position.y + i] && !result.Contains(new Vector2(position.x + i, position.y + h)))
                {
                    result.Add(new Vector2(position.x + i, position.y + h));
                    // Debug.Log("Added new point");
                }
                if (position.y + i >= 0 && position.y + i < mapWeight && position.x - t >= 0 && position.x - t < mapHeight && Map[position.x - t, position.y + i] && !result.Contains(new Vector2(position.x + i, position.y - t)))
                {
                    result.Add(new Vector2(position.x + i, position.y - t));
                    // Debug.Log("Added new point");
                }
            }
            ++h;
            ++t;
        }
        result.Sort(delegate (Vector2 a, Vector2 b)
        {
            return ((a - new Vector2(position.x, position.y)).magnitude.CompareTo((b - new Vector2(position.x, position.y)).magnitude));
        });
        result.Insert(0, new Vector2(position.x, position.y));
        //TO DO: REMAKE THIS
        //if (Map[position.x + 1, position.y + 1])
        //    result.Add(new Vector2(position.x + 1, position.y + 1));
        //if (Map[position.x + 1, position.y])
        //    result.Add(new Vector2(position.x + 1, position.y));
        //if (Map[position.x + 1, position.y - 1])
        //    result.Add(new Vector2(position.x + 1, position.y - 1));
        //if (Map[position.x, position.y + 1])
        //    result.Add(new Vector2(position.x + 1, position.y + 1));
        //if (Map[position.x, position.y - 1])
        //    result.Add(new Vector2(position.x, position.y - 1));
        //if (Map[position.x - 1, position.y + 1])
        //    result.Add(new Vector2(position.x - 1, position.y + 1));
        //if (Map[position.x - 1, position.y])
        //    result.Add(new Vector2(position.x - 1, position.y));
        //if (Map[position.x - 1, position.y - 1])
        //    result.Add(new Vector2(position.x - 1, position.y - 1));
        //for (int i = 0; i < points.Count; ++i)
        //    result.Add(new Vector2(points[i].x, points[i].y));
        return result;
    }
    //public List<Vector2> movingObjects()
    //{
    //    GameObject[] list = GameObject.FindGameObjectsWithTag("YourUnit"); 
    //}
}