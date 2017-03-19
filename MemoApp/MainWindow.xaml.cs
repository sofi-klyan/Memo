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
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public class DeckPresenter
    {
        public string Name { get; set; }

        public DeckPresenter (string name)
        {
            Name = name;
        }
    };

    public partial class MainWindow : Window
    {
        public ObservableCollection<DeckPresenter> DeckCollection { get; set; }

        DeckSet manager;
        public MainWindow()
        {           
            DeckCollection = new ObservableCollection<DeckPresenter>();
            InitializeComponent();
            this.DataContext = this;

            if (false)
            {
                // TODO - add loading of deckset
            }
            else
            {
                // TODO - add creation of db
                manager = new DeckSet();
            }

          //  DeckCollection.Add(new DeckPresenter("hhh"));
           // DeckListView.ItemsSource = DeckCollection;//manager.DeckList;

        }

      

        private void CreateDeckBtn_Click(object sender, RoutedEventArgs e)
        {
            Window wnd = new DeckCreateRenameView(manager);
            wnd.ShowDialog();

            Deck deck;
            if (manager.GetLastDeck(out deck))
            {
                DeckCollection.Add(new DeckPresenter(deck.Name));
            }
           
        }
        private void MenuItem_Rename_Click(object sender, RoutedEventArgs e)
        {
            int deckInd = DeckListView.SelectedIndex;
            Window wnd = new DeckCreateRenameView(manager, true, deckInd);
            wnd.ShowDialog();

            Deck deck;
            if (manager.GetDeck(deckInd, out deck))
            {
                DeckCollection[deckInd].Name = deck.Name;
            }
        }

        private void MenuItem_Edit_Click(object sender, RoutedEventArgs e)
        {
            int deckInd = DeckListView.SelectedIndex;
            var wnd = new DeckEditView(manager, deckInd);
            wnd.Show();
        }

        private void MenuItem_Train_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Delete_Click(object sender, RoutedEventArgs e)
        {
            int deckInd = DeckListView.SelectedIndex;
            DeckCollection.RemoveAt(deckInd);
            manager.DeleteDeck(deckInd);
        }
    }
}
