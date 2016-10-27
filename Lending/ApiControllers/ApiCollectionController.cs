﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Lending.ApiControllers
{
    public class ApiCollectionController : ApiController
    {
        // data
        private Data.LendingDataContext db = new Data.LendingDataContext();

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
                                  Applicant = d.trnLoanApplication.mstApplicant.ApplicantLastName + ", " + d.trnLoanApplication.mstApplicant.ApplicantFirstName + " " + d.trnLoanApplication.mstApplicant.ApplicantMiddleName,
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
                                  IsAdvancedPayment = d.IsAdvancedPayment,
                                  IsDueDate = d.IsDueDate,
                                  IsOverdue = d.IsOverdue,
                                  IsExtended = d.IsExtended,
                                  IsCurrentCollection = d.IsCurrentCollection,
                                  IsProcessed = d.IsProcessed,
                                  IsAction = d.IsAction,
                                  AssignedCollectorId = d.trnLoanApplication.AssignedCollectorId,
                                  AssignedCollector = d.trnLoanApplication.mstCollector.Collector,
                                  AssignedCollectorArea = d.trnLoanApplication.mstCollector.Collector + " (" + d.trnLoanApplication.mstCollector.mstArea.Area + ")",
                                  CurrentCollectorId = d.trnLoanApplication.CurrentCollectorId,
                                  CurrentCollector = d.trnLoanApplication.mstCollector1.Collector,
                                  CurrentCollectorArea = d.trnLoanApplication.mstCollector1.Collector + " (" + d.trnLoanApplication.mstCollector1.mstArea.Area + ")"
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
                                       Applicant = d.trnLoanApplication.mstApplicant.ApplicantLastName + ", " + d.trnLoanApplication.mstApplicant.ApplicantFirstName + " " + d.trnLoanApplication.mstApplicant.ApplicantMiddleName,
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
                                       IsAdvancedPayment = d.IsAdvancedPayment,
                                       IsDueDate = d.IsDueDate,
                                       IsOverdue = d.IsOverdue,
                                       IsExtended = d.IsExtended,
                                       IsCurrentCollection = d.IsCurrentCollection,
                                       IsProcessed = d.IsProcessed,
                                       IsAction = d.IsAction,
                                       AssignedCollectorId = d.trnLoanApplication.AssignedCollectorId,
                                       AssignedCollector = d.trnLoanApplication.mstCollector.Collector,
                                       AssignedCollectorArea = d.trnLoanApplication.mstCollector.Collector + " (" + d.trnLoanApplication.mstCollector.mstArea.Area + ")",
                                       CurrentCollectorId = d.trnLoanApplication.CurrentCollectorId,
                                       CurrentCollector = d.trnLoanApplication.mstCollector1.Collector,
                                       CurrentCollectorArea = d.trnLoanApplication.mstCollector1.Collector + " (" + d.trnLoanApplication.mstCollector1.mstArea.Area + ")"
                                   };

            return collections.ToList();
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
                        var collection = from d in db.trnCollections where d.Id == Convert.ToInt32(id) select d;
                        if (collection.Any())
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
                                        db.SubmitChanges();

                                        var collectionPrevoiusDate = from d in db.trnCollections where d.LoanId == Convert.ToInt32(loanId) && d.CollectionDate == collection.FirstOrDefault().CollectionDate.Date.AddDays(-1) select d;
                                        if (collectionPrevoiusDate.Any())
                                        {
                                            var updateCollectionPrevoiusDate = collectionPrevoiusDate.FirstOrDefault();
                                            updateCollectionPrevoiusDate.IsAction = false;
                                            updateCollectionPrevoiusDate.IsCurrentCollection = false;
                                            db.SubmitChanges();

                                            var collectionNextDate = from d in db.trnCollections where d.LoanId == Convert.ToInt32(loanId) && d.CollectionDate == collection.FirstOrDefault().CollectionDate.Date.AddDays(1) select d;
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
                                            var collectionNextDate = from d in db.trnCollections where d.LoanId == Convert.ToInt32(loanId) && d.CollectionDate == collection.FirstOrDefault().CollectionDate.Date.AddDays(1) select d;
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

                                        return Request.CreateResponse(HttpStatusCode.OK);
                                    }
                                    else
                                    {
                                        return Request.CreateResponse(HttpStatusCode.BadRequest, "Cannot apply actions by this time.");
                                    }
                                }
                                else
                                {
                                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Collection was already cleared.");
                                }
                            }
                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.BadRequest, "No current balance to be cleared.");
                            }
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.NotFound, "Sorry, but there are no data found in the server.");
                        }
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest, "Please lock the loan application first before procceding the collection process.");
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Sorry, but there are no data found in the server.");
                }
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Oops! Something went wrong from the server.");
            }
        }

        // get penalty amount
        public Decimal getPenaltyAmount(Int32 loanId, String collectionDate)
        {
            Decimal penaltyAmount = 10;
            var previousCollectionDay = from d in db.trnCollections
                                        where d.LoanId == loanId
                                        && d.CollectionDate == Convert.ToDateTime(collectionDate).Date.AddDays(-1)
                                        select d;

            if (previousCollectionDay.Any())
            {
                if (previousCollectionDay.FirstOrDefault().IsAbsent)
                {
                    if (previousCollectionDay.FirstOrDefault().PenaltyAmount == 10)
                    {
                        var previousCollectionDay2 = from d in db.trnCollections
                                                     where d.LoanId == loanId
                                                     && d.CollectionDate == Convert.ToDateTime(collectionDate).Date.AddDays(-2)
                                                     select d;

                        if (previousCollectionDay2.Any())
                        {
                            if (previousCollectionDay2.FirstOrDefault().IsAbsent)
                            {
                                if (previousCollectionDay2.FirstOrDefault().PenaltyAmount == 10)
                                {
                                    penaltyAmount = 20;
                                }
                                else
                                {
                                    penaltyAmount = 10;
                                }
                            }
                        }
                    }
                }
            }

            return penaltyAmount;
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
                        var collection = from d in db.trnCollections where d.Id == Convert.ToInt32(id) select d;
                        if (collection.Any())
                        {
                            if (!collection.FirstOrDefault().IsAbsent)
                            {
                                if (collection.FirstOrDefault().IsAction)
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
                                    db.SubmitChanges();

                                    var collectionPrevoiusDate = from d in db.trnCollections where d.LoanId == Convert.ToInt32(loanId) && d.CollectionDate == collection.FirstOrDefault().CollectionDate.Date.AddDays(-1) select d;
                                    if (collectionPrevoiusDate.Any())
                                    {
                                        var updateCollectionPrevoiusDate = collectionPrevoiusDate.FirstOrDefault();
                                        updateCollectionPrevoiusDate.IsAction = false;
                                        updateCollectionPrevoiusDate.IsCurrentCollection = false;
                                        db.SubmitChanges();

                                        var collectionNextDate = from d in db.trnCollections where d.LoanId == Convert.ToInt32(loanId) && d.CollectionDate == collection.FirstOrDefault().CollectionDate.Date.AddDays(1) select d;
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
                                        var collectionNextDate = from d in db.trnCollections where d.LoanId == Convert.ToInt32(loanId) && d.CollectionDate == collection.FirstOrDefault().CollectionDate.Date.AddDays(1) select d;
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

                                    return Request.CreateResponse(HttpStatusCode.OK);
                                }
                                else
                                {
                                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Cannot apply actions by this time.");
                                }
                            }
                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.BadRequest, "Collection was already absent.");
                            }
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.NotFound, "Sorry, but there are no data found in the server.");
                        }
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest, "Please lock the loan application first before procceding the collection process.");
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Sorry, but there are no data found in the server.");
                }
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Oops! Something went wrong from the server.");
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
                            if (collection.FirstOrDefault().IsAction)
                            {
                                var collectionPrevoiusDate = from d in db.trnCollections where d.LoanId == Convert.ToInt32(loanId) && d.CollectionDate == collection.FirstOrDefault().CollectionDate.Date.AddDays(-1) select d;
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
                                    updateCollection.IsProcessed = true;
                                    updateCollection.IsAction = true;
                                    db.SubmitChanges();

                                    var updateCollectionPrevoiusDate = collectionPrevoiusDate.FirstOrDefault();
                                    updateCollectionPrevoiusDate.IsAction = true;
                                    updateCollectionPrevoiusDate.IsCurrentCollection = false;
                                    db.SubmitChanges();

                                    var collectionNextDate = from d in db.trnCollections where d.LoanId == Convert.ToInt32(loanId) && d.CollectionDate == collection.FirstOrDefault().CollectionDate.Date.AddDays(1) select d;
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
                                    updateCollection.IsProcessed = true;
                                    updateCollection.IsAction = true;
                                    db.SubmitChanges();

                                    var collectionNextDate = from d in db.trnCollections where d.LoanId == Convert.ToInt32(loanId) && d.CollectionDate == collection.FirstOrDefault().CollectionDate.Date.AddDays(1) select d;
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

                                return Request.CreateResponse(HttpStatusCode.OK);
                            }
                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.BadRequest, "Cannot apply actions by this time.");
                            }
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.NotFound, "Sorry, but there are no data found in the server.");
                        }
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest, "Please lock the loan application first before procceding the collection process.");
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Sorry, but there are no data found in the server.");
                }
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Oops! Something went wrong from the server.");
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
                    if (loanApplication.FirstOrDefault().IsLocked)
                    {
                        var collections = from d in db.trnCollections where d.Id == Convert.ToInt32(id) select d;
                        if (collections.Any())
                        {
                            if (collections.FirstOrDefault().CurrentBalanceAmount > 0)
                            {
                                if (!collections.FirstOrDefault().IsCleared && !collections.FirstOrDefault().IsAbsent)
                                {
                                    if (collections.FirstOrDefault().IsAction)
                                    {
                                        if (collections.FirstOrDefault().CurrentBalanceAmount >= collection.PaidAmount)
                                        {
                                            var isClearedValue = false;
                                            if (collections.FirstOrDefault().CurrentBalanceAmount - collection.PaidAmount == 0)
                                            {
                                                isClearedValue = true;
                                            }

                                            var updateCollection = collections.FirstOrDefault();
                                            updateCollection.PaidAmount = collection.PaidAmount;
                                            updateCollection.CurrentBalanceAmount = (collections.FirstOrDefault().CollectibleAmount + collections.FirstOrDefault().PreviousBalanceAmount) - collection.PaidAmount;
                                            updateCollection.IsCleared = isClearedValue;
                                            updateCollection.IsAbsent = false;
                                            updateCollection.IsPartialPayment = true;
                                            updateCollection.IsCurrentCollection = false;
                                            updateCollection.IsProcessed = true;
                                            updateCollection.IsAction = false;
                                            db.SubmitChanges();

                                            var collectionPrevoiusDate = from d in db.trnCollections where d.LoanId == Convert.ToInt32(loanId) && d.CollectionDate == collections.FirstOrDefault().CollectionDate.Date.AddDays(-1) select d;
                                            if (collectionPrevoiusDate.Any())
                                            {
                                                var updateCollectionPrevoiusDate = collectionPrevoiusDate.FirstOrDefault();
                                                updateCollectionPrevoiusDate.IsAction = false;
                                                updateCollectionPrevoiusDate.IsCurrentCollection = false;
                                                db.SubmitChanges();

                                                var collectionNextDate = from d in db.trnCollections where d.LoanId == Convert.ToInt32(loanId) && d.CollectionDate == collections.FirstOrDefault().CollectionDate.Date.AddDays(1) select d;
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
                                                var collectionNextDate = from d in db.trnCollections where d.LoanId == Convert.ToInt32(loanId) && d.CollectionDate == collections.FirstOrDefault().CollectionDate.Date.AddDays(1) select d;
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

                                            return Request.CreateResponse(HttpStatusCode.OK);
                                        }
                                        else
                                        {
                                            return Request.CreateResponse(HttpStatusCode.BadRequest, "The amount to be paid must not be greater than the current balance amount.");
                                        }
                                    }
                                    else
                                    {
                                        return Request.CreateResponse(HttpStatusCode.BadRequest, "Cannot apply actions by this time.");
                                    }
                                }
                                else
                                {
                                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Collection actions has already been applied");
                                }
                            }
                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.BadRequest, "No current balance.");
                            }
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.NotFound, "Sorry, but there are no data found in the server.");
                        }
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest, "Please lock the loan application first before procceding the collection process.");
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Sorry, but there are no data found in the server.");
                }
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Oops! Something went wrong from the server.");
            }
        }

        //public Decimal getAdvancePaymentTotalCurrentBalance(Int32 loanId, String collectionStarDate, String collectionEndDate)
        //{
        //    var collectionDateSequences = from d in db.trnCollections
        //                                  where d.LoanId == Convert.ToInt32(loanId)
        //                                  && d.CollectionDate >= Convert.ToDateTime(collectionStarDate)
        //                                  && d.CollectionDate <= Convert.ToDateTime(collectionEndDate)
        //                                  select d;

        //    Decimal currentBalanceAmount = 0;
        //    if (collectionDateSequences.Any())
        //    {
        //        currentBalanceAmount =  collectionDateSequences.Sum(d => d.CurrentBalanceAmount);
        //    }

        //    return currentBalanceAmount;
        //}

        // advance payment collection
        [Authorize]
        [HttpPut]
        [Route("api/collection/advancePayment/update/byId/byLoanId/{id}/{loanId}")]
        public HttpResponseMessage advancePaymentCollection(String id, String loanId, Models.TrnCollection collection)
        {
            try
            {
                var loanApplication = from d in db.trnLoanApplications where d.Id == Convert.ToInt32(loanId) select d;
                if (loanApplication.Any())
                {
                    if (loanApplication.FirstOrDefault().IsLocked)
                    {
                        var collections = from d in db.trnCollections where d.Id == Convert.ToInt32(id) select d;
                        if (collections.Any())
                        {
                            if (collections.FirstOrDefault().IsAction)
                            {
                                var collectionDateSequences = from d in db.trnCollections
                                                              where d.LoanId == Convert.ToInt32(loanId)
                                                              && d.CollectionDate >= collections.FirstOrDefault().CollectionDate
                                                              && d.CollectionDate <= Convert.ToDateTime(collection.CollectionDate)
                                                              select new Models.TrnCollection
                                                              {
                                                                  Id = d.Id,
                                                                  CollectionDate = d.CollectionDate.ToShortDateString(),
                                                                  CollectibleAmount = d.CollectibleAmount,
                                                                  PaidAmount = d.PaidAmount,
                                                                  PreviousBalanceAmount = d.PreviousBalanceAmount,
                                                                  CurrentBalanceAmount = d.CurrentBalanceAmount,
                                                                  CurrentCollectorId = d.trnLoanApplication.CurrentCollectorId,
                                                                  IsDueDate = d.IsDueDate,
                                                              };

                                Decimal totalCurrentBalanceAmount = 0;
                                if (collectionDateSequences.Any())
                                {
                                    totalCurrentBalanceAmount = collectionDateSequences.Sum(d => d.CurrentBalanceAmount);
                                }

                                if (totalCurrentBalanceAmount <= collection.PaidAmount)
                                {
                                    if (collectionDateSequences.Any())
                                    {
                                        Decimal currentBalanceValue = 0;
                                        Decimal advancePaymentAmount = collection.PaidAmount;

                                        foreach (var collectionDateSequence in collectionDateSequences)
                                        {
                                            if (advancePaymentAmount >= collectionDateSequence.CollectibleAmount)
                                            {
                                                var collectionDividedDate = from d in db.trnCollections
                                                                            where d.Id == collectionDateSequence.Id
                                                                            select d;

                                                if (collectionDividedDate.Any())
                                                {
                                                    var updateCollectionDividedDate = collectionDividedDate.FirstOrDefault();
                                                    updateCollectionDividedDate.PaidAmount = collectionDateSequence.CollectibleAmount;
                                                    updateCollectionDividedDate.CurrentBalanceAmount = 0;
                                                    updateCollectionDividedDate.IsCleared = true;
                                                    updateCollectionDividedDate.IsAbsent = false;
                                                    updateCollectionDividedDate.IsAdvancedPayment = true;
                                                    updateCollectionDividedDate.IsCurrentCollection = false;
                                                    updateCollectionDividedDate.IsProcessed = true;
                                                    updateCollectionDividedDate.IsAction = false;
                                                    db.SubmitChanges();

                                                    if (collectionDateSequence.IsDueDate)
                                                    {
                                                        if ((collectionDateSequence.CollectibleAmount + collectionDateSequence.PreviousBalanceAmount) - collectionDateSequence.PaidAmount == 0)
                                                        {
                                                            var updateLoanApplicationFullPayment = loanApplication.FirstOrDefault();
                                                            updateLoanApplicationFullPayment.IsFullyPaid = true;
                                                            db.SubmitChanges();

                                                            if (collectionDateSequence.CollectionDate.Equals(collection.CollectionDate))
                                                            {
                                                                updateCollectionDividedDate.IsAction = true;
                                                                db.SubmitChanges();
                                                            }
                                                        }
                                                    }
                                                }

                                                advancePaymentAmount -= collectionDateSequence.CollectibleAmount;
                                            }
                                            else
                                            {
                                                if (advancePaymentAmount > 0)
                                                {
                                                    var collectionDividedDate = from d in db.trnCollections
                                                                                where d.Id == collectionDateSequence.Id
                                                                                select d;

                                                    if (collectionDividedDate.Any())
                                                    {
                                                        var isActionValue = false;
                                                        if (currentBalanceValue > 0)
                                                        {
                                                            isActionValue = true;
                                                        }

                                                        var updateCollectionDividedDate = collectionDividedDate.FirstOrDefault();
                                                        updateCollectionDividedDate.PaidAmount = advancePaymentAmount;
                                                        updateCollectionDividedDate.CurrentBalanceAmount = collectionDateSequence.CollectibleAmount - advancePaymentAmount;
                                                        updateCollectionDividedDate.IsCleared = true;
                                                        updateCollectionDividedDate.IsAbsent = false;
                                                        updateCollectionDividedDate.IsAdvancedPayment = true;
                                                        updateCollectionDividedDate.IsCurrentCollection = false;
                                                        updateCollectionDividedDate.IsProcessed = true;
                                                        updateCollectionDividedDate.IsAction = isActionValue;
                                                        db.SubmitChanges();
                                                    }

                                                    currentBalanceValue += (collectionDateSequence.CollectibleAmount - advancePaymentAmount);
                                                    advancePaymentAmount *= 0;
                                                }
                                                else
                                                {
                                                    var collectionDividedDate = from d in db.trnCollections
                                                                                where d.Id == collectionDateSequence.Id
                                                                                select d;

                                                    if (collectionDividedDate.Any())
                                                    {
                                                        if (currentBalanceValue > 0)
                                                        {
                                                            var updateCollectionDividedDate = collectionDividedDate.FirstOrDefault();
                                                            updateCollectionDividedDate.PaidAmount = advancePaymentAmount;
                                                            updateCollectionDividedDate.PreviousBalanceAmount = currentBalanceValue;
                                                            updateCollectionDividedDate.CurrentBalanceAmount = collectionDateSequence.CollectibleAmount + currentBalanceValue;
                                                            updateCollectionDividedDate.IsCleared = false;
                                                            updateCollectionDividedDate.IsAbsent = false;
                                                            updateCollectionDividedDate.IsAdvancedPayment = true;
                                                            updateCollectionDividedDate.IsCurrentCollection = false;
                                                            updateCollectionDividedDate.IsProcessed = true;
                                                            updateCollectionDividedDate.IsAction = true;
                                                            db.SubmitChanges();

                                                            currentBalanceValue *= 0;
                                                        }
                                                        else
                                                        {
                                                            var updateCollectionDividedDate = collectionDividedDate.FirstOrDefault();
                                                            updateCollectionDividedDate.PaidAmount = advancePaymentAmount;
                                                            updateCollectionDividedDate.PreviousBalanceAmount = 0;
                                                            updateCollectionDividedDate.CurrentBalanceAmount = 0;
                                                            updateCollectionDividedDate.IsCleared = false;
                                                            updateCollectionDividedDate.IsAbsent = false;
                                                            updateCollectionDividedDate.IsAdvancedPayment = true;
                                                            updateCollectionDividedDate.IsCurrentCollection = false;
                                                            updateCollectionDividedDate.IsProcessed = true;
                                                            updateCollectionDividedDate.IsAction = false;
                                                            db.SubmitChanges();
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    return Request.CreateResponse(HttpStatusCode.OK);
                                }
                                else
                                {
                                    return Request.CreateResponse(HttpStatusCode.BadRequest, "The amount to be paid must not be greater than the total current balance amount.");
                                }
                            }
                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.BadRequest, "Cannot apply actions by this time.");
                            }
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.NotFound, "Sorry, but there are no data found in the server to apply some actions.");
                        }
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest, "Please lock the loan application first before procceding the collection process.");
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Sorry, but there are no data found in the server to apply some actions.");
                }
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Oops! Something went wrong from the server. Please contact the administrator.");
            }
        }

        // full payment collection
        [Authorize]
        [HttpPut]
        [Route("api/collection/fullPayment/update/byId/byLoanId/{id}/{loanId}")]
        public HttpResponseMessage fullPaymentCollection(String id, String loanId, Models.TrnCollection collection)
        {
            try
            {
                var loanApplication = from d in db.trnLoanApplications where d.Id == Convert.ToInt32(loanId) select d;
                if (loanApplication.Any())
                {
                    if (loanApplication.FirstOrDefault().IsLocked)
                    {
                        var collections = from d in db.trnCollections where d.Id == Convert.ToInt32(id) select d;
                        if (collections.Any())
                        {
                            if (collections.FirstOrDefault().IsAction)
                            {
                                var collectionDateSequences = from d in db.trnCollections
                                                              where d.LoanId == Convert.ToInt32(loanId)
                                                              && d.CollectionDate >= collections.FirstOrDefault().CollectionDate
                                                              && d.CollectionDate <= Convert.ToDateTime(collection.CollectionDate)
                                                              select new Models.TrnCollection
                                                              {
                                                                  Id = d.Id,
                                                                  CollectionDate = d.CollectionDate.ToShortDateString(),
                                                                  CollectibleAmount = d.CollectibleAmount,
                                                                  PaidAmount = d.PaidAmount,
                                                                  PreviousBalanceAmount = d.PreviousBalanceAmount,
                                                                  CurrentBalanceAmount = d.CurrentBalanceAmount,
                                                                  CurrentCollectorId = d.trnLoanApplication.CurrentCollectorId,
                                                                  IsDueDate = d.IsDueDate,
                                                              };

                                Decimal totalCurrentBalanceAmount = 0;
                                if (collectionDateSequences.Any())
                                {
                                    totalCurrentBalanceAmount = collectionDateSequences.Sum(d => d.CurrentBalanceAmount);
                                }

                                if (totalCurrentBalanceAmount <= collection.PaidAmount)
                                {
                                    if (collectionDateSequences.Any())
                                    {
                                        Decimal currentBalanceValue = 0;
                                        Decimal advancePaymentAmount = collection.PaidAmount;

                                        foreach (var collectionDateSequence in collectionDateSequences)
                                        {
                                            if (advancePaymentAmount >= collectionDateSequence.CollectibleAmount)
                                            {
                                                var collectionDividedDate = from d in db.trnCollections
                                                                            where d.Id == collectionDateSequence.Id
                                                                            select d;

                                                if (collectionDividedDate.Any())
                                                {
                                                    var updateCollectionDividedDate = collectionDividedDate.FirstOrDefault();
                                                    updateCollectionDividedDate.PaidAmount = collectionDateSequence.CollectibleAmount;
                                                    updateCollectionDividedDate.CurrentBalanceAmount = 0;
                                                    updateCollectionDividedDate.IsCleared = true;
                                                    updateCollectionDividedDate.IsAbsent = false;
                                                    updateCollectionDividedDate.IsAdvancedPayment = true;
                                                    updateCollectionDividedDate.IsCurrentCollection = false;
                                                    updateCollectionDividedDate.IsProcessed = true;
                                                    updateCollectionDividedDate.IsAction = false;
                                                    db.SubmitChanges();

                                                    if (collectionDateSequence.IsDueDate)
                                                    {
                                                        if ((collectionDateSequence.CollectibleAmount + collectionDateSequence.PreviousBalanceAmount) - collectionDateSequence.PaidAmount == 0)
                                                        {
                                                            var updateLoanApplicationFullPayment = loanApplication.FirstOrDefault();
                                                            updateLoanApplicationFullPayment.IsFullyPaid = true;
                                                            db.SubmitChanges();

                                                            if (collectionDateSequence.CollectionDate.Equals(collection.CollectionDate))
                                                            {
                                                                updateCollectionDividedDate.IsAction = true;
                                                                db.SubmitChanges();
                                                            }
                                                        }
                                                    }
                                                }

                                                advancePaymentAmount -= collectionDateSequence.CollectibleAmount;
                                            }
                                            else
                                            {
                                                if (advancePaymentAmount > 0)
                                                {
                                                    var collectionDividedDate = from d in db.trnCollections
                                                                                where d.Id == collectionDateSequence.Id
                                                                                select d;

                                                    if (collectionDividedDate.Any())
                                                    {
                                                        var isActionValue = false;
                                                        if (currentBalanceValue > 0)
                                                        {
                                                            isActionValue = true;
                                                        }

                                                        var updateCollectionDividedDate = collectionDividedDate.FirstOrDefault();
                                                        updateCollectionDividedDate.PaidAmount = advancePaymentAmount;
                                                        updateCollectionDividedDate.CurrentBalanceAmount = collectionDateSequence.CollectibleAmount - advancePaymentAmount;
                                                        updateCollectionDividedDate.IsCleared = true;
                                                        updateCollectionDividedDate.IsAbsent = false;
                                                        updateCollectionDividedDate.IsAdvancedPayment = true;
                                                        updateCollectionDividedDate.IsCurrentCollection = false;
                                                        updateCollectionDividedDate.IsProcessed = true;
                                                        updateCollectionDividedDate.IsAction = isActionValue;
                                                        db.SubmitChanges();
                                                    }

                                                    currentBalanceValue += (collectionDateSequence.CollectibleAmount - advancePaymentAmount);
                                                    advancePaymentAmount *= 0;
                                                }
                                                else
                                                {
                                                    var collectionDividedDate = from d in db.trnCollections
                                                                                where d.Id == collectionDateSequence.Id
                                                                                select d;

                                                    if (collectionDividedDate.Any())
                                                    {
                                                        if (currentBalanceValue > 0)
                                                        {
                                                            var updateCollectionDividedDate = collectionDividedDate.FirstOrDefault();
                                                            updateCollectionDividedDate.PaidAmount = advancePaymentAmount;
                                                            updateCollectionDividedDate.PreviousBalanceAmount = currentBalanceValue;
                                                            updateCollectionDividedDate.CurrentBalanceAmount = collectionDateSequence.CollectibleAmount + currentBalanceValue;
                                                            updateCollectionDividedDate.IsCleared = false;
                                                            updateCollectionDividedDate.IsAbsent = false;
                                                            updateCollectionDividedDate.IsAdvancedPayment = true;
                                                            updateCollectionDividedDate.IsCurrentCollection = false;
                                                            updateCollectionDividedDate.IsProcessed = true;
                                                            updateCollectionDividedDate.IsAction = true;
                                                            db.SubmitChanges();

                                                            currentBalanceValue *= 0;
                                                        }
                                                        else
                                                        {
                                                            var updateCollectionDividedDate = collectionDividedDate.FirstOrDefault();
                                                            updateCollectionDividedDate.PaidAmount = advancePaymentAmount;
                                                            updateCollectionDividedDate.PreviousBalanceAmount = 0;
                                                            updateCollectionDividedDate.CurrentBalanceAmount = 0;
                                                            updateCollectionDividedDate.IsCleared = false;
                                                            updateCollectionDividedDate.IsAbsent = false;
                                                            updateCollectionDividedDate.IsAdvancedPayment = true;
                                                            updateCollectionDividedDate.IsCurrentCollection = false;
                                                            updateCollectionDividedDate.IsProcessed = true;
                                                            updateCollectionDividedDate.IsAction = false;
                                                            db.SubmitChanges();
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    return Request.CreateResponse(HttpStatusCode.OK);
                                }
                                else
                                {
                                    return Request.CreateResponse(HttpStatusCode.BadRequest, "The amount to be paid must not be greater than the total current balance amount.");
                                }
                            }
                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.BadRequest, "Cannot apply actions by this time.");
                            }
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.NotFound, "Sorry, but there are no data found in the server to apply some actions.");
                        }
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest, "Please lock the loan application first before procceding the collection process.");
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Sorry, but there are no data found in the server to apply some actions.");
                }
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Oops! Something went wrong from the server. Please contact the administrator.");
            }
        }
    }
}
