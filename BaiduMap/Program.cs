using System;
using System.Data;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {

            var queryDatas = ReadFile.FormData();
            Http.SearchData(queryDatas);          
            Console.ReadLine();
        }
        


    }
}
