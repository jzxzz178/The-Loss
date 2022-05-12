using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UIElements;
using UnityEngine.Windows.Speech;


[RequireComponent(typeof(LineRenderer))]
public class LineMaker2 : MonoBehaviour
{
    // Start is called before the first frame update
    public Color c1 = Color.yellow;
    public Color c2 = Color.red;
    public Color c3 = Color.blue;
    public Color c4 = Color.white;
    public LineRenderer line;
    public Vector3 endPoint;
    public Vector3 square;
    public Vector3 square1;
    private bool flag;
    private float min;

    public void Start()
    {
        line.startWidth = 0.1f;
        line.endWidth = 0.1f;
        line.positionCount = 0;
        line.SetColors(c4, c4);
    }

    // Update is called once per frame
    public void Update()
    {
        square1 = Control.start;
        square = Control.endForLine2;
        var wall1pos = GameObject.Find("Wall1").transform.position;
        var wall1scale = GameObject.Find("Wall1").transform.localScale;
        var wall2pos = GameObject.Find("Wall2").transform.position;
        var wall2scale = GameObject.Find("Wall2").transform.localScale;
        line.positionCount = 2;
        var startPoint = new Vector3(square1.x, square1.y, 10);

        endPoint = new Vector3(square.x, square.y, 10);
        flag = false;
        min = 100000000000000000000f;
        ChangeEndPoint(wall1pos, wall1scale);
        ChangeEndPoint(wall2pos, wall2scale);
        if (flag)
            PlayerMovement.ConnectionIsTrue[Control.endForLine2Name] = false;
        else
            PlayerMovement.ConnectionIsTrue[Control.endForLine2Name] = true;
        line.SetPosition(0, startPoint);
        line.SetPosition(1, endPoint);
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
                FindIntersectionPoint(square.x, square.y, square1.x, square1.y, p1.x, p1.y, p2.x, p2.y);
            if (intersectionPoint == null) continue;
            //var endPoint = new Vector3(((Vector2) intersectionPoint).x, ((Vector2) intersectionPoint).y, 10);
            var dx = ((Vector2) intersectionPoint).x - square1.x;
            var dy = ((Vector2) intersectionPoint).y - square1.y;
            var sqrt = (float) Math.Sqrt(dx * dx + dy * dy);
            if (sqrt < min)
            {
                min = sqrt;
                endPoint = new Vector3(((Vector2) intersectionPoint).x, ((Vector2) intersectionPoint).y, 10);
            }

            flag = true;
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