using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class TotalNewControl : MonoBehaviour
{
    private SwapSystem swapSystem;
    private static TabList tabList;

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
        tabList = new TabList();
    }

    private void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player").OrderBy(x => x.name).ToArray();
        foreach (var player in players)
            UpdateDictionary(player, false);

        UpdateDictionary(players[index + 1], true);
        tabList.Add(players[index + 1]);

        lines = GameObject.FindGameObjectsWithTag("Line");
        Distribute();
    }

    public static void UpdateDictionary(GameObject player, bool value)
    {
        linkDictionary[player] = value;
        tabList.Add(player);
    }

    public static bool CheckForConnection(GameObject player) => linkDictionary[player];

    private void Distribute()
    {
        /*for (var j = 0; j < players.Length; j++)
        {
            index = (index + 1) % players.Length;
            if (linkDictionary[players[index]])
                break;
            if (j == players.Length - 1) return;
        }*/
        // if (!tabList.CanSwap()) return;
        var player = tabList.TakePlayerToSwap();
        var i = 0;
        foreach (var line in lines)
        {
            if (players[i] != player)
                line.GetComponent<TotalNewLineMaker>()
                    .ChangePlayers(player, players[i]);
            i++;
            
            /*if (tabList.CanSwap())
            {
                line.GetComponent<TotalNewLineMaker>()
                    .ChangePlayers(tabList.Tale.Player, tabList.TakePlayerToSwap());
            }*/
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

    /// <summary>
    /// В поле Head находится следующий на очередь для свапа игрок, в ActivePlayer - активный 
    /// </summary>
    private class TabList
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
            var current = Head;
            while (current != null)
            {
                if (linkDictionary[current.Player])
                    return true;
                current = current.Next;
            }

            return false;
        }

        /// <summary>
        /// Возвращает ссылку на игрока, на который надо поставить линии
        /// </summary>
        /// <returns></returns>
        public GameObject TakePlayerToSwap()
        {
            var current = Head;
            var player = current.Player;
            while (current != null)
            {
                if (linkDictionary[current.Player])
                {
                    player = current.Player;
                    break;
                }

                current = current.Next;
            }

            var newCell = SwapElementWithActivePlayer(current);
            MoveCellToEnd(newCell);
            return player;
        }

        private PlayerCell SwapElementWithActivePlayer(PlayerCell cell)
        {
            cell.Previous.Next = ActivePlayer;
            cell.Next.Previous = ActivePlayer;

            ActivePlayer.Next = cell.Next;
            ActivePlayer.Previous = cell.Previous;

            var newCell = ActivePlayer;

            ActivePlayer = cell;
            ActivePlayer.Next = null;
            ActivePlayer.Previous = null;
            return newCell;
        }

        public void Add(GameObject player)
        {
            if (Head == null || Tale == null)
            {
                ActivePlayer = new PlayerCell(player);
                return;
            }

            var newHead = new PlayerCell(player);
            Head.Previous = newHead;
            newHead.Next = Head;
            Head = newHead;
        }

        private void Remove(GameObject player)
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

        private void MoveCellToEnd(PlayerCell cell)
        {
            if (Head == Tale) return;

            Remove(cell.Player);
            Tale.Next = cell;
            cell.Previous = Tale;
            Tale = cell;
        }
    }
}