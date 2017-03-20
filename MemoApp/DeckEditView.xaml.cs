using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace MemoApp
{
    /// <summary>
    /// Interaction logic for deckEditView.xaml
    /// </summary>
    
    public class CardPresenter
    {
        public int ID { get; set; }
        public string Front { get; set; }
        public string Back { get; set; }

        public CardPresenter(int id, Card card)
        {
            ID = id;
            Front = card.Front;
            Back = card.Back;
        }
    }

    public partial class DeckEditView : Window
    {
        public ObservableCollection<CardPresenter> CardCollection { get; set;}
        DeckSet manager;
        int deckInd;

        Dictionary<int, Card> CardList { get; set; }

        public DeckEditView(DeckSet mgr, int ind)
        {
            CardCollection = new ObservableCollection<CardPresenter>();
            InitializeComponent();
            manager = mgr;
            deckInd = ind;

            loadCards();
              
            this.DataContext = this;
        }

        private void addCardBtn_Click(object sender, RoutedEventArgs e)
        {
            if (manager != null)
            {
                // open card creation window
                CardEditInfo info = new CardEditInfo(false);
                Window wnd = new CardCreateEditView(manager, deckInd, info);
                wnd.ShowDialog();

                // display cards
                int id;
                Card card;
                if (manager.GetLastCard(deckInd, out id, out card))
                {
                    CardCollection.Add(new CardPresenter(id, card));
                }
            }

        }

        private void MenuItem_Edit_Click(object sender, RoutedEventArgs e)
        {            
            int cardID = CardCollection[CardListView.SelectedIndex].ID;
            if (manager != null)
            {
                // open card creation window
                CardEditInfo info = new CardEditInfo(
                    true,
                    cardID,
                    CardCollection[CardListView.SelectedIndex].Front,
                    CardCollection[CardListView.SelectedIndex].Back);
                Window wnd = new CardCreateEditView(manager, deckInd, info);
                wnd.ShowDialog();

                // display cards
                Card card;
                if (manager.GetCard(deckInd, cardID, out card))
                {
                    CardCollection[CardListView.SelectedIndex] = new CardPresenter(cardID, card);
                }
            }
        }

        private void MenuItem_Delete_Click(object sender, RoutedEventArgs e)
        {
            int cardInd = CardListView.SelectedIndex;
            int cardID = CardCollection[CardListView.SelectedIndex].ID;

            CardCollection.RemoveAt(deckInd);
            manager.DeleteCard(deckInd, cardID);
        }

        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private bool loadCards()
        {
            bool result = false;

            Deck deck;
            if (manager.GetDeck(deckInd, out deck))
            {
                Dictionary<int, Card> dict;
                if (deck.GetAllCards(out dict))
                {
                    foreach (var item in dict)
                    {
                        CardCollection.Add(new CardPresenter(item.Key, item.Value));
                    }
                    result = true;
                }
            }
            return result;
        }
    }
}
