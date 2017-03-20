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
using System.ComponentModel;

namespace MemoApp
{
    /// <summary>
    /// Interaction logic for DeckCreation.xaml
    /// </summary>
    
  

    public class CardEditInfo : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void Notify(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public bool Edit
        {
            get { return _edit; }
            set
            {
                if (value != _edit)
                {
                    _edit = value;
                    Notify("Edit");
                }
            }
        }
        private bool _edit = false;

        public int CardID
        {
            get { return _cardID; }
            set
            {
                if (value != _cardID)
                {
                    _cardID = value;
                    Notify("CardID");
                }
            }
        }
        private int _cardID = -1;

        public string Front
        {
            get { return _front; }
            set
            {
                if (value != _front)
                {
                    _front = value;
                    Notify("Front");
                }
            }
        }
        private string _front = "";

        public string Back
        {
            get { return _back; }
            set
            {
                if (value != _back)
                {
                    _back = value;
                    Notify("Back");
                }
            }
        }
        private string _back = "";

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
        public CardEditInfo Info { get; set; }

        private DeckSet manager;
        private int deckInd;
       
        public CardCreateEditView(DeckSet mgr, int ind, CardEditInfo info)
        {
            InitializeComponent();

            deckInd = ind;
            manager = mgr;
            Info = info;

            this.DataContext = this;
        }

        private void okBtn_Click(object sender, RoutedEventArgs e)
        {
            if (manager != null)               
            {
                bool res = false;
                if (Info.Edit)
                {
                    res = manager.ReplaceCard(deckInd, Info.CardID, frontTxtBox.Text, backTxtBox.Text);
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
