using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace AspNetWebApi.DA.ADO
{
    public interface IGenericFactory<T> where T : class
    {
        string ExecuteCommandStr(CommandType cmdType, string query, Hashtable ht, string conString);
        List<T> ExecuteCommandList(CommandType cmdType, string query, Hashtable ht, string conString);
        T ExecuteCommandObject(CommandType cmdType, string query, Hashtable ht, string conString);
        List<T> DataReaderMapToList<Tentity>(IDataReader reader);
    }
}
