using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InfoCardsAppClient
{
    public class UpdateDataCommand : ICommand
    {
        private InfoCardsList _cardsList;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public UpdateDataCommand(InfoCardsList CardsList)
        {
            _cardsList = CardsList;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _cardsList.UpdateInfo();
        }
    }
}
