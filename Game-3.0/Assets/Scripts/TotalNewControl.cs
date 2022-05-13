using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class TotalNewControl : MonoBehaviour
{
    private SwapSystem swapSystem;
    private TabList list; 

    private static GameObject[] players;
    private static GameObject[] lines;

    private static Dictionary<GameObject, bool> linkDictionary = new Dictionary<GameObject, bool>();
    private static int index = -1;

    private static LinkedList<PlayerCell> queueList;

    // private static Queue<GameObject> queue;

    private void Awake()
    {
        swapSystem = new SwapSystem();
        swapSystem.SwapPlayer.Swap.performed += context => Distribute();
        queueList = new LinkedList<PlayerCell>();
        list = new TabList();
        // queue = new Queue<GameObject>();
    }

    private void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player").OrderBy(x => x.name).ToArray();
        foreach (var player in players)
            UpdateDictionary(player, false);

        UpdateDictionary(players[index + 1], true);
        queueList.AddLast(new PlayerCell(players[index + 1], true));

        lines = GameObject.FindGameObjectsWithTag("Line");
        Distribute();
    }

    public static void UpdateDictionary(GameObject player, bool value)
    {
        linkDictionary[player] = value;
        var playerCell = new PlayerCell(player);
        if (value)
            queueList.AddFirst(playerCell);
        else
            queueList.Remove(playerCell);
    }

    public static bool CheckForConnection(GameObject player) => linkDictionary[player];

    private static void Distribute()
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
            // if (i == index) i++;
            if (queueList.Last.Value.ActiveFlag)
            {
                line.GetComponent<TotalNewLineMaker>()
                    .ChangePlayers(queueList.Last.Value.Player, queueList.First.Value.Player);
                queueList.First.Value.ActiveFlag = false;
                queueList.AddLast(queueList.First.Value);
                queueList.RemoveFirst();
            }

            i++;
        }
    }

    private void OnEnable() => swapSystem.Enable();

    private void OnDisable() => swapSystem.Disable();

    public class PlayerCell
    {
        public readonly GameObject Player;
        public PlayerCell Next { get; set; }
        public PlayerCell Previous { get; set; }


        public PlayerCell(GameObject player)
        {
            Player = player;
            Next = null;
            Previous = null;
        }
    }

    public class TabList
    {
        public PlayerCell ActivePlayer { get; set; }
        public PlayerCell Head { get; set; }
        public PlayerCell Tale { get; set; }

        public TabList()
        {
            Head = Tale = null;
        }

        public bool CanSwap()
        {
            return Head != Tale;
        }
        
        public GameObject TakePlayerToSwap()
        {
            var player = Head.Player;
            MoveFirstToEnd();
            return player;
        }
        
        public void Add(GameObject player)
        {
            if (Head == null || Tale == null)
            {
                Head = Tale = new PlayerCell(player);
                ActivePlayer = Tale;
                return;
            }
            var newHead = new PlayerCell(player);
            Head.Previous = newHead;
            newHead.Next = Head;
            Head = newHead;
        }

        public void Remove(GameObject player)
        {
            var current = Head;
            while (current != null)
            {
                if (current.Player == player)
                {
                    current.Previous.Next = current.Next;
                    current.Next.Previous = current.Previous;
                    break;
                }

                current = current.Next;
            }
        }

        public void MoveFirstToEnd()
        {
            if (Head == Tale) return;
            
            var newHead = Head.Next;
            
            Head.Next.Previous = null;
            Head.Next = null;
            Tale.Next = Head;
            Head.Previous = Tale;
            
            Head = newHead;
        }
    }
}