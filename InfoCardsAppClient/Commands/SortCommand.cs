using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InfoCardsAppClient.Commands
{
    public class SortCommand : ICommand
    {
        private InfoCardsList _cardsList;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public SortCommand(InfoCardsList CardsList)
        {
            _cardsList = CardsList;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (!(parameter is SortDescription))
                throw new ArgumentException("Parameter type is not SortDescription");
            _cardsList.Sort((SortDescription)parameter);
        }
    }
}
