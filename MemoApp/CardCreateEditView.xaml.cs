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

namespace MemoApp
{
    /// <summary>
    /// Interaction logic for DeckCreation.xaml
    /// </summary>
    public partial class CardCreateEditView : Window
    {
        private DeckSet manager;
        private int deckInd;
        private bool edit;
        private int cardID;

        public CardCreateEditView(DeckSet mgr, int ind, bool bEdit, int card = -1)
        {
            InitializeComponent();
            manager = mgr;
            deckInd = ind;
            edit = bEdit;
            cardID = card;
        }

        private void okBtn_Click(object sender, RoutedEventArgs e)
        {
            if (manager != null)               
            {
                bool res = false;
                if (edit)
                {
                    res = manager.ReplaceCard(deckInd, cardID, frontTxtBox.Text, backTxtBox.Text);
                }
                else
                {
                    res = manager.AddCard(deckInd, frontTxtBox.Text, backTxtBox.Text);
                }
                if (res)
                {
                    DialogResult = true;
                    return;
                }                
            }
            MessageBox.Show("Unable to add/edit a card");
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }       
    }
}
