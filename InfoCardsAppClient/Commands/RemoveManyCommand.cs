using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InfoCardsAppClient.Commands
{
    public class RemoveManyCommand : ICommand
    {
        private InfoCardsList _cardsList;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RemoveManyCommand(InfoCardsList CardsList)
        {
            _cardsList = CardsList;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (!(parameter is List<InfoCard>))
                throw new ArgumentException("Parameter type is not List<InfoCard>");
            _cardsList.RemoveMany(parameter as List<InfoCard>);
        }
    }
}
