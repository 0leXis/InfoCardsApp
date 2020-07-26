using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfoCardsAppServer
{
    public class XMLSaveLoaderFactory : ICardSaveLoaderFactory
    {
        public string GetLoaderExtension()
        {
            return XMLSaveLoader.Extension;
        }
        public ICardSaveLoader GetNewLoader()
        {
            return new XMLSaveLoader();
        }
    }
}
