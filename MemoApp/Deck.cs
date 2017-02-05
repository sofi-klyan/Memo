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
        public Dictionary<int, Card> CardDict;
        public string Name;
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

        public bool ReplaceCard(int ind, Card card)
        {
            if (!CardDict.ContainsKey(ind)) return false;

            if (DB != null)
            {
                if (!DB.ReplaceRecord(Name, ind, card))
                {
                    //error while adding a record
                    throw new Exception("could not replace a record in db");
                }
            }
            CardDict[ind] = card;
            return true;
        }

        private int getNewInd()
        {
            if (CardDict.Count > 0) return CardDict.Last().Key + 1;
            else return 0;
        }
    }

    public class DeckSet
    {
        public List<Deck> DeckList;
        public DBWrapper DB;

        public DeckSet(DBWrapper db = null)
        {
            DeckList = new List<Deck>();
            DB = db;
        } 

        public bool AddDeck (string name)
        {
            if (DeckList.FindIndex(x => x.Name == name) != -1)
            {
                throw new Exception("Deck with this name already exists");
            }
            DeckList.Add(new Deck(name, DB));
            return true;
        }

        public bool AddCard(int deckInd, string front, string back)
        {
            if ((DeckList.Count <= deckInd) || (deckInd < 0)) return false;

            return DeckList[deckInd].AddCard(new Card(front, back));
        }

        public bool DeleteCard( int deckInd, int cardID)
        {
            if ((DeckList.Count <= deckInd) || (deckInd < 0)) return false;

            return DeckList[deckInd].DeleteCard(cardID);
        }

        public bool ReplaceCard(int deckInd, int cardID, string front, string back)
        {
            if ((DeckList.Count <= deckInd) || (deckInd < 0)) return false;

            return DeckList[deckInd].ReplaceCard(cardID, new Card(front, back));
        }
    }
}
