using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using AspNetWebApi.DA.ADO;
using AspNetWebApi.DA.ViewModels.Operation;
using System.Data;
using System.Collections;
using AspNetWebApi.DA.EF.VssDb;
using System.Resources;
using Google.Protobuf.WellKnownTypes;
using AspNetWebApi.DA.ViewModels.Billing;

namespace AspNetWebApi.BL.Operation
{
    public class JobCardBL
    {
        private IGenericFactory<ClientVM> Generic_T = null;
        private IGenericFactory<JobCardVM> Generic_JobCardVM = null;
        private IGenericFactory<WorkGroupVM> Generic_WorkGroupVM = null;
        private IGenericFactory<JobVM> Generic_JobVM = null;
        private IGenericFactory<EngineSizeVM> Generic_EngineSizeVM = null;
        private IGenericFactory<JobGroupVM> Generic_JobGroupVM = null;
        private IGenericFactory<ItemVM> Generic_ItemVM = null;
        
        public string GetJCNo() 
        {
            string vssDb = ConfigurationManager.ConnectionStrings["VssDb"].ConnectionString;
            Generic_T = new GenericFactory<ClientVM>();
            return Generic_T.ExecuteCommandStr(CommandType.StoredProcedure, StoredProcedure.sp_GetJobCardNo, new Hashtable(), vssDb);
        }

        public List<JobCardVM> GetByVehicleNo(string vehicleNo)
        {
            string vssDb = ConfigurationManager.ConnectionStrings["VssDb"].ConnectionString;
            Generic_JobCardVM = new GenericFactory<JobCardVM>();
            return Generic_JobCardVM.ExecuteCommandList(CommandType.StoredProcedure, StoredProcedure.sp_GetByVehicleNo, new Hashtable() { { "vehicleno", vehicleNo } }, vssDb);
        }

        public List<WorkGroupVM> GetWorkGroupById(int workGroupId)
        {
            string vssDb = ConfigurationManager.ConnectionStrings["VssDb"].ConnectionString;
            Generic_WorkGroupVM = new GenericFactory<WorkGroupVM>();
            return Generic_WorkGroupVM.ExecuteCommandList(CommandType.StoredProcedure, StoredProcedure.sp_GetWorkGroupById, new Hashtable() { { "WorkGroupId", workGroupId } }, vssDb);
        }

        public List<WorkGroupVM> GetReceiver()
        {
            string vssDb = ConfigurationManager.ConnectionStrings["VssDb"].ConnectionString;
            Generic_WorkGroupVM = new GenericFactory<WorkGroupVM>();
            return Generic_WorkGroupVM.ExecuteCommandList(CommandType.StoredProcedure, StoredProcedure.sp_GetReceiver, new Hashtable(), vssDb);
        }

        public List<WorkGroupVM> GetManPower()
        {
            string vssDb = ConfigurationManager.ConnectionStrings["VssDb"].ConnectionString;
            Generic_WorkGroupVM = new GenericFactory<WorkGroupVM>();
            return Generic_WorkGroupVM.ExecuteCommandList(CommandType.StoredProcedure, StoredProcedure.sp_GetReceiver, new Hashtable(), vssDb);
        }

        public List<JobVM> GetJob()
        {
            string vssDb = ConfigurationManager.ConnectionStrings["VssDb"].ConnectionString;
            Generic_JobVM = new GenericFactory<JobVM>();
            return Generic_JobVM.ExecuteCommandList(CommandType.StoredProcedure, StoredProcedure.sp_GetJob, new Hashtable() { }, vssDb);
        }

        public List<EngineSizeVM> GetEngineSize()
        {
            string vssDb = ConfigurationManager.ConnectionStrings["VssDb"].ConnectionString;
            Generic_EngineSizeVM = new GenericFactory<EngineSizeVM>();
            return Generic_EngineSizeVM.ExecuteCommandList(CommandType.StoredProcedure, StoredProcedure.sp_GetEngineSize, new Hashtable() { }, vssDb);
        }

