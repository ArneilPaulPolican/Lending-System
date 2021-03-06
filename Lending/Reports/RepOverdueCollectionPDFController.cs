﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Globalization;

namespace Lending.Reports
{
    public class RepOverdueCollectionPDFController : Controller
    {
        private Data.LendingDataContext db = new Data.LendingDataContext();

        public List<Models.TrnLoan> listLoanApplicationsOverdue(String date, String areaId)
        {
            if (areaId.Equals("0"))
            {
                var loanApplications = from d in db.trnLoans
                                       where d.IsLocked == true
                                       && d.TotalBalanceAmount > 0
                                       && d.IsReturnRelease == false
                                       select new Models.TrnLoan
                                       {
                                           ApplicantId = d.ApplicantId,
                                           Id = d.Id,
                                           Applicant = d.mstApplicant.ApplicantLastName + " " + d.mstApplicant.ApplicantFirstName + " " + (d.mstApplicant.ApplicantMiddleName != null ? d.mstApplicant.ApplicantMiddleName : " "),
                                           LoanNumber = d.LoanNumber,
                                           DateTImeLoanDate = d.LoanDate,
                                           DateTImeMaturityDate = d.MaturityDate,
                                           TotalBalanceAmount = d.TotalBalanceAmount,
                                           CollectibleAmount = d.CollectibleAmount,
                                           IsLoanRenew = d.IsLoanRenew,
                                           IsLoanReconstruct = d.IsLoanReconstruct,
                                           IsLocked = d.IsLocked,
                                           Particulars = d.Particulars,
                                           IsBlocked = d.mstApplicant.IsBlocked
                                       };

                var grouploanApplications = from d in loanApplications.OrderByDescending(d => d.Id)
                                            group d by d.ApplicantId into g
                                            select new Models.TrnLoan
                                            {
                                                ApplicantId = g.FirstOrDefault().ApplicantId,
                                                Id = g.FirstOrDefault().Id,
                                                Applicant = g.FirstOrDefault().Applicant,
                                                LoanNumber = g.FirstOrDefault().LoanNumber,
                                                DateTImeLoanDate = g.FirstOrDefault().DateTImeLoanDate,
                                                DateTImeMaturityDate = g.FirstOrDefault().DateTImeMaturityDate,
                                                TotalBalanceAmount = g.FirstOrDefault().TotalBalanceAmount,
                                                CollectibleAmount = g.FirstOrDefault().CollectibleAmount,
                                                IsLoanRenew = g.FirstOrDefault().IsLoanRenew,
                                                IsLoanReconstruct = g.FirstOrDefault().IsLoanReconstruct,
                                                IsLocked = g.FirstOrDefault().IsLoanRenew,
                                                Particulars = g.FirstOrDefault().Particulars,
                                                IsBlocked = g.FirstOrDefault().IsBlocked
                                            };

                var loanApplicationList = from d in grouploanApplications.OrderByDescending(d => d.Id)
                                          where d.IsLoanReconstruct == true
                                          && d.IsBlocked == false
                                          select new Models.TrnLoan
                                          {
                                              ApplicantId = d.ApplicantId,
                                              Id = d.Id,
                                              Applicant = d.Applicant,
                                              LoanNumber = d.LoanNumber,
                                              LoanDate = d.DateTImeLoanDate.ToLongDateString(),
                                              MaturityDate = d.DateTImeMaturityDate.ToShortDateString(),
                                              TotalBalanceAmount = d.TotalBalanceAmount,
                                              CollectibleAmount = d.CollectibleAmount,
                                              IsLoanRenew = d.IsLoanRenew,
                                              IsLoanReconstruct = d.IsLoanReconstruct,
                                              IsLocked = d.IsLoanRenew,
                                              Particulars = d.Particulars,
                                              IsBlocked = d.IsBlocked,
                                              DateTImeLoanDate = d.DateTImeLoanDate
                                          };

                return loanApplicationList.OrderBy(d => d.Applicant).ToList();
            }
            else
            {
                var loanApplications = from d in db.trnLoans
                                       where d.IsLocked == true
                                       && d.TotalBalanceAmount > 0
                                       && d.mstApplicant.AreaId == Convert.ToInt32(areaId)
                                       && d.IsReturnRelease == false
                                       select new Models.TrnLoan
                                       {
                                           ApplicantId = d.ApplicantId,
                                           Id = d.Id,
                                           Applicant = d.mstApplicant.ApplicantLastName + " " + d.mstApplicant.ApplicantFirstName + " " + (d.mstApplicant.ApplicantMiddleName != null ? d.mstApplicant.ApplicantMiddleName : " "),
                                           LoanNumber = d.LoanNumber,
                                           DateTImeLoanDate = d.LoanDate,
                                           DateTImeMaturityDate = d.MaturityDate,
                                           TotalBalanceAmount = d.TotalBalanceAmount,
                                           CollectibleAmount = d.CollectibleAmount,
                                           IsLoanRenew = d.IsLoanRenew,
                                           IsLoanReconstruct = d.IsLoanReconstruct,
                                           IsLocked = d.IsLocked,
                                           Particulars = d.Particulars,
                                           IsBlocked = d.mstApplicant.IsBlocked
                                       };

                var grouploanApplications = from d in loanApplications.OrderByDescending(d => d.Id)
                                            group d by d.ApplicantId into g
                                            select new Models.TrnLoan
                                            {
                                                ApplicantId = g.FirstOrDefault().ApplicantId,
                                                Id = g.FirstOrDefault().Id,
                                                Applicant = g.FirstOrDefault().Applicant,
                                                LoanNumber = g.FirstOrDefault().LoanNumber,
                                                DateTImeLoanDate = g.FirstOrDefault().DateTImeLoanDate,
                                                DateTImeMaturityDate = g.FirstOrDefault().DateTImeMaturityDate,
                                                TotalBalanceAmount = g.FirstOrDefault().TotalBalanceAmount,
                                                CollectibleAmount = g.FirstOrDefault().CollectibleAmount,
                                                IsLoanRenew = g.FirstOrDefault().IsLoanRenew,
                                                IsLoanReconstruct = g.FirstOrDefault().IsLoanReconstruct,
                                                IsLocked = g.FirstOrDefault().IsLoanRenew,
                                                Particulars = g.FirstOrDefault().Particulars,
                                                IsBlocked = g.FirstOrDefault().IsBlocked
                                            };

                var loanApplicationList = from d in grouploanApplications.OrderByDescending(d => d.Id)
                                          where d.IsLoanReconstruct == true
                                          && d.IsBlocked == false
                                          select new Models.TrnLoan
                                          {
                                              ApplicantId = d.ApplicantId,
                                              Id = d.Id,
                                              Applicant = d.Applicant,
                                              LoanNumber = d.LoanNumber,
                                              LoanDate = d.DateTImeLoanDate.ToLongDateString(),
                                              MaturityDate = d.DateTImeMaturityDate.ToShortDateString(),
                                              TotalBalanceAmount = d.TotalBalanceAmount,
                                              CollectibleAmount = d.CollectibleAmount,
                                              IsLoanRenew = d.IsLoanRenew,
                                              IsLoanReconstruct = d.IsLoanReconstruct,
                                              IsLocked = d.IsLoanRenew,
                                              Particulars = d.Particulars,
                                              IsBlocked = d.IsBlocked,
                                              DateTImeLoanDate = d.DateTImeLoanDate
                                          };

                return loanApplicationList.OrderBy(d => d.Applicant).ToList();
            }
        }

