using System;
using System.Collections.Generic;
using System.Text;

namespace TestApp
{
    public class QueryData
    {
        public string id;
        public string[] pos;
        public string region;
        public string city;
        public string county;
        public QueryData(string id,string region,string[] pos,string city,string county)
        {
            this.id = id;
            this.pos = pos;
            this.region = region;
            this.city = city;
            this.county = county;
        }
    }
    public class ResultData
    {
        public int id;
        public string lat;
        public string lng;
        public string city;
        public string county;
        public ResultData(int id,string lat,string lng,string city,string county)
        {
            this.lat = lat;
            this.lng = lng;
            this.id = id;
            this.city = city;
            this.county = county;
        }
    }

}
