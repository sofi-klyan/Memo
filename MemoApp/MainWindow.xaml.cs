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
        public string Name;
    };

    public partial class MainWindow : Window
    {
        ObservableCollection<DeckPresenter> deckList = new ObservableCollection<DeckPresenter>();
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
