﻿using System;
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
    public partial class DeckCreateView : Window
    {
        public DeckSet manager;
        public DeckCreateView(DeckSet mgr)
        {
            InitializeComponent();
            manager = mgr;
        }

        private void okBtn_Click(object sender, RoutedEventArgs e)
        {
            string deckName = deckNameTxtBox.Text;

            // create new deck
            if (manager != null)
            {
                if (manager.AddDeck(deckName))
                {
                    DialogResult = true;
                    return;
                }                
            }
            MessageBox.Show("Unable to create deck");
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
