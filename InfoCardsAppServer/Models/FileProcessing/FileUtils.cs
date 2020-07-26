using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace InfoCardsAppServer
{
    static public class FileUtils
    {
        //Get free file id + 1 for new file
        static public int GetSaveFileName(string path, string extension)
        {
            var id = 0;
            while (File.Exists(path + id++ + extension)) ;
            return --id;
        }

        static public void DeleteFile(string path)
        {
            while (true)
            {
                try
                {
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                        break;
                    }
                    else
                        throw new FileNotFoundException();
                }
                catch(IOException e)
                {
                    //File not available
                    if (e.HResult == -2147024864)
                        Thread.Sleep(5);
                    else
                        throw e;
                }
            }
        }
    }
}