        public List<JobGroupVM> GetJobGroup()
        {
            string vssDb = ConfigurationManager.ConnectionStrings["VssDb"].ConnectionString;
            Generic_JobGroupVM = new GenericFactory<JobGroupVM>();
            return Generic_JobGroupVM.ExecuteCommandList(CommandType.StoredProcedure, StoredProcedure.sp_GetJobGroup, new Hashtable() { }, vssDb);
        }

        public List<ItemVM> GetItemByParts(string value)
        {
            string vssDb = ConfigurationManager.ConnectionStrings["VssDb"].ConnectionString;
            Generic_ItemVM = new GenericFactory<ItemVM>();
            return Generic_ItemVM.ExecuteCommandList(CommandType.StoredProcedure, StoredProcedure.sp_GetItemByParts, new Hashtable() { { "value", value } }, vssDb);
        }

        public List<ClientVM> GetClientByPhone(string value)
        {
            string vssDb = ConfigurationManager.ConnectionStrings["VssDb"].ConnectionString;
            Generic_T = new GenericFactory<ClientVM>();
            return Generic_T.ExecuteCommandList(CommandType.StoredProcedure, StoredProcedure.sp_GetClinetByPhone, new Hashtable() { { "value", value } }, vssDb);
        }

        ModelVssDb _vssDb = null;
        
        public bool Add(JobCardVM model)
        {
            using (_vssDb = new ModelVssDb())
            {
                using (var _tran = _vssDb.Database.BeginTransaction())
                {
                    try
                    {
                        var oJC = (from x in _vssDb.JobCards where x.JcNo == model.JcNo.Trim() select x).FirstOrDefault();
                        if (oJC != null)
                        {
                            _tran.Rollback();
                            return false;
                        }
                        #region Vehicle Info
                        JobCard oJobCard = new JobCard();
                        oJobCard.JcNo = model.JcNo;
                        oJobCard.VehicleNo = model.VehicleNo;
                        oJobCard.Model = model.Model;
                        oJobCard.Vin = model.Vin;
                        oJobCard.Mileage = model.Mileage;
                        oJobCard.ReceiveDate = model.ReceiveDate;
                        oJobCard.SupervisorId = model.ReceiveBy;
                        oJobCard.Bay = model.Bay;
                        oJobCard.JcStatus = model.JcStatus;
                        oJobCard.EstiCostJob = model.EstiCostJob;
                        oJobCard.EstiCostSpare = model.EstiCostSpare;
                        oJobCard.EstiCostTotal = model.EstiCostTotal;
                        oJobCard.ActualCostJob = model.ActualCostJob;
                        oJobCard.ActualCostSpare = model.ActualCostSpare;
                        oJobCard.ActualCostTotal = model.ActualCostTotal;
                        oJobCard.ClientId = model.ClientId;
                        oJobCard.ClientInfo = model.Description;
                        oJobCard.ContactPerson = model.ContactPerson;
                        oJobCard.ContactPersonNo = model.ContactPersonNo;
                        oJobCard.CreateBy = model.CreateBy;
                        oJobCard.CreateDate = DateTime.Now;
                        _vssDb.JobCards.Add(oJobCard);
                        _vssDb.SaveChanges();
                        model.Id = oJobCard.Id;
                        #endregion
                        #region Job-Details
                        var listJcJobRem = _vssDb.JcJobs.Where(x => x.JcId == model.Id).ToList();
                        _vssDb.JcJobs.RemoveRange(listJcJobRem);
                        _vssDb.SaveChanges();
                        List<JcJob> listJcJob = new List<JcJob>();
                        foreach (var jcJob in model.JobDetails)
                        {
                            JcJob oJcJob = new JcJob();
                            oJcJob.JobGroupId = jcJob.JobGroupId;
                            oJcJob.JobId = jcJob.JobId;
                            oJcJob.EngineSizeId = jcJob.EngineSizeId;
                            oJcJob.Price = jcJob.Price;
                            oJcJob.Duration = jcJob.Duration;
                            oJcJob.JobStatus = jcJob.JobStatus;
                            oJcJob.JcId = oJobCard.Id;
                            listJcJob.Add(oJcJob);
                        }
                        _vssDb.JcJobs.AddRange(listJcJob);
                        _vssDb.SaveChanges();
                        #endregion
                        #region Spare-Parts
                        var listJcSpareRem = _vssDb.JcSpares.Where(x => x.JcId == model.Id).ToList();
                        _vssDb.JcSpares.RemoveRange(listJcSpareRem);
                        _vssDb.SaveChanges();
                        List<JcSpare> listJcSpare = new List<JcSpare>();
                        foreach (var jcSpare in model.JcSpares)
                        {
                            JcSpare oJcSpare = new JcSpare();
                            oJcSpare.ItemId = jcSpare.ItemId;
                            oJcSpare.Quantity = jcSpare.Quantity;
                            oJcSpare.SalePrice = jcSpare.SalePrice;
                            oJcSpare.ItemStatus = jcSpare.ItemStatus;
                            oJcSpare.SpareAmount = jcSpare.SpareAmount;
                            oJcSpare.JcId = oJobCard.Id;
                            listJcSpare.Add(oJcSpare);
                        }
                        _vssDb.JcSpares.AddRange(listJcSpare);
                        _vssDb.SaveChanges();
                        #endregion
                        #region JC HR
                        var listJcHRRe = _vssDb.JcHRs.Where(x => x.JcId == model.Id).ToList();
                        _vssDb.JcHRs.RemoveRange(listJcHRRe);
                        _vssDb.SaveChanges();
                        List<JcHR> listJcHR = new List<JcHR>();
                        foreach (var oJcHR in model.Resources)
                        {
                            JcHR oJcHr = new JcHR();
                            oJcHr.EmployeeId = oJcHR.EmployeeId;
                            oJcHr.JcId = oJobCard.Id;
                            listJcHR.Add(oJcHr);
                        }
                        _vssDb.JcHRs.AddRange(listJcHR);
                        _vssDb.SaveChanges();
                        #endregion
                        _tran.Commit();
                        return true;
                    }
                    catch
                    {
                        _tran.Rollback();
                        return false;
                    }
                }
            }
        }

