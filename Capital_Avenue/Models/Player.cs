﻿namespace Capital_Avenue.Models
{
    public class Player
    {
        public string Name { get; set; }

        public int Pawn { get; set; }

        public int Capital { get; set; }
        public int Monney { get; set; }
       public  List<string> OwnedProperties { get; }

        public List<string> Cards { get; private set; }
        public int TotalDouble { get; set; }

        public bool isBankrupt;

        public Player(string name, int pawn, int capital)
        {
            this.Name = name;
            this.Pawn = pawn;
            this.Capital = capital;
            this.Monney = 1200;
            this.OwnedProperties = new List<string>();
            this.Cards = new List<string>();
            isBankrupt = false;
            this.TotalDouble = 0; //Will be displaced into board or monopoly
        }
        public void AddCards(string Card)
        {
            Cards.Add(Card);
        }
    }
}
