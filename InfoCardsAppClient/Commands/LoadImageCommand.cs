using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace InfoCardsAppClient
{
    public class LoadImageCommand : ICommand
    {
        private readonly InfoCard _card;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public LoadImageCommand(InfoCard Card) 
        {
            _card = Card;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var imageDialog = new OpenFileDialog();
            imageDialog.Filter = "Файлы .jpg и .png (*.jpg, *.png)|*.jpg;*.png";
            if (imageDialog.ShowDialog() == true)
            {
                try
                {
                    _card.Image = File.ReadAllBytes(imageDialog.FileName);
                }
                catch
                {
                    //Bad image type
                    _card.Image = null;
                    MessageBox.Show("Файл поврежден или имеет неизвестный формат!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
