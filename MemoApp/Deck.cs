using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoApp
{
    public struct Card
    {
        public string Front;
        public string Back;

        public Card(string front, string back)
        {
            Front = front;
            Back = back;
        }
    }

    public class Deck
    {
        private Dictionary<int, Card> CardDict { get; set; }
        public string Name { get; set; }
        public DBWrapper DB;

        public Deck(string name, DBWrapper db = null)
        {
            CardDict = new Dictionary<int, Card>();
            Name = name;
            DB = db;
        }

        public bool AddCard (Card card)
        {
            int ind = getNewInd();            
            if (DB != null)
            {
                if (!DB.AddRecord(Name, ind, card))
                {
                    //error while adding a record
                    throw new Exception("could not add a record to db");
                }
            }
            CardDict.Add(ind, card);

            return true;
        }

        public bool DeleteCard (int ind)
        {
            if (!CardDict.ContainsKey(ind)) return false;
            if (DB != null)
            {
                if (!DB.DeleteRecord(Name, ind))
                {
                    //error while adding a record
                    throw new Exception("could not delete a record from db");
                }
            }
            CardDict.Remove(ind);
            return true;
        }

        public bool ReplaceCard(int id, Card card)
        {
            if (!CardDict.ContainsKey(id)) return false;

            if (DB != null)
            {
                if (!DB.ReplaceRecord(Name, id, card))
                {
                    //error while adding a record
                    throw new Exception("could not replace a record in db");
                }
            }
            CardDict[id] = card;
            return true;
        }

        public bool GetCard (int id, out Card card)
        {
            if (!CardDict.ContainsKey(id))
            {
                card = new Card();
                return false;
            }

            card = CardDict[id];
            return true;
        }

        public bool GetLastCard(out int id, out Card card)
        {
            if (CardDict.Count == 0)
            {
                id = -1;
                card = new Card();
                return false;
            }

            id = CardDict.Last().Key;
            card = CardDict.Last().Value;            
            return true;
        }

        private int getNewInd()
        {
            if (CardDict.Count > 0) return CardDict.Last().Key + 1;
            else return 0;
        }
    }   
}
