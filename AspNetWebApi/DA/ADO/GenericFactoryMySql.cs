//using DataUtilities;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
//using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetWebApi.DA.ADO
{
    //public class GenericFactory<T> where T : class, new() //No Interface
    public class GenericFactoryMySql<T> : IGenericFactoryMySql<T> where T : class, new()
    {
        /// <summary>
        /// fetch data as json from database.
        /// </summary>
        public Task<string> ExecuteCommandStr(CommandType cmdType, string query, Hashtable ht, string connString)
        {
            return Task.Run(() =>
            {
                string result = "";
                try
                {
                    using (MySqlConnection con = new MySqlConnection(connString))
                    {
                        con.Open();
                        MySqlCommand cmd = new MySqlCommand();
                        cmd.CommandText = query;
                        cmd.CommandType = cmdType;
                        cmd.Connection = con;
                        if (ht != null)
                        {
                            foreach (object obj in ht.Keys)
                            {
                                string str = Convert.ToString(obj);
                                MySqlParameter parameter = new MySqlParameter("@" + str, ht[obj]);
                                cmd.Parameters.Add(parameter);
                            }
                        }
                        IDataReader dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            result = dr.IsDBNull(0) ? "[]" : dr.GetString(0);
                        }
                        if (ht != null)
                        {
                            cmd.Parameters.Clear();
                        }
                    }
                }
                catch (Exception ex)
                {
                    //Logs.WriteBug(ex);
                }
                return result;
            });
        }

        public Task<List<T>> ExecuteCommandList(CommandType cmdType, string query, Hashtable ht, string connString)
        {
            return Task.Run(() =>
            {
                List<T> Results = null;
                try
                {
                    using (MySqlConnection con = new MySqlConnection(connString))
                    {
                        con.Open();
                        MySqlCommand cmd = new MySqlCommand();
                        cmd.CommandText = query;
                        cmd.CommandType = cmdType;
                        cmd.Connection = con;
                        if (ht != null)
                        {
                            foreach (object obj in ht.Keys)
                            {
                                string str = Convert.ToString(obj);
                                MySqlParameter parameter = new MySqlParameter("@" + str, ht[obj]);
                                cmd.Parameters.Add(parameter);
                            }
                        }
                        Results = DataReaderMapToList<T>(cmd.ExecuteReader());
                        if (ht != null)
                        {
                            cmd.Parameters.Clear();
                        }
                    }
                }
                catch (Exception ex)
                {
                    //Logs.WriteBug(ex);
                }
                return Results;
            });
        }

        public Task<T> ExecuteCommandObject(CommandType cmdType, string query, Hashtable ht, string connString)
        {
            return Task.Run(() =>
            {
                T Results = null;
                try
                {
                    using (MySqlConnection con = new MySqlConnection(connString))
                    {
                        con.Open();
                        MySqlCommand cmd = new MySqlCommand();
                        cmd.CommandText = query;
                        cmd.CommandType = cmdType;
                        cmd.Connection = con;
                        foreach (object obj in ht.Keys)
                        {
                            string str = Convert.ToString(obj);
                            MySqlParameter parameter = new MySqlParameter("@" + str, ht[obj]);
                            cmd.Parameters.Add(parameter);
                        }
                        Results = DataReaderMapToList<T>(cmd.ExecuteReader()).FirstOrDefault();
                        cmd.Parameters.Clear();
                    }
                }
                catch (Exception ex)
                {
                    //Logs.WriteBug(ex);
                }

                return Results;
            });
        }

        public List<T> DataReaderMapToList<Tentity>(IDataReader reader)
        {
            var results = new List<T>();

            var columnCount = reader.FieldCount;
            while (reader.Read())
            {
                var item = Activator.CreateInstance<T>();
                try
                {
                    var rdrProperties = Enumerable.Range(0, columnCount).Select(i => reader.GetName(i)).ToArray();
                    foreach (var property in typeof(T).GetProperties())
                    {
                        if ((typeof(T).GetProperty(property.Name).GetGetMethod().IsVirtual) || (!rdrProperties.Contains(property.Name)))
                        {
                            continue;
                        }
                        else
                        {
                            if (!reader.IsDBNull(reader.GetOrdinal(property.Name)))
                            {
                                Type convertTo = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                                property.SetValue(item, Convert.ChangeType(reader[property.Name], convertTo), null);
                            }
                        }
                    }
                    results.Add(item);
                }
                catch (Exception ex)
                {
                    //Logs.WriteBug(ex);
                }
            }
            return results;
        }
       
    }
}
