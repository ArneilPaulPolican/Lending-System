﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;


namespace Lending.ApiControllers
{
    public class ApiCollectionController : ApiController
    {
        // data
        private Data.LendingDataContext db = new Data.LendingDataContext();
        private Business.CollectionStatus collectionStatus = new Business.CollectionStatus();

        // collection list by applicantId and by loanId for advance payment
        [Authorize]
        [HttpGet]
        [Route("api/collection/list/advancePayment/byApplicantId/byLoanId/{applicantId}/{loanId}")]
        public List<Models.TrnCollection> listCollectionForAdvancePayment(String applicantId, String loanId)
        {
            var collections = from d in db.trnCollections
                              where d.trnLoanApplication.ApplicantId == Convert.ToInt32(applicantId)
                              && d.LoanId == Convert.ToInt32(loanId)
                              && d.IsProcessed == false
                              select new Models.TrnCollection
                              {
                                  Id = d.Id,
                                  LoanId = d.LoanId,
                                  LoanNumber = d.trnLoanApplication.LoanNumber,
                                  ApplicantId = d.trnLoanApplication.ApplicantId,
                                  Applicant = d.trnLoanApplication.mstApplicant.ApplicantLastName + ", " + d.trnLoanApplication.mstApplicant.ApplicantFirstName + " " + (d.trnLoanApplication.mstApplicant.ApplicantMiddleName != null ? d.trnLoanApplication.mstApplicant.ApplicantMiddleName : " "),
                                  Area = d.trnLoanApplication.mstApplicant.mstArea.Area,
                                  IsFullyPaid = d.trnLoanApplication.IsFullyPaid,
                                  AccountId = d.AccountId,
                                  Account = d.mstAccount.Account,
                                  CollectionDate = d.CollectionDate.ToShortDateString(),
                                  NetAmount = d.NetAmount,
                                  CollectibleAmount = d.CollectibleAmount,
                                  PenaltyAmount = d.PenaltyAmount,
                                  PaidAmount = d.PaidAmount,
                                  PreviousBalanceAmount = d.PreviousBalanceAmount,
                                  CurrentBalanceAmount = d.CurrentBalanceAmount,
                                  IsCleared = d.IsCleared,
                                  IsAbsent = d.IsAbsent,
                                  IsPartialPayment = d.IsPartialPayment,
                                  IsAdvancePayment = d.IsAdvancePayment,
                                  IsFullPayment = d.IsFullPayment,
                                  IsDueDate = d.IsDueDate,
                                  IsExtendCollection = d.IsExtendCollection,
                                  IsOverdueCollection = d.IsOverdueCollection,
                                  IsCurrentCollection = d.IsCurrentCollection,
                                  IsProcessed = d.IsProcessed,
                                  IsAction = d.IsAction,
                                  IsLastDay = d.IsLastDay,
                                  Status = collectionStatus.getStatus(d.IsCleared, d.IsAbsent, d.IsPartialPayment, d.IsAdvancePayment, d.IsFullPayment, d.IsExtendCollection, d.IsOverdueCollection)
                              };

            return collections.ToList();
        }

        // get current collection by applicantId and by loanId
        [Authorize]
        [HttpGet]
        [Route("api/collection/get/currentCollection/byApplicantId/byLoanId/{applicantId}/{loanId}")]
        public Models.TrnCollection getCurrentCollectionForPartialPayment(String applicantId, String loanId)
        {
            var collections = from d in db.trnCollections
                              where d.trnLoanApplication.ApplicantId == Convert.ToInt32(applicantId)
                              && d.LoanId == Convert.ToInt32(loanId)
                              && d.IsCurrentCollection == true
                              select new Models.TrnCollection
                              {
                                  Id = d.Id,
                                  LoanId = d.LoanId,
                                  LoanNumber = d.trnLoanApplication.LoanNumber,
                                  ApplicantId = d.trnLoanApplication.ApplicantId,
                                  Applicant = d.trnLoanApplication.mstApplicant.ApplicantLastName + ", " + d.trnLoanApplication.mstApplicant.ApplicantFirstName + " " + (d.trnLoanApplication.mstApplicant.ApplicantMiddleName != null ? d.trnLoanApplication.mstApplicant.ApplicantMiddleName : " "),
                                  Area = d.trnLoanApplication.mstApplicant.mstArea.Area,
                                  IsFullyPaid = d.trnLoanApplication.IsFullyPaid,
                                  AccountId = d.AccountId,
                                  Account = d.mstAccount.Account,
                                  CollectionDate = d.CollectionDate.ToShortDateString(),
                                  NetAmount = d.NetAmount,
                                  CollectibleAmount = d.CollectibleAmount,
                                  PenaltyAmount = d.PenaltyAmount,
                                  PaidAmount = d.PaidAmount,
                                  PreviousBalanceAmount = d.PreviousBalanceAmount,
                                  CurrentBalanceAmount = d.CurrentBalanceAmount,
                                  IsCleared = d.IsCleared,
                                  IsAbsent = d.IsAbsent,
                                  IsPartialPayment = d.IsPartialPayment,
                                  IsAdvancePayment = d.IsAdvancePayment,
                                  IsFullPayment = d.IsFullPayment,
                                  IsDueDate = d.IsDueDate,
                                  IsExtendCollection = d.IsExtendCollection,
                                  IsOverdueCollection = d.IsOverdueCollection,
                                  IsCurrentCollection = d.IsCurrentCollection,
                                  IsProcessed = d.IsProcessed,
                                  IsAction = d.IsAction,
                                  IsLastDay = d.IsLastDay,
                                  Status = collectionStatus.getStatus(d.IsCleared, d.IsAbsent, d.IsPartialPayment, d.IsAdvancePayment, d.IsFullPayment, d.IsExtendCollection, d.IsOverdueCollection)
                              };

            return (Models.TrnCollection)collections.FirstOrDefault();
        }

        // collection list by applicantId and by loanId
        [Authorize]
        [HttpGet]
        [Route("api/collection/list/byApplicantId/byLoanId/{applicantId}/{loanId}")]
        public List<Models.TrnCollection> listCollectionByApplicantIdByLoanId(String applicantId, String loanId)
        {
            var collections = from d in db.trnCollections
                              where d.trnLoanApplication.ApplicantId == Convert.ToInt32(applicantId)
                              && d.LoanId == Convert.ToInt32(loanId)
                              select new Models.TrnCollection
                              {
                                  Id = d.Id,
                                  LoanId = d.LoanId,
                                  LoanNumber = d.trnLoanApplication.LoanNumber,
                                  ApplicantId = d.trnLoanApplication.ApplicantId,
                                  Applicant = d.trnLoanApplication.mstApplicant.ApplicantLastName + ", " + d.trnLoanApplication.mstApplicant.ApplicantFirstName + " " + (d.trnLoanApplication.mstApplicant.ApplicantMiddleName != null ? d.trnLoanApplication.mstApplicant.ApplicantMiddleName : " "),
                                  Area = d.trnLoanApplication.mstApplicant.mstArea.Area,
                                  IsFullyPaid = d.trnLoanApplication.IsFullyPaid,
                                  AccountId = d.AccountId,
                                  Account = d.mstAccount.Account,
                                  CollectionDate = d.CollectionDate.ToShortDateString(),
                                  NetAmount = d.NetAmount,
                                  CollectibleAmount = d.CollectibleAmount,
                                  PenaltyAmount = d.PenaltyAmount,
                                  PaidAmount = d.PaidAmount,
                                  PreviousBalanceAmount = d.PreviousBalanceAmount,
                                  CurrentBalanceAmount = d.CurrentBalanceAmount,
                                  IsCleared = d.IsCleared,
                                  IsAbsent = d.IsAbsent,
                                  IsPartialPayment = d.IsPartialPayment,
                                  IsAdvancePayment = d.IsAdvancePayment,
                                  IsFullPayment = d.IsFullPayment,
                                  IsDueDate = d.IsDueDate,
                                  IsExtendCollection = d.IsExtendCollection,
                                  IsOverdueCollection = d.IsOverdueCollection,
                                  IsCurrentCollection = d.IsCurrentCollection,
                                  IsProcessed = d.IsProcessed,
                                  IsAction = d.IsAction,
                                  IsLastDay = d.IsLastDay,
                                  Status = collectionStatus.getStatus(d.IsCleared, d.IsAbsent, d.IsPartialPayment, d.IsAdvancePayment, d.IsFullPayment, d.IsExtendCollection, d.IsOverdueCollection)
                              };

            return collections.ToList();
        }

