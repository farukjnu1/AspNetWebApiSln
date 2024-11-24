using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetWebApi.DA.EF.VssDb;
using AspNetWebApi.DA.ViewModels.Operation;

namespace AspNetWebApi.BL.Operation
{
    public class JobGroupBL
    {
        ModelVssDb _vssDb = new ModelVssDb();
        public IEnumerable<JobGroupVM> Get(int pageIndex = 0, int pageSize = 5)
        {
            int nRow = _vssDb.JobGroups.Count();
            var listJobGroup = _vssDb.JobGroups
                .Select(x => new JobGroupVM
                {
                    GroupId = x.GroupId,
                    Name = x.Name,
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    RowCount = nRow
                })
                .OrderByDescending(s => s.GroupId)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToList();
            return listJobGroup;
        }

        public bool Add(JobGroup model)
        {
            try
            {
                model.GroupId = GetNewId();
                _vssDb.JobGroups.Add(model);
                _vssDb.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Remove(int id)
        {
            try
            {
                var selectedJobGroup = _vssDb.JobGroups
                 .Where(x => x.GroupId == id)
                 .FirstOrDefault();
                if (selectedJobGroup != null)
                {
                    _vssDb.JobGroups.Remove(selectedJobGroup);
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

        public bool Update(JobGroup model)
        {
            try
            {
                var selectedJobGroup = _vssDb.JobGroups
                 .Where(x => x.GroupId == model.GroupId).FirstOrDefault();
                if (selectedJobGroup != null)
                {
                    selectedJobGroup.Name = model.Name;
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
                var jobId = Convert.ToInt32(_vssDb.JobGroups.Max(x => x.GroupId)) + 1;
                return jobId;
            }
            catch
            {
                return 1;
            }
        }
    }
}
