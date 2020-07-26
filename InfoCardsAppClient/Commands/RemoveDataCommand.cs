using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InfoCardsAppClient
{
    public class RemoveDataCommand : ICommand
    {
        private InfoCardsList _cardsList;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RemoveDataCommand(InfoCardsList CardsList)
        {
            _cardsList = CardsList;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (!(parameter is int))
                throw new ArgumentException("Parameter type is not int");
            _cardsList.RemoveInfoCard((int)parameter);
        }
    }
}
