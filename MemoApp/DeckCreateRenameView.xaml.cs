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
    /// 
    public class DeckRenameInfo : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void Notify(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public bool Rename
        {
            get { return _rename; }
            set
            {
                if (value != _rename)
                {
                    _rename = value;
                    Notify("Rename");
                }
            }
        }
        private bool _rename = false;

        public int DeckInd
        {
            get { return _deckInd; }
            set
            {
                if (value != _deckInd)
                {
                    _deckInd = value;
                    Notify("DeckInd");
                }
            }
        }
        private int _deckInd = -1;

        public string PrevName
        {
            get { return _prevName; }
            set
            {
                if (value != _prevName)
                {                    
                    _prevName = value;
                    Notify("PrevName");
                }
            }
        }
        private string _prevName = "";

        public DeckRenameInfo(
              bool rename = false,
              int ind = -1,
              string name = ""
              )
        {
            Rename = rename;
            DeckInd = ind;
            PrevName = name;
        }
    }

    public partial class DeckCreateRenameView : Window
    {        
        public DeckRenameInfo Info { get; set; }

        private DeckSet manager;

        public DeckCreateRenameView(DeckSet mgr, DeckRenameInfo info)
        {
            InitializeComponent();
           
            manager = mgr;
            Info = info;

            this.DataContext = this;
        }

        private void okBtn_Click(object sender, RoutedEventArgs e)
        {
            string deckName = deckNameTxtBox.Text;

            if (manager != null)
            {
                if (!Info.Rename)
                {
                    if (manager.AddDeck(deckName))
                    {
                        DialogResult = true;
                        return;
                    }
                }
                else
                {
                    if (manager.RenameDeck(deckName, Info.DeckInd))
                    {
                        DialogResult = true;
                        return;
                    }
                }             
            }
            MessageBox.Show("Unable to create/rename deck");
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
