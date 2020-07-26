using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfoCardsAppServer
{
    public interface ICardSaveLoader
    {
        static string Extension { get; }
        void Save(string path, InfoCard card);
        void Save(string path, string fileName, InfoCard card);
        InfoCard Load(string path, string fileName, bool addExtension = true);
        List<InfoCard> LoadAll(string path);
    }
}
