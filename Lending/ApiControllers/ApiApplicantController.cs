﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Web;
using System.Data.Linq;

namespace Lending.ApiControllers
{
    public class ApiApplicantController : ApiController
    {
        // data
        private Data.LendingDataContext db = new Data.LendingDataContext();

        // applicant list
        [Authorize]
        [HttpGet]
        [Route("api/applicant/list/{applicantType}")]
        public List<Models.MstApplicant> listApplicant(String applicantType)
        {
            if (applicantType.Equals("Applicant"))
            {
                var applicants = from d in db.mstApplicants.OrderByDescending(d => d.Id)
                                 where d.IsCoMaker == false
                                 && d.IsBlocked == false
                                 select new Models.MstApplicant
                                 {
                                     Id = d.Id,
                                     ApplicantNumber = d.ApplicantNumber,
                                     IsCoMaker = d.IsCoMaker,
                                     AreaId = d.AreaId,
                                     Area = d.mstArea.Area,
                                     ApplicantFullName = d.ApplicantLastName + ", " + d.ApplicantFirstName + " " + (d.ApplicantMiddleName != null ? d.ApplicantMiddleName : " "),
                                     ApplicantLastName = d.ApplicantLastName,
                                     ApplicantFirstName = d.ApplicantFirstName,
                                     ApplicantMiddleName = d.ApplicantMiddleName != null ? d.ApplicantMiddleName : " ",
                                     BirthDate = d.BirthDate.ToShortDateString(),
                                     CivilStatusId = d.CivilStatusId,
                                     CivilStatus = d.sysCivilStatus.CivilStatus,
                                     CityAddress = d.CityAddress,
                                     ProvinceAddress = d.ProvinceAddress,
                                     ContactNumber = d.ContactNumber,
                                     ResidenceTypeId = d.ResidenceTypeId,
                                     ResidenceType = d.sysResidenceType.ResidenceType,
                                     ResidenceMonthlyRentAmount = d.ResidenceMonthlyRentAmount,
                                     LandResidenceTypeId = d.LandResidenceTypeId,
                                     LandResidenceType = d.sysResidenceType1.ResidenceType,
                                     LandResidenceMonthlyRentAmount = d.LandResidenceMonthlyRentAmount,
                                     LengthOfStay = d.LengthOfStay,
                                     BusinessAddress = d.BusinessAddress,
                                     BusinessKaratulaName = d.BusinessKaratulaName,
                                     BusinessTelephoneNumber = d.BusinessTelephoneNumber,
                                     BusinessYear = d.BusinessYear,
                                     BusinessMerchandise = d.BusinessMerchandise,
                                     BusinessStockValues = d.BusinessStockValues,
                                     BusinessBeginningCapital = d.BusinessBeginningCapital,
                                     BusinessLowSalesPeriod = d.BusinessLowSalesPeriod,
                                     BusinessLowestDailySales = d.BusinessLowestDailySales,
                                     BusinessAverageDailySales = d.BusinessAverageDailySales,
                                     EmployedCompany = d.EmployedCompany,
                                     EmployedCompanyAddress = d.EmployedCompanyAddress,
                                     EmployedPositionOccupied = d.EmployedPositionOccupied,
                                     EmployedServiceLength = d.EmployedServiceLength,
                                     EmployedTelephoneNumber = d.EmployedTelephoneNumber,
                                     SpouseFullName = d.SpouseFullName,
                                     SpouseEmployerBusiness = d.SpouseEmployerBusiness,
                                     SpouseEmployerBusinessAddress = d.SpouseEmployerBusinessAddress,
                                     SpouseBusinessTelephoneNumber = d.SpouseBusinessTelephoneNumber,
                                     SpousePositionOccupied = d.SpousePositionOccupied,
                                     SpouseMonthlySalary = d.SpouseMonthlySalary,
                                     SpouseLengthOfService = d.SpouseLengthOfService,
                                     NumberOfChildren = d.NumberOfChildren,
                                     Studying = d.Studying,
                                     Schools = d.Schools,
                                     IsBlocked = d.IsBlocked,
                                     IsLocked = d.IsLocked,
                                     CreatedByUserId = d.CreatedByUserId,
                                     CreatedByUser = d.mstUser.FullName,
                                     CreatedDateTime = d.CreatedDateTime.ToShortDateString(),
                                     UpdatedByUserId = d.UpdatedByUserId,
                                     UpdatedByUser = d.mstUser1.FullName,
                                     UpdatedDateTime = d.UpdatedDateTime.ToShortDateString()
                                 };

                return applicants.ToList();
            }
            else
            {
                if (applicantType.Equals("Co-Maker"))
                {
                    var applicants = from d in db.mstApplicants.OrderByDescending(d => d.Id)
                                     where d.IsCoMaker == true
                                     && d.IsBlocked == false
                                     select new Models.MstApplicant
                                     {
                                         Id = d.Id,
                                         ApplicantNumber = d.ApplicantNumber,
                                         IsCoMaker = d.IsCoMaker,
                                         AreaId = d.AreaId,
                                         Area = d.mstArea.Area,
                                         ApplicantFullName = d.ApplicantLastName + ", " + d.ApplicantFirstName + " " + (d.ApplicantMiddleName != null ? d.ApplicantMiddleName : " "),
                                         ApplicantLastName = d.ApplicantLastName,
                                         ApplicantFirstName = d.ApplicantFirstName,
                                         ApplicantMiddleName = d.ApplicantMiddleName != null ? d.ApplicantMiddleName : " ",
                                         BirthDate = d.BirthDate.ToShortDateString(),
                                         CivilStatusId = d.CivilStatusId,
                                         CivilStatus = d.sysCivilStatus.CivilStatus,
                                         CityAddress = d.CityAddress,
                                         ProvinceAddress = d.ProvinceAddress,
                                         ContactNumber = d.ContactNumber,
                                         ResidenceTypeId = d.ResidenceTypeId,
                                         ResidenceType = d.sysResidenceType.ResidenceType,
                                         ResidenceMonthlyRentAmount = d.ResidenceMonthlyRentAmount,
                                         LandResidenceTypeId = d.LandResidenceTypeId,
                                         LandResidenceType = d.sysResidenceType1.ResidenceType,
                                         LandResidenceMonthlyRentAmount = d.LandResidenceMonthlyRentAmount,
                                         LengthOfStay = d.LengthOfStay,
                                         BusinessAddress = d.BusinessAddress,
                                         BusinessKaratulaName = d.BusinessKaratulaName,
                                         BusinessTelephoneNumber = d.BusinessTelephoneNumber,
                                         BusinessYear = d.BusinessYear,
                                         BusinessMerchandise = d.BusinessMerchandise,
                                         BusinessStockValues = d.BusinessStockValues,
                                         BusinessBeginningCapital = d.BusinessBeginningCapital,
                                         BusinessLowSalesPeriod = d.BusinessLowSalesPeriod,
                                         BusinessLowestDailySales = d.BusinessLowestDailySales,
                                         BusinessAverageDailySales = d.BusinessAverageDailySales,
                                         EmployedCompany = d.EmployedCompany,
                                         EmployedCompanyAddress = d.EmployedCompanyAddress,
                                         EmployedPositionOccupied = d.EmployedPositionOccupied,
                                         EmployedServiceLength = d.EmployedServiceLength,
                                         EmployedTelephoneNumber = d.EmployedTelephoneNumber,
                                         SpouseFullName = d.SpouseFullName,
                                         SpouseEmployerBusiness = d.SpouseEmployerBusiness,
                                         SpouseEmployerBusinessAddress = d.SpouseEmployerBusinessAddress,
                                         SpouseBusinessTelephoneNumber = d.SpouseBusinessTelephoneNumber,
                                         SpousePositionOccupied = d.SpousePositionOccupied,
                                         SpouseMonthlySalary = d.SpouseMonthlySalary,
                                         SpouseLengthOfService = d.SpouseLengthOfService,
                                         NumberOfChildren = d.NumberOfChildren,
                                         Studying = d.Studying,
                                         Schools = d.Schools,
                                         IsBlocked = d.IsBlocked,
                                         IsLocked = d.IsLocked,
                                         CreatedByUserId = d.CreatedByUserId,
                                         CreatedByUser = d.mstUser.FullName,
                                         CreatedDateTime = d.CreatedDateTime.ToShortDateString(),
                                         UpdatedByUserId = d.UpdatedByUserId,
                                         UpdatedByUser = d.mstUser1.FullName,
                                         UpdatedDateTime = d.UpdatedDateTime.ToShortDateString()
                                     };

                    return applicants.ToList();
                }
                else
                {
                    if (applicantType.Equals("Blocked Applicant"))
                    {
                        var applicants = from d in db.mstApplicants.OrderByDescending(d => d.Id)
                                         where d.IsCoMaker == false
                                         && d.IsBlocked == true
                                         select new Models.MstApplicant
                                         {
                                             Id = d.Id,
                                             ApplicantNumber = d.ApplicantNumber,
                                             IsCoMaker = d.IsCoMaker,
                                             AreaId = d.AreaId,
                                             Area = d.mstArea.Area,
                                             ApplicantFullName = d.ApplicantLastName + ", " + d.ApplicantFirstName + " " + (d.ApplicantMiddleName != null ? d.ApplicantMiddleName : " "),
                                             ApplicantLastName = d.ApplicantLastName,
                                             ApplicantFirstName = d.ApplicantFirstName,
                                             ApplicantMiddleName = d.ApplicantMiddleName != null ? d.ApplicantMiddleName : " ",
                                             BirthDate = d.BirthDate.ToShortDateString(),
                                             CivilStatusId = d.CivilStatusId,
                                             CivilStatus = d.sysCivilStatus.CivilStatus,
                                             CityAddress = d.CityAddress,
                                             ProvinceAddress = d.ProvinceAddress,
                                             ContactNumber = d.ContactNumber,
                                             ResidenceTypeId = d.ResidenceTypeId,
                                             ResidenceType = d.sysResidenceType.ResidenceType,
                                             ResidenceMonthlyRentAmount = d.ResidenceMonthlyRentAmount,
                                             LandResidenceTypeId = d.LandResidenceTypeId,
                                             LandResidenceType = d.sysResidenceType1.ResidenceType,
                                             LandResidenceMonthlyRentAmount = d.LandResidenceMonthlyRentAmount,
                                             LengthOfStay = d.LengthOfStay,
                                             BusinessAddress = d.BusinessAddress,
                                             BusinessKaratulaName = d.BusinessKaratulaName,
                                             BusinessTelephoneNumber = d.BusinessTelephoneNumber,
                                             BusinessYear = d.BusinessYear,
                                             BusinessMerchandise = d.BusinessMerchandise,
                                             BusinessStockValues = d.BusinessStockValues,
                                             BusinessBeginningCapital = d.BusinessBeginningCapital,
                                             BusinessLowSalesPeriod = d.BusinessLowSalesPeriod,
                                             BusinessLowestDailySales = d.BusinessLowestDailySales,
                                             BusinessAverageDailySales = d.BusinessAverageDailySales,
                                             EmployedCompany = d.EmployedCompany,
                                             EmployedCompanyAddress = d.EmployedCompanyAddress,
                                             EmployedPositionOccupied = d.EmployedPositionOccupied,
                                             EmployedServiceLength = d.EmployedServiceLength,
                                             EmployedTelephoneNumber = d.EmployedTelephoneNumber,
                                             SpouseFullName = d.SpouseFullName,
                                             SpouseEmployerBusiness = d.SpouseEmployerBusiness,
                                             SpouseEmployerBusinessAddress = d.SpouseEmployerBusinessAddress,
                                             SpouseBusinessTelephoneNumber = d.SpouseBusinessTelephoneNumber,
                                             SpousePositionOccupied = d.SpousePositionOccupied,
                                             SpouseMonthlySalary = d.SpouseMonthlySalary,
                                             SpouseLengthOfService = d.SpouseLengthOfService,
                                             NumberOfChildren = d.NumberOfChildren,
                                             Studying = d.Studying,
                                             Schools = d.Schools,
                                             IsBlocked = d.IsBlocked,
                                             IsLocked = d.IsLocked,
                                             CreatedByUserId = d.CreatedByUserId,
                                             CreatedByUser = d.mstUser.FullName,
                                             CreatedDateTime = d.CreatedDateTime.ToShortDateString(),
                                             UpdatedByUserId = d.UpdatedByUserId,
                                             UpdatedByUser = d.mstUser1.FullName,
                                             UpdatedDateTime = d.UpdatedDateTime.ToShortDateString()
                                         };

                        return applicants.ToList();
                    }
                    else
                    {
                        if (applicantType.Equals("Blocked Co-Maker"))
                        {
                            var applicants = from d in db.mstApplicants.OrderByDescending(d => d.Id)
                                             where d.IsCoMaker == true
                                             && d.IsBlocked == true
                                             select new Models.MstApplicant
                                             {
                                                 Id = d.Id,
                                                 ApplicantNumber = d.ApplicantNumber,
                                                 IsCoMaker = d.IsCoMaker,
                                                 AreaId = d.AreaId,
                                                 Area = d.mstArea.Area,
                                                 ApplicantFullName = d.ApplicantLastName + ", " + d.ApplicantFirstName + " " + (d.ApplicantMiddleName != null ? d.ApplicantMiddleName : " "),
                                                 ApplicantLastName = d.ApplicantLastName,
                                                 ApplicantFirstName = d.ApplicantFirstName,
                                                 ApplicantMiddleName = d.ApplicantMiddleName != null ? d.ApplicantMiddleName : " ",
                                                 BirthDate = d.BirthDate.ToShortDateString(),
                                                 CivilStatusId = d.CivilStatusId,
                                                 CivilStatus = d.sysCivilStatus.CivilStatus,
                                                 CityAddress = d.CityAddress,
                                                 ProvinceAddress = d.ProvinceAddress,
                                                 ContactNumber = d.ContactNumber,
                                                 ResidenceTypeId = d.ResidenceTypeId,
                                                 ResidenceType = d.sysResidenceType.ResidenceType,
                                                 ResidenceMonthlyRentAmount = d.ResidenceMonthlyRentAmount,
                                                 LandResidenceTypeId = d.LandResidenceTypeId,
                                                 LandResidenceType = d.sysResidenceType1.ResidenceType,
                                                 LandResidenceMonthlyRentAmount = d.LandResidenceMonthlyRentAmount,
                                                 LengthOfStay = d.LengthOfStay,
                                                 BusinessAddress = d.BusinessAddress,
                                                 BusinessKaratulaName = d.BusinessKaratulaName,
                                                 BusinessTelephoneNumber = d.BusinessTelephoneNumber,
                                                 BusinessYear = d.BusinessYear,
                                                 BusinessMerchandise = d.BusinessMerchandise,
                                                 BusinessStockValues = d.BusinessStockValues,
                                                 BusinessBeginningCapital = d.BusinessBeginningCapital,
                                                 BusinessLowSalesPeriod = d.BusinessLowSalesPeriod,
                                                 BusinessLowestDailySales = d.BusinessLowestDailySales,
                                                 BusinessAverageDailySales = d.BusinessAverageDailySales,
                                                 EmployedCompany = d.EmployedCompany,
                                                 EmployedCompanyAddress = d.EmployedCompanyAddress,
                                                 EmployedPositionOccupied = d.EmployedPositionOccupied,
                                                 EmployedServiceLength = d.EmployedServiceLength,
                                                 EmployedTelephoneNumber = d.EmployedTelephoneNumber,
                                                 SpouseFullName = d.SpouseFullName,
                                                 SpouseEmployerBusiness = d.SpouseEmployerBusiness,
                                                 SpouseEmployerBusinessAddress = d.SpouseEmployerBusinessAddress,
                                                 SpouseBusinessTelephoneNumber = d.SpouseBusinessTelephoneNumber,
                                                 SpousePositionOccupied = d.SpousePositionOccupied,
                                                 SpouseMonthlySalary = d.SpouseMonthlySalary,
                                                 SpouseLengthOfService = d.SpouseLengthOfService,
                                                 NumberOfChildren = d.NumberOfChildren,
                                                 Studying = d.Studying,
                                                 Schools = d.Schools,
                                                 IsBlocked = d.IsBlocked,
                                                 IsLocked = d.IsLocked,
                                                 CreatedByUserId = d.CreatedByUserId,
                                                 CreatedByUser = d.mstUser.FullName,
                                                 CreatedDateTime = d.CreatedDateTime.ToShortDateString(),
                                                 UpdatedByUserId = d.UpdatedByUserId,
                                                 UpdatedByUser = d.mstUser1.FullName,
                                                 UpdatedDateTime = d.UpdatedDateTime.ToShortDateString()
                                             };

                            return applicants.ToList();
                        }
                        else
                        {
                            if (applicantType.Equals("loanApplicant"))
                            {
                                var applicants = from d in db.mstApplicants.OrderByDescending(d => d.Id)
                                                 where d.IsCoMaker == false
                                                 select new Models.MstApplicant
                                                 {
                                                     Id = d.Id,
                                                     ApplicantNumber = d.ApplicantNumber,
                                                     IsCoMaker = d.IsCoMaker,
                                                     AreaId = d.AreaId,
                                                     Area = d.mstArea.Area,
                                                     ApplicantFullName = d.ApplicantLastName + ", " + d.ApplicantFirstName + " " + (d.ApplicantMiddleName != null ? d.ApplicantMiddleName : " "),
                                                     ApplicantLastName = d.ApplicantLastName,
                                                     ApplicantFirstName = d.ApplicantFirstName,
                                                     ApplicantMiddleName = d.ApplicantMiddleName != null ? d.ApplicantMiddleName : " ",
                                                     BirthDate = d.BirthDate.ToShortDateString(),
                                                     CivilStatusId = d.CivilStatusId,
                                                     CivilStatus = d.sysCivilStatus.CivilStatus,
                                                     CityAddress = d.CityAddress,
                                                     ProvinceAddress = d.ProvinceAddress,
                                                     ContactNumber = d.ContactNumber,
                                                     ResidenceTypeId = d.ResidenceTypeId,
                                                     ResidenceType = d.sysResidenceType.ResidenceType,
                                                     ResidenceMonthlyRentAmount = d.ResidenceMonthlyRentAmount,
                                                     LandResidenceTypeId = d.LandResidenceTypeId,
                                                     LandResidenceType = d.sysResidenceType1.ResidenceType,
                                                     LandResidenceMonthlyRentAmount = d.LandResidenceMonthlyRentAmount,
                                                     LengthOfStay = d.LengthOfStay,
                                                     BusinessAddress = d.BusinessAddress,
                                                     BusinessKaratulaName = d.BusinessKaratulaName,
                                                     BusinessTelephoneNumber = d.BusinessTelephoneNumber,
                                                     BusinessYear = d.BusinessYear,
                                                     BusinessMerchandise = d.BusinessMerchandise,
                                                     BusinessStockValues = d.BusinessStockValues,
                                                     BusinessBeginningCapital = d.BusinessBeginningCapital,
                                                     BusinessLowSalesPeriod = d.BusinessLowSalesPeriod,
                                                     BusinessLowestDailySales = d.BusinessLowestDailySales,
                                                     BusinessAverageDailySales = d.BusinessAverageDailySales,
                                                     EmployedCompany = d.EmployedCompany,
                                                     EmployedCompanyAddress = d.EmployedCompanyAddress,
                                                     EmployedPositionOccupied = d.EmployedPositionOccupied,
                                                     EmployedServiceLength = d.EmployedServiceLength,
                                                     EmployedTelephoneNumber = d.EmployedTelephoneNumber,
                                                     SpouseFullName = d.SpouseFullName,
                                                     SpouseEmployerBusiness = d.SpouseEmployerBusiness,
                                                     SpouseEmployerBusinessAddress = d.SpouseEmployerBusinessAddress,
                                                     SpouseBusinessTelephoneNumber = d.SpouseBusinessTelephoneNumber,
                                                     SpousePositionOccupied = d.SpousePositionOccupied,
                                                     SpouseMonthlySalary = d.SpouseMonthlySalary,
                                                     SpouseLengthOfService = d.SpouseLengthOfService,
                                                     NumberOfChildren = d.NumberOfChildren,
                                                     Studying = d.Studying,
                                                     Schools = d.Schools,
                                                     IsBlocked = d.IsBlocked,
                                                     IsLocked = d.IsLocked,
                                                     CreatedByUserId = d.CreatedByUserId,
                                                     CreatedByUser = d.mstUser.FullName,
                                                     CreatedDateTime = d.CreatedDateTime.ToShortDateString(),
                                                     UpdatedByUserId = d.UpdatedByUserId,
                                                     UpdatedByUser = d.mstUser1.FullName,
                                                     UpdatedDateTime = d.UpdatedDateTime.ToShortDateString()
                                                 };

                                return applicants.ToList();
                            }
                            else
                            {
                                return null;
                            };
                        }
                    }
                }
            }
        }