        public bool Update(JobCardVM model)
        {
            using (_vssDb = new ModelVssDb())
            {
                using (var _tran = _vssDb.Database.BeginTransaction())
                {
                    try
                    {
                        #region Validation
                        Invoice oI = (from x in _vssDb.Invoices where x.JcId == model.Id select x).FirstOrDefault();
                        if (oI != null) 
                        {
                            _tran.Rollback();
                            return false;
                        }
                        #endregion
                        #region Vehicle Info
                        JobCard oJobCard = (from x in _vssDb.JobCards where x.Id == model.Id select x).FirstOrDefault();
                        if (oJobCard != null)
                        {
                            oJobCard.JcNo = model.JcNo;
                            oJobCard.VehicleNo = model.VehicleNo;
                            oJobCard.Model = model.Model;
                            oJobCard.Vin = model.Vin;
                            oJobCard.Mileage = model.Mileage;
                            oJobCard.ReceiveDate = model.ReceiveDate;
                            oJobCard.SupervisorId = model.ReceiveBy;
                            oJobCard.Bay = model.Bay;
                            oJobCard.JcStatus = model.JcStatus;
                            oJobCard.EstiCostJob = model.EstiCostJob;
                            oJobCard.EstiCostSpare = model.EstiCostSpare;
                            oJobCard.EstiCostTotal = model.EstiCostTotal;
                            oJobCard.ActualCostJob = model.ActualCostJob;
                            oJobCard.ActualCostSpare = model.ActualCostSpare;
                            oJobCard.ActualCostTotal = model.ActualCostTotal;
                            oJobCard.ClientId = model.ClientId;
                            oJobCard.ClientInfo = model.Description;
                            oJobCard.ContactPerson = model.ContactPerson;
                            oJobCard.ContactPersonNo = model.ContactPersonNo;
                            oJobCard.UpdateBy = model.CreateBy;
                            oJobCard.UpdateDate = DateTime.Now;
                            _vssDb.SaveChanges();
                        }
                        #endregion
                        #region Job-Details
                        var listJcJobRem = _vssDb.JcJobs.Where(x => x.JcId == model.Id).ToList();
                        _vssDb.JcJobs.RemoveRange(listJcJobRem);
                        _vssDb.SaveChanges();
                        List<JcJob> listJcJob = new List<JcJob>();
                        foreach (var jcJob in model.JobDetails)
                        {
                            JcJob oJcJob = new JcJob();
                            oJcJob.JobGroupId = jcJob.JobGroupId;
                            oJcJob.JobId = jcJob.JobId;
                            oJcJob.EngineSizeId = jcJob.EngineSizeId;
                            oJcJob.Price = jcJob.Price;
                            oJcJob.Duration = jcJob.Duration;
                            oJcJob.JobStatus = jcJob.JobStatus;
                            oJcJob.JcId = oJobCard.Id;
                            listJcJob.Add(oJcJob);
                        }
                        _vssDb.JcJobs.AddRange(listJcJob);
                        _vssDb.SaveChanges();
                        #endregion
                        #region Spare-Parts
                        var listJcSpareRem = _vssDb.JcSpares.Where(x => x.JcId == model.Id).ToList();
                        _vssDb.JcSpares.RemoveRange(listJcSpareRem);
                        _vssDb.SaveChanges();
                        List<JcSpare> listJcSpare = new List<JcSpare>();
                        foreach (var jcSpare in model.JcSpares)
                        {
                            JcSpare oJcSpare = new JcSpare();
                            oJcSpare.ItemId = jcSpare.ItemId;
                            oJcSpare.Quantity = jcSpare.Quantity;
                            oJcSpare.SalePrice = jcSpare.SalePrice;
                            oJcSpare.ItemStatus = jcSpare.ItemStatus;
                            oJcSpare.SpareAmount = jcSpare.SpareAmount;
                            oJcSpare.JcId = oJobCard.Id;
                            listJcSpare.Add(oJcSpare);
                        }
                        _vssDb.JcSpares.AddRange(listJcSpare);
                        _vssDb.SaveChanges();
                        #endregion
                        #region JC HR
                        var listJcHRRe = _vssDb.JcHRs.Where(x => x.JcId == model.Id).ToList();
                        _vssDb.JcHRs.RemoveRange(listJcHRRe);
                        _vssDb.SaveChanges();
                        List<JcHR> listJcHR = new List<JcHR>();
                        foreach (var oJcHR in model.Resources)
                        {
                            JcHR oJcHr = new JcHR();
                            oJcHr.EmployeeId = oJcHR.EmployeeId;
                            oJcHr.JcId = oJobCard.Id;
                            listJcHR.Add(oJcHr);
                        }
                        _vssDb.JcHRs.AddRange(listJcHR);
                        _vssDb.SaveChanges();
                        #endregion
                        _tran.Commit();
                        return true;
                    }
                    catch
                    {
                        _tran.Rollback();
                        return false;
                    }
                }
            }
        }

