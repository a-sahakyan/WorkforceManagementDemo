using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WorkforceManagement.DAL.DataProvider
{
    public class DataAccess
    {
        public static string LoadData(string DataBase)
        {
            try
            {
                string path = $@"wwwroot\js\{DataBase}.json";
                var json = "";
                using (FileStream fs = new FileStream(path, FileMode.Open))
                using (StreamReader sr = new StreamReader(fs))
                {
                    json = sr.ReadToEnd();
                }

                return json;
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        public static void WriteData(string DataBase,string content)
        {
            try
            {
                string path = $@"wwwroot\js\{DataBase}.json";

                using (FileStream fs = new FileStream(path, FileMode.Truncate))
                using (StreamWriter sr = new StreamWriter(fs))
                {
                    sr.Write(content);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
