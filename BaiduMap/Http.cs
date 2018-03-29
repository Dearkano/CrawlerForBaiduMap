using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    class Http
    {
        public static async void SearchData(List<QueryData> queryDatas)
        {
            var ak = "Wc63qC1R0MxNhI1zlLYMw0APrD9xN4h5";
            DataTable dt = new DataTable();
            DataColumn c1 = new DataColumn("序号");
            DataColumn c2 = new DataColumn("县级市");
            DataColumn c3 = new DataColumn("地级市");
            DataColumn c4 = new DataColumn("地点");
            DataColumn c5 = new DataColumn("lat");
            DataColumn c6 = new DataColumn("lng");
            dt.Columns.Add(c1);
            dt.Columns.Add(c2);
            dt.Columns.Add(c3);
            dt.Columns.Add(c4);
            dt.Columns.Add(c5);
            dt.Columns.Add(c6);
            foreach (var queryData in queryDatas)
            {
                var id = queryData.id;
                var pos = queryData.pos;
                var region = queryData.region;
                var city = queryData.city;
                var county = queryData.county;
                foreach (var p in pos)
                {
                    var _p = p.Split('、');
                    foreach(var __p in _p)
                    {
                       dt= await WriteFile.WriteF(__p, region,city,county, id, ak, dt);
                        //System.Threading.Thread.Sleep(500);
                    }
                   
                }
            }
            Console.WriteLine("开始写入");
            WriteFile.SaveCSV(dt, Environment.CurrentDirectory + "\\data.csv");
             Console.WriteLine("写入完成");
        }



        public static async Task<JArray> HttpGetRequest(string url)
        {
            HttpClient httpClient = new HttpClient();
            byte[] data=null;
            do
            {
                data = await httpClient.GetByteArrayAsync(url);
            } while (data == null);
            var jsonText = Encoding.UTF8.GetString(data);
           // Console.WriteLine(jsonText);
            JObject json = (JObject)JsonConvert.DeserializeObject(jsonText);
            JArray jsonArray = (JArray)JsonConvert.DeserializeObject(json["results"].ToString());
            return jsonArray;
        }
    }
}
