using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.ComponentModel;
using InfoCardsAppClient.Commands;

namespace InfoCardsAppClient
{
    public class MainVM : INotifyPropertyChanged
    {
        //Data
        public ReadOnlyObservableCollection<InfoCard> InfoCards => _model.InfoCards;
        public bool WaitWebResponse => _model.WaitWebResponse;
        //Commands
        public ICommand UpdateCommand { get; }
        public ICommand AddEditDataCommand { get; }
        public ICommand RemoveDataCommand { get; }
        public ICommand RemoveManyCommand { get; }
        public ICommand SortCommand { get; }
        //Models
        private readonly InfoCardsList _model = new InfoCardsList();

        public MainVM()
        {
            UpdateCommand = new UpdateDataCommand(_model);
            AddEditDataCommand = new AddEditDataCommand(_model);
            RemoveDataCommand = new RemoveDataCommand(_model);
            RemoveManyCommand = new RemoveManyCommand(_model);
            SortCommand = new SortCommand(_model);
            _model.PropertyChanged += OnModelPropertyChanged;

            UpdateCommand.Execute(null);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void OnModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);
        }
    }
}
