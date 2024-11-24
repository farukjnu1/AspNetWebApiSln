using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetWebApi.DA.EF.VssDb;
using AspNetWebApi.DA.ViewModels.Operation;

namespace AspNetWebApi.BL.Operation
{
    public class EngineSizeBL
    {
        ModelVssDb _vssDb = new ModelVssDb();
        List<EngineSize> listEngineSize = null;

        public IEnumerable<EngineSize> Get()
        {
            return listEngineSize = _vssDb.EngineSizes.ToList();
        }

        public bool Add(EngineSize model)
        {
            try
            {
                model.EngineSizeId = GetNewId();
                _vssDb.EngineSizes.Add(model);
                _vssDb.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool RemoveEngine(int engineId)
        {
            try
            {
                var removeEngineDetails = _vssDb.EngineSizes
                 .Where(x => x.EngineSizeId == engineId).FirstOrDefault();
                if (removeEngineDetails != null)
                {
                    _vssDb.EngineSizes.Remove(removeEngineDetails);
                    _vssDb.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
            return false;
        }
        public bool updateEngine(EngineSize selectedEngine)
        {
            try
            {
                var existingEngine = _vssDb.EngineSizes
                 .Where(x =>x.EngineSizeId == selectedEngine.EngineSizeId).FirstOrDefault();
                if (existingEngine != null)
                {
                    existingEngine.Code = selectedEngine.Code;
                    existingEngine.Description = selectedEngine.Description;
                    existingEngine.CC = selectedEngine.CC;
                    _vssDb.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
            return false;

        }
        private int GetNewId()
        {
            try
            {
                var nid = Convert.ToInt32(_vssDb.EngineSizes.Max(x => x.EngineSizeId)) + 1;
                return nid;
            }
            catch
            {
                return 1;
            }
        }


    }
}
