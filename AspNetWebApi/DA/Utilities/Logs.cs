//using DataModels.EntityModels.ERPLogModel;
//using DataModels.EntityModels.ERPModel;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace AspNetWebApi.DA.Utilities
{
    public class Logs
    {
        public static string path = CurrentAssemblyDirectory().ToString();
        public static string assemblyFile = path.Remove(path.IndexOf("\\bin\\Debug")).ToString();
        private const string FILE_NAME = "wwwroot\\systemLog\\LogTextFile.txt";

        private static string ConfigFilePath
        {
            get { return assemblyFile + "\\" + FILE_NAME; }
        }

        public static void WriteLogFile(string message)
        {
            FileStream fs = null;
            if (!File.Exists(ConfigFilePath))
            {
                using (fs = File.Create(ConfigFilePath))
                {
                }
            }

            try
            {
                if (!string.IsNullOrEmpty(message))
                {
                    StreamWriter streamWriter = new StreamWriter(ConfigFilePath, true);
                    streamWriter.WriteLine(message);
                    streamWriter.Close();
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        static public string CurrentAssemblyDirectory()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            try
            {
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
            }
            catch (Exception)
            {
            }
            return Path.GetDirectoryName(path);
        }

        /*public static void WriteBug(Exception ex)
        {
            using (var ctx = new dbRGLERPLogContext())
            {
                using (var _ctxTran = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        var oBugLog = new BugLog
                        {
                            BugDetails = "Error~On:" + Convert.ToString(Extension.UtcToday) + "~Message:" + (ex.InnerException == null ? "" : Convert.ToString(ex.InnerException.Message)) + "~StackTrace:" + Convert.ToString(ex.StackTrace),
                            CreateDate = Extension.UtcToday,
                            IsSolved = false,
                            CompanyId = StaticInfos.CompanyID,
                            CreatedBy = StaticInfos.LoggedUserID
                        };
                        ctx.BugLog.Add(oBugLog);
                        ctx.SaveChanges();
                        _ctxTran.Commit();
                    }
                    catch
                    {
                        _ctxTran.Rollback();
                    }
                }
            }
        }*/

        /*public static void WriteBug(string error)
        {
            using (var ctx = new dbRGLERPLogContext())
            {
                using (var _ctxTran = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        var oBugLog = new BugLog
                        {
                            BugDetails = "Error~On:" + Extension.UtcToday.ToString() + "~Message:" + Convert.ToString(error),
                            CreateDate = Extension.UtcToday,
                            IsSolved = false,
                            CompanyId = StaticInfos.CompanyID,
                            CreatedBy = StaticInfos.LoggedUserID
                        };
                        ctx.BugLog.Add(oBugLog);
                        ctx.SaveChanges();
                        _ctxTran.Commit();
                    }
                    catch
                    {
                        _ctxTran.Rollback();
                    }
                }
            }
        }*/
    }
}
