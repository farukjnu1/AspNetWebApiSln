using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Drawing.Printing;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using AspNetWebApi.DA.ADO;
using AspNetWebApi.DA.EF.VssDb;
using AspNetWebApi.DA.Utilities;
using AspNetWebApi.DA.ViewModels.Operation;
using AspNetWebApi.DA.ViewModels.Requisition;
using AspNetWebApi.DA.ViewModels.Stores;
using static AspNetWebApi.DA.Utilities.BLStatus;

namespace AspNetWebApi.BL.Requisition
{
    public class RequisitionBL
    {
        //ModelVssDb _vssDb = null;
        //List<StoreReq> listEngineSize = null;
        private IGenericFactory<RequisitionVM> Generic_Requisition = null;

        #region CRUD
        public List<RequisitionVM> Get(int requisitionTypeId, int pageIndex, int pageSize)
        {
            string VssDb = ConfigurationManager.ConnectionStrings["VssDb"].ConnectionString;
            Generic_Requisition = new GenericFactory<RequisitionVM>();
            Hashtable hashTable = new Hashtable()
            {
                { "QueryOption", (int)Enams.QueryOption.GetAll },
                { "PageIndex", pageIndex },
                { "PageSize", pageSize },
                { "RequisitionTypeId", requisitionTypeId }
            };
            return Generic_Requisition.ExecuteCommandList(CommandType.StoredProcedure, StoredProcedure.sp_GetRequisition, hashTable, VssDb);
        }

        public List<RequisitionVM> ReadByType(int requisitionTypeId, int pageIndex, int pageSize)
        {
            string VssDb = ConfigurationManager.ConnectionStrings["VssDb"].ConnectionString;
            Generic_Requisition = new GenericFactory<RequisitionVM>();
            Hashtable hashTable = new Hashtable()
            {
                { "QueryOption", (int)QueryOption.RequisitionForReceive },
                { "PageIndex", pageIndex },
                { "PageSize", pageSize },
                { "RequisitionTypeId", requisitionTypeId }
            };
            return Generic_Requisition.ExecuteCommandList(CommandType.StoredProcedure, StoredProcedure.sp_GetRequisition, hashTable, VssDb);
        }

        public bool Create(RequisitionVM viewModel)
        {
            bool isSuccess = false;
            try
            {
                string VssDb = ConfigurationManager.ConnectionStrings["VssDb"].ConnectionString;
                Generic_Requisition = new GenericFactory<RequisitionVM>();
                Hashtable hashTable = new Hashtable()
                {
                    { "QueryOption", Enams.QueryOption.Insert},
                    { "RequisitionId", viewModel.RequisitionId },
                    { "RequisitionTypeId", viewModel.RequisitionTypeId },
                    { "ItemId", viewModel.ItemId },
                    { "Quantity", viewModel.Quantity },
                    { "Remark", viewModel.Remark },
                    { "CreateBy", viewModel.CreateBy }
                };
                var result = Generic_Requisition.ExecuteCommandObject(CommandType.StoredProcedure, StoredProcedure.sp_SetRequisition, hashTable, VssDb);
                if (result.RequisitionId > 0)
                    isSuccess = true;
            }
            catch 
            {

            }
            return isSuccess;
        }

        public bool Update(RequisitionVM viewModel)
        {
            bool isSuccess = false;
            try
            {
                string VssDb = ConfigurationManager.ConnectionStrings["VssDb"].ConnectionString;
                Generic_Requisition = new GenericFactory<RequisitionVM>();
                Hashtable hashTable = new Hashtable()
                {
                    { "QueryOption", Enams.QueryOption.Update},
                    { "RequisitionId", viewModel.RequisitionId },
                    { "RequisitionTypeId", viewModel.RequisitionTypeId },
                    { "ItemId", viewModel.ItemId },
                    { "Quantity", viewModel.Quantity },
                    { "Remark", viewModel.Remark },
                    { "UpdateBy", viewModel.UpdateBy }
                };
                var result = Generic_Requisition.ExecuteCommandObject(CommandType.StoredProcedure, StoredProcedure.sp_SetRequisition, hashTable, VssDb);
                if (result.RequisitionId > 0)
                    isSuccess = true;
            }
            catch { }
            
            return isSuccess;
        }

        public bool Delete(RequisitionVM viewModel)
        {
            bool isSuccess = false;
            try
            {
                string VssDb = ConfigurationManager.ConnectionStrings["VssDb"].ConnectionString;
                Generic_Requisition = new GenericFactory<RequisitionVM>();
                Hashtable hashTable = new Hashtable()
                {
                    { "QueryOption", Enams.QueryOption.Delete},
                    { "RequisitionId", viewModel.RequisitionId },
                    { "DeleteBy", viewModel.DeleteBy }
                };
                var result = Generic_Requisition.ExecuteCommandObject(CommandType.StoredProcedure, StoredProcedure.sp_SetRequisition, hashTable, VssDb);
                if (result.RequisitionId > 0)
                    isSuccess = true;
            }
            catch { }
            
            return isSuccess;
        }

        #endregion

        public enum QueryOption
        {
            RequisitionForReceive = 6
        }

    }
}