        // get applicant
        [Authorize]
        [HttpGet]
        [Route("api/applicant/getById/{id}")]
        public Models.MstApplicant getApplicantById(String id)
        {
            var applicants = from d in db.mstApplicants
                             where d.Id == Convert.ToInt32(id)
                             select new Models.MstApplicant
                             {
                                 Id = d.Id,
                                 ApplicantNumber = d.ApplicantNumber,
                                 IsCoMaker = d.IsCoMaker,
                                 Photo = d.Photo.ToArray(),
                                 AreaId = d.AreaId,
                                 Area = d.mstArea.Area,
                                 ApplicantFullName = d.ApplicantLastName + ", " + d.ApplicantFirstName + " " + (d.ApplicantMiddleName != null ? d.ApplicantMiddleName : " "),
                                 ApplicantLastName = d.ApplicantLastName,
                                 ApplicantFirstName = d.ApplicantFirstName,
                                 ApplicantMiddleName = d.ApplicantMiddleName,
                                 BirthDate = d.BirthDate.ToShortDateString(),
                                 CivilStatusId = d.CivilStatusId,
                                 CivilStatus = d.sysCivilStatus.CivilStatus,
                                 CityAddress = d.CityAddress,
                                 ProvinceAddress = d.ProvinceAddress,
                                 ContactNumber = d.ContactNumber,
                                 ResidenceTypeId = d.ResidenceTypeId,
                                 ResidenceType = d.sysResidenceType.ResidenceType,
                                 ResidenceMonthlyRentAmount = d.ResidenceMonthlyRentAmount,
                                 LandResidenceTypeId = d.LandResidenceTypeId,
                                 LandResidenceType = d.sysResidenceType1.ResidenceType,
                                 LandResidenceMonthlyRentAmount = d.LandResidenceMonthlyRentAmount,
                                 LengthOfStay = d.LengthOfStay,
                                 BusinessAddress = d.BusinessAddress,
                                 BusinessKaratulaName = d.BusinessKaratulaName,
                                 BusinessTelephoneNumber = d.BusinessTelephoneNumber,
                                 BusinessYear = d.BusinessYear,
                                 BusinessMerchandise = d.BusinessMerchandise,
                                 BusinessStockValues = d.BusinessStockValues,
                                 BusinessBeginningCapital = d.BusinessBeginningCapital,
                                 BusinessLowSalesPeriod = d.BusinessLowSalesPeriod,
                                 BusinessLowestDailySales = d.BusinessLowestDailySales,
                                 BusinessAverageDailySales = d.BusinessAverageDailySales,
                                 EmployedCompany = d.EmployedCompany,
                                 EmployedCompanyAddress = d.EmployedCompanyAddress,
                                 EmployedPositionOccupied = d.EmployedPositionOccupied,
                                 EmployedServiceLength = d.EmployedServiceLength,
                                 EmployedTelephoneNumber = d.EmployedTelephoneNumber,
                                 SpouseFullName = d.SpouseFullName,
                                 SpouseEmployerBusiness = d.SpouseEmployerBusiness,
                                 SpouseEmployerBusinessAddress = d.SpouseEmployerBusinessAddress,
                                 SpouseBusinessTelephoneNumber = d.SpouseBusinessTelephoneNumber,
                                 SpousePositionOccupied = d.SpousePositionOccupied,
                                 SpouseMonthlySalary = d.SpouseMonthlySalary,
                                 SpouseLengthOfService = d.SpouseLengthOfService,
                                 NumberOfChildren = d.NumberOfChildren,
                                 Studying = d.Studying,
                                 Schools = d.Schools,
                                 IsBlocked = d.IsBlocked,
                                 IsLocked = d.IsLocked,
                                 CreatedByUserId = d.CreatedByUserId,
                                 CreatedByUser = d.mstUser.FullName,
                                 CreatedDateTime = d.CreatedDateTime.ToShortDateString(),
                                 UpdatedByUserId = d.UpdatedByUserId,
                                 UpdatedByUser = d.mstUser1.FullName,
                                 UpdatedDateTime = d.UpdatedDateTime.ToShortDateString(),
                                 PhotoSignature = d.PhotoSignature.ToArray()
                             };

            return (Models.MstApplicant)applicants.FirstOrDefault();
        }

