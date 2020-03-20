using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Services;

namespace WebApplication1
{
    /// <summary>
    /// Description résumée de DataFetchService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Pour autoriser l'appel de ce service Web depuis un script à l'aide d'ASP.NET AJAX, supprimez les marques de commentaire de la ligne suivante. 
    // [System.Web.Script.Services.ScriptService]
    public class DataFetchService : WebService
    {

        [WebMethod]
        public void GetData()
        {
            string connectionString = @"Data Source=SHOGUN;Initial Catalog=archives;Integrated Security=True";

            var datas = new List<DataSQL>();
            using (SqlConnection sqlConn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("Select * from ArchivesV2", sqlConn);
                sqlConn.Open();

                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    var dataSQL = new DataSQL
                    {
                        ID = Convert.ToInt32(dr["ID"].ToString()),
                        Versement = string.IsNullOrEmpty(dr["versement"].ToString()) ? new DateTime(1900, 1, 1) : Convert.ToDateTime(dr["versement"].ToString()),
                        Etablissement = dr["etablissement"].ToString(),
                        Direction = dr["direction"].ToString(),
                        Service = dr["service"].ToString(),
                        Dossiers = dr["dossiers"].ToString(),
                        Extremes = dr["extremes"].ToString(),
                        Elimination = dr["elimination"].ToString(),
                        Communication = dr["communication"].ToString(),
                        Cote = dr["cote"].ToString(),
                        Localisation = dr["localisation"].ToString(),
                        CL = string.IsNullOrEmpty(dr["CL"].ToString()) ? 0 : Convert.ToInt32(dr["CL"].ToString()),
                        Chrono = string.IsNullOrEmpty(dr["chrono"].ToString()) ? 0 : Convert.ToInt32(dr["chrono"].ToString()),
                        Calc = dr["calc"].ToString()
                    };
                    datas.Add(dataSQL);
                }
                Context.Response.Write(JsonConvert.SerializeObject(datas));
            }
            /*
            MongoClient dbClient = new MongoClient("mongodb+srv://Show_Goon:Y3uzOLmDj0Ps5KTj@cluster0-ovbgf.mongodb.net/test?authSource=admin&replicaSet=Cluster0-shard-0&readPreference=primary&appname=MongoDB%20Compass&ssl=true");
            
            var database = dbClient.GetDatabase("ArchivesV2");

            var collection = database.GetCollection<BsonDocument>("ArchivesV2");

            string sentence = "";
            string JSONResult = "[";

            var cursor = collection.Find(new BsonDocument()).ToCursor();
            int i = 0;
            foreach (var document in cursor.ToEnumerable())
            {
                if (i < 2)
                {
                    sentence += "{ ";
                    foreach (var elm in document)
                    {
                        if (elm.Name != "_id")
                        {
                            sentence += " \"" + elm.Name + "\" : \"" + elm.Value + "\",";
                        }
                    }
                    sentence += " },";
                    i++;
                }
            }
            JSONResult += sentence;
            JSONResult += "]";
            Context.Response.Write(JSONResult);*/

            /*var cursor = collection.Find(new BsonDocument()).ToCursor();
            foreach (var document in cursor.ToEnumerable())
            {
                *//*MessageBox.Show(document.GetElement("Direction").Value.ToString());*//*
                var dataSQL = new DataSQL
                {
                    ID = Convert.ToInt32(document.GetElement("ID").Value.ToString()),
                    Versement = string.IsNullOrEmpty(document.GetElement("versement").Value.ToString()) ? new DateTime(1900, 1, 1) : Convert.ToDateTime(document.GetElement("versement").Value.ToString()),
                    Etablissement = document.GetElement("etablissement").Value.ToString(),
                    Direction = document.GetElement("direction").Value.ToString(),
                    Service = document.GetElement("service").Value.ToString(),
                    Dossiers = string.IsNullOrEmpty(document.GetElement("dossiers").Value.ToString()) ? null : document.GetElement("dossiers").Value.ToString(),
                    Extremes = document.GetElement("extremes").Value.ToString(),
                    Elimination = File.Exists(Convert.ToBoolean(document.GetElement("elimination")).ToString()) ? document.GetElement("elimination").Value.ToString() : "",
                    *//*document.GetElement("elimination").Value.ToString(),*//*
                    Communication = document.GetElement("communication").Value.ToString(),
                    Localisation = document.GetElement("localisation").Value.ToString(),
                    CL = string.IsNullOrEmpty(document.GetElement("CL").Value.ToString()) ? 0 : Convert.ToInt32(document.GetElement("CL").Value.ToString()),
                    Chrono = string.IsNullOrEmpty(document.GetElement("chrono").Value.ToString()) ? 0 : Convert.ToInt32(document.GetElement("chrono").Value.ToString()),
                    Calc = document.GetElement("calc").Value.ToString()
                };
                datas.Add(dataSQL);
            }*/
        }
    }
}
