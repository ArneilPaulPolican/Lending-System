﻿using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace Lending.Reports
{
    public class RepCollectiblesPDFController : Controller
    {
        // data
        private Data.LendingDataContext db = new Data.LendingDataContext();

        // GET: RepCashVoucherPDF
        public ActionResult Collectibles(String loanId)
        {
            if (loanId != null)
            {
                var loan = from d in db.trnLoans
                           where d.Id == Convert.ToInt32(loanId)
                           && d.IsLocked == true
                           select d;

                if (loan.Any())
                {
                    // PDF settings
                    MemoryStream workStream = new MemoryStream();
                    Rectangle rectangle = new Rectangle(612f, 936f);
                    Document document = new Document(rectangle, 72, 72, 72, 72);
                    document.SetMargins(30f, 30f, 50f, 20f);
                    PdfWriter.GetInstance(document, workStream).CloseStream = false;

                    // Document Starts
                    document.Open();

                    //// Fonts
                    //Font fontArial19Bold = FontFactory.GetFont("Arial", 20, Font.BOLD);
                    //Font fontArial17Bold = FontFactory.GetFont("Arial", 17, Font.BOLD);
                    //Font fontArial16Bold = FontFactory.GetFont("Arial", 16, Font.BOLD);
                    //Font fontArial12Bold = FontFactory.GetFont("Arial", 12, Font.BOLD);
                    //Font fontArial13Bold = FontFactory.GetFont("Arial", 13, Font.BOLD);
                    //Font fontArial12 = FontFactory.GetFont("Arial", 12);
                    //Font fontArial11Bold = FontFactory.GetFont("Arial", 11, Font.BOLD);
                    //Font fontArial11 = FontFactory.GetFont("Arial", 11);
                    //Font fontArial11ITALIC = FontFactory.GetFont("Arial", 12, Font.ITALIC);
                    //Font fontArial10Bold = FontFactory.GetFont("Arial", 10, Font.BOLD);
                    //Font fontArial10 = FontFactory.GetFont("Arial", 10);
                    //Font fontArial10ITALIC = FontFactory.GetFont("Arial", 10, Font.ITALIC);

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

                    // line
                    Paragraph line = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));

                    // user company detail
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

                    //  title
                    PdfPTable titleHeader = new PdfPTable(1);
                    float[] titleHeaderWithCells = new float[] { 100f };
                    titleHeader.SetWidths(titleHeaderWithCells);
                    titleHeader.WidthPercentage = 100;
                    titleHeader.AddCell(new PdfPCell(new Phrase("INDEX CARD (Applincants' Daily Collection)", fontArial13Bold)) { Border = 0, PaddingBottom = 10f, PaddingTop = 1f, HorizontalAlignment = 1 });
                    document.Add(titleHeader);

                    String docNumber = "";
                    if (loan.FirstOrDefault().IsLoanApplication)
                    {
                        docNumber = "LN-" + loan.FirstOrDefault().LoanNumber;
                    }
                    else
                    {
                        if (loan.FirstOrDefault().IsLoanReconstruct)
                        {
                            docNumber = "RC-" + loan.FirstOrDefault().LoanNumber;
                        }
                        else
                        {
                            if (loan.FirstOrDefault().IsLoanRenew)
                            {
                                docNumber = "RN-" + loan.FirstOrDefault().LoanNumber;
                            }
                        }
                    }

                    var staffs = from d in db.mstAreaStaffs.OrderByDescending(d => d.Id)
                                 where d.AreaId == loan.FirstOrDefault().mstApplicant.AreaId
                                 select d;

                    var staffName = "";
                    if (staffs.Any())
                    {
                        staffName = staffs.FirstOrDefault().mstStaff.Staff;
                    }

                    PdfPTable loanDetailData = new PdfPTable(4);
                    float[] loanDetailDataHeaderWithCells = new float[] { 15f, 50f, 15f, 20f };
                    loanDetailData.SetWidths(loanDetailDataHeaderWithCells);
                    loanDetailData.WidthPercentage = 100;
                    loanDetailData.AddCell(new PdfPCell(new Phrase("Applicant", fontArial12Bold)) { PaddingTop = 1f, PaddingBottom = 3f, PaddingLeft = 5f, PaddingRight = 5f });
                    loanDetailData.AddCell(new PdfPCell(new Phrase(loan.FirstOrDefault().mstApplicant.ApplicantLastName + ", " + loan.FirstOrDefault().mstApplicant.ApplicantFirstName + " " + loan.FirstOrDefault().mstApplicant.ApplicantMiddleName, fontArial12)) { PaddingTop = 1f, PaddingBottom = 3f, PaddingLeft = 5f, PaddingRight = 5f });
                    loanDetailData.AddCell(new PdfPCell(new Phrase("Doc. No", fontArial12Bold)) { PaddingTop = 1f, PaddingBottom = 3f, PaddingLeft = 5f, PaddingRight = 5f });
                    loanDetailData.AddCell(new PdfPCell(new Phrase(docNumber, fontArial12)) { PaddingTop = 1f, PaddingBottom = 3f, PaddingLeft = 5f, PaddingRight = 5f });
                    loanDetailData.AddCell(new PdfPCell(new Phrase("Address", fontArial12Bold)) { PaddingTop = 1f, PaddingBottom = 3f, PaddingLeft = 5f, PaddingRight = 5f });
                    loanDetailData.AddCell(new PdfPCell(new Phrase(loan.FirstOrDefault().mstApplicant.CityAddress, fontArial12)) { PaddingTop = 1f, PaddingBottom = 3f, PaddingLeft = 5f, PaddingRight = 5f });
                    loanDetailData.AddCell(new PdfPCell(new Phrase("Start Date", fontArial12Bold)) { PaddingTop = 1f, PaddingBottom = 3f, PaddingLeft = 5f, PaddingRight = 5f });
                    loanDetailData.AddCell(new PdfPCell(new Phrase(loan.FirstOrDefault().LoanDate.ToShortDateString(), fontArial12)) { PaddingTop = 1f, PaddingBottom = 3f, PaddingLeft = 5f, PaddingRight = 5f });
                    loanDetailData.AddCell(new PdfPCell(new Phrase("Area", fontArial12Bold)) { PaddingTop = 1f, PaddingBottom = 3f, PaddingLeft = 5f, PaddingRight = 5f });
                    loanDetailData.AddCell(new PdfPCell(new Phrase(loan.FirstOrDefault().mstApplicant.mstArea.Area, fontArial12)) { PaddingTop = 1f, PaddingBottom = 3f, PaddingLeft = 5f, PaddingRight = 5f });
                    loanDetailData.AddCell(new PdfPCell(new Phrase("Maturity Date", fontArial12Bold)) { PaddingTop = 1f, PaddingBottom = 3f, PaddingLeft = 5f, PaddingRight = 5f });
                    loanDetailData.AddCell(new PdfPCell(new Phrase(loan.FirstOrDefault().MaturityDate.ToShortDateString(), fontArial12)) { PaddingTop = 1f, PaddingBottom = 3f, PaddingLeft = 5f, PaddingRight = 5f });
                    loanDetailData.AddCell(new PdfPCell(new Phrase("Collector", fontArial12Bold)) { PaddingTop = 1f, PaddingBottom = 3f, PaddingLeft = 5f, PaddingRight = 5f });
                    loanDetailData.AddCell(new PdfPCell(new Phrase(staffName, fontArial12)) { PaddingTop = 1f, PaddingBottom = 3f, PaddingLeft = 5f, PaddingRight = 5f });
                    loanDetailData.AddCell(new PdfPCell(new Phrase("Net Amount", fontArial12Bold)) { PaddingTop = 1f, PaddingBottom = 3f, PaddingLeft = 5f, PaddingRight = 5f });
                    loanDetailData.AddCell(new PdfPCell(new Phrase(loan.FirstOrDefault().NetAmount.ToString("#,##0.00"), fontArial12)) { HorizontalAlignment = 2, PaddingTop = 1f, PaddingBottom = 3f, PaddingLeft = 5f, PaddingRight = 5f });
                    document.Add(loanDetailData);

                    //  space
                    PdfPTable spaceTable = new PdfPTable(1);
                    float[] spaceTableWithCells = new float[] { 100f };
                    spaceTable.SetWidths(spaceTableWithCells);
                    spaceTable.WidthPercentage = 100;
                    spaceTable.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0, PaddingTop = 2f, HorizontalAlignment = 1 });
                    document.Add(spaceTable);

                    PdfPTable loanDetaiAmountlData = new PdfPTable(5);
                    float[] lloanDetaiAmountlDataWithCells = new float[] { 30f, 15f, 10f, 30f, 15f };
                    loanDetaiAmountlData.SetWidths(lloanDetaiAmountlDataWithCells);
                    loanDetaiAmountlData.WidthPercentage = 100;
                    loanDetaiAmountlData.AddCell(new PdfPCell(new Phrase("Collection Amount", fontArial12Bold)) { Border = 0, PaddingTop = 1f, PaddingBottom = 6f, PaddingLeft = 5f, PaddingRight = 5f });
                    loanDetaiAmountlData.AddCell(new PdfPCell(new Phrase(loan.FirstOrDefault().NetCollectionAmount.ToString("#,##0.00"), fontArial12)) { Border = 0, HorizontalAlignment = 2, PaddingTop = 1f, PaddingBottom = 6f, PaddingLeft = 5f, PaddingRight = 5f });
                    loanDetaiAmountlData.AddCell(new PdfPCell(new Phrase(" ", fontArial12)) { Border = 0, PaddingTop = 1f, PaddingBottom = 6f, PaddingLeft = 5f, PaddingRight = 5f });
                    loanDetaiAmountlData.AddCell(new PdfPCell(new Phrase("Total Paid Amount", fontArial12Bold)) { Border = 0, PaddingTop = 1f, PaddingBottom = 6f, PaddingLeft = 15f, PaddingRight = 5f });
                    loanDetaiAmountlData.AddCell(new PdfPCell(new Phrase(loan.FirstOrDefault().TotalPaidAmount.ToString("#,##0.00"), fontArial12)) { Border = 0, HorizontalAlignment = 2, PaddingTop = 1f, PaddingBottom = 6f, PaddingLeft = 5f, PaddingRight = 5f });
                    loanDetaiAmountlData.AddCell(new PdfPCell(new Phrase("Balance Amount", fontArial12Bold)) { Border = 0, PaddingTop = 1f, PaddingBottom = 3f, PaddingLeft = 5f, PaddingRight = 5f });
                    loanDetaiAmountlData.AddCell(new PdfPCell(new Phrase(loan.FirstOrDefault().TotalBalanceAmount.ToString("#,##0.00"), fontArial12)) { Border = 0, HorizontalAlignment = 2, PaddingTop = 1f, PaddingBottom = 3f, PaddingLeft = 5f, PaddingRight = 5f });
                    loanDetaiAmountlData.AddCell(new PdfPCell(new Phrase(" ", fontArial12)) { Border = 0, PaddingTop = 1f, PaddingBottom = 3f, PaddingLeft = 5f, PaddingRight = 5f });
                    loanDetaiAmountlData.AddCell(new PdfPCell(new Phrase("Total Penalty Amount", fontArial12Bold)) { Border = 0, PaddingTop = 1f, PaddingBottom = 3f, PaddingLeft = 15f, PaddingRight = 5f });
                    loanDetaiAmountlData.AddCell(new PdfPCell(new Phrase(loan.FirstOrDefault().TotalPenaltyAmount.ToString("#,##0.00"), fontArial12)) { Border = 0, HorizontalAlignment = 2, PaddingTop = 1f, PaddingBottom = 3f, PaddingLeft = 5f, PaddingRight = 5f });
                    document.Add(loanDetaiAmountlData);

                    document.Add(spaceTable);

                    var loanLines = from d in db.trnCollections
                                    where d.LoanId == Convert.ToInt32(loanId)
                                    && d.IsLocked == true
                                    select d;

                    if (loanLines.Any())
                    {
                        PdfPTable loanLineslData = new PdfPTable(5);
                        float[] loanLineslDataWithCells = new float[] { 25f, 20f, 15f, 15f, 25f };
                        loanLineslData.SetWidths(loanLineslDataWithCells);
                        loanLineslData.WidthPercentage = 100;
                        loanLineslData.AddCell(new PdfPCell(new Phrase("Date", fontArial12Bold)) { HorizontalAlignment = 1, PaddingTop = 1f, PaddingBottom = 3f, PaddingLeft = 5f, PaddingRight = 5f, BackgroundColor = BaseColor.LIGHT_GRAY });
                        loanLineslData.AddCell(new PdfPCell(new Phrase("Status", fontArial12Bold)) { HorizontalAlignment = 1, PaddingTop = 1f, PaddingBottom = 3f, PaddingLeft = 5f, PaddingRight = 5f, BackgroundColor = BaseColor.LIGHT_GRAY });
                        loanLineslData.AddCell(new PdfPCell(new Phrase("Paid", fontArial12Bold)) { HorizontalAlignment = 1, PaddingTop = 1f, PaddingBottom = 3f, PaddingLeft = 5f, PaddingRight = 5f, BackgroundColor = BaseColor.LIGHT_GRAY });
                        loanLineslData.AddCell(new PdfPCell(new Phrase("Panalty", fontArial12Bold)) { HorizontalAlignment = 1, PaddingTop = 1f, PaddingBottom = 3f, PaddingLeft = 5f, PaddingRight = 5f, BackgroundColor = BaseColor.LIGHT_GRAY });
                        loanLineslData.AddCell(new PdfPCell(new Phrase("Particulars", fontArial12Bold)) { HorizontalAlignment = 1, PaddingTop = 1f, PaddingBottom = 3f, PaddingLeft = 5f, PaddingRight = 5f, BackgroundColor = BaseColor.LIGHT_GRAY });

                        foreach (var loanLine in loanLines)
                        {
                            loanLineslData.AddCell(new PdfPCell(new Phrase(loanLine.CollectionDate.ToString("MM-dd-yyyy dddd"), fontArial11)) { PaddingTop = 1f, PaddingBottom = 3f, PaddingLeft = 5f, PaddingRight = 5f });
                            loanLineslData.AddCell(new PdfPCell(new Phrase(loanLine.sysCollectionStatus.Status, fontArial11)) { PaddingTop = 1f, PaddingBottom = 3f, PaddingLeft = 5f, PaddingRight = 5f });
                            loanLineslData.AddCell(new PdfPCell(new Phrase(loanLine.TotalPaidAmount.ToString("#,##0.00"), fontArial11)) { HorizontalAlignment = 2, PaddingTop = 1f, PaddingBottom = 3f, PaddingLeft = 5f, PaddingRight = 5f });
                            loanLineslData.AddCell(new PdfPCell(new Phrase(loanLine.TotalPenaltyAmount.ToString("#,##0.00"), fontArial11)) { HorizontalAlignment = 2, PaddingTop = 1f, PaddingBottom = 3f, PaddingLeft = 5f, PaddingRight = 5f });
                            loanLineslData.AddCell(new PdfPCell(new Phrase(loanLine.Particulars, fontArial11)) { PaddingTop = 1f, PaddingBottom = 3f, PaddingLeft = 5f, PaddingRight = 5f });
                        }

                        document.Add(loanLineslData);
                    }

                    // Document End
                    document.Close();

                    byte[] byteInfo = workStream.ToArray();
                    workStream.Write(byteInfo, 0, byteInfo.Length);
                    workStream.Position = 0;

                    return new FileStreamResult(workStream, "application/pdf");
                }
                else
                {
                    return RedirectToAction("LoanApplicationList", "Software");
                }
            }
            else
            {
                return RedirectToAction("NotFound", "Software");
            }
        }
    }
}