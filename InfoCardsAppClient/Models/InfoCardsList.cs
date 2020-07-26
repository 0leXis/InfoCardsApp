using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Threading;
using System.Net;
using System.Windows;
using System.Net.Http;

namespace InfoCardsAppClient
{
    public class InfoCardsList : INotifyPropertyChanged
    {
        public readonly ReadOnlyObservableCollection<InfoCard> InfoCards;

        public bool WaitWebResponse
        { 
            get => _waitWebResponse;
            private set
            {
                _waitWebResponse = value;
                OnPropertyChanged("WaitWebResponse");
            }
        }

        private bool _waitWebResponse = false;
        private readonly ObservableCollection<InfoCard> _infoCards = new ObservableCollection<InfoCard>();
        private readonly InfoCardsRESTClient _RESTClient;
        public InfoCardsList()
        {
            InfoCards = new ReadOnlyObservableCollection<InfoCard>(_infoCards);
            _RESTClient = new InfoCardsRESTClient(ConfigurationManager.AppSettings["ServerControllerUrl"]);
        }

        public async void AddInfoCard(InfoCard card)
        {
            WaitWebResponse = true;
            _infoCards.Add(new InfoCard(card));
            try
            {
                await _RESTClient.AddInfoCard(card);
                UpdateInfo(false);
            }
            catch (HttpRequestException e)
            {
                HandleWebError(e.Message);
            }
            OnPropertyChanged("InfoCards");
            WaitWebResponse = false;
        }

        public async void ChangeInfoCard(InfoCard card)
        {
            WaitWebResponse = true;
            var changingCard = _infoCards.Single((x) => x.CardId == card.CardId);
            changingCard.CardName = card.CardName;
            changingCard.Image = card.Image;
            try
            {
                await _RESTClient.ChangeInfoCard(card);
            }
            catch (HttpRequestException e)
            {
                HandleWebError(e.Message);
            }
            WaitWebResponse = false;
        }

        public async void RemoveInfoCard(int cardId)
        {
            WaitWebResponse = true;
            _infoCards.Remove(_infoCards.Single((x) => x.CardId == cardId));
            try
            {
                await _RESTClient.RemoveInfoCard(cardId);
            }
            catch (HttpRequestException e)
            {
                HandleWebError(e.Message);
            }
            OnPropertyChanged("InfoCards");
            WaitWebResponse = false;
        }

        public async void RemoveMany(List<InfoCard> cards)
        {
            WaitWebResponse = true;
            foreach (var card in cards)
            {
                _infoCards.Remove(card);
                try
                {
                    await _RESTClient.RemoveInfoCard(card.CardId);
                }
                catch (HttpRequestException e)
                {
                    HandleWebError(e.Message);
                    break;
                }
            }
            OnPropertyChanged("InfoCards");
            WaitWebResponse = false;
        }

        public async void UpdateInfo(bool raisePropertyChanged = true)
        {
            WaitWebResponse = true;
            try
            {
                var cards = await _RESTClient.GetInfoCardsList();
                _infoCards.Clear();
                cards.ForEach((x) => _infoCards.Add(x));
            }
            catch (HttpRequestException e)
            {
                HandleWebError(e.Message);
            }
            if(raisePropertyChanged)
                OnPropertyChanged("InfoCards");
            WaitWebResponse = false;
        }

        public void Sort(SortDescription description)
        {
            var sortingProperty = typeof(InfoCard).GetProperty(description.PropertyName);
            IEnumerable<InfoCard> ordered;
            if (description.Direction == ListSortDirection.Ascending)
                ordered = _infoCards.OrderBy((x) => sortingProperty.GetValue(x)).ToList();
            else
                ordered = _infoCards.OrderByDescending((x) => sortingProperty.GetValue(x)).ToList();
            _infoCards.Clear();
            foreach (var element in ordered)
                _infoCards.Add(element);
            OnPropertyChanged("InfoCards");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void HandleWebError(string message)
        {
            MessageBox.Show(
                "При выполнении запроса произошла ошибка: "
                + Environment.NewLine
                + message,
                "Сетевая ошибка",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            _infoCards.Clear();
        }
    }
}
