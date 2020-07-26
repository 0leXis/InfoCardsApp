using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoCardsAppClient
{
    [Serializable]
    public class InfoCard : INotifyPropertyChanged
    {
        private int _cardId = -1;
        private string _cardName;
        private byte[] _image;
        [NonSerialized]
        private bool _isValid;
        public int CardId
        {
            get => _cardId;
            set
            {
                _cardId = value;
                OnPropertyChanged("CardId");
            }
        }
        public string CardName
        {
            get => _cardName;
            set
            {
                _cardName = value;
                CheckValidation();
                OnPropertyChanged("CardName");
            }
        }
        public byte[] Image
        {
            get => _image;
            set
            {
                _image = value;
                CheckValidation();
                OnPropertyChanged("Image");
            }
        }
        public bool IsValid
        {
            get => _isValid;
        }

        public InfoCard() { }

        public InfoCard(int CardId, string CardName, byte[] Image)
        {
            this.CardId = CardId;
            this.CardName = CardName;
            this.Image = Image;
        }

        public InfoCard(string CardName, byte[] Image)
        {
            this.CardName = CardName;
            this.Image = Image;
        }

        public InfoCard(InfoCard CardToCopy)
        {
            CardId = CardToCopy.CardId;
            CardName = CardToCopy.CardName;
            Image = CardToCopy.Image;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void CheckValidation()
        {
            _isValid = true;
            if (_cardName == "" || _cardName == null)
                _isValid = false;
            else
            if (_image == null)
                _isValid = false;
        }
    }
}
