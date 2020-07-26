using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using System.Threading;

namespace InfoCardsAppServer
{
    public class XMLSaveLoader : ICardSaveLoader
    {
        static public string Extension { get; } = ".xml";

        public void Save(string path, InfoCard card)
        {
            var id = FileUtils.GetSaveFileName(path, Extension);
            card.CardId = id;
            Save(path, id.ToString(), card);
        }

        public void Save(string path, string fileName, InfoCard card)
        {
            while (true)
            {
                try
                {
                    using (var file = new StreamWriter(path + fileName + Extension, false, Encoding.UTF8))
                    {
                        new XmlSerializer(typeof(InfoCard)).Serialize(file, card);
                    }
                    break;
                }
                catch (IOException e)
                {
                    //File not available
                    if (e.HResult == -2147024864)
                        Thread.Sleep(5);
                    else
                        throw e;
                }
            }
        }

        public InfoCard Load(string path, string fileName, bool addExtension = true)
        {
            while (true)
            {
                try
                {
                    using (var file = new StreamReader(path + fileName + (addExtension ? Extension : ""), Encoding.UTF8))
                    {
                        return new XmlSerializer(typeof(InfoCard)).Deserialize(file) as InfoCard;
                    }
                }
                catch (IOException e)
                {
                    //File not available
                    if (e.HResult == -2147024864)
                        Thread.Sleep(5);
                    else
                        throw e;
                }
            }
        }

        public List<InfoCard> LoadAll(string path)
        {
            var cards = new List<InfoCard>();
            foreach (var file in Directory.GetFiles(path, "*" + Extension))
                cards.Add(Load("", file, false));
            return cards;
        }
    }
}
