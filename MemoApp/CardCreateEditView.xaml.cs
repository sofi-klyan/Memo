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
    
    public class CardEditInfo
    {
        public bool Edit;
        public int CardID;
        public string Front;
        public string Back;

        public CardEditInfo(
            bool edit = false,
            int cardID = -1,
            string front = "",
            string back = ""
            )
        {
            Edit = edit;
            CardID = cardID;
            Front = front;
            Back = back;
        }
    }

    public partial class CardCreateEditView : Window
    {
        private DeckSet manager;
        private int deckInd;
        private bool edit;
        private int cardID;

        public string FrontText, BackText;

        public CardCreateEditView(DeckSet mgr, int ind, CardEditInfo info)
        {
            InitializeComponent();
            manager = mgr;
            deckInd = ind;
            edit = info.Edit;
            cardID = info.CardID;
            FrontText = info.Front;
            BackText = info.Back;

            this.DataContext = this;
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
