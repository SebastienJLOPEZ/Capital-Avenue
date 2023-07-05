﻿using Capital_Avenue.Views;
using Capital_Avenue.Views.Board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Capital_Avenue.Models
{
    public class Game
    {
        Random rnd = new Random();
        public List<Player> PlayerList { get; set; }
        public int CurrentPlayer;
        public Dice Dice { get; private set; }
        private int Ldice = 0;
        private int NbDice = 2;
        public Board GameBoard { get; set; }
        int TotalPlayer;
        public Game(List<Player> pList)
        {
            PlayerList = pList;
            CurrentPlayer = 0;
            Dice = new Dice();
        }
        
        public void DiceInit()
        {
            TotalPlayer = PlayerList.Count;
            Dice.addDice(Ldice, NbDice);
        }
        
       public void DiceResult()
        { //Change it to actual double rule
            Dice.DiceThrower();
            switch(Dice.isDouble)
            {
                case false: 
                    PlayerList[CurrentPlayer].TotalDouble = 0;
                    break;
                case true:
                    PlayerList[CurrentPlayer].TotalDouble++;
                    if (PlayerList[CurrentPlayer].TotalDouble > 2)
                    {
                        Dice.ResultDice = 0;
                        PlayerList[CurrentPlayer].TotalDouble = 0;
                        MessageBox.Show($"Trois Double à la suite. En Prison.");
                        PlayerList[CurrentPlayer].isInJail = true;
                        PlayerList[CurrentPlayer].JailTurn = 3;
                        GameBoard.MovePlayerToJail(PlayerList[CurrentPlayer]);
                    }
                    break;
            }
        }

        public void JailAction()
        {

            string message = $"Souhaitez-vous payer 50 pour sortir de prison ?";
            DialogResult result = MessageBox.Show(message, " Sortir de Prison ",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                PlayerList[CurrentPlayer].Capital -= 50;
                PlayerList[CurrentPlayer].isInJail = false;
            }
            // TODO :
            // Check if Player have the Card "GetOutOfJail"
            //If yes, show message asking if he want to play it.
        }
        public void JailDice()
        {
            switch (Dice.isDouble)
            {
                case false:
                    PlayerList[CurrentPlayer].JailTurn--;
                    break;
                case true:
                    PlayerList[CurrentPlayer].JailTurn = 0;
                    break;
            }
        }

        public void EndTurn()
        {
            int WinNumber = 1;
            if (PlayerList[CurrentPlayer].isBankrupt == true)
            {
                TotalPlayer--;
            }
                CurrentPlayer++;
                if (CurrentPlayer >= PlayerList.Count)
                {
                    CurrentPlayer = 0;
                }
                while (PlayerList[CurrentPlayer].isBankrupt == true)
                {
                    CurrentPlayer++;
                    if (CurrentPlayer >= PlayerList.Count)
                    {
                      CurrentPlayer = 0;
                    }
                TotalPlayer--;
                
                }
                if (WinNumber == TotalPlayer)
                {
                    this.WinGame(PlayerList[CurrentPlayer]);
                }
                
        }

        public void WinGame(Player winner)
        {
            string message = $"Player {winner.Name} has won the game!";
            DialogResult result = MessageBox.Show(message, "Game Over",
            MessageBoxButtons.OK,
            MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                System.Windows.Forms.Application.Exit();
            }
        }

        public void Bankruptcy()
        {
            Player currentPlayer = PlayerList[CurrentPlayer];

            if (currentPlayer.Capital <= 0 && currentPlayer.OwnedProperties.Count !=0 || currentPlayer.OwnedProperties.Count >= 0)
            {
                var test = currentPlayer.OwnedProperties.All(p => p.IsInBank == true);
                if (test == true)
                {
                    currentPlayer.isBankrupt = true;
                    // If time, give mortgaged properties to either bank or the other players, to not become bankrup. Not obligatory.
                    currentPlayer.Capital = 0;

                    MessageBox.Show($"Player {currentPlayer.Name} has gone bankrupt!", "Bankruptcy Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    currentPlayer.Capital = 0;
                    SellMortgage SMA = new SellMortgage();
                    string Action = "Sell";
                    SMA.SMABox(currentPlayer, Action);
                    SMA.ShowDialog();
                }
            }
            EndTurn();
        }
    }
}

