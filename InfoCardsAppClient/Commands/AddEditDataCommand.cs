using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InfoCardsAppClient
{
    public class AddEditDataCommand : ICommand
    {
        private InfoCardsList _cardsList;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public AddEditDataCommand(InfoCardsList CardsList)
        {
            _cardsList = CardsList;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (!(parameter is InfoCard))
                throw new ArgumentException("Parameter type is not InfoCard");
            var card = parameter as InfoCard;
            if (card.CardId == -1)
                _cardsList.AddInfoCard(parameter as InfoCard);
            else
                _cardsList.ChangeInfoCard(parameter as InfoCard);
        }
    }
}
