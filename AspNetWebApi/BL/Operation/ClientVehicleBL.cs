using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.Mvc;
using AspNetWebApi.DA.ADO;
using AspNetWebApi.DA.EF.VssDb;
using AspNetWebApi.DA.ViewModels.Operation;

namespace AspNetWebApi.BL.Operation
{
    public class ClientVehicleBL
    {
        ModelVssDb _vssDb = null;
        List<ClientVehicleVM> listClientVehicle = null;
        //ClientVehicleVM oClientVehicle = null;
        private IGenericFactory<ClientVehicleVM> Generic_ClientVehicle = null;

        public ClientVehicleBL()
        {
            _vssDb = new ModelVssDb();
        }

        public IEnumerable<ClientVehicleVM> Get(string phone, string vehicle, int pageIndex, int pageSize)
        {
            string vssDb = ConfigurationManager.ConnectionStrings["VssDb"].ConnectionString;
            Generic_ClientVehicle = new GenericFactory<ClientVehicleVM>();
            var oHashTable = new Hashtable()
            {
                { "PageIndex", pageIndex },
                { "PageSize", pageSize },
                { "phone", phone },
                { "vehicle", vehicle }
            };
            return Generic_ClientVehicle.ExecuteCommandList(CommandType.StoredProcedure, StoredProcedure.sp_GetClientVehicle, oHashTable, vssDb);
        }

        public ClientVehicleVM Get(long id)
        {
            var oCV = (from x in _vssDb.ClientVehicles
                       where x.Id == id
                       select new ClientVehicleVM
                       {
                           Id = x.Id,
                           VehicleNo = x.VehicleNo,
                           Model = x.Model,
                           Vin = x.Vin,
                           ClientId = x.ClientId,
                       }).FirstOrDefault();
            return oCV;
        }

