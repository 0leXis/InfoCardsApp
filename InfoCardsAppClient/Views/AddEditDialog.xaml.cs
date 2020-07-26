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
using System.Windows.Shapes;

namespace InfoCardsAppClient
{
    /// <summary>
    /// Логика взаимодействия для AddEditDialog.xaml
    /// </summary>
    public partial class AddEditDialog : Window
    {
        public InfoCard EditCard { get; private set; } = null;

        public AddEditDialog()
        {
            InitializeComponent();

            DataContext = new AddEditDialogVM();
        }

        public AddEditDialog(InfoCard Card)
        {
            InitializeComponent();

            DataContext = new AddEditDialogVM(Card);
        }

        private void OKBtn_Click(object sender, RoutedEventArgs e)
        {
            var context = DataContext as AddEditDialogVM;
            context.EditCard.CardName = nameTextBox.Text;
            if (context.EditCard.IsValid)
            {
                EditCard = context.EditCard;
                DialogResult = true;
            }
            else
                MessageBox.Show(
                    "Для добавления карточки необходимо ввести название и загрузить изображение!", 
                    "Ошибка", 
                    MessageBoxButton.OK, 
                    MessageBoxImage.Error);
        }
    }
}
