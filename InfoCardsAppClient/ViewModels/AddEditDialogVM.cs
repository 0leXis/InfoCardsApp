using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InfoCardsAppClient
{
    public class AddEditDialogVM
    {
        //Data
        public InfoCard EditCard { get; private set; }
        //Commands
        public ICommand LoadImageCommand { get; private set; }

        public AddEditDialogVM()
        {
            EditCard = new InfoCard();
            InitCommands();
        }

        public AddEditDialogVM(InfoCard EditCard)
        {
            this.EditCard = new InfoCard(EditCard);
            InitCommands();
        }

        private void InitCommands()
        {
            LoadImageCommand = new LoadImageCommand(EditCard);
        }
    }
}
