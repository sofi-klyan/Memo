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
        public string Name { get; set; }
        public DBWrapper DB;

        private Dictionary<int, Card> CardDict { get; set; }
        private int maxInd;

        public Deck(string name, DBWrapper db = null)
        {
            CardDict = new Dictionary<int, Card>();
            Name = name;
            DB = db;
            maxInd = -1;
        }
        

        public bool Rename (string name)
        {
           if (Name == name) return true;           
                
            if (DB != null)
            {
                if (!DB.RenameTable(Name, name))
                {
                   return false;
                }
            }

            Name = name;
            return true;
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

        public bool DeleteCard (int id)
        {
            if (!CardDict.ContainsKey(id)) return false;
            if (DB != null)
            {
                if (!DB.DeleteRecord(Name, id))
                {
                    //error while adding a record
                    throw new Exception("could not delete a record from db");
                }
            }
            CardDict.Remove(id);
            if (id == maxInd) maxInd--;

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

            id = maxInd;//CardDict.Keys.Max();
            card = CardDict[id];            
            return true;
        }

        public bool GetCardCount(out int count)
        {
            count = CardDict.Count;

            return true;
        }

        public bool GetAllCards (out Dictionary<int, Card> dict)
        {
            dict = new Dictionary<int, Card>(CardDict);

            return true;
        }

        private int getNewInd()
        {
            maxInd++;
            return maxInd;            
        }
    }   
}
