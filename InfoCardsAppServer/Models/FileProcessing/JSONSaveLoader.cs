using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
using System.Text;
using System.Threading;

namespace InfoCardsAppServer
{
    public class JSONSaveLoader : ICardSaveLoader
    {
        public const string Extension = ".json";

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
                        file.Write(JsonSerializer.Serialize(card));
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

        public InfoCard Load(string path, string fileName, bool addExtention = true)
        {
            while (true)
            {
                try
                {
                    using (var file = new StreamReader(path + fileName + (addExtention ? Extension : ""), Encoding.UTF8))
                    {
                        return JsonSerializer.Deserialize<InfoCard>(file.ReadToEnd());
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
