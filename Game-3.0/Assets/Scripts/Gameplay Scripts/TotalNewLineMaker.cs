using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.Windows.Speech;


[RequireComponent(typeof(LineRenderer))]
public class TotalNewLineMaker : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)] private float lineStartWidth = 0.4f;
    [SerializeField, Range(0f, 1f)] private float lineEndWidth = 0.4f;
    
    public LineRenderer line;
    private List<ParticleSystem> paricles=new List<ParticleSystem>();

    public Vector3 startPoint;
    public Vector3 endPoint;

    public Vector3 firstPlayerPosition;
    public Vector3 secondPlayerPosition;

    private GameObject firstPlayer;
    private GameObject secondPlayer;

    private bool flag;
    private float min;

    public void ChangePlayers(GameObject first, GameObject second)
    {
        firstPlayer = first;
        secondPlayer = second;
    }

    public void Start()
    {
        line.startWidth = lineStartWidth;
        line.endWidth = lineEndWidth;
        line.positionCount = 0;
        for(int i=0;i<gameObject.transform.childCount;i++)
            paricles.Add(gameObject.transform.GetChild(i).GetComponent<ParticleSystem>());

    }

    public void Update()
    {
        firstPlayerPosition = firstPlayer.transform.position;
        firstPlayerPosition.y += 1.2f;

        secondPlayerPosition = secondPlayer.transform.position;
        secondPlayerPosition.y += 1.2f;

        line.positionCount = 2;

        startPoint = new Vector3(firstPlayerPosition.x, firstPlayerPosition.y, -2);
        endPoint = new Vector3(secondPlayerPosition.x, secondPlayerPosition.y, -2);

        flag = true;
        min = float.MaxValue;

        var walls = GameObject.FindGameObjectsWithTag("Wall");
        foreach (var wall in walls)
        {
            var wallPos = wall.transform.position;
            var wallScale = wall.transform.localScale;

            ChangeEndPoint(wallPos, wallScale);
        }

        
        TotalNewControl.UpdateDictionary(secondPlayer, flag);
        
        line.SetPosition(0, startPoint);
        line.SetPosition(1, endPoint);
        for (var i=0; i < paricles.Count; i++)
        {
            paricles[i].transform.position = line.GetPosition(1);
        }
        // if(TotalNewControl.CheckForConnection(secondPlayer))
        //     foreach (var t in paricles)
        //         t.Stop();
        // else
        //     foreach (var t in paricles)
        //         t.Play();
    }

    private void ChangeEndPoint(Vector3 pos, Vector3 scale)
    {
        var points = new List<Vector2>
        {
            new Vector2(pos.x + scale.x / 2, pos.y + scale.y / 2),
            new Vector2(pos.x - scale.x / 2, pos.y + scale.y / 2),
            new Vector2(pos.x - scale.x / 2, pos.y - scale.y / 2),
            new Vector2(pos.x + scale.x / 2, pos.y - scale.y / 2),
            new Vector2(pos.x + scale.x / 2, pos.y + scale.y / 2)
        };
        for (var i = 0; i < points.Count - 1; i++)
        {
            var p1 = points[i];
            var p2 = points[i + 1];

            Vector2? intersectionPoint =
                FindIntersectionPoint(secondPlayerPosition.x, secondPlayerPosition.y, firstPlayerPosition.x,
                    firstPlayerPosition.y, p1.x, p1.y, p2.x, p2.y);
            if (intersectionPoint == null) continue;
            var dx = ((Vector2) intersectionPoint).x - firstPlayerPosition.x;
            var dy = ((Vector2) intersectionPoint).y - firstPlayerPosition.y;
            var sqrt = (float) Math.Sqrt(dx * dx + dy * dy);
            if (sqrt < min)
            {
                min = sqrt;
                endPoint = new Vector3(((Vector2) intersectionPoint).x, ((Vector2) intersectionPoint).y, -2);
            }

            flag = false;
        }
    }

    private Vector2? FindIntersectionPoint(float x1, float y1, float x2, float y2, float x3, float y3, float x4,
        float y4)
    {
        if (x1 > x2)
        {
            (x1, x2) = (x2, x1);
            (y1, y2) = (y2, y1);
        }

        if (x3 > x4)
        {
            (x3, x4) = (x4, x3);
            (y3, y4) = (y4, y3);
        }

        var x = 0f;
        var y = 0f;
        if (Math.Abs(x4 - x3) < 10e-7 && Math.Abs(x1 - x2) < 10e-7)
            return null;
        if (Math.Abs(x4 - x3) < 10e-7)
        {
            float k1 = (y2 - y1) / (x2 - x1);
            var b1 = y1 - k1 * x1;
            x = x3;
            y = k1 * x + b1;
            if (Math.Min(y1, y2) <= y && Math.Max(y1, y2) >= y && Math.Min(y3, y4) <= y && Math.Max(y3, y4) >= y)
                return new Vector2(x, y);
        }
        else if (Math.Abs(x1 - x2) < 10e-7)
        {
            float k2 = (y4 - y3) / (x4 - x3);
            var b2 = y3 - k2 * x3;
            x = x1;
            y = k2 * x + b2;
            if (Math.Min(y1, y2) <= y && Math.Max(y1, y2) >= y && Math.Min(y3, y4) <= y && Math.Max(y3, y4) >= y)
                return new Vector2(x, y);
        }
        else
        {
            var k1 = (y2 - y1) / (x2 - x1);
            var k2 = (y4 - y3) / (x4 - x3);
            if ((float) Math.Abs(k1 - k2) <= 10e-7f) return null;
            var b1 = y1 - k1 * x1;
            var b2 = y3 - k2 * x3;
            x = (b2 - b1) / (k1 - k2);
            y = k1 * x + b1;
            if (x1 <= x && x2 >= x && x3 <= x && x4 >= x)
                return new Vector2(x, y);
        }

        return null;
    }
}