        // collection list by collectionDate and by areaId
        [Authorize]
        [HttpGet]
        [Route("api/collection/list/byCollectionDate/byAreaId/{collectionDate}/{areaId}")]
        public List<Models.TrnCollection> listCollectionByCollectionDateByAreaId(String collectionDate, String areaId)
        {
            var collections = from d in db.trnCollections
                              where d.CollectionDate == Convert.ToDateTime(collectionDate)
                              && d.trnLoanApplication.mstApplicant.AreaId == Convert.ToInt32(areaId)
                              select new Models.TrnCollection
                              {
                                  Id = d.Id,
                                  LoanId = d.LoanId,
                                  LoanNumber = d.trnLoanApplication.LoanNumber,
                                  ApplicantId = d.trnLoanApplication.ApplicantId,
                                  Applicant = d.trnLoanApplication.mstApplicant.ApplicantLastName + ", " + d.trnLoanApplication.mstApplicant.ApplicantFirstName + " " + (d.trnLoanApplication.mstApplicant.ApplicantMiddleName != null ? d.trnLoanApplication.mstApplicant.ApplicantMiddleName : " "),
                                  Area = d.trnLoanApplication.mstApplicant.mstArea.Area,
                                  IsFullyPaid = d.trnLoanApplication.IsFullyPaid,
                                  AccountId = d.AccountId,
                                  Account = d.mstAccount.Account,
                                  CollectionDate = d.CollectionDate.ToShortDateString(),
                                  NetAmount = d.NetAmount,
                                  CollectibleAmount = d.CollectibleAmount,
                                  PenaltyAmount = d.PenaltyAmount,
                                  PaidAmount = d.PaidAmount,
                                  PreviousBalanceAmount = d.PreviousBalanceAmount,
                                  CurrentBalanceAmount = d.CurrentBalanceAmount,
                                  IsCleared = d.IsCleared,
                                  IsAbsent = d.IsAbsent,
                                  IsPartialPayment = d.IsPartialPayment,
                                  IsAdvancePayment = d.IsAdvancePayment,
                                  IsFullPayment = d.IsFullPayment,
                                  IsDueDate = d.IsDueDate,
                                  IsExtendCollection = d.IsExtendCollection,
                                  IsOverdueCollection = d.IsOverdueCollection,
                                  IsCurrentCollection = d.IsCurrentCollection,
                                  IsProcessed = d.IsProcessed,
                                  IsAction = d.IsAction,
                                  IsLastDay = d.IsLastDay,
                                  Status = collectionStatus.getStatus(d.IsCleared, d.IsAbsent, d.IsPartialPayment, d.IsAdvancePayment, d.IsFullPayment, d.IsExtendCollection, d.IsOverdueCollection)
                              };

            return collections.ToList();
        }

        // get penalty amount
        [Authorize]
        [HttpGet]
        [Route("api/collection/penaltyAmount/get/byLoanId/byCollectionDate/{loanId}/{collectionDate}")]
        public Decimal getPenaltyAmount(Int32 loanId, String collectionDate)
        {
            var loanApplication = from d in db.trnLoanApplications where d.Id == loanId select d;

            Decimal defaultPenaltyAmount = loanApplication.FirstOrDefault().mstPenalty.PenaltyAmount;
            Decimal numberOfAbsentLimit = loanApplication.FirstOrDefault().mstPenalty.NoOfAbsentLimit;
            Decimal termsNoOfDays = loanApplication.FirstOrDefault().TermNoOfDays;

            Int32 absentCount = 0;
            for (Int32 i = Convert.ToInt32(numberOfAbsentLimit); i >= 1; i--)
            {
                var collection = from d in db.trnCollections
                                 where d.LoanId == loanId
                                 && d.CollectionDate == Convert.ToDateTime(collectionDate).Date.AddDays(-(Convert.ToInt32(i) * Convert.ToInt32(termsNoOfDays)))
                                 select d;

                if (collection.Any())
                {
                    if (collection.FirstOrDefault().IsAbsent)
                    {
                        if (collection.FirstOrDefault().PenaltyAmount == defaultPenaltyAmount)
                        {
                            absentCount += 1;
                            if (absentCount == Convert.ToInt32(numberOfAbsentLimit))
                            {
                                defaultPenaltyAmount = loanApplication.FirstOrDefault().mstPenalty.PenaltyAmountOverNoOfAbsentLimit;
                            }
                        }
                    }
                    else
                    {
                        absentCount -= 1;
                    }
                }
            }

            return defaultPenaltyAmount;
        }

        // get advance payment total balance to be paid.
        public Decimal getAdvancePaymentTotalCurrentBalance(Int32 loanId, String collectionStarDate, String collectionEndDate)
        {
            var collectionDateSequences = from d in db.trnCollections
                                          where d.LoanId == Convert.ToInt32(loanId)
                                          && d.CollectionDate >= Convert.ToDateTime(collectionStarDate)
                                          && d.CollectionDate <= Convert.ToDateTime(collectionEndDate)
                                          select d;

            Decimal currentBalanceAmount = 0;
            if (collectionDateSequences.Any())
            {
                currentBalanceAmount = collectionDateSequences.Sum(d => d.CurrentBalanceAmount);
            }

            return currentBalanceAmount;
        }

        // get colllection from loan application fully paid
        [Authorize]
        [HttpGet]
        [Route("api/collection/isFullyPaid/get/byId/{id}")]
        public Boolean getIsFullyPaidCollection(String id)
        {
            var collection = from d in db.trnCollections
                             where d.Id == Convert.ToInt32(id)
                             select d;

            Boolean isFullyPaidValue = false;
            if (collection.Any())
            {
                isFullyPaidValue = collection.FirstOrDefault().trnLoanApplication.IsFullyPaid;
            }

            return isFullyPaidValue;
        }

        // get collection previous balance paid
        [Authorize]
        [HttpGet]
        [Route("api/collection/previousBalance/get/byLoanId/byCollectionDate/{loanId}/{collectionDate}")]
        public Decimal getPreviousBalanceCollection(String loanId, String collectionDate)
        {
            var loanApplication = from d in db.trnLoanApplications where d.Id == Convert.ToInt32(loanId) select d;
            var termNoOfDays = loanApplication.FirstOrDefault().TermNoOfDays;

            var collectionNextDate = from d in db.trnCollections where d.LoanId == Convert.ToInt32(loanId) && d.CollectionDate == Convert.ToDateTime(collectionDate).Date.AddDays(-Convert.ToInt32(termNoOfDays)) select d;

            Decimal previousBalanceValue = 0;
            if (collectionNextDate.Any())
            {
                previousBalanceValue = collectionNextDate.FirstOrDefault().CurrentBalanceAmount;
            }

            return previousBalanceValue;
        }

