
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text;

namespace TestApp
{
    class ReadFile
    {
        public static List<QueryData> FormData()
        {
            var dt = OpenExcel(Environment.CurrentDirectory + "\\test.xlsx");
            var queryDatas = new List<QueryData>();
            var rows = dt.Select();
            foreach(var row in rows)
            {
                var id = row[0].ToString();
                var region = row[5].ToString();
                var city = row[5].ToString();
                var county = row[6].ToString();
                var pos = new string[4];
                pos[0] = row[8].ToString();
                pos[1] = row[9].ToString();
                pos[2] = row[10].ToString();
                pos[3] = row[11].ToString();
                var queryData = new QueryData(id,region,pos,city,county);
                queryDatas.Add(queryData);
            }
            return queryDatas;
        }
        public static DataTable OpenExcel(string filePath)
        {
            string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + filePath + ";" + "Extended Properties='Excel 12.0;HDR=NO;IMEX=1'";
            OleDbConnection conn = new  OleDbConnection(strConn);
            conn.Open();
 
            string strExcel = "";
            OleDbDataAdapter myCommand = null;
            strExcel = "select * from [test$]";
            myCommand = new OleDbDataAdapter(strExcel, strConn);
            var dt = new DataTable();
            myCommand.Fill(dt);
            return dt;
        }
        /// <summary>
        /// 将CSV文件的数据读取到DataTable中
        /// </summary>
        /// <param name="fileName">CSV文件路径</param>
        /// <returns>返回读取了CSV数据的DataTable</returns>
        public static DataTable OpenCSV(string filePath)
        {
            //  Encoding encoding = GetType(filePath); //Encoding.ASCII;//
            DataTable dt = new DataTable();
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            //StreamReader sr = new StreamReader(fs, Encoding.UTF8);
            StreamReader sr = new StreamReader(fs);
            //string fileContent = sr.ReadToEnd();
            //encoding = sr.CurrentEncoding;
            //记录每次读取的一行记录
            string strLine = "";
            //记录每行记录中的各字段内容
            string[] aryLine = null;
            string[] tableHead = null;
            //标示列数
            int columnCount = 12;
            //标示是否是读取的第一行
            bool IsFirst = true;
            //逐行读取CSV中的数据
            for (int i = 0; i < 12; i++)
            {
                DataColumn dc = new DataColumn();
                dt.Columns.Add(dc);
            }
            while ((strLine = sr.ReadLine()) != null)
            {
                strLine = strLine.Replace("\"", "");
                    aryLine = strLine.Split(',');
                    DataRow dr = dt.NewRow();
                    for (int j = 0; j < columnCount; j++)
                    {
                        dr[j] = aryLine[j];
                    }
                    dt.Rows.Add(dr);
            }
            sr.Close();
            fs.Close();
            return dt;
        }
    }
}
