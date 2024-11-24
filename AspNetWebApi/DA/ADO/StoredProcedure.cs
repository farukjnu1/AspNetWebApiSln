using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetWebApi.DA.ADO
{
    public class StoredProcedure
    {
        //Operation       
        public const string sp_GetJobCardNo = "[dbo].[sp_GetJobCardNo]";
        public const string sp_GetByVehicleNo = "[dbo].[sp_GetByVehicleNo]";
        public const string sp_GetWorkGroupById = "[dbo].[sp_GetWorkGroupById]";
        public const string sp_GetReceiver = "[dbo].[sp_GetReceiver]";
        public const string sp_GetManPower = "[dbo].[sp_GetManPower]";
        public const string sp_GetCompany = "[dbo].[sp_GetCompany]";
        public const string sp_GetJob = "[dbo].[sp_GetJob]";
        public const string sp_GetEngineSize = "[dbo].[sp_GetEngineSize]";
        public const string sp_GetJobGroup = "[dbo].[sp_GetJobGroup]";
        public const string sp_GetItemByParts = "[dbo].[sp_GetItemByParts]";
        public const string sp_GetClinetByPhone = "[dbo].[sp_GetClinetByPhone]";
        public const string sp_GetJobCard = "[dbo].[sp_GetJobCard]";
        public const string sp_GetClient = "[dbo].[sp_GetClient]";
        public const string sp_GetBusinessPartner = "[dbo].[sp_GetBusinessPartner]";
        public const string sp_GetSupplier = "[dbo].[sp_GetSupplier]";
        public const string sp_GetClientVehicle = "[dbo].[sp_GetClientVehicle]";
        public const string sp_GetUserRole = "[dbo].[sp_GetUserRole]";
        public const string sp_GetJcInvoice = "[dbo].[sp_GetJcInvoice]";
        public const string sp_GetMenuPermission = "[dbo].[sp_GetMenuPermission]";
        public const string sp_GetMenuPermissionByUserId = "[dbo].[sp_GetMenuPermissionByUserId]";
        public const string sp_GetStoreReq = "[dbo].[sp_GetStoreReq]";
        public const string sp_GetJobs = "[dbo].[sp_GetJobs]";
        public const string sp_GetJcReq = "[dbo].[sp_GetJcReq]";
        public const string sp_GetSalesPrice = "[dbo].[sp_GetSalesPrice]";
        public const string sp_GetPo = "[dbo].[sp_GetPo]";
        public const string sp_GetPoById = "[dbo].[sp_GetPoById]";
        // inventory
        public const string sp_GetInventoryReceive = "[dbo].[sp_GetInventoryReceive]";
        public const string sp_SetInventoryReceive = "[dbo].[sp_SetInventoryReceive]";
        // requisition
        public const string sp_GetRequisition = "[dbo].[sp_GetRequisition]";
        public const string sp_SetRequisition = "[dbo].[sp_SetRequisition]";
    }
}