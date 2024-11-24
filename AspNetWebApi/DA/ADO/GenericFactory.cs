//using DataUtilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetWebApi.DA.ADO
{
    //public class GenericFactory<T> where T : class, new() //No Interface
    public class GenericFactory<T> : IGenericFactory<T> where T : class, new()
    {
        /// <summary>
        /// fetch data as json from database.
        /// </summary>
        public string ExecuteCommandStr(CommandType cmdType, string query, Hashtable ht, string connString)
        {
            string result = "";
            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = query;
                    cmd.CommandType = cmdType;
                    cmd.Connection = con;
                    if (ht != null)
                    {
                        foreach (object obj in ht.Keys)
                        {
                            string str = Convert.ToString(obj);
                            SqlParameter parameter = new SqlParameter("@" + str, ht[obj]);
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
        }

        public List<T> ExecuteCommandList(CommandType cmdType, string query, Hashtable ht, string connString)
        {
            List<T> Results = null;
            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = query;
                    cmd.CommandType = cmdType;
                    cmd.Connection = con;
                    if (ht != null)
                    {
                        foreach (object obj in ht.Keys)
                        {
                            string str = Convert.ToString(obj);
                            SqlParameter parameter = new SqlParameter("@" + str, ht[obj]);
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
        }

        public T ExecuteCommandObject(CommandType cmdType, string query, Hashtable ht, string connString)
        {
            T Results = null;
            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = query;
                    cmd.CommandType = cmdType;
                    cmd.Connection = con;
                    foreach (object obj in ht.Keys)
                    {
                        string str = Convert.ToString(obj);
                        SqlParameter parameter = new SqlParameter("@" + str, ht[obj]);
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
