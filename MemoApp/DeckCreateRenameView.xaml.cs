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
    /// 
    public class DeckRenameInfo
    {

    }
    public partial class DeckCreateRenameView : Window
    {
        private DeckSet manager;
        private bool rename;
        private int deckInd;

        public DeckCreateRenameView(DeckSet mgr, bool bRename = false, int ind = -1)
        {
            InitializeComponent();
            manager = mgr;

            rename = bRename;
            deckInd = ind;
        }

        private void okBtn_Click(object sender, RoutedEventArgs e)
        {
            string deckName = deckNameTxtBox.Text;

            if (manager != null)
            {
                if (!rename)
                {
                    if (manager.AddDeck(deckName))
                    {
                        DialogResult = true;
                        return;
                    }
                }
                else
                {
                    if (manager.RenameDeck(deckName, deckInd))
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
