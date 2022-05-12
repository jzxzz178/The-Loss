using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class TotalNewControl : MonoBehaviour
{
    private SwapSystem swapSystem;
    
    private static GameObject[] players;
    private static GameObject[] lines;
    
    private static Dictionary<GameObject, bool> linkDictionary = new Dictionary<GameObject, bool>();
    private static int index = -1;

    private void Awake()
    {
        swapSystem = new SwapSystem();
        swapSystem.SwapPlayer.Swap.performed += context => Distribute();
    }

    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player").OrderBy(x => x.name).ToArray();
        foreach (var player in players)
        {
            linkDictionary[player] = false;
        }

        lines = GameObject.FindGameObjectsWithTag("Line");
        Distribute();
    }


    void Update()
    {
    }

    void Distribute()
    {
        index = (index + 1) % players.Length;
        linkDictionary[players[index]] = true;
        var i = 0;
        foreach (var line in lines)
        {
            if (i == index) i++;
            line.GetComponent<TotalNewLineMaker>().ChangePlayers(players[index], players[i]);
            i++;
        }
        Debug.Log("Distribute()");
    }
}