using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    // public class DES
    // {
    //     private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["DES"].ConnectionString;
    //     private static string _etsOrDir;
    //     private static string _relatedTable;
    //     private static int _relatedDataId;
    //
    //     public int ID { get; set; }
    //     public string Name { get; set; }
    //
    //     public static List<DES> GetDataZero(string table)
    //     {
    //         List<DES> newArray = new List<DES>();
    //
    //         if (table == "etablissement")
    //         {
    //             _etsOrDir = "ets";
    //             _relatedTable = "direction";
    //         }
    //         else if (table == "direction")
    //         {
    //             _etsOrDir = "dir";
    //             _relatedTable = "service";
    //         }
    //
    //         // Connect to the Database
    //         using (SqlConnection sqlConn = new SqlConnection(ConnectionString))
    //         {
    //             // TODO : Rework this to be more readable (Linq?)
    //             string cmdString = "SELECT " + table + ".id, " + table + ".name"
    //                                 + " FROM " + table;
    //
    //             using (SqlCommand cmd = new SqlCommand())
    //             {
    //                 cmd.Connection = sqlConn;
    //                 cmd.CommandText = cmdString;
    //
    //                 sqlConn.Open();
    //
    //                 var dr = cmd.ExecuteReader();
    //
    //                 while (dr.Read())
    //                 {
    //                     DES des = new DES()
    //                     {
    //                         ID = Convert.ToInt32(dr["id"]),
    //                         Name = dr["name"].ToString()
    //                     };
    //                     newArray.Add(des);
    //                 }
    //             }
    //         }
    //         return newArray;
    //     }
    //     public static List<DES> GetData(string table, string relatedData)
    //     {
    //         List<DES> newArray = new List<DES>();
    //
    //         if (table == "etablissement")
    //         {
    //             _etsOrDir = "ets";
    //             _relatedTable = "direction";
    //         }
    //         else if (table == "direction")
    //         {
    //             _etsOrDir = "dir";
    //             _relatedTable = "service";
    //         }
    //
    //         // Connect to the Database
    //         using (SqlConnection sqlConn = new SqlConnection(ConnectionString))
    //         {
    //             string cmdString = "SELECT id FROM " + table + " WHERE name = '" + relatedData + "'";
    //             using (SqlCommand cmd = new SqlCommand())
    //             {
    //                 cmd.Connection = sqlConn;
    //                 cmd.CommandText = cmdString;
    //
    //                 sqlConn.Open();
    //
    //                 var dr = cmd.ExecuteReader();
    //
    //                 while (dr.Read())
    //                 {
    //                     _relatedDataId = Convert.ToInt32(dr[0]);
    //                 }
    //             }
    //         }
    //
    //         // Connect to the Database
    //         using (SqlConnection sqlConn = new SqlConnection(ConnectionString))
    //         {
    //             // TODO : Rework this to be more readable (Linq?)
    //             string cmdString = "SELECT " + _relatedTable + ".id, " + _relatedTable + ".name"
    //                             + " FROM " + table
    //                             + " LEFT JOIN " + _relatedTable + " ON " + table + ".id = " + _relatedTable + ".linked_" + _etsOrDir + ""
    //                             + " WHERE " + _relatedTable + ".linked_" + _etsOrDir + " = '" + _relatedDataId + "'";
    //
    //             using (SqlCommand cmd = new SqlCommand())
    //             {
    //                 cmd.Connection = sqlConn;
    //                 cmd.CommandText = cmdString;
    //
    //                 sqlConn.Open();
    //
    //                 var dr = cmd.ExecuteReader();
    //
    //                 while (dr.Read())
    //                 {
    //                     DES des = new DES()
    //                     {
    //                         ID = Convert.ToInt32(dr["id"]),
    //                         Name = dr["name"].ToString()
    //                     };
    //                     newArray.Add(des);
    //                 }
    //             }
    //         }
    //         return newArray;
    //     }
    //
    //     public static List<string> GetDatabaseElements()
    //     {
    //         List<string> databaseItems = new List<string>();
    //         // Connect to the Database
    //         using (SqlConnection sqlConn = new SqlConnection(ConnectionString))
    //         {
    //             string cmdString = " SELECT *"
    //                             + " FROM INFORMATION_SCHEMA.TABLES"
    //                             + " WHERE TABLE_NAME NOT LIKE 'sysdiagrams'";
    //             using (SqlCommand cmd = new SqlCommand())
    //             {
    //                 cmd.Connection = sqlConn;
    //                 cmd.CommandText = cmdString;
    //
    //                 sqlConn.Open();
    //
    //                 var dr = cmd.ExecuteReader();
    //
    //                 while (dr.Read())
    //                 {
    //                     databaseItems.Add(dr["TABLE_NAME"].ToString());
    //                 }
    //             }
    //             return databaseItems;
    //         }            
    //     }
    //
    //     public static List<string> GetTableElements(string table)
    //     {
    //         List<string> tableItems = new List<string>();
    //         // Connect to the Database
    //         using (SqlConnection sqlConn = new SqlConnection(ConnectionString))
    //         {
    //             string cmdString = " SELECT name"
    //                             + " FROM " + table;
    //             using (SqlCommand cmd = new SqlCommand())
    //             {
    //                 cmd.Connection = sqlConn;
    //                 cmd.CommandText = cmdString;
    //
    //                 sqlConn.Open();
    //
    //                 var dr = cmd.ExecuteReader();
    //
    //                 while (dr.Read())
    //                 {
    //                     tableItems.Add(dr["name"].ToString());
    //                 }
    //             }
    //             return tableItems;
    //         }
    //     }
    //
    //     public static void AddSmth(string Elem)
    //     {
    //         int id = 0;
    //         // Connect to the Database
    //         using (SqlConnection sqlConn = new SqlConnection(ConnectionString))
    //         {
    //             string cmdString = "SELECT id FROM " + Elem;
    //
    //             using (SqlCommand cmd = new SqlCommand())
    //             {
    //                 cmd.Connection = sqlConn;
    //                 cmd.CommandText = cmdString;
    //
    //                 sqlConn.Open();
    //
    //                 var dr = cmd.ExecuteReader();
    //
    //                 while (dr.Read())
    //                 {
    //                     id = Convert.ToInt32(dr["id"]);
    //                 }
    //             }
    //
    //             cmdString = "INSERT INTO " + Elem + " VALUES ("+ (id + 1) +", 'ETS_TEST', '" + Elem + "')";
    //             using (SqlCommand cmd = new SqlCommand())
    //             {
    //                 cmd.Connection = sqlConn;
    //                 cmd.CommandText = cmdString;
    //
    //                 sqlConn.Open();
    //
    //                 cmd.ExecuteNonQuery();
    //             }
    //         }
    //     }
    // }
}