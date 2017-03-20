using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoApp
{
    public class DeckSet
    {
        private List<Deck> DeckList { get; set; }
        public DBWrapper DB;

        public DeckSet(DBWrapper db = null)
        {
            DeckList = new List<Deck>();
            DB = db;
        }

        public bool AddDeck(string name)
        {
            if (DeckList.FindIndex(x => x.Name == name) != -1)
            {
                throw new Exception("Deck with this name already exists");
            }
            DeckList.Add(new Deck(name));
            return true;
        }

        public bool RenameDeck(string name, int ind)
        {
            if (DeckList.FindIndex(x => x.Name == name) != -1)
            {
                throw new Exception("Deck with this name already exists");
            }
            DeckList[ind].Rename(name);
            return true;
        }

        public bool DeleteDeck(int ind)
        {
            if ((ind < 0) || (ind >= DeckList.Count)) return false;
            DeckList.RemoveAt(ind);
            return true;
        }

        public bool GetDeck(int ind, out Deck deck)
        {
            if ((ind < 0) || (ind >= DeckList.Count))
            {
                deck = new Deck("");
                return false;
            }

            deck = DeckList[ind];
            return true;
        }

        public bool GetLastDeck(out Deck deck)
        {
            if (0 == DeckList.Count)
            {
                deck = new Deck("");
                return false;
            }

            deck = DeckList.Last();
            return true;
        }

        public bool GetDeckCount(out int count)
        {
            count = DeckList.Count;

            return true;
        }

        public bool GetAllDecks(out List<Deck> list)
        {
            list = new List<Deck>(DeckList);

            return true;
        }

        public bool AddCard(int deckInd, string front, string back)
        {
            if ((DeckList.Count <= deckInd) || (deckInd < 0)) return false;

            return DeckList[deckInd].AddCard(new Card(front, back));
        }

        public bool DeleteCard(int deckInd, int cardID)
        {
            if ((DeckList.Count <= deckInd) || (deckInd < 0)) return false;

            return DeckList[deckInd].DeleteCard(cardID);
        }

        public bool ReplaceCard(int deckInd, int cardID, string front, string back)
        {
            if ((DeckList.Count <= deckInd) || (deckInd < 0)) return false;

            return DeckList[deckInd].ReplaceCard(cardID, new Card(front, back));
        }

        public bool GetCard(int deckInd, int id, out Card card)
        {
            if ((DeckList.Count <= deckInd) || (deckInd < 0))
            {
                card = new Card();
                return false;
            }
            return DeckList[deckInd].GetCard(id, out card);
        }

        public bool GetLastCard(int deckInd, out int id, out Card card)
        {
            if ((DeckList.Count <= deckInd) || (deckInd < 0))
            {
                id = -1;
                card = new Card();
                return false;
            }
            return DeckList[deckInd].GetLastCard(out id, out card);
        }

    }
}
