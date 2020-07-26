using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfoCardsAppServer
{
    public interface ICardSaveLoaderFactory
    {
        string GetLoaderExtension();
        ICardSaveLoader GetNewLoader();
    }
}
