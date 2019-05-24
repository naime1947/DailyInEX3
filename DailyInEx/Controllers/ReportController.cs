using DailyInEx.DataManager;
using DailyInEx.Models;
using DailyInEx.Models.ViewModel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DailyInEx.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        public ActionResult Yearly(int? year)
        {
            #region YearList ViewBag.Years
            List<int> years = new List<int>();
            for (int i = DateTime.Today.Year; i >= 2000; i--)
            {
                years.Add(i);
            }
            ViewBag.Years = years;
            #endregion

            if (year != null)
            {
                List<MonthlyInExProfit> monthlyInExProfitList = YearlyReportManager.LoadMonthlyInExProfitList((int) year);
                ViewBag.DataList = monthlyInExProfitList;
                ViewBag.Year = year;
            }
            
            return View();
        }

        public ActionResult YearlyReportPdf(int year)
        {
            Document pdfDoc = new Document(PageSize.A4,30f,30f,30f,25f);
            PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);

            iTextSharp.text.Font headFont = new iTextSharp.text.Font(bfTimes, 20);
            Paragraph head = new Paragraph("Year income and expense report \nYear :" + year,headFont);
            head.Alignment = Element.ALIGN_CENTER;
            pdfDoc.Add(head);

            Paragraph line = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
            pdfDoc.Add(line);


            //Table
            PdfPTable table = new PdfPTable(2);
            table.WidthPercentage = 100;
            table.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            table.SpacingBefore = 5f;
            table.SpacingAfter = 5f;

            //Cell no 1
            PdfPCell cell = new PdfPCell();
            iTextSharp.text.Font companyInfoFont = new iTextSharp.text.Font(bfTimes, 16);
            CompanyModel user = (CompanyModel)Session["User"];
            Paragraph companyInfo = new Paragraph("Company: "+user.CompanyName+ "\nEmail: "+user.CompanyEmail+"\n"+user.Address, companyInfoFont);
            cell.Border = 0;
            cell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;

            cell.AddElement(companyInfo);
            table.AddCell(cell);

            //Cell no 2
            cell = new PdfPCell();
            cell.Border = 0;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            Image image = Image.GetInstance(Server.MapPath("~/Content/img/logo.png"));
            image.ScaleAbsolute(100, 100);
            cell.AddElement(image);
            table.AddCell(cell);

            //Add table to document    
            pdfDoc.Add(table);
            
            line = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
            pdfDoc.Add(line);
            //Pdf Creation date
            Font bodyFont = new Font(bfTimes, 12);
            Paragraph pdfCreationDate = new Paragraph("Date: "+DateTime.Today.ToShortDateString(), bodyFont);
            pdfCreationDate.Alignment = Element.ALIGN_RIGHT;
            pdfDoc.Add(pdfCreationDate);

            #region Generting Yeraly Data and data table
            
            table = new PdfPTable(5);
            table.WidthPercentage = 100;
            table.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            table.SpacingBefore = 15f;
            table.SpacingAfter = 5f;

            //ading tale column name
            table.AddCell("Sl");
            table.AddCell("Month");
            table.AddCell("Income");
            table.AddCell("Expense");
            table.AddCell("Profit");

            List<MonthlyInExProfit> monthlyInExProfitList = YearlyReportManager.LoadMonthlyInExProfitList((int)year);
            int serialNumber = 1;
            double totalIncome = 0;
            double totalExpense = 0;
            double totalPofit = 0;
            foreach (MonthlyInExProfit data in monthlyInExProfitList)
            {
                table.AddCell(serialNumber++.ToString());
                table.AddCell(data.Month);
                table.AddCell(data.MonthlyTotlaIncome.ToString());
                table.AddCell(data.MonthlyTotalExpense.ToString());
                table.AddCell(data.MonthlyProfit.ToString());

                totalIncome += data.MonthlyTotlaIncome;
                totalExpense += data.MonthlyTotalExpense;
                totalPofit += data.MonthlyProfit;
            }
            cell = new PdfPCell();
            cell.AddElement(new Paragraph("Total"));
            cell.Colspan = 2;
            table.AddCell(cell);
            
            table.AddCell(totalIncome.ToString());
            table.AddCell(totalExpense.ToString());
            table.AddCell(totalPofit.ToString());

            pdfDoc.Add(table);
            #endregion



            //Generatin
            pdfWriter.CloseStream = false;
            pdfDoc.Close();
            Response.Buffer = true;
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=YearlyReport-"+year+".pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Write(pdfDoc);
            Response.End();

            return RedirectToAction("Yearly", "Report");
        }

    }
}