        public IEnumerable<JobCardVM> Get(int pageIndex = 0, int pageSize = 5, int jcStatus = 0, string JcNo = "", DateTime? StartDate = null, DateTime? EndDate = null)
        {
            string vssDb = ConfigurationManager.ConnectionStrings["VssDb"].ConnectionString;
            Generic_JobCardVM = new GenericFactory<JobCardVM>();
            var oHashTable = new Hashtable()
            {
                { "PageIndex", pageIndex },
                { "PageSize", pageSize },
                { "JcStatus", jcStatus },
                { "JcNo", JcNo },
                { "StartDate", StartDate },
                { "EndDate", EndDate }
            };
            return Generic_JobCardVM.ExecuteCommandList(CommandType.StoredProcedure, StoredProcedure.sp_GetJobCard, oHashTable, vssDb);
        }

        public JobCardVM Get(int id)
        {
            _vssDb = new ModelVssDb();
            var oI = _vssDb.Invoices.Where(x => x.JcId == id).FirstOrDefault();
            int isInvoice = oI == null ? 0 : 1;
            long invoiceId = oI == null ? 0 : oI.Id;
            JobCardVM oJobCard = null;
            oJobCard = _vssDb.Database.SqlQuery<JobCardVM>(@"SELECT jc.Id,
            jc.JcNo,                            
            jc.VehicleNo,
            jc.Model,
            jc.Vin,
            ISNULL(jc.Mileage,0) Mileage,
            ReceiveDate = jc.ReceiveDate,
            jc.SupervisorId ReceiveBy,
            jc.Bay,
            jc.JcStatus,
            jc.EstiCostJob,
            jc.EstiCostSpare,
            jc.EstiCostTotal,
            ISNULL(jc.ActualCostJob,0) ActualCostJob,
            ISNULL(jc.ActualCostSpare,0) ActualCostSpare,
            ISNULL(jc.ActualCostTotal,0) ActualCostTotal,
            jc.ClientId,
            jc.ClientInfo [Description],
            jc.CreateBy,
            jc.CreateDate,
            ISNULL(jc.ContactPerson,'') ContactPerson,
            ISNULL(jc.ContactPersonNo,'') ContactPersonNo,
            ISNULL(jc.UpdateDate,'') UpdateDate,
            ISNULL(C.Address, '') ClientAddress,
            ISNULL(C.Email, '') ClientEmail,
            ISNULL(C.BpId, '') ClientId,
            ISNULL(C.Name, '') ClientName,
            ISNULL(C.Phone, '') ClientPhone,
            ISNULL(C.MembershipNo, '') MembershipNo,
			ISNULL(u.FirstName, '') + ' ' + ISNULL(u.MiddleName, '') + ' ' + ISNULL(u.LastName, '') CreateByName,
			ISNULL(CV.Model, '') Model,
			ISNULL(CV.SubModel, '') SubModel
            FROM JobCard jc
			LEFT JOIN BusinessPartner C ON C.BpId=jc.ClientId
			LEFT JOIN [User] u ON u.UserID=jc.CreateBy
			LEFT JOIN [ClientVehicle] CV ON CV.ClientId=jc.ClientId
            WHERE jc.Id=" + id).FirstOrDefault();
            if (oJobCard != null)
            {
                oJobCard.JobDetails = _vssDb.Database.SqlQuery<JobDetailVm>(@"SELECT j.Id,
                j.JobId,
                j.JobStatus,
                j.JcId,
                j.Duration,
                j.JobGroupId,
                jg.[Name] JobGroupName,
                j.EngineSizeId,
                es.CC EngineSizeName,
                CASE WHEN j.JobStatus=1 THEN 'Close' WHEN j.JobStatus = 2 THEN 'Open' ELSE '' END JobStatusName,
                jj.[Description] JobName,
                j.Price 
                FROM JcJob j
                LEFT JOIN JobGroup jg ON j.JobGroupId=jg.GroupId
                LEFT JOIN EngineSize es ON j.EngineSizeId=es.EngineSizeId
                LEFT JOIN Job jj ON j.JobId=jj.JobId                                        
                WHERE j.JcId=" + id).ToList();
                oJobCard.JcSpares = _vssDb.Database.SqlQuery<JcSpareVm>(@"SELECT s.Id,
                s.JcId JcId,
                i.Id ItemId,
                ISNULL(i.ItemCode,'') ItemCode,
                i.ItemName,
                i.BrandId,
                ISNULL(b.[Name],'') BrandName,
                ISNULL(s.SalePrice,0) SalePrice,
                ISNULL(s.Quantity,0) Quantity,
                ISNULL(s.SpareAmount,0) SpareAmount,
                ISNULL(s.ItemStatus,'') ItemStatus,
                CASE WHEN s.ItemStatus = 1 THEN 'Used' ELSE 'Refund' END ItemStatusName,
                i.PartNoOld,
                i.PartNoNew 
                FROM JcSpare s
                LEFT JOIN Item i ON s.ItemId=i.Id
                LEFT JOIN Brand b ON i.BrandId = b.Id
                WHERE s.JcId =" + id).ToList();
                oJobCard.Resources = _vssDb.Database.SqlQuery<JcHRVM>(@"SELECT hr.Id,
                hr.JcId,
                hr.EmployeeId,
                ISNULL(e.FirstName,'') + ' ' + ISNULL(e.MiddleName,'') + ' ' + ISNULL(e.LastName,'') FullName
                FROM JcHR hr
                LEFT JOIN Employee e ON hr.EmployeeId=e.EmployeeId
                where hr.JcId =" + id).ToList();
                oJobCard.BalanceAmount = (from b in _vssDb.BusinessPartnerBalances where b.BusinessPartnerId == oJobCard.ClientId select b.BalanceAmount).FirstOrDefault();
                oJobCard.PaySettles = _vssDb.Database.SqlQuery<PaySettleVM>(@"SELECT ps.Amount,
                ps.Id,
                ps.InvoiceId,
                ISNULL(ps.PayDate,'') PayDate,
                ISNULL(ps.PaymentMethod,'') PaymentMethod,
                ISNULL(pm.Name,'') PayMethodName
                FROM PaySettle ps
                LEFT JOIN PayMethod pm on ps.PaymentMethod = pm.MethodId
                where ps.InvoiceId =" + invoiceId).ToList();
            }
            return oJobCard;
        }

        public JobCardVM Get1(int id)
        {
            _vssDb = new ModelVssDb();
            var oI = _vssDb.Invoices.Where(x=>x.JcId == id).FirstOrDefault();
            int isInvoice = oI == null ? 0 : 1;
            long invoiceId = oI == null ? 0 : oI.Id;
            JobCardVM oJobCard = null;
            oJobCard = (from jc in _vssDb.JobCards
                        //join bp in _vssDb.BusinessPartners on jc.ClientId equals bp.BpId
                        where jc.Id == id
                        select new JobCardVM
                        {
                            Id = jc.Id,
                            JcNo = jc.JcNo,
                            IsInvoice = isInvoice,
                            VehicleNo = jc.VehicleNo,
                            Model = jc.Model,
                            Vin = jc.Vin,
                            Mileage = jc.Mileage,
                            ReceiveDate = jc.ReceiveDate,
                            ReceiveBy = jc.SupervisorId,
                            Bay = jc.Bay,
                            JcStatus = jc.JcStatus,
                            EstiCostJob = jc.EstiCostJob,
                            EstiCostSpare = jc.EstiCostSpare,
                            EstiCostTotal = jc.EstiCostTotal,
                            ActualCostJob = jc.ActualCostJob,
                            ActualCostSpare = jc.ActualCostSpare,
                            ActualCostTotal = jc.ActualCostTotal,
                            ClientId = jc.ClientId,
                            Description = jc.ClientInfo,
                            CreateBy = jc.CreateBy,
                            CreateDate = jc.CreateDate,
                            ContactPerson = jc.ContactPerson == null ? "" : jc.ContactPerson,
                            ContactPersonNo = jc.ContactPersonNo == null ? "" : jc.ContactPersonNo,
                            UpdateDate = jc.UpdateDate,

                            JobDetails = (from j in _vssDb.JcJobs
                                          join jg in _vssDb.JobGroups on j.JobGroupId equals jg.GroupId
                                          join es in _vssDb.EngineSizes on j.EngineSizeId equals es.EngineSizeId
                                          join jj in _vssDb.Jobs on j.JobId equals jj.JobId
                                          where j.JcId == jc.Id
                                          select new JobDetailVm
                                          {
                                              Id = j.Id,
                                              JobId = j.JobId,
                                              JobStatus = j.JobStatus,
                                              JcId = jc.Id,
                                              Duration = j.Duration,
                                              JobGroupId = j.JobGroupId,
                                              JobGroupName = jg.Name,
                                              EngineSizeId = j.EngineSizeId,
                                              EngineSizeName = es.CC,
                                              JobStatusName = j.JobStatus == 1 ? "Close" : j.JobStatus == 2 ? "Open" : "",
                                              JobName = jj.Description,
                                              Price = j.Price
                                          }).ToList(),
                            JcSpares = (from s in _vssDb.JcSpares
                                        join i in _vssDb.Items on s.ItemId equals i.Id
                                        join b in _vssDb.Brands on i.BrandId equals b.Id
                                        where s.JcId == jc.Id
                                        select new JcSpareVm
                                        {
                                            Id = s.Id,
                                            JcId = jc.Id,
                                            ItemId = i.Id,
                                            ItemCode = i.ItemCode,
                                            ItemName = i.ItemName,
                                            BrandId = i.BrandId,
                                            BrandName = b.Name,
                                            SalePrice = s.SalePrice,
                                            Quantity = s.Quantity,
                                            SpareAmount = s.SpareAmount,
                                            ItemStatus = s.ItemStatus,
                                            ItemStatusName = s.ItemStatus == 1 ? "Used" : "Refund",
                                            PartNoOld = i.PartNoOld,
                                            PartNoNew = i.PartNoNew
                                        }).ToList(),
                            Resources = (from hr in _vssDb.JcHRs
                                         join e in _vssDb.Employees on hr.EmployeeId equals e.EmployeeId
                                         where hr.JcId == jc.Id
                                         select new JcHRVM
                                         {
                                             Id = hr.Id,
                                             JcId = jc.Id,
                                             EmployeeId = hr.EmployeeId,
                                             FullName = e.FirstName + " " + e.MiddleName + " " + e.LastName
                                         }).ToList(),
                            BalanceAmount = (from b in _vssDb.BusinessPartnerBalances where b.BusinessPartnerId == jc.ClientId select b.BalanceAmount).FirstOrDefault(),
                            PaySettles = (from ps in _vssDb.PaySettles
                                          join pm in _vssDb.PayMethods on ps.PaymentMethod equals pm.MethodId
                                          where ps.InvoiceId == invoiceId
                                          select new PaySettleVM()
                                          {
                                              Amount = ps.Amount,
                                              Id = ps.Id,
                                              InvoiceId = invoiceId,
                                              PayDate = ps.PayDate,
                                              PaymentMethod = ps.PaymentMethod,
                                              PayMethodName = pm.Name
                                          }).ToList()
                        }).FirstOrDefault();
            if (oJobCard != null)
            {
                #region jc created by
                var oUser = (from x in _vssDb.Users where x.UserID == oJobCard.CreateBy select x).FirstOrDefault();
                if (oUser != null)
                {
                    oJobCard.CreateByName = oUser != null ? oUser.FirstName + " " + oUser.MiddleName + " " + oUser.LastName : "";
                }
                #endregion
                #region Client
                var oClient = (from x in _vssDb.BusinessPartners where x.BpId == oJobCard.ClientId && x.BpTypeId == 1 select x).FirstOrDefault();
                if (oClient != null)
                {
                    oJobCard.ClientAddress = oClient.Address;
                    oJobCard.ClientEmail = oClient.Email;
                    oJobCard.ClientId = oClient.BpId;
                    oJobCard.ClientName = oClient.Name;
                    oJobCard.ClientPhone = oClient.Phone;
                    oJobCard.MembershipNo = oClient.MembershipNo;
                }
                #endregion
                #region Client Vehicle
                if (oClient != null)
                {
                    var oClientVehicle = (from x in _vssDb.ClientVehicles where x.ClientId == oClient.BpId && x.VehicleNo.Trim()==oJobCard.VehicleNo.Trim() select x).FirstOrDefault();
                    if (oClientVehicle != null)
                    {
                        oJobCard.Model = oClientVehicle.Model;
                        oJobCard.SubModel = oClientVehicle.SubModel;
                    }
                }
                #endregion
            }
            return oJobCard;
        }

        public bool AddJcReq(JcReq model)
        {
            bool isSuccess = false;
            using (_vssDb = new ModelVssDb())
            {
                using (var _tran = _vssDb.Database.BeginTransaction())
                {
                    try
                    {
                        model.CreateDate = DateTime.Now;
                        _vssDb.JcReqs.Add(model);
                        _vssDb.SaveChanges();
                        _tran.Commit();
                        isSuccess = true;
                    }
                    catch
                    {
                        _tran.Rollback();
                    }
                }
            }
            return isSuccess;
        }

        public bool UpdateJcReq(JcReq model)
        {
            bool isSuccess = false;
            using (_vssDb = new ModelVssDb())
            {
                using (var _tran = _vssDb.Database.BeginTransaction())
                {
                    try
                    {
                        var oJcReq = (from x in _vssDb.JcReqs where x.Id == model.Id select x).FirstOrDefault();
                        if (oJcReq != null)
                        {
                            oJcReq.CreateDate = DateTime.Now;
                            _vssDb.SaveChanges();
                            _tran.Commit();
                            isSuccess = true;
                        }
                    }
                    catch
                    {
                        _tran.Rollback();
                    }
                }
            }
            return isSuccess;
        }
    }
}