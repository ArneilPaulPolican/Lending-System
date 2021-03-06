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
    public class RepTotalCollectionPDFController : Controller
    {
        private Data.LendingDataContext db = new Data.LendingDataContext();

        public ActionResult totalCollection(String startCollectionDate, String endCollectionDate)
        {
            if (startCollectionDate != null && endCollectionDate != null)
            {
                var collections = from d in db.mstAreas
                                  join c in db.trnCollections
                                  on d.Id equals c.trnLoan.mstApplicant.AreaId
                                  into joinAreaCollections
                                  from listAreaCollections in joinAreaCollections.DefaultIfEmpty()
                                  select new Models.TrnCollection
                                  {
                                      AreaId = d.Id,
                                      Area = d.Area,
                                      Active = joinAreaCollections.Where(a => a.CollectionDate >= Convert.ToDateTime(startCollectionDate)
                                          && a.CollectionDate <= Convert.ToDateTime(endCollectionDate)
                                          && a.IsLocked == true
                                          && a.trnLoan.IsLoanReconstruct != true).Sum(a => a.TotalPaidAmount) != null ? joinAreaCollections.Where(a => a.CollectionDate >= Convert.ToDateTime(startCollectionDate)
                                          && a.CollectionDate <= Convert.ToDateTime(endCollectionDate)
                                          && a.IsLocked == true
                                          && a.trnLoan.IsLoanReconstruct != true).Sum(a => a.TotalPaidAmount) : 0,
                                      Overdue = joinAreaCollections.Where(a => a.CollectionDate >= Convert.ToDateTime(startCollectionDate)
                                          && a.CollectionDate <= Convert.ToDateTime(endCollectionDate)
                                          && a.IsLocked == true
                                          && a.trnLoan.IsLoanReconstruct == true).Sum(a => a.TotalPaidAmount) != null ? joinAreaCollections.Where(a => a.CollectionDate >= Convert.ToDateTime(startCollectionDate)
                                          && a.CollectionDate <= Convert.ToDateTime(endCollectionDate)
                                          && a.IsLocked == true
                                          && a.trnLoan.IsLoanReconstruct == true).Sum(a => a.TotalPaidAmount) : 0
                                  };

                var collectionsGroupByAreas = from d in collections
                                              group d by new
                                              {
                                                  AreaId = d.AreaId,
                                                  Area = d.Area,
                                                  Active = d.Active,
                                                  Overdue = d.Overdue
                                              } into g
                                              select new Models.TrnCollection
                                              {
                                                  AreaId = g.Key.AreaId,
                                                  Area = g.Key.Area,
                                                  Active = g.Key.Active,
                                                  Overdue = g.Key.Overdue,
                                                  TotalCollection = Convert.ToDecimal(g.Key.Active) + Convert.ToDecimal(g.Key.Overdue)
                                              };

                if (collectionsGroupByAreas.Any())
                {
                    MemoryStream workStream = new MemoryStream();
                    Rectangle rectangle = new Rectangle(612f, 936f);
                    Document document = new Document(rectangle, 72, 72, 72, 72);
                    document.SetMargins(30f, 30f, 50f, 20f);
                    PdfWriter.GetInstance(document, workStream).CloseStream = false;

                    document.Open();

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

                    PdfPTable titleHeader = new PdfPTable(1);
                    float[] titleHeaderWithCells = new float[] { 100f };
                    titleHeader.SetWidths(titleHeaderWithCells);
                    titleHeader.WidthPercentage = 100;

                    titleHeader.AddCell(new PdfPCell(new Phrase("Total Collection Report", fontArial13Bold)) { Border = 0, PaddingBottom = 5f, PaddingTop = 1f, HorizontalAlignment = 1 });
                    titleHeader.AddCell(new PdfPCell(new Phrase("From " + Convert.ToDateTime(startCollectionDate).ToString("MMMM dd, yyyy") + " to " + Convert.ToDateTime(endCollectionDate).ToString("MMMM dd, yyyy"), fontArial12)) { Border = 0, PaddingBottom = 12f, PaddingTop = 2f, HorizontalAlignment = 1 });
                    document.Add(titleHeader);

                    PdfPTable collectionData = new PdfPTable(4);
                    float[] collectionDataWithCells = new float[] { 40f, 20f, 20f, 20f };
                    collectionData.SetWidths(collectionDataWithCells);
                    collectionData.WidthPercentage = 100;
                    collectionData.AddCell(new PdfPCell(new Phrase("Area", fontArial12Bold)) { HorizontalAlignment = 1, PaddingTop = 1f, PaddingBottom = 3f, PaddingLeft = 5f, PaddingRight = 5f, BackgroundColor = BaseColor.LIGHT_GRAY });
                    collectionData.AddCell(new PdfPCell(new Phrase("Active", fontArial12Bold)) { HorizontalAlignment = 1, PaddingTop = 1f, PaddingBottom = 3f, PaddingLeft = 5f, PaddingRight = 5f, BackgroundColor = BaseColor.LIGHT_GRAY });
                    collectionData.AddCell(new PdfPCell(new Phrase("Overdue", fontArial12Bold)) { HorizontalAlignment = 1, PaddingTop = 1f, PaddingBottom = 3f, PaddingLeft = 5f, PaddingRight = 5f, BackgroundColor = BaseColor.LIGHT_GRAY });
                    collectionData.AddCell(new PdfPCell(new Phrase("Gross Collection", fontArial12Bold)) { HorizontalAlignment = 1, PaddingTop = 1f, PaddingBottom = 3f, PaddingLeft = 5f, PaddingRight = 5f, BackgroundColor = BaseColor.LIGHT_GRAY });

                    Decimal totalActive = 0;
                    Decimal totalOverdue = 0;
                    Decimal totaGross = 0;
                    foreach (var collectionsGroupByArea in collectionsGroupByAreas.OrderBy(d => d.Area))
                    {
                        totalActive += collectionsGroupByArea.Active;
                        totalOverdue += collectionsGroupByArea.Overdue;
                        totaGross += collectionsGroupByArea.TotalCollection;

                        collectionData.AddCell(new PdfPCell(new Phrase(collectionsGroupByArea.Area, fontArial11)) { PaddingTop = 1f, PaddingBottom = 3f, PaddingLeft = 5f, PaddingRight = 5f });
                        collectionData.AddCell(new PdfPCell(new Phrase(collectionsGroupByArea.Active.ToString("#,##0.00"), fontArial11)) { HorizontalAlignment = 2, PaddingTop = 1f, PaddingBottom = 3f, PaddingLeft = 5f, PaddingRight = 5f });
                        collectionData.AddCell(new PdfPCell(new Phrase(collectionsGroupByArea.Overdue.ToString("#,##0.00"), fontArial11)) { HorizontalAlignment = 2, PaddingTop = 1f, PaddingBottom = 3f, PaddingLeft = 5f, PaddingRight = 5f });
                        collectionData.AddCell(new PdfPCell(new Phrase(collectionsGroupByArea.TotalCollection.ToString("#,##0.00"), fontArial11)) { HorizontalAlignment = 2, PaddingTop = 1f, PaddingBottom = 3f, PaddingLeft = 5f, PaddingRight = 5f });
                    }

                    collectionData.AddCell(new PdfPCell(new Phrase("TOTAL", fontArial11Bold)) { HorizontalAlignment = 2, PaddingTop = 6f, PaddingBottom = 9f, PaddingLeft = 5f, PaddingRight = 5f });
                    collectionData.AddCell(new PdfPCell(new Phrase(totalActive.ToString("#,##0.00"), fontArial11)) { HorizontalAlignment = 2, PaddingTop = 6f, PaddingBottom = 9f, PaddingLeft = 5f, PaddingRight = 5f });
                    collectionData.AddCell(new PdfPCell(new Phrase(totalOverdue.ToString("#,##0.00"), fontArial11)) { HorizontalAlignment = 2, PaddingTop = 6f, PaddingBottom = 9f, PaddingLeft = 5f, PaddingRight = 5f });
                    collectionData.AddCell(new PdfPCell(new Phrase(totaGross.ToString("#,##0.00"), fontArial11)) { HorizontalAlignment = 2, PaddingTop = 6f, PaddingBottom = 9f, PaddingLeft = 5f, PaddingRight = 5f });

                    document.Add(collectionData);
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