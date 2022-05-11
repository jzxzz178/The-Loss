using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlForPrefabs : MonoBehaviour
{
    // Start is called before the first frame update
    public static Vector3 start;
    public static Vector3 endForLine1;
    public static Vector3 endForLine2;
    
    public static string startName;
    public static string endForLine1Name;
    public static string endForLine2Name;

    private static Vector3 player1StInstanceVector3;
    private static Vector3 player2StInstanceVector3;
    private static Vector3 player3StInstanceVector3;
    
    public static readonly Dictionary<int, string> Squares = new Dictionary<int, string>();

    void Start()
    {
        player1StInstanceVector3 = GameObject.Find("Player").transform.position;
        player2StInstanceVector3 = GameObject.Find("Player1").transform.position;
        // player3stInstance = GameObject.Find("Square2").transform.position;
        Squares[0] = "Player";
        Squares[1] = "Player1";
        // Squares[3] = "Square1";
        
        startName = Squares[0];
        endForLine1Name = Squares[1];
        // endForLine2Name = Squares[2];
    }

    // Update is called once per frame
    void Update()
    {
        player1StInstanceVector3 = GameObject.Find("Square").transform.position;
        player2StInstanceVector3 = GameObject.Find("Square1").transform.position;
        // player3StInstanceVector3 = GameObject.Find("Square2").transform.position;
        
        start = Squares[0] == "Square" ? player1StInstanceVector3 : Squares[0] == "Square1" ? player2StInstanceVector3 : player3StInstanceVector3;
        endForLine2 = Squares[1] == "Square" ? player1StInstanceVector3 : Squares[1] == "Square1" ? player2StInstanceVector3 : player3StInstanceVector3;
        //endForLine1 = Squares[3] == "Square" ? player1StInstanceVector3 : Squares[3] == "Square1" ? player2StInstanceVector3 : player3StInstanceVector3;
        
        startName = Squares[1];
        endForLine1Name = Squares[3];
        // endForLine2Name = Squares[2];
        
        PlayerMovement.ConnectionIsTrue[startName] = true;
    }
}
