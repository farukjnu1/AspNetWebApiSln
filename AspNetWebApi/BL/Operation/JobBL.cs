using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetWebApi.DA.ADO;
using AspNetWebApi.DA.EF.VssDb;
using AspNetWebApi.DA.ViewModels.Operation;
using AspNetWebApi.DA.ViewModels.Stores;

namespace AspNetWebApi.BL.Operation
{
    public class JobBL
    {
        ModelVssDb _vssDb = null;
        private IGenericFactory<JobVM> Generic_Job = null;

        public List<JobVM> Get(string description, int jobGroupId, int pageIndex = 0, int pageSize = 10)
        {
            string vssDb = ConfigurationManager.ConnectionStrings["VssDb"].ConnectionString;
            Generic_Job = new GenericFactory<JobVM>();
            return Generic_Job.ExecuteCommandList(CommandType.StoredProcedure, StoredProcedure.sp_GetJobs, new Hashtable() { { "PageIndex", pageIndex }, { "PageSize", pageSize }, { "description", description }, { "jobGroupId", jobGroupId } }, vssDb);
        }

        /*public IEnumerable<JobVM> Get(string description, int jobGroupId, int pageIndex = 0, int pageSize = 10)
        {
            List<JobVM> listJob = null;
            int nRow = _vssDb.Jobs.Where(x => x.JobGroupId == (jobGroupId == 0 ? x.JobGroupId : jobGroupId)).Count();
            listJob = _vssDb.Jobs.Where(x => x.JobGroupId == (jobGroupId == 0 ? x.JobGroupId : jobGroupId))
                .Select(x => new JobVM
                {
                    JobId = x.JobId,
                    Description = x.Description,
                    JobGroupId = x.JobGroupId,
                    JobGroupName = (from y in _vssDb.JobGroups where y.GroupId == x.JobGroupId select y).FirstOrDefault() == null ? "" : (from y in _vssDb.JobGroups where y.GroupId == x.JobGroupId select y).FirstOrDefault().Name,
                    A = x.A,
                    B = x.B,
                    C = x.C,
                    DurationA = x.DurationA,
                    DurationB = x.DurationB,
                    DurationC = x.DurationC,
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    RowCount = nRow
                })
                .OrderByDescending(s => s.JobId)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToList();
            return listJob;
        }*/

        public bool AddJob(Job model)
        {
            try
            {
                model.JobId = GetNewId();
                model.CreateDate = DateTime.Now;
                model.CreateBy = model.CreateBy;
                _vssDb.Jobs.Add(model);
                _vssDb.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateJob(Job model)
        {
            try
            {
                _vssDb = new ModelVssDb();
                var selectedJob = _vssDb.Jobs
                 .Where(x =>x.JobId == model.JobId).FirstOrDefault();
                if (selectedJob != null)
                {
                    selectedJob.Description = model.Description;
                    selectedJob.JobGroupId = model.JobGroupId;
                    selectedJob.A = model.A;
                    selectedJob.B = model.B;
                    selectedJob.C = model.C;
                    selectedJob.DurationA = model.DurationA;
                    selectedJob.DurationB = model.DurationB;
                    selectedJob.DurationC = model.DurationC;
                    model.UpdateDate = DateTime.Now;
                    model.UpdateBy = model.CreateBy;
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

        public bool RemoveJob(int id)
        {
            try
            {
                _vssDb = new ModelVssDb();
                var selectedJob = _vssDb.Jobs
                 .Where(x => x.JobId == id)
                 .FirstOrDefault();
                if (selectedJob != null)
                {
                    _vssDb.Jobs.Remove(selectedJob);
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
                _vssDb = new ModelVssDb();
                var jobId = Convert.ToInt32(_vssDb.Jobs.Max(x => x.JobId)) + 1;
                return jobId;
            }
            catch
            {
                return 1;
            }
        }
    }
}