        // clear collection
        [Authorize]
        [HttpPut]
        [Route("api/collection/cleared/update/byId/byLoanId/{id}/{loanId}")]
        public HttpResponseMessage clearedCollection(String id, String loanId)
        {
            try
            {
                var loanApplication = from d in db.trnLoanApplications where d.Id == Convert.ToInt32(loanId) select d;
                if (loanApplication.Any())
                {
                    if (loanApplication.FirstOrDefault().IsLocked)
                    {
                        var termNoOfDays = loanApplication.FirstOrDefault().TermNoOfDays;
                        var collection = from d in db.trnCollections where d.Id == Convert.ToInt32(id) select d;
                        if (collection.Any())
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
                                String matchPageString = "CollectionList";
                                String matchPageString2 = "CollectionDetail";
                                Boolean canPerformActions = false;

                                foreach (var mstUserForm in mstUserForms)
                                {
                                    if (mstUserForm.Form.Equals(matchPageString) || mstUserForm.Form.Equals(matchPageString2))
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
                                    if (collection.FirstOrDefault().CurrentBalanceAmount > 0)
                                    {
                                        if (!collection.FirstOrDefault().IsCleared)
                                        {
                                            if (collection.FirstOrDefault().IsAction)
                                            {
                                                var updateCollection = collection.FirstOrDefault();
                                                updateCollection.PaidAmount = collection.FirstOrDefault().CollectibleAmount + collection.FirstOrDefault().PreviousBalanceAmount;
                                                updateCollection.CurrentBalanceAmount = 0;
                                                updateCollection.PenaltyAmount = 0;
                                                updateCollection.IsCleared = true;
                                                updateCollection.IsAbsent = false;
                                                updateCollection.IsProcessed = true;
                                                updateCollection.IsCurrentCollection = false;
                                                updateCollection.IsPartialPayment = false;
                                                updateCollection.IsAdvancePayment = false;
                                                updateCollection.IsFullPayment = false;
                                                db.SubmitChanges();

                                                var collectionPrevoiusDate = from d in db.trnCollections where d.LoanId == Convert.ToInt32(loanId) && d.CollectionDate == collection.FirstOrDefault().CollectionDate.Date.AddDays(-Convert.ToInt32(termNoOfDays)) select d;
                                                if (collectionPrevoiusDate.Any())
                                                {
                                                    var updateCollectionPrevoiusDate = collectionPrevoiusDate.FirstOrDefault();
                                                    updateCollectionPrevoiusDate.IsAction = false;
                                                    updateCollectionPrevoiusDate.IsCurrentCollection = false;
                                                    db.SubmitChanges();

                                                    var collectionNextDate = from d in db.trnCollections where d.LoanId == Convert.ToInt32(loanId) && d.CollectionDate == collection.FirstOrDefault().CollectionDate.Date.AddDays(Convert.ToInt32(termNoOfDays)) select d;
                                                    if (collectionNextDate.Any())
                                                    {
                                                        var updateCollectionNextDate = collectionNextDate.FirstOrDefault();
                                                        updateCollectionNextDate.PreviousBalanceAmount = 0;
                                                        updateCollectionNextDate.CurrentBalanceAmount = collectionNextDate.FirstOrDefault().CollectibleAmount;
                                                        updateCollectionNextDate.IsAction = true;
                                                        updateCollectionNextDate.IsCurrentCollection = true;
                                                        db.SubmitChanges();
                                                    }
                                                    else
                                                    {
                                                        var updateLoanApplicationFullPayment = loanApplication.FirstOrDefault();
                                                        updateLoanApplicationFullPayment.IsFullyPaid = true;
                                                        db.SubmitChanges();
                                                    }
                                                }
                                                else
                                                {
                                                    var collectionNextDate = from d in db.trnCollections where d.LoanId == Convert.ToInt32(loanId) && d.CollectionDate == collection.FirstOrDefault().CollectionDate.Date.AddDays(Convert.ToInt32(termNoOfDays)) select d;
                                                    if (collectionNextDate.Any())
                                                    {
                                                        var updateCollectionNextDate = collectionNextDate.FirstOrDefault();
                                                        updateCollectionNextDate.PreviousBalanceAmount = 0;
                                                        updateCollectionNextDate.CurrentBalanceAmount = collectionNextDate.FirstOrDefault().CollectibleAmount;
                                                        updateCollectionNextDate.IsAction = true;
                                                        updateCollectionNextDate.IsCurrentCollection = true;
                                                        db.SubmitChanges();
                                                    }
                                                    else
                                                    {
                                                        var updateLoanApplicationFullPayment = loanApplication.FirstOrDefault();
                                                        updateLoanApplicationFullPayment.IsFullyPaid = true;
                                                        db.SubmitChanges();
                                                    }
                                                }

                                                Business.Journal journal = new Business.Journal();
                                                journal.postCollectionJournal(Convert.ToInt32(id));

                                                return Request.CreateResponse(HttpStatusCode.OK);
                                            }
                                            else
                                            {
                                                return Request.CreateResponse(HttpStatusCode.BadRequest, "Can't process.");
                                            }
                                        }
                                        else
                                        {
                                            return Request.CreateResponse(HttpStatusCode.BadRequest, "Already cleared.");
                                        }
                                    }
                                    else
                                    {
                                        return Request.CreateResponse(HttpStatusCode.BadRequest, "No current balance.");
                                    }
                                }
                                else
                                {
                                    return Request.CreateResponse(HttpStatusCode.BadRequest, "No rights to perform some actions.");
                                }
                            }
                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.BadRequest, "No rights to access this page.");
                            }
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.NotFound, "No data found in the server.");
                        }
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest, "The loan appliction must be locked before proceeding the collection.");
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "No data found in the server.");
                }
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Oops! Something's went wrong from the server.");
            }
        }

        // absent collection
        [Authorize]
        [HttpPut]
        [Route("api/collection/absent/update/byId/byLoanId/{id}/{loanId}")]
        public HttpResponseMessage absentCollection(String id, String loanId)
        {
            try
            {
                var loanApplication = from d in db.trnLoanApplications where d.Id == Convert.ToInt32(loanId) select d;
                if (loanApplication.Any())
                {
                    if (loanApplication.FirstOrDefault().IsLocked)
                    {
                        var termNoOfDays = loanApplication.FirstOrDefault().TermNoOfDays;
                        var collection = from d in db.trnCollections where d.Id == Convert.ToInt32(id) select d;
                        if (collection.Any())
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
                                String matchPageString = "CollectionList";
                                String matchPageString2 = "CollectionDetail";
                                Boolean canPerformActions = false;

                                foreach (var mstUserForm in mstUserForms)
                                {
                                    if (mstUserForm.Form.Equals(matchPageString) || mstUserForm.Form.Equals(matchPageString2))
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
                                    if (!collection.FirstOrDefault().IsAbsent)
                                    {
                                        if (collection.FirstOrDefault().IsAction)
                                        {
                                            if (!collection.FirstOrDefault().IsDueDate)
                                            {
                                                Decimal penaltyAmount = getPenaltyAmount(collection.FirstOrDefault().LoanId, collection.FirstOrDefault().CollectionDate.ToShortDateString());

                                                var updateCollection = collection.FirstOrDefault();
                                                updateCollection.PaidAmount = 0;
                                                updateCollection.CurrentBalanceAmount = collection.FirstOrDefault().CollectibleAmount + penaltyAmount + collection.FirstOrDefault().PreviousBalanceAmount;
                                                updateCollection.PenaltyAmount = penaltyAmount;
                                                updateCollection.IsCleared = false;
                                                updateCollection.IsAbsent = true;
                                                updateCollection.IsProcessed = true;
                                                updateCollection.IsCurrentCollection = false;
                                                updateCollection.IsPartialPayment = false;
                                                updateCollection.IsAdvancePayment = false;
                                                updateCollection.IsFullPayment = false;
                                                db.SubmitChanges();

                                                var collectionPrevoiusDate = from d in db.trnCollections where d.LoanId == Convert.ToInt32(loanId) && d.CollectionDate == collection.FirstOrDefault().CollectionDate.Date.AddDays(-Convert.ToInt32(termNoOfDays)) select d;
                                                if (collectionPrevoiusDate.Any())
                                                {
                                                    var updateCollectionPrevoiusDate = collectionPrevoiusDate.FirstOrDefault();
                                                    updateCollectionPrevoiusDate.IsAction = false;
                                                    updateCollectionPrevoiusDate.IsCurrentCollection = false;
                                                    db.SubmitChanges();

                                                    var collectionNextDate = from d in db.trnCollections where d.LoanId == Convert.ToInt32(loanId) && d.CollectionDate == collection.FirstOrDefault().CollectionDate.Date.AddDays(Convert.ToInt32(termNoOfDays)) select d;
                                                    if (collectionNextDate.Any())
                                                    {
                                                        var updateCollectionNextDate = collectionNextDate.FirstOrDefault();
                                                        updateCollectionNextDate.PreviousBalanceAmount = collection.FirstOrDefault().CollectibleAmount + penaltyAmount + collection.FirstOrDefault().PreviousBalanceAmount;
                                                        updateCollectionNextDate.CurrentBalanceAmount = collectionNextDate.FirstOrDefault().CollectibleAmount + (collection.FirstOrDefault().CollectibleAmount + penaltyAmount + collection.FirstOrDefault().PreviousBalanceAmount);
                                                        updateCollectionNextDate.IsAction = true;
                                                        updateCollectionNextDate.IsCurrentCollection = true;
                                                        db.SubmitChanges();
                                                    }
                                                    else
                                                    {
                                                        var updateLoanApplicationFullPayment = loanApplication.FirstOrDefault();
                                                        updateLoanApplicationFullPayment.IsFullyPaid = true;
                                                        db.SubmitChanges();
                                                    }
                                                }
                                                else
                                                {
                                                    var collectionNextDate = from d in db.trnCollections where d.LoanId == Convert.ToInt32(loanId) && d.CollectionDate == collection.FirstOrDefault().CollectionDate.Date.AddDays(Convert.ToInt32(termNoOfDays)) select d;
                                                    if (collectionNextDate.Any())
                                                    {
                                                        var updateCollectionNextDate = collectionNextDate.FirstOrDefault();
                                                        updateCollectionNextDate.PreviousBalanceAmount = collection.FirstOrDefault().CollectibleAmount + penaltyAmount + collection.FirstOrDefault().PreviousBalanceAmount;
                                                        updateCollectionNextDate.CurrentBalanceAmount = collectionNextDate.FirstOrDefault().CollectibleAmount + (collection.FirstOrDefault().CollectibleAmount + penaltyAmount + collection.FirstOrDefault().PreviousBalanceAmount);
                                                        updateCollectionNextDate.IsAction = true;
                                                        updateCollectionNextDate.IsCurrentCollection = true;
                                                        db.SubmitChanges();
                                                    }
                                                    else
                                                    {
                                                        var updateLoanApplicationFullPayment = loanApplication.FirstOrDefault();
                                                        updateLoanApplicationFullPayment.IsFullyPaid = true;
                                                        db.SubmitChanges();
                                                    }
                                                }

                                                Business.Journal journal = new Business.Journal();
                                                journal.deleteCollectionJournal(Convert.ToInt32(id));

                                                return Request.CreateResponse(HttpStatusCode.OK);
                                            }
                                            else
                                            {
                                                if (!collection.FirstOrDefault().IsLastDay)
                                                {
                                                    Decimal penaltyAmount = getPenaltyAmount(collection.FirstOrDefault().LoanId, collection.FirstOrDefault().CollectionDate.ToShortDateString());

                                                    var updateCollection = collection.FirstOrDefault();
                                                    updateCollection.PaidAmount = 0;
                                                    updateCollection.CurrentBalanceAmount = collection.FirstOrDefault().CollectibleAmount + penaltyAmount + collection.FirstOrDefault().PreviousBalanceAmount;
                                                    updateCollection.PenaltyAmount = penaltyAmount;
                                                    updateCollection.IsCleared = false;
                                                    updateCollection.IsAbsent = true;
                                                    updateCollection.IsProcessed = true;
                                                    updateCollection.IsCurrentCollection = false;
                                                    updateCollection.IsPartialPayment = false;
                                                    updateCollection.IsAdvancePayment = false;
                                                    updateCollection.IsFullPayment = false;
                                                    db.SubmitChanges();

                                                    var collectionPrevoiusDate = from d in db.trnCollections where d.LoanId == Convert.ToInt32(loanId) && d.CollectionDate == collection.FirstOrDefault().CollectionDate.Date.AddDays(-Convert.ToInt32(termNoOfDays)) select d;
                                                    if (collectionPrevoiusDate.Any())
                                                    {
                                                        var updateCollectionPrevoiusDate = collectionPrevoiusDate.FirstOrDefault();
                                                        updateCollectionPrevoiusDate.IsAction = false;
                                                        updateCollectionPrevoiusDate.IsCurrentCollection = false;
                                                        db.SubmitChanges();

                                                        var collectionNextDate = from d in db.trnCollections where d.LoanId == Convert.ToInt32(loanId) && d.CollectionDate == collection.FirstOrDefault().CollectionDate.Date.AddDays(Convert.ToInt32(termNoOfDays)) select d;
                                                        if (collectionNextDate.Any())
                                                        {
                                                            var updateCollectionNextDate = collectionNextDate.FirstOrDefault();
                                                            updateCollectionNextDate.PreviousBalanceAmount = collection.FirstOrDefault().CollectibleAmount + penaltyAmount + collection.FirstOrDefault().PreviousBalanceAmount;
                                                            updateCollectionNextDate.CurrentBalanceAmount = collectionNextDate.FirstOrDefault().CollectibleAmount + (collection.FirstOrDefault().CollectibleAmount + penaltyAmount + collection.FirstOrDefault().PreviousBalanceAmount);
                                                            updateCollectionNextDate.IsAction = true;
                                                            updateCollectionNextDate.IsCurrentCollection = true;
                                                            db.SubmitChanges();
                                                        }
                                                        else
                                                        {
                                                            var updateLoanApplicationFullPayment = loanApplication.FirstOrDefault();
                                                            updateLoanApplicationFullPayment.IsFullyPaid = true;
                                                            db.SubmitChanges();
                                                        }
                                                    }
                                                    else
                                                    {
                                                        var collectionNextDate = from d in db.trnCollections where d.LoanId == Convert.ToInt32(loanId) && d.CollectionDate == collection.FirstOrDefault().CollectionDate.Date.AddDays(Convert.ToInt32(termNoOfDays)) select d;
                                                        if (collectionNextDate.Any())
                                                        {
                                                            var updateCollectionNextDate = collectionNextDate.FirstOrDefault();
                                                            updateCollectionNextDate.PreviousBalanceAmount = collection.FirstOrDefault().CollectibleAmount + penaltyAmount + collection.FirstOrDefault().PreviousBalanceAmount;
                                                            updateCollectionNextDate.CurrentBalanceAmount = collectionNextDate.FirstOrDefault().CollectibleAmount + (collection.FirstOrDefault().CollectibleAmount + penaltyAmount + collection.FirstOrDefault().PreviousBalanceAmount);
                                                            updateCollectionNextDate.IsAction = true;
                                                            updateCollectionNextDate.IsCurrentCollection = true;
                                                            db.SubmitChanges();
                                                        }
                                                        else
                                                        {
                                                            var updateLoanApplicationFullPayment = loanApplication.FirstOrDefault();
                                                            updateLoanApplicationFullPayment.IsFullyPaid = true;
                                                            db.SubmitChanges();
                                                        }
                                                    }

                                                    Business.Journal journal = new Business.Journal();
                                                    journal.deleteCollectionJournal(Convert.ToInt32(id));

                                                    return Request.CreateResponse(HttpStatusCode.OK);
                                                }
                                                else
                                                {
                                                    return Request.CreateResponse(HttpStatusCode.BadRequest, "The current collection is due date. Please do extensions.");
                                                }
                                            }
                                        }
                                        else
                                        {
                                            return Request.CreateResponse(HttpStatusCode.BadRequest, "Can't process");
                                        }
                                    }
                                    else
                                    {
                                        return Request.CreateResponse(HttpStatusCode.BadRequest, "Already absent.");
                                    }
                                }
                                else
                                {
                                    return Request.CreateResponse(HttpStatusCode.BadRequest, "No rights to perform some actions.");
                                }
                            }
                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.BadRequest, "No rights to access this page.");
                            }
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.NotFound, "No data found in the server.");
                        }
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest, "The loan appliction must be locked before proceeding the collection.");
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "No data found in the server.");
                }
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Oops! Something's went wrong from the server.");
            }
        }

        // undo changes collection
        [Authorize]
        [HttpPut]
        [Route("api/collection/undoChanges/update/byId/byLoanId/{id}/{loanId}")]
        public HttpResponseMessage undoChangesCollection(String id, String loanId)
        {
            try
            {
                var loanApplication = from d in db.trnLoanApplications where d.Id == Convert.ToInt32(loanId) select d;
                if (loanApplication.Any())
                {
                    if (loanApplication.FirstOrDefault().IsLocked)
                    {
                        var collection = from d in db.trnCollections where d.Id == Convert.ToInt32(id) select d;
                        if (collection.Any())
                        {
                            var termNoOfDays = loanApplication.FirstOrDefault().TermNoOfDays;
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
                                String matchPageString = "CollectionList";
                                String matchPageString2 = "CollectionDetail";
                                Boolean canPerformActions = false;

                                foreach (var mstUserForm in mstUserForms)
                                {
                                    if (mstUserForm.Form.Equals(matchPageString) || mstUserForm.Form.Equals(matchPageString2))
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
                                    if (collection.FirstOrDefault().IsExtendCollection || collection.FirstOrDefault().IsOverdueCollection)
                                    {
                                        if (collection.FirstOrDefault().IsLastDay)
                                        {
                                            var collectionPrevoiusDate = from d in db.trnCollections where d.LoanId == Convert.ToInt32(loanId) && d.CollectionDate == collection.FirstOrDefault().CollectionDate.Date.AddDays(-Convert.ToInt32(termNoOfDays)) select d;
                                            if (collectionPrevoiusDate.Any())
                                            {
                                                if (loanApplication.FirstOrDefault().IsFullyPaid)
                                                {
                                                    var updateCollectionPrevoiusDate = collectionPrevoiusDate.FirstOrDefault();
                                                    updateCollectionPrevoiusDate.PaidAmount = 0;
                                                    updateCollectionPrevoiusDate.CurrentBalanceAmount = collectionPrevoiusDate.FirstOrDefault().CollectibleAmount + collectionPrevoiusDate.FirstOrDefault().PreviousBalanceAmount;
                                                    updateCollectionPrevoiusDate.PenaltyAmount = 0;
                                                    updateCollectionPrevoiusDate.IsCleared = false;
                                                    updateCollectionPrevoiusDate.IsAbsent = false;
                                                    updateCollectionPrevoiusDate.IsCurrentCollection = true;
                                                    updateCollectionPrevoiusDate.IsProcessed = false;
                                                    updateCollectionPrevoiusDate.IsAction = true;
                                                    updateCollectionPrevoiusDate.IsPartialPayment = false;
                                                    updateCollectionPrevoiusDate.IsAdvancePayment = false;
                                                    updateCollectionPrevoiusDate.IsFullPayment = false;
                                                    updateCollectionPrevoiusDate.IsLastDay = true;
                                                    db.SubmitChanges();

                                                    db.trnCollections.DeleteOnSubmit(collection.First());
                                                    db.SubmitChanges();

                                                    var updateLoanApplicationFullPayment = loanApplication.FirstOrDefault();
                                                    updateLoanApplicationFullPayment.IsFullyPaid = false;
                                                    db.SubmitChanges();
                                                }
                                                else
                                                {
                                                    var updateCollectionPrevoiusDate = collectionPrevoiusDate.FirstOrDefault();

                                                    if (collection.FirstOrDefault().IsCurrentCollection)
                                                    {
                                                        updateCollectionPrevoiusDate.PaidAmount = 0;
                                                        updateCollectionPrevoiusDate.CurrentBalanceAmount = collectionPrevoiusDate.FirstOrDefault().CollectibleAmount + collectionPrevoiusDate.FirstOrDefault().PreviousBalanceAmount;
                                                        updateCollectionPrevoiusDate.PenaltyAmount = 0;
                                                        updateCollectionPrevoiusDate.IsCleared = false;
                                                        updateCollectionPrevoiusDate.IsAbsent = false;
                                                        updateCollectionPrevoiusDate.IsCurrentCollection = true;
                                                        updateCollectionPrevoiusDate.IsProcessed = false;
                                                        updateCollectionPrevoiusDate.IsAction = true;
                                                        updateCollectionPrevoiusDate.IsPartialPayment = false;
                                                        updateCollectionPrevoiusDate.IsAdvancePayment = false;
                                                        updateCollectionPrevoiusDate.IsFullPayment = false;
                                                    }

                                                    updateCollectionPrevoiusDate.IsLastDay = true;
                                                    db.SubmitChanges();

                                                    db.trnCollections.DeleteOnSubmit(collection.First());
                                                    db.SubmitChanges();

                                                    var updateLoanApplicationFullPayment = loanApplication.FirstOrDefault();
                                                    updateLoanApplicationFullPayment.IsFullyPaid = false;
                                                    db.SubmitChanges();
                                                }
                                            }

                                            Business.Journal journal = new Business.Journal();
                                            journal.deleteCollectionJournal(Convert.ToInt32(id));

                                            return Request.CreateResponse(HttpStatusCode.OK);
                                        }
                                        else
                                        {
                                            return Request.CreateResponse(HttpStatusCode.BadRequest, "Can't process.");
                                        }
                                    }
                                    else
                                    {
                                        if (collection.FirstOrDefault().IsAction)
                                        {
                                            var collectionPrevoiusDate = from d in db.trnCollections where d.LoanId == Convert.ToInt32(loanId) && d.CollectionDate == collection.FirstOrDefault().CollectionDate.Date.AddDays(-Convert.ToInt32(termNoOfDays)) select d;
                                            if (collectionPrevoiusDate.Any())
                                            {
                                                var updateCollection = collection.FirstOrDefault();
                                                updateCollection.PaidAmount = 0;
                                                updateCollection.PreviousBalanceAmount = collectionPrevoiusDate.FirstOrDefault().CurrentBalanceAmount;
                                                updateCollection.CurrentBalanceAmount = collection.FirstOrDefault().CollectibleAmount + collectionPrevoiusDate.FirstOrDefault().CurrentBalanceAmount;
                                                updateCollection.PenaltyAmount = 0;
                                                updateCollection.IsCleared = false;
                                                updateCollection.IsAbsent = false;
                                                updateCollection.IsCurrentCollection = true;
                                                updateCollection.IsProcessed = false;
                                                updateCollection.IsAction = true;
                                                updateCollection.IsPartialPayment = false;
                                                updateCollection.IsAdvancePayment = false;
                                                updateCollection.IsFullPayment = false;
                                                db.SubmitChanges();

                                                var updateCollectionPrevoiusDate = collectionPrevoiusDate.FirstOrDefault();
                                                updateCollectionPrevoiusDate.IsAction = true;
                                                updateCollectionPrevoiusDate.IsCurrentCollection = false;
                                                db.SubmitChanges();

                                                var collectionNextDate = from d in db.trnCollections where d.LoanId == Convert.ToInt32(loanId) && d.CollectionDate == collection.FirstOrDefault().CollectionDate.Date.AddDays(Convert.ToInt32(termNoOfDays)) select d;
                                                if (collectionNextDate.Any())
                                                {
                                                    var updateCollectionNextDate = collectionNextDate.FirstOrDefault();
                                                    updateCollectionNextDate.PreviousBalanceAmount = 0;
                                                    updateCollectionNextDate.CurrentBalanceAmount = collectionNextDate.FirstOrDefault().CollectibleAmount;
                                                    updateCollectionNextDate.IsAction = false;
                                                    updateCollectionNextDate.IsCurrentCollection = false;
                                                    db.SubmitChanges();
                                                }
                                                else
                                                {
                                                    var updateLoanApplicationFullPayment = loanApplication.FirstOrDefault();
                                                    updateLoanApplicationFullPayment.IsFullyPaid = false;
                                                    db.SubmitChanges();
                                                }
                                            }
                                            else
                                            {
                                                var updateCollection = collection.FirstOrDefault();
                                                updateCollection.PaidAmount = 0;
                                                updateCollection.PreviousBalanceAmount = 0;
                                                updateCollection.CurrentBalanceAmount = collection.FirstOrDefault().CollectibleAmount;
                                                updateCollection.PenaltyAmount = 0;
                                                updateCollection.IsCleared = false;
                                                updateCollection.IsAbsent = false;
                                                updateCollection.IsCurrentCollection = true;
                                                updateCollection.IsProcessed = false;
                                                updateCollection.IsAction = true;
                                                updateCollection.IsPartialPayment = false;
                                                updateCollection.IsAdvancePayment = false;
                                                updateCollection.IsFullPayment = false;
                                                db.SubmitChanges();

                                                var collectionNextDate = from d in db.trnCollections where d.LoanId == Convert.ToInt32(loanId) && d.CollectionDate == collection.FirstOrDefault().CollectionDate.Date.AddDays(Convert.ToInt32(termNoOfDays)) select d;
                                                if (collectionNextDate.Any())
                                                {
                                                    var updateCollectionNextDate = collectionNextDate.FirstOrDefault();
                                                    updateCollectionNextDate.PreviousBalanceAmount = 0;
                                                    updateCollectionNextDate.CurrentBalanceAmount = collectionNextDate.FirstOrDefault().CollectibleAmount;
                                                    updateCollectionNextDate.IsAction = false;
                                                    updateCollectionNextDate.IsCurrentCollection = false;
                                                    db.SubmitChanges();
                                                }
                                                else
                                                {
                                                    var updateLoanApplicationFullPayment = loanApplication.FirstOrDefault();
                                                    updateLoanApplicationFullPayment.IsFullyPaid = false;
                                                    db.SubmitChanges();
                                                }
                                            }

                                            Business.Journal journal = new Business.Journal();
                                            journal.deleteCollectionJournal(Convert.ToInt32(id));

                                            return Request.CreateResponse(HttpStatusCode.OK);
                                        }
                                        else
                                        {
                                            return Request.CreateResponse(HttpStatusCode.BadRequest, "Can't process.");
                                        }
                                    }
                                }
                                else
                                {
                                    return Request.CreateResponse(HttpStatusCode.BadRequest, "No rights to perform some actions.");
                                }
                            }
                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.BadRequest, "No rights to access this page.");
                            }
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.NotFound, "No data found in the server.");
                        }
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest, "The loan appliction must be locked before proceeding the collection.");
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "No data found in the server.");
                }
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Oops! Something's went wrong from the server.");
            }
        }

        // partial payment collection
        [Authorize]
        [HttpPut]
        [Route("api/collection/partialPayment/update/byId/byLoanId/{id}/{loanId}")]
        public HttpResponseMessage partialPaymentCollection(String id, String loanId, Models.TrnCollection collection)
        {
            try
            {
                var loanApplication = from d in db.trnLoanApplications where d.Id == Convert.ToInt32(loanId) select d;
                if (loanApplication.Any())
                {
                    var termNoOfDays = loanApplication.FirstOrDefault().TermNoOfDays;
                    if (loanApplication.FirstOrDefault().IsLocked)
                    {
                        var collections = from d in db.trnCollections
                                          where d.Id == Convert.ToInt32(id)
                                          select d;

                        if (collections.Any())
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
                                String matchPageString = "CollectionDetail";
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
                                    if (collections.FirstOrDefault().IsCurrentCollection)
                                    {
                                        if (!collections.FirstOrDefault().IsDueDate)
                                        {
                                            if (collections.FirstOrDefault().CurrentBalanceAmount > 0)
                                            {
                                                if (!collections.FirstOrDefault().IsCleared && !collections.FirstOrDefault().IsAbsent)
                                                {
                                                    if (collections.FirstOrDefault().IsAction)
                                                    {
                                                        if (collection.PaidAmount > 0)
                                                        {
                                                            if (collections.FirstOrDefault().CurrentBalanceAmount >= collection.PaidAmount)
                                                            {
                                                                var isClearedValue = false;
                                                                var IsPartialPaymentValue = true;
                                                                if (collections.FirstOrDefault().CurrentBalanceAmount - collection.PaidAmount == 0)
                                                                {
                                                                    isClearedValue = true;
                                                                    IsPartialPaymentValue = false;
                                                                }

                                                                var updateCollection = collections.FirstOrDefault();
                                                                updateCollection.PaidAmount = collection.PaidAmount;
                                                                updateCollection.CurrentBalanceAmount = (collections.FirstOrDefault().CollectibleAmount + collections.FirstOrDefault().PreviousBalanceAmount) - collection.PaidAmount;
                                                                updateCollection.IsCleared = isClearedValue;
                                                                updateCollection.IsAbsent = false;
                                                                updateCollection.IsCurrentCollection = false;
                                                                updateCollection.IsProcessed = true;
                                                                updateCollection.IsPartialPayment = IsPartialPaymentValue;
                                                                updateCollection.IsAdvancePayment = false;
                                                                updateCollection.IsFullPayment = false;
                                                                db.SubmitChanges();

                                                                var collectionPrevoiusDate = from d in db.trnCollections where d.LoanId == Convert.ToInt32(loanId) && d.CollectionDate == collections.FirstOrDefault().CollectionDate.Date.AddDays(-Convert.ToInt32(termNoOfDays)) select d;
                                                                if (collectionPrevoiusDate.Any())
                                                                {
                                                                    var updateCollectionPrevoiusDate = collectionPrevoiusDate.FirstOrDefault();
                                                                    updateCollectionPrevoiusDate.IsAction = false;
                                                                    updateCollectionPrevoiusDate.IsCurrentCollection = false;
                                                                    db.SubmitChanges();

                                                                    var collectionNextDate = from d in db.trnCollections where d.LoanId == Convert.ToInt32(loanId) && d.CollectionDate == collections.FirstOrDefault().CollectionDate.Date.AddDays(Convert.ToInt32(termNoOfDays)) select d;
                                                                    if (collectionNextDate.Any())
                                                                    {
                                                                        var updateCollectionNextDate = collectionNextDate.FirstOrDefault();
                                                                        updateCollectionNextDate.PreviousBalanceAmount = (collections.FirstOrDefault().CollectibleAmount + collections.FirstOrDefault().PreviousBalanceAmount) - collection.PaidAmount;
                                                                        updateCollectionNextDate.CurrentBalanceAmount = collectionNextDate.FirstOrDefault().CollectibleAmount + ((collections.FirstOrDefault().CollectibleAmount + collections.FirstOrDefault().PreviousBalanceAmount) - collection.PaidAmount);
                                                                        updateCollectionNextDate.IsAction = true;
                                                                        updateCollectionNextDate.IsCurrentCollection = true;
                                                                        db.SubmitChanges();
                                                                    }
                                                                    else
                                                                    {
                                                                        if (collections.FirstOrDefault().CurrentBalanceAmount == 0)
                                                                        {
                                                                            var updateLoanApplicationFullPayment = loanApplication.FirstOrDefault();
                                                                            updateLoanApplicationFullPayment.IsFullyPaid = true;
                                                                            db.SubmitChanges();
                                                                        }
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    var collectionNextDate = from d in db.trnCollections where d.LoanId == Convert.ToInt32(loanId) && d.CollectionDate == collections.FirstOrDefault().CollectionDate.Date.AddDays(Convert.ToInt32(termNoOfDays)) select d;
                                                                    if (collectionNextDate.Any())
                                                                    {
                                                                        var updateCollectionNextDate = collectionNextDate.FirstOrDefault();
                                                                        updateCollectionNextDate.PreviousBalanceAmount = (collections.FirstOrDefault().CollectibleAmount + collections.FirstOrDefault().PreviousBalanceAmount) - collection.PaidAmount;
                                                                        updateCollectionNextDate.CurrentBalanceAmount = collectionNextDate.FirstOrDefault().CollectibleAmount + ((collections.FirstOrDefault().CollectibleAmount + collections.FirstOrDefault().PreviousBalanceAmount) - collection.PaidAmount);
                                                                        updateCollectionNextDate.IsAction = true;
                                                                        updateCollectionNextDate.IsCurrentCollection = true;
                                                                        db.SubmitChanges();
                                                                    }
                                                                    else
                                                                    {
                                                                        if (collections.FirstOrDefault().CurrentBalanceAmount == 0)
                                                                        {
                                                                            var updateLoanApplicationFullPayment = loanApplication.FirstOrDefault();
                                                                            updateLoanApplicationFullPayment.IsFullyPaid = true;
                                                                            db.SubmitChanges();
                                                                        }
                                                                    }
                                                                }

                                                                Business.Journal journal = new Business.Journal();
                                                                journal.postCollectionJournal(Convert.ToInt32(id));

                                                                return Request.CreateResponse(HttpStatusCode.OK);
                                                            }
                                                            else
                                                            {
                                                                return Request.CreateResponse(HttpStatusCode.BadRequest, "The amount must not be greater than the current balance amount.");
                                                            }
                                                        }
                                                        else
                                                        {
                                                            return Request.CreateResponse(HttpStatusCode.BadRequest, "Zero(0) amount. Please enter an amount.");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        return Request.CreateResponse(HttpStatusCode.BadRequest, "Can't process.");
                                                    }
                                                }
                                                else
                                                {
                                                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Already proceesed.");
                                                }
                                            }
                                            else
                                            {
                                                return Request.CreateResponse(HttpStatusCode.BadRequest, "No current balance.");
                                            }
                                        }
                                        else
                                        {
                                            if (collections.FirstOrDefault().IsOverdueCollection || collections.FirstOrDefault().IsExtendCollection)
                                            {
                                                if (!collections.FirstOrDefault().IsLastDay)
                                                {
                                                    Boolean isExtendCollection = true;
                                                    if (collections.FirstOrDefault().CollectionDate == loanApplication.FirstOrDefault().MaturityDate)
                                                    {
                                                        isExtendCollection = false;
                                                    }

                                                    if (collections.FirstOrDefault().CurrentBalanceAmount > 0)
                                                    {
                                                        if (!collections.FirstOrDefault().IsCleared && !collections.FirstOrDefault().IsAbsent)
                                                        {
                                                            if (collections.FirstOrDefault().IsAction)
                                                            {
                                                                if (collection.PaidAmount > 0)
                                                                {
                                                                    if (collections.FirstOrDefault().CurrentBalanceAmount >= collection.PaidAmount)
                                                                    {
                                                                        var isClearedValue = false;
                                                                        var IsPartialPaymentValue = true;
                                                                        if (collections.FirstOrDefault().CurrentBalanceAmount - collection.PaidAmount == 0)
                                                                        {
                                                                            isClearedValue = true;
                                                                            IsPartialPaymentValue = false;
                                                                        }

                                                                        var updateCollection = collections.FirstOrDefault();
                                                                        updateCollection.PaidAmount = collection.PaidAmount;
                                                                        updateCollection.CurrentBalanceAmount = (collections.FirstOrDefault().CollectibleAmount + collections.FirstOrDefault().PreviousBalanceAmount) - collection.PaidAmount;
                                                                        updateCollection.IsCleared = isClearedValue;
                                                                        updateCollection.IsAbsent = false;
                                                                        updateCollection.IsCurrentCollection = false;
                                                                        updateCollection.IsProcessed = true;
                                                                        updateCollection.IsPartialPayment = IsPartialPaymentValue;
                                                                        updateCollection.IsAdvancePayment = false;
                                                                        updateCollection.IsExtendCollection = isExtendCollection;
                                                                        updateCollection.IsOverdueCollection = false;
                                                                        updateCollection.IsFullPayment = false;
                                                                        db.SubmitChanges();

                                                                        var collectionPrevoiusDate = from d in db.trnCollections where d.LoanId == Convert.ToInt32(loanId) && d.CollectionDate == collections.FirstOrDefault().CollectionDate.Date.AddDays(-Convert.ToInt32(termNoOfDays)) select d;
                                                                        if (collectionPrevoiusDate.Any())
                                                                        {
                                                                            var updateCollectionPrevoiusDate = collectionPrevoiusDate.FirstOrDefault();
                                                                            updateCollectionPrevoiusDate.IsAction = false;
                                                                            updateCollectionPrevoiusDate.IsCurrentCollection = false;
                                                                            db.SubmitChanges();

                                                                            var collectionNextDate = from d in db.trnCollections where d.LoanId == Convert.ToInt32(loanId) && d.CollectionDate == collections.FirstOrDefault().CollectionDate.Date.AddDays(Convert.ToInt32(termNoOfDays)) select d;
                                                                            if (collectionNextDate.Any())
                                                                            {
                                                                                var updateCollectionNextDate = collectionNextDate.FirstOrDefault();
                                                                                updateCollectionNextDate.PreviousBalanceAmount = (collections.FirstOrDefault().CollectibleAmount + collections.FirstOrDefault().PreviousBalanceAmount) - collection.PaidAmount;
                                                                                updateCollectionNextDate.CurrentBalanceAmount = collectionNextDate.FirstOrDefault().CollectibleAmount + ((collections.FirstOrDefault().CollectibleAmount + collections.FirstOrDefault().PreviousBalanceAmount) - collection.PaidAmount);
                                                                                updateCollectionNextDate.IsAction = true;
                                                                                updateCollectionNextDate.IsCurrentCollection = true;
                                                                                db.SubmitChanges();
                                                                            }
                                                                            else
                                                                            {
                                                                                if (collections.FirstOrDefault().CurrentBalanceAmount == 0)
                                                                                {
                                                                                    var updateLoanApplicationFullPayment = loanApplication.FirstOrDefault();
                                                                                    updateLoanApplicationFullPayment.IsFullyPaid = true;
                                                                                    db.SubmitChanges();
                                                                                }
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            var collectionNextDate = from d in db.trnCollections where d.LoanId == Convert.ToInt32(loanId) && d.CollectionDate == collections.FirstOrDefault().CollectionDate.Date.AddDays(Convert.ToInt32(termNoOfDays)) select d;
                                                                            if (collectionNextDate.Any())
                                                                            {
                                                                                var updateCollectionNextDate = collectionNextDate.FirstOrDefault();
                                                                                updateCollectionNextDate.PreviousBalanceAmount = (collections.FirstOrDefault().CollectibleAmount + collections.FirstOrDefault().PreviousBalanceAmount) - collection.PaidAmount;
                                                                                updateCollectionNextDate.CurrentBalanceAmount = collectionNextDate.FirstOrDefault().CollectibleAmount + ((collections.FirstOrDefault().CollectibleAmount + collections.FirstOrDefault().PreviousBalanceAmount) - collection.PaidAmount);
                                                                                updateCollectionNextDate.IsAction = true;
                                                                                updateCollectionNextDate.IsCurrentCollection = true;
                                                                                db.SubmitChanges();
                                                                            }
                                                                            else
                                                                            {
                                                                                if (collections.FirstOrDefault().CurrentBalanceAmount == 0)
                                                                                {
                                                                                    var updateLoanApplicationFullPayment = loanApplication.FirstOrDefault();
                                                                                    updateLoanApplicationFullPayment.IsFullyPaid = true;
                                                                                    db.SubmitChanges();
                                                                                }
                                                                            }
                                                                        }

                                                                        Business.Journal journal = new Business.Journal();
                                                                        journal.postCollectionJournal(Convert.ToInt32(id));

                                                                        return Request.CreateResponse(HttpStatusCode.OK);
                                                                    }
                                                                    else
                                                                    {
                                                                        return Request.CreateResponse(HttpStatusCode.BadRequest, "The amount must not be greater than the current balance amount.");
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Zero(0) amount. Please enter an amount.");
                                                                }
                                                            }
                                                            else
                                                            {
                                                                return Request.CreateResponse(HttpStatusCode.BadRequest, "Can't process.");
                                                            }
                                                        }
                                                        else
                                                        {
                                                            return Request.CreateResponse(HttpStatusCode.BadRequest, "Already processed.");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        return Request.CreateResponse(HttpStatusCode.BadRequest, "No current balance.");
                                                    }
                                                }
                                                else
                                                {
                                                    return Request.CreateResponse(HttpStatusCode.BadRequest, "The current collection is due date. Please do extensions.");
                                                }
                                            }
                                            else
                                            {
                                                return Request.CreateResponse(HttpStatusCode.BadRequest, "The current collection is due date. Please do extensions.");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        return Request.CreateResponse(HttpStatusCode.BadRequest, "Not a current collection.");
                                    }
                                }
                                else
                                {
                                    return Request.CreateResponse(HttpStatusCode.BadRequest, "No rights to perform some actions.");
                                }
                            }
                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.BadRequest, "No rights to access this page.");
                            }
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.NotFound, "No data found in the server.");
                        }
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest, "The loan appliction must be locked before proceeding the collection.");
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "No data found in the server.");
                }
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Oops! Something's went wrong from the server.");
            }
        }

        // advance payment collection
        [Authorize]
        [HttpPut]
        [Route("api/collection/advancePayment/update/byId/byLoanId/{id}/{loanId}")]
        public HttpResponseMessage advancePaymentCollection(String id, String loanId)
        {
            try
            {
                var loanApplication = from d in db.trnLoanApplications where d.Id == Convert.ToInt32(loanId) select d;
                if (loanApplication.Any())
                {
                    var termNoOfDays = loanApplication.FirstOrDefault().TermNoOfDays;
                    if (loanApplication.FirstOrDefault().IsLocked)
                    {
                        var currentCollection = from d in db.trnCollections
                                                where d.IsCurrentCollection == true
                                                && d.LoanId == Convert.ToInt32(loanId)
                                                select d;

                        if (currentCollection.Any())
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
                                String matchPageString = "CollectionDetail";
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
                                    var advancePaymentCollections = from d in db.trnCollections
                                                                    where d.IsProcessed == false
                                                                    && d.Id >= currentCollection.FirstOrDefault().Id
                                                                    && d.Id <= Convert.ToInt32(id)
                                                                    && d.LoanId == Convert.ToInt32(loanId)
                                                                    select new Models.TrnCollection
                                                                    {
                                                                        Id = d.Id,
                                                                        CollectionDate = d.CollectionDate.ToShortDateString(),
                                                                        CollectibleAmount = d.CollectibleAmount,
                                                                        PreviousBalanceAmount = d.PreviousBalanceAmount,
                                                                        CurrentBalanceAmount = d.CurrentBalanceAmount
                                                                    };

                                    if (advancePaymentCollections.Any())
                                    {
                                        foreach (var advancePaymentCollection in advancePaymentCollections)
                                        {
                                            var collectionForUpdate = from d in db.trnCollections where d.Id == advancePaymentCollection.Id select d;
                                            if (collectionForUpdate.Any())
                                            {
                                                var updateCollection = collectionForUpdate.FirstOrDefault();
                                                updateCollection.PaidAmount = advancePaymentCollection.CollectibleAmount + advancePaymentCollection.PreviousBalanceAmount;
                                                updateCollection.PreviousBalanceAmount = 0;
                                                updateCollection.CurrentBalanceAmount = 0;
                                                updateCollection.PenaltyAmount = 0;
                                                updateCollection.IsCleared = true;
                                                updateCollection.IsAbsent = false;
                                                updateCollection.IsProcessed = true;
                                                updateCollection.IsCurrentCollection = false;
                                                updateCollection.IsPartialPayment = false;
                                                updateCollection.IsAdvancePayment = true;
                                                updateCollection.IsFullPayment = false;
                                                db.SubmitChanges();

                                                var collectionPrevoiusDate = from d in db.trnCollections where d.LoanId == Convert.ToInt32(loanId) && d.CollectionDate == Convert.ToDateTime(advancePaymentCollection.CollectionDate).Date.AddDays(-Convert.ToInt32(termNoOfDays)) select d;
                                                if (collectionPrevoiusDate.Any())
                                                {
                                                    var updateCollectionPrevoiusDate = collectionPrevoiusDate.FirstOrDefault();
                                                    updateCollectionPrevoiusDate.IsAction = false;
                                                    updateCollectionPrevoiusDate.IsCurrentCollection = false;
                                                    db.SubmitChanges();

                                                    var collectionNextDate = from d in db.trnCollections where d.LoanId == Convert.ToInt32(loanId) && d.CollectionDate == Convert.ToDateTime(advancePaymentCollection.CollectionDate).Date.AddDays(Convert.ToInt32(termNoOfDays)) select d;
                                                    if (collectionNextDate.Any())
                                                    {
                                                        var updateCollectionNextDate = collectionNextDate.FirstOrDefault();
                                                        updateCollectionNextDate.IsAction = true;
                                                        updateCollectionNextDate.IsCurrentCollection = true;
                                                        db.SubmitChanges();
                                                    }
                                                    else
                                                    {
                                                        var updateLoanApplicationFullPayment = loanApplication.FirstOrDefault();
                                                        updateLoanApplicationFullPayment.IsFullyPaid = true;
                                                        db.SubmitChanges();
                                                    }
                                                }
                                                else
                                                {
                                                    var collectionNextDate = from d in db.trnCollections where d.LoanId == Convert.ToInt32(loanId) && d.CollectionDate == Convert.ToDateTime(advancePaymentCollection.CollectionDate).Date.AddDays(Convert.ToInt32(termNoOfDays)) select d;
                                                    if (collectionNextDate.Any())
                                                    {
                                                        var updateCollectionNextDate = collectionNextDate.FirstOrDefault();
                                                        updateCollectionNextDate.IsAction = true;
                                                        updateCollectionNextDate.IsCurrentCollection = true;
                                                        db.SubmitChanges();
                                                    }
                                                    else
                                                    {
                                                        var updateLoanApplicationFullPayment = loanApplication.FirstOrDefault();
                                                        updateLoanApplicationFullPayment.IsFullyPaid = true;
                                                        db.SubmitChanges();
                                                    }
                                                }
                                            }

                                            Business.Journal journal = new Business.Journal();
                                            journal.postCollectionJournal(advancePaymentCollection.Id);
                                        }

                                        return Request.CreateResponse(HttpStatusCode.OK);
                                    }
                                    else
                                    {
                                        return Request.CreateResponse(HttpStatusCode.BadRequest, "No Data found from the server.");
                                    }
                                }
                                else
                                {
                                    return Request.CreateResponse(HttpStatusCode.BadRequest, "No rights to perform some actions.");
                                }
                            }
                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.BadRequest, "No rights to access this page.");
                            }
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.BadRequest, "No current collection.");
                        }
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest, "The loan appliction must be locked before proceeding the collection.");
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "No data found in the server.");
                }
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Oops! Something's went wrong from the server.");
            }
        }

        // full payment collection
        [Authorize]
        [HttpPut]
        [Route("api/collection/fullPayment/update/byId/byLoanId/{id}/{loanId}")]
        public HttpResponseMessage fullPaymentCollection(String id, String loanId)
        {
            try
            {
                var loanApplication = from d in db.trnLoanApplications where d.Id == Convert.ToInt32(loanId) select d;
                if (loanApplication.Any())
                {
                    var termNoOfDays = loanApplication.FirstOrDefault().TermNoOfDays;
                    if (loanApplication.FirstOrDefault().IsLocked)
                    {
                        var currentCollection = from d in db.trnCollections
                                                where d.IsCurrentCollection == true
                                                && d.LoanId == Convert.ToInt32(loanId)
                                                select d;

                        if (currentCollection.Any())
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
                                String matchPageString = "CollectionDetail";
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
                                    var fullPaymentCollections = from d in db.trnCollections
                                                                 where d.IsProcessed == false
                                                                 && d.LoanId == Convert.ToInt32(loanId)
                                                                 && d.Id >= currentCollection.FirstOrDefault().Id
                                                                 select new Models.TrnCollection
                                                                 {
                                                                     Id = d.Id,
                                                                     CollectionDate = d.CollectionDate.ToShortDateString(),
                                                                     CollectibleAmount = d.CollectibleAmount,
                                                                     PreviousBalanceAmount = d.PreviousBalanceAmount,
                                                                     CurrentBalanceAmount = d.CurrentBalanceAmount
                                                                 };

                                    if (fullPaymentCollections.Any())
                                    {
                                        foreach (var fullPaymentCollection in fullPaymentCollections)
                                        {
                                            var collectionForUpdate = from d in db.trnCollections where d.Id == fullPaymentCollection.Id select d;
                                            if (collectionForUpdate.Any())
                                            {
                                                var updateCollection = collectionForUpdate.FirstOrDefault();
                                                updateCollection.PaidAmount = fullPaymentCollection.CollectibleAmount + fullPaymentCollection.PreviousBalanceAmount;
                                                updateCollection.PreviousBalanceAmount = 0;
                                                updateCollection.CurrentBalanceAmount = 0;
                                                updateCollection.PenaltyAmount = 0;
                                                updateCollection.IsCleared = true;
                                                updateCollection.IsAbsent = false;
                                                updateCollection.IsProcessed = true;
                                                updateCollection.IsCurrentCollection = false;
                                                updateCollection.IsPartialPayment = false;
                                                updateCollection.IsAdvancePayment = false;
                                                updateCollection.IsFullPayment = true;
                                                db.SubmitChanges();

                                                var collectionPrevoiusDate = from d in db.trnCollections where d.LoanId == Convert.ToInt32(loanId) && d.CollectionDate == Convert.ToDateTime(fullPaymentCollection.CollectionDate).Date.AddDays(-Convert.ToInt32(termNoOfDays)) select d;
                                                if (collectionPrevoiusDate.Any())
                                                {
                                                    var updateCollectionPrevoiusDate = collectionPrevoiusDate.FirstOrDefault();
                                                    updateCollectionPrevoiusDate.IsAction = false;
                                                    updateCollectionPrevoiusDate.IsCurrentCollection = false;
                                                    db.SubmitChanges();

                                                    var collectionNextDate = from d in db.trnCollections where d.LoanId == Convert.ToInt32(loanId) && d.CollectionDate == Convert.ToDateTime(fullPaymentCollection.CollectionDate).Date.AddDays(Convert.ToInt32(termNoOfDays)) select d;
                                                    if (collectionNextDate.Any())
                                                    {
                                                        var updateCollectionNextDate = collectionNextDate.FirstOrDefault();
                                                        updateCollectionNextDate.IsAction = true;
                                                        updateCollectionNextDate.IsCurrentCollection = true;
                                                        db.SubmitChanges();
                                                    }
                                                    else
                                                    {
                                                        var updateLoanApplicationFullPayment = loanApplication.FirstOrDefault();
                                                        updateLoanApplicationFullPayment.IsFullyPaid = true;
                                                        db.SubmitChanges();
                                                    }
                                                }
                                                else
                                                {
                                                    var collectionNextDate = from d in db.trnCollections where d.LoanId == Convert.ToInt32(loanId) && d.CollectionDate == Convert.ToDateTime(fullPaymentCollection.CollectionDate).Date.AddDays(Convert.ToInt32(termNoOfDays)) select d;
                                                    if (collectionNextDate.Any())
                                                    {
                                                        var updateCollectionNextDate = collectionNextDate.FirstOrDefault();
                                                        updateCollectionNextDate.IsAction = true;
                                                        updateCollectionNextDate.IsCurrentCollection = true;
                                                        db.SubmitChanges();
                                                    }
                                                    else
                                                    {
                                                        var updateLoanApplicationFullPayment = loanApplication.FirstOrDefault();
                                                        updateLoanApplicationFullPayment.IsFullyPaid = true;
                                                        db.SubmitChanges();
                                                    }
                                                }
                                            }

                                            Business.Journal journal = new Business.Journal();
                                            journal.postCollectionJournal(fullPaymentCollection.Id);
                                        }

                                        return Request.CreateResponse(HttpStatusCode.OK);
                                    }
                                    else
                                    {
                                        return Request.CreateResponse(HttpStatusCode.BadRequest, "No Data found from the server.");
                                    }
                                }
                                else
                                {
                                    return Request.CreateResponse(HttpStatusCode.BadRequest, "No rights to perform some actions.");
                                }
                            }
                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.BadRequest, "No rights to access this page.");
                            }
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.BadRequest, "No current collection.");
                        }
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest, "The loan appliction must be locked before proceeding the collection.");
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "No data found in the server to apply some actions.");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Oops! Something's went wrong from the server.");
            }
        }

        // extend collection
        [Authorize]
        [HttpPut]
        [Route("api/collection/extend/update/byId/byLoanId/{id}/{loanId}/{noOfDays}/{dailyCollectibles}")]
        public HttpResponseMessage extendCollection(String id, String loanId, String noOfDays, String dailyCollectibles)
        {
            try
            {
                var loanApplication = from d in db.trnLoanApplications where d.Id == Convert.ToInt32(loanId) select d;
                if (loanApplication.Any())
                {
                    if (loanApplication.FirstOrDefault().IsLocked)
                    {
                        var collection = from d in db.trnCollections
                                         where d.Id == Convert.ToInt32(id)
                                         select d;

                        if (!loanApplication.FirstOrDefault().IsFullyPaid)
                        {
                            if (collection.Any())
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
                                    String matchPageString = "CollectionDetail";
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
                                        if (collection.FirstOrDefault().IsCurrentCollection)
                                        {
                                            if (collection.FirstOrDefault().IsLastDay)
                                            {
                                                if (Convert.ToInt32(noOfDays) <= 5)
                                                {
                                                    var updateCollection = collection.FirstOrDefault();
                                                    updateCollection.IsLastDay = false;
                                                    db.SubmitChanges();

                                                    for (Int32 i = 1; i <= Convert.ToInt32(noOfDays); i++)
                                                    {
                                                        Boolean isLastDay = false;
                                                        if (i == Convert.ToInt32(noOfDays))
                                                        {
                                                            isLastDay = true;
                                                        }

                                                        Data.trnCollection newCollection = new Data.trnCollection();
                                                        newCollection.LoanId = collection.FirstOrDefault().LoanId;
                                                        newCollection.AccountId = (from d in db.mstAccounts where d.AccountTransactionTypeId == 2 select d.Id).FirstOrDefault();
                                                        newCollection.CollectionDate = Convert.ToDateTime(collection.FirstOrDefault().CollectionDate).Date.AddDays(i);
                                                        newCollection.NetAmount = collection.FirstOrDefault().NetAmount;
                                                        newCollection.CollectibleAmount = Convert.ToDecimal(dailyCollectibles);
                                                        newCollection.PenaltyAmount = 0;
                                                        newCollection.PaidAmount = 0;
                                                        newCollection.PreviousBalanceAmount = 0;
                                                        newCollection.CurrentBalanceAmount = Convert.ToDecimal(dailyCollectibles);
                                                        newCollection.IsCleared = false;
                                                        newCollection.IsAbsent = false;
                                                        newCollection.IsPartialPayment = false;
                                                        newCollection.IsAdvancePayment = false;
                                                        newCollection.IsFullPayment = false;
                                                        newCollection.IsDueDate = false;
                                                        newCollection.IsExtendCollection = true;
                                                        newCollection.IsOverdueCollection = false;
                                                        newCollection.IsCurrentCollection = false;
                                                        newCollection.IsProcessed = false;
                                                        newCollection.IsAction = false;
                                                        newCollection.IsLastDay = isLastDay;
                                                        db.trnCollections.InsertOnSubmit(newCollection);
                                                        db.SubmitChanges();
                                                    }

                                                    return Request.CreateResponse(HttpStatusCode.OK);
                                                }
                                                else
                                                {
                                                    return Request.CreateResponse(HttpStatusCode.BadRequest, "The number of days must not be greater than five (5).");
                                                }
                                            }
                                            else
                                            {
                                                return Request.CreateResponse(HttpStatusCode.BadRequest, "The current collection is not yet on the due date.");
                                            }
                                        }
                                        else
                                        {
                                            return Request.CreateResponse(HttpStatusCode.BadRequest, "No Current Collection.");
                                        }
                                    }
                                    else
                                    {
                                        return Request.CreateResponse(HttpStatusCode.BadRequest, "No rights to perform some actions.");
                                    }
                                }
                                else
                                {
                                    return Request.CreateResponse(HttpStatusCode.BadRequest, "No rights to access this page.");
                                }
                            }
                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.BadRequest, "No data found in the server.");
                            }
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.NotFound, "No data found in the server.");
                        }
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest, "The loan appliction must be locked before proceeding the collection.");
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "No data found in the server.");
                }
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Oops! Something's went wrong from the server.");
            }
        }

        // overdue collection
        [Authorize]
        [HttpPut]
        [Route("api/collection/overdue/update/byId/byLoanId/{id}/{loanId}")]
        public HttpResponseMessage overdueCollection(String id, String loanId)
        {
            try
            {
                var loanApplication = from d in db.trnLoanApplications where d.Id == Convert.ToInt32(loanId) select d;
                if (loanApplication.Any())
                {
                    var termNoOfDays = loanApplication.FirstOrDefault().TermNoOfDays;
                    if (loanApplication.FirstOrDefault().IsLocked)
                    {
                        var collection = from d in db.trnCollections
                                         where d.Id == Convert.ToInt32(id)
                                         select d;

                        if (!loanApplication.FirstOrDefault().IsFullyPaid)
                        {
                            if (collection.Any())
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
                                    String matchPageString = "CollectionDetail";
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
                                        if (collection.FirstOrDefault().IsCurrentCollection)
                                        {
                                            if (collection.FirstOrDefault().IsLastDay)
                                            {
                                                var updateCollection = collection.FirstOrDefault();
                                                updateCollection.IsLastDay = false;
                                                db.SubmitChanges();

                                                Data.trnCollection newCollection = new Data.trnCollection();
                                                newCollection.LoanId = collection.FirstOrDefault().LoanId;
                                                newCollection.AccountId = (from d in db.mstAccounts where d.AccountTransactionTypeId == 2 select d.Id).FirstOrDefault();
                                                newCollection.CollectionDate = Convert.ToDateTime(collection.FirstOrDefault().CollectionDate).Date.AddDays(Convert.ToInt32(termNoOfDays));
                                                newCollection.NetAmount = collection.FirstOrDefault().NetAmount;
                                                newCollection.CollectibleAmount = collection.FirstOrDefault().CollectibleAmount;
                                                newCollection.PenaltyAmount = 0;
                                                newCollection.PaidAmount = 0;
                                                newCollection.PreviousBalanceAmount = 0;
                                                newCollection.CurrentBalanceAmount = collection.FirstOrDefault().CurrentBalanceAmount;
                                                newCollection.IsCleared = false;
                                                newCollection.IsAbsent = false;
                                                newCollection.IsPartialPayment = false;
                                                newCollection.IsAdvancePayment = false;
                                                newCollection.IsFullPayment = false;
                                                newCollection.IsDueDate = false;
                                                newCollection.IsExtendCollection = false;
                                                newCollection.IsOverdueCollection = true;
                                                newCollection.IsCurrentCollection = false;
                                                newCollection.IsProcessed = false;
                                                newCollection.IsAction = false;
                                                newCollection.IsLastDay = true;
                                                db.trnCollections.InsertOnSubmit(newCollection);
                                                db.SubmitChanges();

                                                return Request.CreateResponse(HttpStatusCode.OK);
                                            }
                                            else
                                            {
                                                return Request.CreateResponse(HttpStatusCode.BadRequest, "Can't process.");
                                            }
                                        }
                                        else
                                        {
                                            return Request.CreateResponse(HttpStatusCode.BadRequest, "No Current Collection.");
                                        }
                                    }
                                    else
                                    {
                                        return Request.CreateResponse(HttpStatusCode.BadRequest, "No rights to perform some actions.");
                                    }
                                }
                                else
                                {
                                    return Request.CreateResponse(HttpStatusCode.BadRequest, "No rights to access this page.");
                                }
                            }
                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.BadRequest, "No data found in the server.");
                            }
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.NotFound, "No data found in the server.");
                        }
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest, "The loan appliction must be locked before proceeding the collection.");
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "No data found in the server.");
                }
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Oops! Something's went wrong from the server.");
            }
        }
    }
}
