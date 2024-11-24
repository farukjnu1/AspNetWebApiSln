using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Web;
using AspNetWebApi.DA.EF.VssDb;
using AspNetWebApi.DA.ViewModels.System;
using AspNetWebApi.DA.ViewModels.Operation;

namespace AspNetWebApi.BL.System
{
    public class CompanyBL
    {
        ModelVssDb _vssDb = new ModelVssDb();

        public IEnumerable<CompanyVM> GetCompany()
        {
            //oClient = new ClientVM();
            //var listClient = _vssDb.BusinessPartners
            //    .Where(x => x.BpTypeId == 1)
            //    .ToList();
            var listCompanies = from x in _vssDb.Companies
                             select new CompanyVM
                             {
                                 CompanyId = x.CompanyId,
                                 CompanyCode = x.CompanyCode,
                                 CompanyName = x.CompanyName,
                                 Description = x.Description,
                                 DateFormat = x.DateFormat,
                                 DecimalPlace = x.DecimalPlace,
                                 Bay = x.Bay,
                                 Vat = x.Vat,
                                 Address = x.Address,
                                 Email = x.Email,
                                 Phone = x.Phone,
                                 IsActive = x.IsActive,
                                 Website = x.Website,
                             };
            return listCompanies;
        }

        public CompanyVM Get(int CompanyId)
        {
            _vssDb = new ModelVssDb();
            var oCompany = _vssDb.Companies.Where(y => y.CompanyId == CompanyId)
                .Select(x => new CompanyVM
                {
                    Address = x.Address == null ? "" : x.Address,
                    Bay = x.Bay,
                    CompanyCode = x.CompanyCode,
                    CompanyName = x.CompanyName,
                    CompanyId = x.CompanyId,
                    DateFormat = x.DateFormat,
                    DecimalPlace = x.DecimalPlace,
                    Description = x.Description,
                    Email = x.Email == null ? "" : x.Email,
                    IsActive = x.IsActive,
                    Phone = x.Phone == null ? "" : x.Phone,
                    Vat = x.Vat,
                    Website = x.Website,
                    Logos = (from z in _vssDb.CompanyLogoes
                             where z.CompanyId == x.CompanyId
                             select new CompanyLogoVM()
                             {
                                 CompanyId = x.CompanyId,
                                 LogoId = z.LogoId,
                                 LogoUrl = z.LogoUrl,
                                 Name = z.Name
                             }).ToList()
                }).FirstOrDefault();
            return oCompany;
        }

        public bool Update(Company model)
        {
            try
            {
                var oCompany = _vssDb.Companies
                 .Where(x => x.CompanyId == model.CompanyId)
                 .FirstOrDefault();
                if (oCompany != null)
                {
                    oCompany.CompanyId = model.CompanyId;
                    oCompany.CompanyName = model.CompanyName;
                    oCompany.Bay = model.Bay;
                    oCompany.Vat = model.Vat;
                    oCompany.DateFormat = model.DateFormat;
                    oCompany.DecimalPlace = model.DecimalPlace;
                    oCompany.Phone = model.Phone;
                    oCompany.Email = model.Email;
                    oCompany.Website = model.Website;
                    oCompany.Address = model.Address;
                    oCompany.Description = model.Description;

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
    }
}