using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfoCardsAppServer
{
    public class JSONSaveLoaderFactory : ICardSaveLoaderFactory
    {
        public string GetLoaderExtension()
        {
            return JSONSaveLoader.Extension;
        }
        public ICardSaveLoader GetNewLoader()
        {
            return new JSONSaveLoader();
        }
    }
}
