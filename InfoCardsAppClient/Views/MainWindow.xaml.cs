using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace InfoCardsAppClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GridViewColumnHeader _lastHeaderClicked = null;
        private ListSortDirection _lastDirection = ListSortDirection.Ascending;

        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainVM();
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AddEditDialog();
            if (dialog.ShowDialog() == true)
                (DataContext as MainVM).AddEditDataCommand.Execute(dialog.EditCard);
        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AddEditDialog((sender as Button).CommandParameter as InfoCard);
            if (dialog.ShowDialog() == true)
                (DataContext as MainVM).AddEditDataCommand.Execute(dialog.EditCard);
        }

        //Sort
        private void cardsListHeader_Click(object sender, RoutedEventArgs e)
        {
            var header = sender as GridViewColumnHeader;
            if (header == _lastHeaderClicked)
                if (_lastDirection == ListSortDirection.Ascending)
                    _lastDirection = ListSortDirection.Descending;
                else
                    _lastDirection = ListSortDirection.Ascending;
            else
                _lastDirection = ListSortDirection.Ascending;
            _lastHeaderClicked = header;

            (DataContext as MainVM).SortCommand.Execute(new SortDescription(header.CommandParameter.ToString(), _lastDirection));
        }

        private void deleteManyBtn_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as MainVM).RemoveManyCommand.Execute(cardsList.SelectedItems.Cast<InfoCard>().ToList());
        }
    }
}