        public bool Add(ClientVehicleVM model)
        {
            try
            {
                var oClientVehicle = new ClientVehicle();
                //oClientVehicle.ClientId = model.ClientId;
                oClientVehicle.ClientId = GetClientId(model.Value);
                oClientVehicle.Manufacturer = model.Manufacturer;
                oClientVehicle.Vin = model.Vin;
                oClientVehicle.Model = model.Model;
                oClientVehicle.SubModel = model.SubModel;
                oClientVehicle.From = model.From;
                oClientVehicle.To = model.To;
                oClientVehicle.VehicleNo = model.VehicleNo;
                oClientVehicle.CreateBy = model.CreateBy;
                oClientVehicle.CreateDate = DateTime.Now;
                oClientVehicle.Body = model.Body;
                oClientVehicle.Engine = model.Engine;
                oClientVehicle.Transmission = model.Transmission;
                oClientVehicle.Remark = model.Remark;
                _vssDb.ClientVehicles.Add(oClientVehicle);
                _vssDb.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private int GetClientId(string value)
        {
            int clientId = 0;
            if (!string.IsNullOrEmpty(value))
            {
                if (value.Contains("-"))
                {
                    string phone = value.Split('-')[0].Trim();
                    var oClient = _vssDb.BusinessPartners.Where(x => x.Phone.Trim() == phone).FirstOrDefault();
                    if (oClient != null)
                    {
                        clientId = oClient.BpId;   
                    }
                }
            }
            return clientId;
        }

        public bool Update(ClientVehicleVM model)
        {
            try
            {
                var oClientVehicle = _vssDb.ClientVehicles
                 .Where(x => x.Id == model.Id).FirstOrDefault();
                if (oClientVehicle != null)
                {
                    oClientVehicle.Id = model.Id;
                    //oClientVehicle.ClientId = model.ClientId;
                    oClientVehicle.ClientId = GetClientId(model.Value);
                    oClientVehicle.VehicleNo = model.VehicleNo;
                    oClientVehicle.Model = model.Model;
                    oClientVehicle.Vin = model.Vin;
                    oClientVehicle.Manufacturer = model.Manufacturer;
                    oClientVehicle.Model = model.Model;
                    oClientVehicle.SubModel = model.SubModel;
                    oClientVehicle.From = model.From;
                    oClientVehicle.To = model.To;
                    oClientVehicle.Body = model.Body;
                    oClientVehicle.Engine = model.Engine;
                    oClientVehicle.CreateBy = model.CreateBy;
                    oClientVehicle.CreateDate = DateTime.Now;
                    oClientVehicle.Transmission = model.Transmission;
                    oClientVehicle.Remark = model.Remark;
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

        public List<ClientVehicleVM> GetVehiclesByClient(long id)
        {
            listClientVehicle = new List<ClientVehicleVM>();
            var listCV = (from x in _vssDb.ClientVehicles
                       where x.ClientId == id
                       select new ClientVehicleVM
                       {
                           Id = x.Id,
                           VehicleNo = x.VehicleNo,
                           Model = x.Model,
                           Vin = x.Vin,
                           ClientId = x.ClientId,
                       }).ToList();
            return listCV;
        }

        public List<VehicleStda110UVm> GetManufacturer(string manufacturer, int offset = 0,int fetch = 20)
        {
            var listManufacturer = _vssDb.Database.SqlQuery<VehicleStda110UVm>(@"SELECT DISTINCT Manufacturer 
            FROM VehicleStda110U 
            WHERE Manufacturer IS NOT NULL AND Manufacturer <> ''
            AND Manufacturer LIKE '" + manufacturer + @"%'
            ORDER BY Manufacturer
            OFFSET " + offset + @" ROWS 
            FETCH NEXT " + fetch + " ROWS ONLY;").ToList();
            return listManufacturer;
        }

        public List<VehicleStda110UVm> GetModel(string manufacturer, string model, int offset = 0, int fetch = 20)
        {
            var listManufacturer = _vssDb.Database.SqlQuery<VehicleStda110UVm>(@"SELECT DISTINCT Model 
            FROM VehicleStda110U 
            WHERE Model IS NOT NULL AND Model <> ''
            AND Manufacturer = '"+ manufacturer + @"' AND Model LIKE '"+ model + @"%'
            ORDER BY Model
            OFFSET " + offset + @" ROWS 
            FETCH NEXT " + fetch + " ROWS ONLY;").ToList();
            return listManufacturer;
        }

        public List<VehicleStda110UVm> GetSubModel(string manufacturer, string model, string subModel, int offset = 0, int fetch = 20)
        {
            var listManufacturer = _vssDb.Database.SqlQuery<VehicleStda110UVm>(@"SELECT DISTINCT SubModel
            FROM VehicleStda110U 
            WHERE SubModel IS NOT NULL AND SubModel <> ''
            AND Manufacturer = '" + manufacturer + @"' AND Model='" + model + @"' AND SubModel LIKE '"+ subModel + @"%'
            ORDER BY SubModel
            OFFSET " + offset + @" ROWS 
            FETCH NEXT " + fetch + " ROWS ONLY;").ToList();
            return listManufacturer;
        }

        public List<VehicleStda110UVm> GetFrom(string manufacturer, string model, string subModel, string from, int offset = 0, int fetch = 20)
        {
            var listManufacturer = _vssDb.Database.SqlQuery<VehicleStda110UVm>(@"SELECT DISTINCT [From]
            FROM VehicleStda110U 
            WHERE SubModel IS NOT NULL AND SubModel <> ''
            AND Manufacturer = '" + manufacturer + @"' AND Model='" + model + @"' AND SubModel = '" + subModel + @"' AND [From] LIKE '" + from + @"%'
            ORDER BY [From]
            OFFSET " + offset + @" ROWS 
            FETCH NEXT " + fetch + " ROWS ONLY;").ToList();
            return listManufacturer;
        }

        public List<VehicleStda110UVm> GetTo(string manufacturer, string model, string subModel, string from, string to, int offset = 0, int fetch = 20)
        {
            var listManufacturer = _vssDb.Database.SqlQuery<VehicleStda110UVm>(@"SELECT DISTINCT [To]
            FROM VehicleStda110U 
            WHERE SubModel IS NOT NULL AND SubModel <> ''
            AND Manufacturer = '" + manufacturer + @"' AND Model='" + model + @"' AND SubModel = '" + subModel + @"' AND [From] = '" + from + @"' AND [To] LIKE '" + to + @"%'
            ORDER BY [To]
            OFFSET " + offset + @" ROWS 
            FETCH NEXT " + fetch + " ROWS ONLY;").ToList();
            return listManufacturer;
        }

    }
}