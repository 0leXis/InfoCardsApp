using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfoCardsAppServer
{
    [Serializable]
    public class InfoCard
    {
        private int _cardId = -1;
        private string _cardName;
        private byte[] _image;

        public int CardId { get => _cardId; set => _cardId = value; }
        public string CardName { get => _cardName; set => _cardName = value; }
        public byte[] Image { get => _image; set => _image = value; }

        public InfoCard() { }

        public InfoCard(int CardId, string CardName, byte[] Image)
        {
            _cardId = CardId;
            _cardName = CardName;
            _image = Image;
        }

        static public InfoCard LoadFromFile(string path, string fileName, ICardSaveLoader loader)
        {
            if (loader == null)
                throw new ArgumentNullException("Loader can not be null");
            return loader.Load(path, fileName);
        }

        static public List<InfoCard> LoadAllFiles(string path, ICardSaveLoader loader)
        {
            if (loader == null)
                throw new ArgumentNullException("Loader can not be null");
            return loader.LoadAll(path);
        }

        public void SaveToFile(string path, ICardSaveLoader saver, bool isNewObject)
        {
            if (saver == null)
                throw new ArgumentNullException("Saver can not be null");
            //No id, new object
            if (isNewObject && CardId == -1)
                saver.Save(path, this);
            else
            //Has id, rewrite
            if (!isNewObject && CardId > -1)
                saver.Save(path, CardId.ToString(), this);
            else
                throw new ArgumentException("Bad object id");
        }
    }
}
