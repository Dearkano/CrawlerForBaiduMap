using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    class WriteFile
    {
        public async static Task<DataTable> WriteF(string pos,string region,string city,string county,string id,string ak,DataTable dt)
        {


            var url = string.Format("http://api.map.baidu.com/place/v2/search?query={0}&region={1}&output=json&page_size=20&ak={2}", pos, region, ak);
            Console.WriteLine(url);
            try
            {
                var jsonArray = await Http.HttpGetRequest(url);
                foreach (var json in jsonArray)
                {
                    if (json["location"] != null)
                    {
                        var lat = json["location"]["lat"].ToString();
                        var lng = json["location"]["lng"].ToString();
                        DataRow dr = dt.NewRow();
                        dr[0] = id;
                        dr[1] = city;
                        dr[2] = county;
                        dr[3] = pos;
                        dr[4] = lat;
                        dr[5] = lng;
                        dt.Rows.Add(dr);
                    }

                }
            }
            catch (Exception e)
            {

            }
            return dt;
        }
        /// <summary>
        /// 将DataTable中数据写入到CSV文件中
        /// </summary>
        /// <param name="dt">提供保存数据的DataTable</param>
        /// <param name="fileName">CSV的文件路径</param>
        public static void SaveCSV(DataTable dt, string fullPath)
        {
            FileInfo fi = new FileInfo(fullPath);
            if (!fi.Directory.Exists)
            {
                fi.Directory.Create();
            }
            FileStream fs = new FileStream(fullPath, System.IO.FileMode.Create, System.IO.FileAccess.Write);
            //StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Default);
            StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
            string data = "";
            //写出列名称
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                data += dt.Columns[i].ColumnName.ToString();
                if (i < dt.Columns.Count - 1)
                {
                    data += ",";
                }
            }
            sw.WriteLine(data);
            //写出各行数据
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                data = "";
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    string str = dt.Rows[i][j].ToString();
                    str = str.Replace("\"", "\"\"");//替换英文冒号 英文冒号需要换成两个冒号
                    if (str.Contains(",") || str.Contains("\"")
                        || str.Contains("\r") || str.Contains("\n")) //含逗号 冒号 换行符的需要放到引号中
                    {
                        str = string.Format("\"{0}\"", str);
                    }

                    data += str;
                    if (j < dt.Columns.Count - 1)
                    {
                        data += ",";
                    }
                }
                sw.WriteLine(data);
            }
            sw.Close();
            fs.Close();
            //DialogResult result = MessageBox.Show("CSV文件保存成功！");
            //if (result == DialogResult.OK)
            //{
            //    System.Diagnostics.Process.Start("explorer.exe", Common.PATH_LANG);
            //}
        }

    }
}
