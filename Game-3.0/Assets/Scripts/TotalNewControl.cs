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

        linkDictionary[players[index + 1]] = true;

        lines = GameObject.FindGameObjectsWithTag("Line");
        Distribute();
    }

    public static void UpdateDictionary(GameObject player, bool value)
    {
        linkDictionary[player] = value;
        if (!value) return;
        
        var i = 0;
        foreach (var o in players)
        {
            if (o == player)
                index = i;
            i++;
        }
    }

    public static bool CheckForConnection(GameObject player) => linkDictionary[player];

    void Update()
    {
    }

    void Distribute()
    {
        for (var j = 0; j < players.Length; j++)
        {
            index = (index + 1) % players.Length;
            if (linkDictionary[players[index]])
                break;
            if (j == players.Length - 1) return;
        }

        var i = 0;
        foreach (var line in lines)
        {
            if (i == index) i++;
            line.GetComponent<TotalNewLineMaker>().ChangePlayers(players[index], players[i]);
            i++;
        }
    }

    private void OnEnable() => swapSystem.Enable();

    private void OnDisable() => swapSystem.Disable();
}