        public ActionResult overdueCollection(String date, String areaId)
        {
            if (date != null && areaId != null)
            {
                var loanApplications = from d in listLoanApplicationsOverdue(date, areaId)
                                       select new Models.TrnLoan
                                       {
                                           ApplicantId = d.ApplicantId,
                                           Id = d.Id,
                                           Applicant = d.Applicant,
                                           LoanNumber = d.LoanNumber,
                                           LoanDate = d.DateTImeLoanDate.ToShortDateString(),
                                           MaturityDate = d.DateTImeMaturityDate.ToShortDateString(),
                                           TotalBalanceAmount = d.TotalBalanceAmount,
                                           CollectibleAmount = d.CollectibleAmount,
                                           IsLoanRenew = d.IsLoanRenew,
                                           IsLoanReconstruct = d.IsLoanReconstruct,
                                           IsLocked = d.IsLoanRenew,
                                           Particulars = d.Particulars,
                                       };

                if (loanApplications.Any())
                {
                    MemoryStream workStream = new MemoryStream();
                    Rectangle rectangle = new Rectangle(612f, 936f);
                    Document document = new Document(rectangle, 72, 72, 72, 72);
                    document.SetMargins(30f, 30f, 50f, 20f);
                    PdfWriter.GetInstance(document, workStream).CloseStream = false;

                    document.Open();

                    // Fonts
                    Font fontArial19Bold = FontFactory.GetFont("Arial", 17, Font.BOLD);
                    Font fontArial17Bold = FontFactory.GetFont("Arial", 14, Font.BOLD);
                    Font fontArial16Bold = FontFactory.GetFont("Arial", 13, Font.BOLD);
                    Font fontArial12Bold = FontFactory.GetFont("Arial", 9, Font.BOLD);
                    Font fontArial13Bold = FontFactory.GetFont("Arial", 10, Font.BOLD);
                    Font fontArial12 = FontFactory.GetFont("Arial", 9);
                    Font fontArial11Bold = FontFactory.GetFont("Arial", 8, Font.BOLD);
                    Font fontArial11 = FontFactory.GetFont("Arial", 8);
                    Font fontArial11ITALIC = FontFactory.GetFont("Arial", 9, Font.ITALIC);
                    Font fontArial10Bold = FontFactory.GetFont("Arial", 7, Font.BOLD);
                    Font fontArial10 = FontFactory.GetFont("Arial", 7);
                    Font fontArial10ITALIC = FontFactory.GetFont("Arial", 7, Font.ITALIC);

                    Paragraph line = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));

                    var userCompanyDetail = (from d in db.mstUsers where d.AspUserId == User.Identity.GetUserId() select d).FirstOrDefault();

                    // image
                    string imagepath = Server.MapPath("~/Images/dlhicon.jpg");
                    Image logo = Image.GetInstance(imagepath);
                    logo.ScalePercent(11f);
                    PdfPCell imageCell = new PdfPCell(logo);

                    // header
                    PdfPTable loanApplicationheader = new PdfPTable(2);
                    float[] loanApplicationheaderWidthCells = new float[] { 7f, 100f };
                    loanApplicationheader.SetWidths(loanApplicationheaderWidthCells);
                    loanApplicationheader.WidthPercentage = 100;
                    loanApplicationheader.AddCell(new PdfPCell(imageCell) { Rowspan = 3, Border = 0, PaddingRight = 10f, PaddingBottom = 5f, PaddingTop = 4f });
                    loanApplicationheader.AddCell(new PdfPCell(new Phrase(userCompanyDetail.mstCompany.Company, fontArial19Bold)) { HorizontalAlignment = 0, Border = 0, PaddingBottom = 2f });
                    loanApplicationheader.AddCell(new PdfPCell(new Phrase("Address: " + userCompanyDetail.mstCompany.Address, fontArial12)) { HorizontalAlignment = 0, Border = 0 });
                    loanApplicationheader.AddCell(new PdfPCell(new Phrase("Contact: " + userCompanyDetail.mstCompany.ContactNumber, fontArial12)) { HorizontalAlignment = 0, Border = 0 });
                    document.Add(loanApplicationheader);

                    // line header
                    PdfPTable lineHeader = new PdfPTable(1);
                    float[] lineHeaderWithCells = new float[] { 100f };
                    lineHeader.SetWidths(lineHeaderWithCells);
                    lineHeader.WidthPercentage = 100;
                    lineHeader.AddCell(new PdfPCell(new Phrase(" ", fontArial11)) { Border = 1, Padding = 0f });
                    document.Add(lineHeader);

                    var area = "";
                    var areaQuery = from d in db.mstAreas
                                    where d.Id == Convert.ToInt32(areaId)
                                    select d;

                    PdfPTable titleHeader = new PdfPTable(1);
                    float[] titleHeaderWithCells = new float[] { 100f };
                    titleHeader.SetWidths(titleHeaderWithCells);
                    titleHeader.WidthPercentage = 100;

                    if (areaQuery.Any())
                    {
                        area = areaQuery.FirstOrDefault().Area;
                        titleHeader.AddCell(new PdfPCell(new Phrase(area + " OVERDUE - " + Convert.ToDateTime(date).ToString("MMMM", CultureInfo.InvariantCulture).ToUpper(), fontArial13Bold)) { Border = 0, PaddingBottom = 10f, PaddingTop = 2f, HorizontalAlignment = 0 });
                    }
                    else
                    {
                        titleHeader.AddCell(new PdfPCell(new Phrase("OVERDUE in All Areas - " + Convert.ToDateTime(date).ToString("MMMM", CultureInfo.InvariantCulture).ToUpper(), fontArial13Bold)) { Border = 0, PaddingBottom = 10f, PaddingTop = 2f, HorizontalAlignment = 0 });
                    }

                    document.Add(titleHeader);

                    PdfPTable loanData = new PdfPTable(9);
                    float[] loanDataWithCells = new float[] { 30f, 12f, 8f, 8f, 8f, 8f, 8f, 8f, 20f };
                    loanData.SetWidths(loanDataWithCells);
                    loanData.WidthPercentage = 100;
                    loanData.AddCell(new PdfPCell(new Phrase("Applicant", fontArial11Bold)) { HorizontalAlignment = 1, PaddingTop = 1f, PaddingBottom = 3f, PaddingLeft = 5f, PaddingRight = 5f, BackgroundColor = BaseColor.LIGHT_GRAY });
                    loanData.AddCell(new PdfPCell(new Phrase("Balance", fontArial11Bold)) { HorizontalAlignment = 1, PaddingTop = 1f, PaddingBottom = 3f, PaddingLeft = 5f, PaddingRight = 5f, BackgroundColor = BaseColor.LIGHT_GRAY });
                    DateTime weekFirstDay = Convert.ToDateTime(date).AddDays(DayOfWeek.Sunday - Convert.ToDateTime(date).DayOfWeek);
                    for (var i = 1; i <= 6; i++)
                    {
                        DateTime weekLastDay = weekFirstDay.AddDays(i);
                        loanData.AddCell(new PdfPCell(new Phrase(weekLastDay.Day.ToString(), fontArial11Bold)) { HorizontalAlignment = 1, PaddingTop = 1f, PaddingBottom = 3f, PaddingLeft = 5f, PaddingRight = 5f, BackgroundColor = BaseColor.LIGHT_GRAY });
                    }
                    loanData.AddCell(new PdfPCell(new Phrase("Particulars", fontArial11Bold)) { HorizontalAlignment = 1, PaddingTop = 1f, PaddingBottom = 3f, PaddingLeft = 5f, PaddingRight = 5f, BackgroundColor = BaseColor.LIGHT_GRAY });
                    var loanYears = loanApplications.GroupBy(year => Convert.ToDateTime(year.LoanDate).Year).Select(group =>
                            new
                            {
                                Name = group.Key,
                                Elements = group.OrderByDescending(y => Convert.ToDateTime(y.DateTImeLoanDate).Year)
                            }
                        ).OrderByDescending(group => Convert.ToDateTime(group.Elements.First().LoanDate).Year);
                    if (loanYears.Any())
                    {
                        Decimal noOfCustomers = 0;
                        foreach (var loanYear in loanYears)
                        {
                            loanData.AddCell(new PdfPCell(new Phrase(Convert.ToDateTime(loanYear.Elements.First().LoanDate).Year.ToString(), fontArial13Bold)) { Colspan = 10, PaddingTop = 10f, PaddingBottom = 9f, PaddingLeft = 5f, PaddingRight = 5f });

                            Decimal totalBalanceEveryYear = 0;
                            Decimal noOfCustomer = 0;

                            foreach (var loanApplication in loanApplications)
                            {
                                if (Convert.ToDateTime(loanApplication.LoanDate).Year == Convert.ToDateTime(loanYear.Elements.First().LoanDate).Year)
                                {
                                    var applicant = loanApplication.Applicant;
                                    loanData.AddCell(new PdfPCell(new Phrase(applicant, fontArial11)) { PaddingTop = 1f, PaddingBottom = 3f, PaddingLeft = 5f, PaddingRight = 5f });
                                    loanData.AddCell(new PdfPCell(new Phrase(loanApplication.TotalBalanceAmount.ToString("#,##0.00"), fontArial11)) { HorizontalAlignment = 2, PaddingTop = 1f, PaddingBottom = 3f, PaddingLeft = 5f, PaddingRight = 5f });

                                    for (var i = 1; i <= 6; i++)
                                    {
                                        loanData.AddCell(new PdfPCell(new Phrase(" ", fontArial11)) { PaddingTop = 1f, PaddingBottom = 3f, PaddingLeft = 5f, PaddingRight = 5f });
                                    }

                                    loanData.AddCell(new PdfPCell(new Phrase(loanApplication.Particulars, fontArial11)) { PaddingTop = 1f, PaddingBottom = 3f, PaddingLeft = 5f, PaddingRight = 5f });

                                    totalBalanceEveryYear += loanApplication.TotalBalanceAmount;
                                    noOfCustomer += 1;
                                }
                            }

                            loanData.AddCell(new PdfPCell(new Phrase("Total (" + Convert.ToDateTime(loanYear.Elements.First().LoanDate).Year.ToString() + ")", fontArial11Bold)) { PaddingTop = 5f, PaddingBottom = 7f, PaddingLeft = 5f, PaddingRight = 5f });
                            loanData.AddCell(new PdfPCell(new Phrase(totalBalanceEveryYear.ToString("#,##0.00"), fontArial11Bold)) { HorizontalAlignment = 2, PaddingTop = 5f, PaddingBottom = 7f, PaddingLeft = 5f, PaddingRight = 5f });
                            loanData.AddCell(new PdfPCell(new Phrase(" ", fontArial11)) { Colspan = 8, PaddingTop = 5f, PaddingBottom = 7f, PaddingLeft = 5f, PaddingRight = 5f });

                            loanData.AddCell(new PdfPCell(new Phrase("No. of Overdue Applicants (" + Convert.ToDateTime(loanYear.Elements.First().LoanDate).Year.ToString() + ")", fontArial11Bold)) { PaddingTop = 5f, PaddingBottom = 7f, PaddingLeft = 5f, PaddingRight = 5f });
                            loanData.AddCell(new PdfPCell(new Phrase(noOfCustomer.ToString("#,##0"), fontArial11Bold)) { HorizontalAlignment = 2, PaddingTop = 5f, PaddingBottom = 7f, PaddingLeft = 5f, PaddingRight = 5f });
                            loanData.AddCell(new PdfPCell(new Phrase(" ", fontArial11)) { Colspan = 8, PaddingTop = 5f, PaddingBottom = 7f, PaddingLeft = 5f, PaddingRight = 5f });
                            noOfCustomers += noOfCustomer;
                        }

                        loanData.AddCell(new PdfPCell(new Phrase(" ", fontArial11Bold)) { Colspan = 9, PaddingTop = 10f, PaddingBottom = 10f, PaddingLeft = 5f, PaddingRight = 5f });

                        loanData.AddCell(new PdfPCell(new Phrase("Total Overdue Applicants", fontArial11Bold)) { PaddingTop = 6f, PaddingBottom = 9f, PaddingLeft = 5f, PaddingRight = 5f });
                        loanData.AddCell(new PdfPCell(new Phrase(noOfCustomers.ToString("#,##0"), fontArial11Bold)) { HorizontalAlignment = 2, PaddingTop = 6f, PaddingBottom = 9f, PaddingLeft = 5f, PaddingRight = 5f });
                        loanData.AddCell(new PdfPCell(new Phrase(" ", fontArial11)) { Colspan = 8, PaddingTop = 6f, PaddingBottom = 9f, PaddingLeft = 5f, PaddingRight = 5f });
                    }
                    document.Add(loanData);

                    // Document End
                    document.Close();

                    byte[] byteInfo = workStream.ToArray();
                    workStream.Write(byteInfo, 0, byteInfo.Length);
                    workStream.Position = 0;

                    return new FileStreamResult(workStream, "application/pdf");

                }
                else
                {
                    return RedirectToAction("Index", "Software");
                }
            }
            else
            {
                return RedirectToAction("NotFound", "Software");
            }
        }
    }
}