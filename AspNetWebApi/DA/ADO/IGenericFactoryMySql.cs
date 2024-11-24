using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace AspNetWebApi.DA.ADO
{
    public interface IGenericFactoryMySql<T> where T : class
    {
        Task<string> ExecuteCommandStr(CommandType cmdType, string query, Hashtable ht, string conString);
        Task<List<T>> ExecuteCommandList(CommandType cmdType, string query, Hashtable ht, string conString);
        Task<T> ExecuteCommandObject(CommandType cmdType, string query, Hashtable ht, string conString);
        List<T> DataReaderMapToList<Tentity>(IDataReader reader);
    }
}