        // zero fill
        public String zeroFill(Int32 number, Int32 length)
        {
            var result = number.ToString();
            var pad = length - result.Length;
            while (pad > 0)
            {
                result = "0" + result;
                pad--;
            }

            return result;
        }

        // add applicant 
        [Authorize]
        [HttpPost]
        [Route("api/applicant/add")]
        public Int32 addApplicant()
        {
            try
            {
                var userId = (from d in db.mstUsers where d.AspUserId == User.Identity.GetUserId() select d.Id).SingleOrDefault();
                var mstUserForms = from d in db.mstUserForms
                                   where d.UserId == userId
                                   select new Models.MstUserForm
                                   {
                                       Id = d.Id,
                                       Form = d.sysForm.Form,
                                       CanPerformActions = d.CanPerformActions
                                   };

                if (mstUserForms.Any())
                {
                    String matchPageString = "ApplicantList";
                    Boolean canPerformActions = false;

                    foreach (var mstUserForm in mstUserForms)
                    {
                        if (mstUserForm.Form.Equals(matchPageString))
                        {
                            if (mstUserForm.CanPerformActions)
                            {
                                canPerformActions = true;
                            }

                            break;
                        }
                    }

                    if (canPerformActions)
                    {
                        Data.mstApplicant newApplicant = new Data.mstApplicant();

                        string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Images\applicantPhotoPlaceHolder.png");

                        Byte[] bytes = File.ReadAllBytes(HttpContext.Current.Server.MapPath("~/Images/applicantPhotoPlaceHolder.png"));
                        String file = Convert.ToBase64String(bytes);
                        byte[] imgarr = Convert.FromBase64String(file);


                        string pathSignature = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Images\applicantPhotoPlaceHolder.png");

                        Byte[] bytesSignature = File.ReadAllBytes(HttpContext.Current.Server.MapPath("~/Images/signature.png"));
                        String fileSignature = Convert.ToBase64String(bytesSignature);
                        byte[] imgarrSignature = Convert.FromBase64String(fileSignature);

                        String applicantNumber = "0000000001";
                        var applicants = from d in db.mstApplicants.OrderByDescending(d => d.Id) select d;
                        if (applicants.Any())
                        {
                            var newApplicantNumber = Convert.ToInt32(applicants.FirstOrDefault().ApplicantNumber) + 0000000001;
                            applicantNumber = newApplicantNumber.ToString();
                        }

                        newApplicant.ApplicantNumber = zeroFill(Convert.ToInt32(applicantNumber), 10);
                        newApplicant.IsCoMaker = false;
                        newApplicant.Photo = imgarr;
                        newApplicant.AreaId = (from d in db.mstAreas select d.Id).FirstOrDefault();
                        newApplicant.ApplicantLastName = "NA";
                        newApplicant.ApplicantFirstName = "NA";
                        newApplicant.ApplicantMiddleName = null;
                        newApplicant.BirthDate = DateTime.Today;
                        newApplicant.CivilStatusId = (from d in db.sysCivilStatus select d.Id).FirstOrDefault();
                        newApplicant.CityAddress = "NA";
                        newApplicant.ProvinceAddress = "NA";
                        newApplicant.ContactNumber = "NA";
                        newApplicant.ResidenceTypeId = (from d in db.sysResidenceTypes select d.Id).FirstOrDefault();
                        newApplicant.ResidenceMonthlyRentAmount = 0;
                        newApplicant.LandResidenceTypeId = (from d in db.sysResidenceTypes select d.Id).FirstOrDefault();
                        newApplicant.LandResidenceMonthlyRentAmount = 0;
                        newApplicant.LengthOfStay = "NA";
                        newApplicant.BusinessAddress = "NA";
                        newApplicant.BusinessKaratulaName = "NA";
                        newApplicant.BusinessTelephoneNumber = "NA";
                        newApplicant.BusinessYear = "NA";
                        newApplicant.BusinessMerchandise = "NA";
                        newApplicant.BusinessStockValues = 0;
                        newApplicant.BusinessBeginningCapital = 0;
                        newApplicant.BusinessLowSalesPeriod = "NA";
                        newApplicant.BusinessLowestDailySales = 0;
                        newApplicant.BusinessAverageDailySales = 0;
                        newApplicant.EmployedCompany = "NA";
                        newApplicant.EmployedCompanyAddress = "NA";
                        newApplicant.EmployedPositionOccupied = "NA";
                        newApplicant.EmployedServiceLength = "NA";
                        newApplicant.EmployedTelephoneNumber = "NA";
                        newApplicant.SpouseFullName = "NA";
                        newApplicant.SpouseEmployerBusiness = "NA";
                        newApplicant.SpouseEmployerBusinessAddress = "NA";
                        newApplicant.SpouseBusinessTelephoneNumber = "NA";
                        newApplicant.SpousePositionOccupied = "NA";
                        newApplicant.SpouseMonthlySalary = 0;
                        newApplicant.SpouseLengthOfService = "NA";
                        newApplicant.NumberOfChildren = "NA";
                        newApplicant.Studying = "NA";
                        newApplicant.Schools = "NA";
                        newApplicant.IsBlocked = false;
                        newApplicant.IsLocked = false;
                        newApplicant.CreatedByUserId = userId;
                        newApplicant.CreatedDateTime = DateTime.Now;
                        newApplicant.UpdatedByUserId = userId;
                        newApplicant.UpdatedDateTime = DateTime.Now;
                        newApplicant.PhotoSignature = imgarrSignature;

                        db.mstApplicants.InsertOnSubmit(newApplicant);
                        db.SubmitChanges();

                        return newApplicant.Id;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return 0;
                }
            }
            catch
            {
                return 0;
            }
        }

        // lock applicant
        [Authorize]
        [HttpPut]
        [Route("api/applicant/lock/{id}")]
        public HttpResponseMessage lockApplicant(String id, Models.MstApplicant applicant)
        {
            try
            {
                var applicants = from d in db.mstApplicants where d.Id == Convert.ToInt32(id) select d;
                if (applicants.Any())
                {
                    if (!applicants.FirstOrDefault().IsLocked)
                    {
                        var userId = (from d in db.mstUsers where d.AspUserId == User.Identity.GetUserId() select d.Id).SingleOrDefault();
                        var mstUserForms = from d in db.mstUserForms
                                           where d.UserId == userId
                                           select new Models.MstUserForm
                                           {
                                               Id = d.Id,
                                               Form = d.sysForm.Form,
                                               CanPerformActions = d.CanPerformActions
                                           };

                        if (mstUserForms.Any())
                        {
                            String matchPageString = "ApplicantDetail";
                            Boolean canPerformActions = false;

                            foreach (var mstUserForm in mstUserForms)
                            {
                                if (mstUserForm.Form.Equals(matchPageString))
                                {
                                    if (mstUserForm.CanPerformActions)
                                    {
                                        canPerformActions = true;
                                    }

                                    break;
                                }
                            }

                            if (canPerformActions)
                            {
                                var lockApplicant = applicants.FirstOrDefault();
                                lockApplicant.IsLocked = true;
                                lockApplicant.UpdatedByUserId = userId;
                                lockApplicant.UpdatedDateTime = DateTime.Now;
                                db.SubmitChanges();

                                return Request.CreateResponse(HttpStatusCode.OK);
                            }
                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.BadRequest);
                            }
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.BadRequest);
                        }
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest);
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // unlock applicant
        [Authorize]
        [HttpPut]
        [Route("api/applicant/unlock/{id}")]
        public HttpResponseMessage unlockApplicant(String id, Models.MstApplicant applicant)
        {
            try
            {
                var applicants = from d in db.mstApplicants where d.Id == Convert.ToInt32(id) select d;
                if (applicants.Any())
                {
                    if (applicants.FirstOrDefault().IsLocked)
                    {
                        var userId = (from d in db.mstUsers where d.AspUserId == User.Identity.GetUserId() select d.Id).SingleOrDefault();
                        var mstUserForms = from d in db.mstUserForms
                                           where d.UserId == userId
                                           select new Models.MstUserForm
                                           {
                                               Id = d.Id,
                                               Form = d.sysForm.Form,
                                               CanPerformActions = d.CanPerformActions
                                           };

                        if (mstUserForms.Any())
                        {
                            String matchPageString = "ApplicantDetail";
                            Boolean canPerformActions = false;

                            foreach (var mstUserForm in mstUserForms)
                            {
                                if (mstUserForm.Form.Equals(matchPageString))
                                {
                                    if (mstUserForm.CanPerformActions)
                                    {
                                        canPerformActions = true;
                                    }

                                    break;
                                }
                            }

                            if (canPerformActions)
                            {
                                var unlockApplicant = applicants.FirstOrDefault();
                                unlockApplicant.IsLocked = false;
                                unlockApplicant.UpdatedByUserId = userId;
                                unlockApplicant.UpdatedDateTime = DateTime.Now;
                                db.SubmitChanges();

                                return Request.CreateResponse(HttpStatusCode.OK);
                            }
                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.BadRequest);
                            }
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.BadRequest);
                        }
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest);
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // update applicant
        [Authorize]
        [HttpPut]
        [Route("api/applicant/update/{id}")]
        public HttpResponseMessage updateApplicant(String id, Models.MstApplicant applicant)
        {
            try
            {
                var applicants = from d in db.mstApplicants where d.Id == Convert.ToInt32(id) select d;
                if (applicants.Any())
                {
                    if (!applicants.FirstOrDefault().IsLocked)
                    {
                        var userId = (from d in db.mstUsers where d.AspUserId == User.Identity.GetUserId() select d.Id).SingleOrDefault();
                        var mstUserForms = from d in db.mstUserForms
                                           where d.UserId == userId
                                           select new Models.MstUserForm
                                           {
                                               Id = d.Id,
                                               Form = d.sysForm.Form,
                                               CanPerformActions = d.CanPerformActions
                                           };

                        if (mstUserForms.Any())
                        {
                            String matchPageString = "ApplicantDetail";
                            Boolean canPerformActions = false;

                            foreach (var mstUserForm in mstUserForms)
                            {
                                if (mstUserForm.Form.Equals(matchPageString))
                                {
                                    if (mstUserForm.CanPerformActions)
                                    {
                                        canPerformActions = true;
                                    }

                                    break;
                                }
                            }

                            if (canPerformActions)
                            {
                                var updateApplicant = applicants.FirstOrDefault();
                                updateApplicant.IsCoMaker = applicant.IsCoMaker;
                                updateApplicant.AreaId = applicant.AreaId;
                                updateApplicant.ApplicantLastName = applicant.ApplicantLastName;
                                updateApplicant.ApplicantFirstName = applicant.ApplicantFirstName;
                                updateApplicant.ApplicantMiddleName = applicant.ApplicantMiddleName;
                                updateApplicant.BirthDate = Convert.ToDateTime(applicant.BirthDate);
                                updateApplicant.CivilStatusId = applicant.CivilStatusId;
                                updateApplicant.CityAddress = applicant.CityAddress;
                                updateApplicant.ProvinceAddress = applicant.ProvinceAddress;
                                updateApplicant.ContactNumber = applicant.ContactNumber;
                                updateApplicant.ResidenceTypeId = applicant.ResidenceTypeId;
                                updateApplicant.ResidenceMonthlyRentAmount = applicant.ResidenceMonthlyRentAmount;
                                updateApplicant.LandResidenceTypeId = applicant.LandResidenceTypeId;
                                updateApplicant.LandResidenceMonthlyRentAmount = applicant.LandResidenceMonthlyRentAmount;
                                updateApplicant.LengthOfStay = applicant.LengthOfStay;
                                updateApplicant.BusinessAddress = applicant.BusinessAddress;
                                updateApplicant.BusinessKaratulaName = applicant.BusinessKaratulaName;
                                updateApplicant.BusinessTelephoneNumber = applicant.BusinessTelephoneNumber;
                                updateApplicant.BusinessYear = applicant.BusinessYear;
                                updateApplicant.BusinessMerchandise = applicant.BusinessMerchandise;
                                updateApplicant.BusinessStockValues = applicant.BusinessStockValues;
                                updateApplicant.BusinessBeginningCapital = applicant.BusinessBeginningCapital;
                                updateApplicant.BusinessLowSalesPeriod = applicant.BusinessLowSalesPeriod;
                                updateApplicant.BusinessLowestDailySales = applicant.BusinessLowestDailySales;
                                updateApplicant.BusinessAverageDailySales = applicant.BusinessAverageDailySales;
                                updateApplicant.EmployedCompany = applicant.EmployedCompany;
                                updateApplicant.EmployedCompanyAddress = applicant.EmployedCompanyAddress;
                                updateApplicant.EmployedPositionOccupied = applicant.EmployedPositionOccupied;
                                updateApplicant.EmployedServiceLength = applicant.EmployedServiceLength;
                                updateApplicant.EmployedTelephoneNumber = applicant.EmployedTelephoneNumber;
                                updateApplicant.SpouseFullName = applicant.SpouseFullName;
                                updateApplicant.SpouseEmployerBusiness = applicant.SpouseEmployerBusiness;
                                updateApplicant.SpouseEmployerBusinessAddress = applicant.SpouseEmployerBusinessAddress;
                                updateApplicant.SpouseBusinessTelephoneNumber = applicant.SpouseBusinessTelephoneNumber;
                                updateApplicant.SpousePositionOccupied = applicant.SpousePositionOccupied;
                                updateApplicant.SpouseMonthlySalary = applicant.SpouseMonthlySalary;
                                updateApplicant.SpouseLengthOfService = applicant.SpouseLengthOfService;
                                updateApplicant.NumberOfChildren = applicant.NumberOfChildren;
                                updateApplicant.Studying = applicant.Studying;
                                updateApplicant.Schools = applicant.Schools;
                                updateApplicant.UpdatedByUserId = userId;
                                updateApplicant.UpdatedDateTime = DateTime.Now;
                                db.SubmitChanges();

                                return Request.CreateResponse(HttpStatusCode.OK);
                            }
                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.BadRequest);
                            }
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.BadRequest);
                        }
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest);
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // update applicant photo
        [Authorize]
        [HttpPut]
        [Route("api/applicant/updatePhoto/{id}")]
        public HttpResponseMessage updateApplicantPhoto(String id, Models.MstApplicant applicant)
        {
            try
            {
                var applicants = from d in db.mstApplicants where d.Id == Convert.ToInt32(id) select d;
                if (applicants.Any())
                {
                    if (!applicants.FirstOrDefault().IsLocked)
                    {
                        var userId = (from d in db.mstUsers where d.AspUserId == User.Identity.GetUserId() select d.Id).SingleOrDefault();
                        var mstUserForms = from d in db.mstUserForms
                                           where d.UserId == userId
                                           select new Models.MstUserForm
                                           {
                                               Id = d.Id,
                                               Form = d.sysForm.Form,
                                               CanPerformActions = d.CanPerformActions
                                           };

                        if (mstUserForms.Any())
                        {
                            String matchPageString = "ApplicantDetail";
                            Boolean canPerformActions = false;

                            foreach (var mstUserForm in mstUserForms)
                            {
                                if (mstUserForm.Form.Equals(matchPageString))
                                {
                                    if (mstUserForm.CanPerformActions)
                                    {
                                        canPerformActions = true;
                                    }

                                    break;
                                }
                            }

                            if (canPerformActions)
                            {
                                var updateApplicant = applicants.FirstOrDefault();
                                byte[] imgarr = applicant.Photo;
                                updateApplicant.Photo = new Binary(imgarr);
                                updateApplicant.UpdatedByUserId = userId;
                                updateApplicant.UpdatedDateTime = DateTime.Now;
                                db.SubmitChanges();

                                return Request.CreateResponse(HttpStatusCode.OK);
                            }
                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.BadRequest);
                            }
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.BadRequest);
                        }
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest);
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // update applicant photo signature
        [Authorize]
        [HttpPut]
        [Route("api/applicant/updatePhoto/signature/{id}")]
        public HttpResponseMessage updateApplicantPhotoSignature(String id, Models.MstApplicant applicant)
        {
            try
            {
                var applicants = from d in db.mstApplicants where d.Id == Convert.ToInt32(id) select d;
                if (applicants.Any())
                {
                    if (!applicants.FirstOrDefault().IsLocked)
                    {
                        var userId = (from d in db.mstUsers where d.AspUserId == User.Identity.GetUserId() select d.Id).SingleOrDefault();
                        var mstUserForms = from d in db.mstUserForms
                                           where d.UserId == userId
                                           select new Models.MstUserForm
                                           {
                                               Id = d.Id,
                                               Form = d.sysForm.Form,
                                               CanPerformActions = d.CanPerformActions
                                           };

                        if (mstUserForms.Any())
                        {
                            String matchPageString = "ApplicantDetail";
                            Boolean canPerformActions = false;

                            foreach (var mstUserForm in mstUserForms)
                            {
                                if (mstUserForm.Form.Equals(matchPageString))
                                {
                                    if (mstUserForm.CanPerformActions)
                                    {
                                        canPerformActions = true;
                                    }

                                    break;
                                }
                            }

                            if (canPerformActions)
                            {
                                var updateApplicant = applicants.FirstOrDefault();
                                byte[] imgarr = applicant.PhotoSignature;
                                updateApplicant.PhotoSignature = new Binary(imgarr);
                                updateApplicant.UpdatedByUserId = userId;
                                updateApplicant.UpdatedDateTime = DateTime.Now;
                                db.SubmitChanges();

                                return Request.CreateResponse(HttpStatusCode.OK);
                            }
                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.BadRequest);
                            }
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.BadRequest);
                        }
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest);
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // block applicant
        [Authorize]
        [HttpPut]
        [Route("api/applicant/block/{id}")]
        public HttpResponseMessage blockApplicant(String id)
        {
            try
            {
                var applicants = from d in db.mstApplicants where d.Id == Convert.ToInt32(id) select d;
                if (applicants.Any())
                {
                    var userId = (from d in db.mstUsers where d.AspUserId == User.Identity.GetUserId() select d.Id).SingleOrDefault();
                    var mstUserForms = from d in db.mstUserForms
                                       where d.UserId == userId
                                       select new Models.MstUserForm
                                       {
                                           Id = d.Id,
                                           Form = d.sysForm.Form,
                                           CanPerformActions = d.CanPerformActions
                                       };

                    if (mstUserForms.Any())
                    {
                        String matchPageString = "ApplicantList";
                        Boolean canPerformActions = false;

                        foreach (var mstUserForm in mstUserForms)
                        {
                            if (mstUserForm.Form.Equals(matchPageString))
                            {
                                if (mstUserForm.CanPerformActions)
                                {
                                    canPerformActions = true;
                                }

                                break;
                            }
                        }

                        if (canPerformActions)
                        {
                            var updateApplicant = applicants.FirstOrDefault();
                            updateApplicant.IsBlocked = true;
                            updateApplicant.UpdatedByUserId = userId;
                            updateApplicant.UpdatedDateTime = DateTime.Now;
                            db.SubmitChanges();

                            return Request.CreateResponse(HttpStatusCode.OK);
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.BadRequest);
                        }
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest);
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // unblock applicant
        [Authorize]
        [HttpPut]
        [Route("api/applicant/unblock/{id}")]
        public HttpResponseMessage unblockApplicant(String id)
        {
            try
            {
                var applicants = from d in db.mstApplicants where d.Id == Convert.ToInt32(id) select d;
                if (applicants.Any())
                {
                    var userId = (from d in db.mstUsers where d.AspUserId == User.Identity.GetUserId() select d.Id).SingleOrDefault();
                    var mstUserForms = from d in db.mstUserForms
                                       where d.UserId == userId
                                       select new Models.MstUserForm
                                       {
                                           Id = d.Id,
                                           Form = d.sysForm.Form,
                                           CanPerformActions = d.CanPerformActions
                                       };

                    if (mstUserForms.Any())
                    {
                        String matchPageString = "ApplicantList";
                        Boolean canPerformActions = false;

                        foreach (var mstUserForm in mstUserForms)
                        {
                            if (mstUserForm.Form.Equals(matchPageString))
                            {
                                if (mstUserForm.CanPerformActions)
                                {
                                    canPerformActions = true;
                                }

                                break;
                            }
                        }

                        if (canPerformActions)
                        {
                            var updateApplicant = applicants.FirstOrDefault();
                            updateApplicant.IsBlocked = false;
                            updateApplicant.UpdatedByUserId = userId;
                            updateApplicant.UpdatedDateTime = DateTime.Now;
                            db.SubmitChanges();

                            return Request.CreateResponse(HttpStatusCode.OK);
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.BadRequest);
                        }
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest);
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // delete
        [Authorize]
        [HttpPut]
        [Route("api/applicant/deletePhoto/{id}")]
        public HttpResponseMessage deleteApplicantPhoto(String id)
        {
            try
            {
                var applicants = from d in db.mstApplicants where d.Id == Convert.ToInt32(id) select d;
                if (applicants.Any())
                {
                    if (!applicants.FirstOrDefault().IsLocked)
                    {
                        var userId = (from d in db.mstUsers where d.AspUserId == User.Identity.GetUserId() select d.Id).SingleOrDefault();
                        var mstUserForms = from d in db.mstUserForms
                                           where d.UserId == userId
                                           select new Models.MstUserForm
                                           {
                                               Id = d.Id,
                                               Form = d.sysForm.Form,
                                               CanPerformActions = d.CanPerformActions
                                           };

                        if (mstUserForms.Any())
                        {
                            String matchPageString = "ApplicantDetail";
                            Boolean canPerformActions = false;

                            foreach (var mstUserForm in mstUserForms)
                            {
                                if (mstUserForm.Form.Equals(matchPageString))
                                {
                                    if (mstUserForm.CanPerformActions)
                                    {
                                        canPerformActions = true;
                                    }

                                    break;
                                }
                            }

                            if (canPerformActions)
                            {
                                var updateApplicant = applicants.FirstOrDefault();

                                Byte[] bytes = File.ReadAllBytes(HttpContext.Current.Server.MapPath("~/Images/applicantPhotoPlaceHolder.png"));
                                String file = Convert.ToBase64String(bytes);
                                byte[] imgarr = Convert.FromBase64String(file);

                                updateApplicant.Photo = imgarr;
                                updateApplicant.UpdatedByUserId = userId;
                                updateApplicant.UpdatedDateTime = DateTime.Now;
                                db.SubmitChanges();

                                return Request.CreateResponse(HttpStatusCode.OK);
                            }
                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.BadRequest);
                            }
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.BadRequest);
                        }
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest);
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // delete
        [Authorize]
        [HttpPut]
        [Route("api/applicant/deletePhoto/signature/{id}")]
        public HttpResponseMessage deleteApplicantPhotoSignature(String id)
        {
            try
            {
                var applicants = from d in db.mstApplicants where d.Id == Convert.ToInt32(id) select d;
                if (applicants.Any())
                {
                    if (!applicants.FirstOrDefault().IsLocked)
                    {
                        var userId = (from d in db.mstUsers where d.AspUserId == User.Identity.GetUserId() select d.Id).SingleOrDefault();
                        var mstUserForms = from d in db.mstUserForms
                                           where d.UserId == userId
                                           select new Models.MstUserForm
                                           {
                                               Id = d.Id,
                                               Form = d.sysForm.Form,
                                               CanPerformActions = d.CanPerformActions
                                           };

                        if (mstUserForms.Any())
                        {
                            String matchPageString = "ApplicantDetail";
                            Boolean canPerformActions = false;

                            foreach (var mstUserForm in mstUserForms)
                            {
                                if (mstUserForm.Form.Equals(matchPageString))
                                {
                                    if (mstUserForm.CanPerformActions)
                                    {
                                        canPerformActions = true;
                                    }

                                    break;
                                }
                            }

                            if (canPerformActions)
                            {
                                var updateApplicant = applicants.FirstOrDefault();

                                Byte[] bytes = File.ReadAllBytes(HttpContext.Current.Server.MapPath("~/Images/signature.png"));
                                String file = Convert.ToBase64String(bytes);
                                byte[] imgarr = Convert.FromBase64String(file);

                                updateApplicant.PhotoSignature = imgarr;
                                updateApplicant.UpdatedByUserId = userId;
                                updateApplicant.UpdatedDateTime = DateTime.Now;
                                db.SubmitChanges();

                                return Request.CreateResponse(HttpStatusCode.OK);
                            }
                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.BadRequest);
                            }
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.BadRequest);
                        }
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest);
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // delete applicant
        [Authorize]
        [HttpDelete]
        [Route("api/applicant/delete/{id}")]
        public HttpResponseMessage deleteApplicant(String id)
        {
            try
            {
                var applicants = from d in db.mstApplicants where d.Id == Convert.ToInt32(id) select d;
                if (applicants.Any())
                {
                    if (applicants.FirstOrDefault().IsLocked != true)
                    {
                        var userId = (from d in db.mstUsers where d.AspUserId == User.Identity.GetUserId() select d.Id).SingleOrDefault();
                        var mstUserForms = from d in db.mstUserForms
                                           where d.UserId == userId
                                           select new Models.MstUserForm
                                           {
                                               Id = d.Id,
                                               Form = d.sysForm.Form,
                                               CanPerformActions = d.CanPerformActions
                                           };

                        if (mstUserForms.Any())
                        {
                            String matchPageString = "ApplicantList";
                            Boolean canPerformActions = false;

                            foreach (var mstUserForm in mstUserForms)
                            {
                                if (mstUserForm.Form.Equals(matchPageString))
                                {
                                    if (mstUserForm.CanPerformActions)
                                    {
                                        canPerformActions = true;
                                    }

                                    break;
                                }
                            }

                            if (canPerformActions)
                            {
                                db.mstApplicants.DeleteOnSubmit(applicants.First());
                                db.SubmitChanges();

                                return Request.CreateResponse(HttpStatusCode.OK);
                            }
                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.BadRequest);
                            }
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.BadRequest);
                        }
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest);
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}
