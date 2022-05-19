using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = System.Object;

public class TotalNewControl : MonoBehaviour
{
    private SwapSystem swapSystem;
    private static TabList tabList;


    private static GameObject[] players;
    private static GameObject[] lines;

    public int SpiritCount;
    private int MaxSpiritCount = 1;

    private static readonly Dictionary<GameObject, bool> LinkDictionary = new Dictionary<GameObject, bool>();

    private void Awake()
    {
        swapSystem = new SwapSystem();
        swapSystem.SwapPlayer.Swap.performed += context => Distribute();
    }

    private void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player").OrderBy(x => x.name).ToArray();
        tabList = new TabList(players);

        foreach (var player in players)
            UpdateDictionary(player, false);

        UpdateDictionary(players[0], true);
        lines = GameObject.FindGameObjectsWithTag("Line");

        var i = 0;
        foreach (var line in lines)
        {
            if (players[i] == tabList.ActivePlayer)
                i++;
            line.GetComponent<TotalNewLineMaker>()
                .ChangePlayers(tabList.ActivePlayer, players[i]);
            i++;
        }
    }

    public void Update()
    {
        if (SpiritCount == MaxSpiritCount) OpenDoor();
    }

    public void SpiritCounterIncrement() => SpiritCount++;

    private static void OpenDoor()
    {
       // Debug.Log("OK");
    }

    public static void UpdateDictionary(GameObject player, bool value)
    {
        if (LinkDictionary.ContainsKey(player))
        {
            if (LinkDictionary[player] == value) return;
            LinkDictionary[player] = value;
            if (value) tabList.AddOrGoToUp(player);
        }

        else LinkDictionary[player] = value;
    }

    public static bool CheckForConnection(GameObject player) => LinkDictionary[player];

    private static void Distribute()
    {
        if (!tabList.IsAbleToSwap()) return;
        var player = tabList.TakePlayerToSwap();
        var i = 0;
        foreach (var line in lines)
        {
            if (players[i] == player)
                i++;
            line.GetComponent<TotalNewLineMaker>()
                .ChangePlayers(player, players[i]);
            i++;
        }
    }

    private void OnEnable() => swapSystem.Enable();

    private void OnDisable() => swapSystem.Disable();

    private class TabList
    {
        private readonly List<GameObject> playerTabList;
        public GameObject ActivePlayer;

        public TabList(GameObject[] array)
        {
            playerTabList = array.ToList();
            ActivePlayer = playerTabList[0];
            playerTabList.Remove(ActivePlayer);
        }

        public void AddOrGoToUp(GameObject player)
        {
            foreach (var o in playerTabList.Where(o => o == player && o != ActivePlayer))
            {
                playerTabList.Remove(o);
                break;
            }

            if (player != null && player != ActivePlayer)
                playerTabList.Insert(0, player);
        }

        public bool IsAbleToSwap()
        {
            return playerTabList.Any(o => LinkDictionary[o] && o != ActivePlayer);
        }

        public GameObject TakePlayerToSwap()
        {
            foreach (var player in playerTabList.Where(p => LinkDictionary[p] && p != ActivePlayer))
            {
                playerTabList.Remove(player);
                GoToBottom(ActivePlayer);
                ActivePlayer = player;
                return player;
            }

            throw new Exception();
        }

        private void GoToBottom(GameObject player)
        {
            playerTabList.Add(player);
        }
    }
}