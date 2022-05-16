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

    private static readonly Dictionary<GameObject, bool> LinkDictionary = new Dictionary<GameObject, bool>();
    private static int index = -1;

    private void Awake()
    {
        swapSystem = new SwapSystem();
        swapSystem.SwapPlayer.Swap.performed += context => Distribute();
    }

    private void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player").OrderBy(x => x.name).ToArray();
        tabList = new TabList(GameObject.Find("Player"));
        
        foreach (var player in players)
            UpdateDictionary(player, false);
        
        UpdateDictionary(players[0], true);
        lines = GameObject.FindGameObjectsWithTag("Line");
        
        var i = 0;
        foreach (var line in lines)
        {
            if (players[i] == tabList.ActivePlayerCell.Player)
                i++;
            line.GetComponent<TotalNewLineMaker>()
                .ChangePlayers(tabList.ActivePlayerCell.Player, players[i]);
            i++;
        }
    }

    public static void UpdateDictionary(GameObject player, bool value)
    {
        LinkDictionary[player] = value;
        if (value) tabList.Add(player);
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
        public PlayerCell ActivePlayerCell;
        private PlayerCell head;
        private PlayerCell tale;

        public TabList(GameObject activePlayer)
        {
            ActivePlayerCell = new PlayerCell(activePlayer);
            head = tale = null;
            foreach (var player in players)
            {
                if (player != activePlayer)
                    Add(player);
            }
        }
        
        public void Add(GameObject player)
        {
            if (ActivePlayerCell != null)
                if (player == ActivePlayerCell.Player) 
                    return;
            
            if (head == null || tale == null)
            {
                tale = head = new PlayerCell(player);
                return;
            }

            Remove(player);
            var newHead = new PlayerCell(player)
            {
                Next = head,
                Previous = null
            };
            
            head.Previous = newHead;
            head = newHead;
        }

        public bool IsAbleToSwap()
        {
            var current = head;
            while (current != null)
            {
                if (LinkDictionary[current.Player])
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
            var current = head;
            var player = current.Player;
            while (current != null)
            {
                if (LinkDictionary[current.Player])
                {
                    player = current.Player;
                    break;
                }

                current = current.Next;
            }

            SwapElementWithActivePlayerAndMoveElementToEnd(current);
            return player;
        }

        private void SwapElementWithActivePlayerAndMoveElementToEnd(PlayerCell newActivePlayer)
        {
            Remove(newActivePlayer.Player);
            newActivePlayer.Next = null;
            newActivePlayer.Previous = null;
            
            tale.Next = ActivePlayerCell;
            ActivePlayerCell.Previous = tale;
            tale = ActivePlayerCell;

            ActivePlayerCell = newActivePlayer;

            /*if (newActivePlayer.Previous != null) newActivePlayer.Previous.Next = ActivePlayerCell;
            if (newActivePlayer.Next != null) newActivePlayer.Next.Previous = ActivePlayerCell;

            ActivePlayerCell.Next = newActivePlayer.Next;
            ActivePlayerCell.Previous = newActivePlayer.Previous;

            var newCell = ActivePlayerCell;

            ActivePlayerCell = newActivePlayer;
            ActivePlayerCell.Next = null;
            ActivePlayerCell.Previous = null;
            MoveCellToEnd(newCell);*/
        }

        private void Remove (GameObject player)
        {
            var current = head;
            while (current != null)
            {
                if (current.Player == player)
                {
                    if (current.Previous != null) current.Previous.Next = current.Next;
                    if (current.Next != null) current.Next.Previous = current.Previous;
                    if (head == current)
                    {
                        head = head.Next;
                        if (head != null) head.Previous = null;
                    }
                    if (tale == current)
                    {
                        tale = tale.Previous;
                        if (tale != null) tale.Next = null;
                    }
                    break;
                }

                current = current.Next;
            }
        }

        private void MoveCellToEnd(PlayerCell cell)
        {
            if (head == tale) return;

            Remove(cell.Player);
            tale.Next = cell;
            cell.Previous = tale;
            cell.Next = null;
            tale = cell;
        }
    }
}