using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Control : MonoBehaviour
{
    // Start is called before the first frame update
    public static Vector3 start;
    public static Vector3 endForLine1;
    public static Vector3 endForLine2;
    public static string startName;
    public static string endForLine1Name;
    public static string endForLine2Name;

    private static Vector3 square;
    private static Vector3 square1;
    private static Vector3 square2;
    public static readonly Dictionary<int, string> Squares = new Dictionary<int, string>();

    void Start()
    {
        square = GameObject.Find("Square").transform.position;
        square1 = GameObject.Find("Square1").transform.position;
        square2 = GameObject.Find("Square2").transform.position;
        
        Squares[1] = "Square2";
        Squares[2] = "Square";
        Squares[3] = "Square1";
        
        startName = Squares[1];
        endForLine1Name = Squares[2];
        endForLine2Name = Squares[3];
    }

    // Update is called once per frame
    void Update()
    {
        square = GameObject.Find("Square").transform.position;
        square1 = GameObject.Find("Square1").transform.position;
        square2 = GameObject.Find("Square2").transform.position;
        
        start = Squares[1] == "Square" ? square : Squares[1] == "Square1" ? square1 : square2;
        endForLine2 = Squares[2] == "Square" ? square : Squares[2] == "Square1" ? square1 : square2;
        endForLine1 = Squares[3] == "Square" ? square : Squares[3] == "Square1" ? square1 : square2;
        
        startName = Squares[1];
        endForLine1Name = Squares[3];
        endForLine2Name = Squares[2];
        
        PlayerMovement.ConnectionIsTrue[startName] = true;
    }
}