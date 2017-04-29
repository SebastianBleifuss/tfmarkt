using Microsoft.Win32;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.Printing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Xps.Packaging;
using xmlserializer.Models;


namespace tfMarktMain.Export
{
    public static class PDFFactory
    {

        private static void GenerateTotalCalculation(this Customer Customer, String FilePath)
        {



            XPdfFontOptions options = new XPdfFontOptions(PdfFontEncoding.Unicode, PdfFontEmbedding.Always);


            // Create a font
            XFont ft8Fett = new XFont("Verdana", 8, XFontStyle.Bold);
            XFont ft8Std = new XFont("Verdana", 8, XFontStyle.Regular);
            XFont ft9Fett = new XFont("Verdana", 9, XFontStyle.Bold);
            XFont ft9Std = new XFont("Verdana", 9, XFontStyle.Regular);
            XFont ft11Fett = new XFont("Verdana", 11, XFontStyle.Bold);
            XFont ft11Std = new XFont("Verdana", 11, XFontStyle.Regular);
            XFont ft14Fett = new XFont("Verdana", 14, XFontStyle.Bold);
            XFont ft14Std = new XFont("Verdana", 14, XFontStyle.Regular);

            XFont Headerft10Std = new XFont("Sylfaen", 10, XFontStyle.Regular);

            XFont Headerft16Fett = new XFont("Sylfaen", 16, XFontStyle.Bold);
            XFont Headerft16Std = new XFont("Sylfaen", 16, XFontStyle.Regular);
            XFont Headerft16U = new XFont("Sylfaen", 16, XFontStyle.Underline);
            XFont Headerft18Fett = new XFont("Sylfaen", 18, XFontStyle.Bold);
            XFont Headerft18Std = new XFont("Sylfaen", 18, XFontStyle.Underline);
            XFont Headerft18U = new XFont("Sylfaen", 18, XFontStyle.Regular);
            XFont Headerft20Fett = new XFont("Sylfaen", 20, XFontStyle.Bold);
            XFont Headerft20Std = new XFont("Sylfaen", 20, XFontStyle.Regular);
            XFont Headerft20U = new XFont("Sylfaen", 20, XFontStyle.Underline);



            // Create a new PDF document
            PdfDocument doc = new PdfDocument();

            // Create an empty page
            PdfPage PageOne = doc.AddPage();

            // Get an XGraphics object for drawing
            XGraphics gfx = XGraphics.FromPdfPage(PageOne);

            // Kopf
            XImage CompanyLogo = XImage.FromFile("..\\datastorage\\CompanyLogo.png");
            int CompanyLogoWidth = CompanyLogo.PixelWidth / 2;
            int CompanyLogoHeight = CompanyLogo.PixelHeight / 2;

            //X,Y,Width,Height
            gfx.DrawImage(CompanyLogo, gfx.PageSize.Width - 50 - CompanyLogoWidth, 55, 250, 100);

            XColor XColorBlack = XColor.FromArgb(0, 0, 0);
            XColor XColorLightBrown = XColor.FromArgb(249, 245, 238);
            XColor XColorLightBrownLine = XColor.FromArgb(220, 197, 156);

            double hPos = 120;
            double vPosR = gfx.PageSize.Width - 70;
            double vPosL = 60;

            // Briefkopf ohne Empfänger
            gfx.DrawString("tfMarkt GmbH & Co. KG.", Headerft20Fett, XBrushes.Black, new XRect(vPosL, hPos, PageOne.Width, PageOne.Height), XStringFormats.TopLeft);

            hPos += 25;
            gfx.DrawLine(new XPen(XColorBlack), vPosL, hPos, vPosR, hPos);

            hPos += 5;
            gfx.DrawString(DateTime.Now.ToShortDateString(), ft9Fett, XBrushes.Black, new XRect(vPosL, hPos, PageOne.Width, PageOne.Height), XStringFormats.TopLeft);

            hPos += 15;
            gfx.DrawLine(new XPen(XColorBlack), vPosL, hPos, vPosR, hPos);

            hPos += 30;
            gfx.DrawString("Kundenname:", ft11Fett, XBrushes.Black, new XRect(vPosL, hPos, PageOne.Width, PageOne.Height), XStringFormats.TopLeft);
            gfx.DrawString(Customer.Name, ft9Std, XBrushes.Black, new XRect(vPosL + 120, hPos + 2, PageOne.Width, PageOne.Height), XStringFormats.TopLeft);

            hPos += 15;
            gfx.DrawString("Kundennummer:", ft11Fett, XBrushes.Black, new XRect(vPosL, hPos, PageOne.Width, PageOne.Height), XStringFormats.TopLeft);
            gfx.DrawString(Customer.Customernumber.ToString(), ft9Std, XBrushes.Black, new XRect(vPosL + 120, hPos + 3, PageOne.Width, PageOne.Height), XStringFormats.TopLeft);

            hPos += 50;
            gfx.DrawLine(new XPen(XColorBlack), vPosL, hPos, vPosR, hPos);

            // Calculations
            Dictionary<Guid, Calculation> Calculations = Customer.Calculations;

            hPos += 5;
            gfx.DrawString("MENGE", Headerft10Std, XBrushes.Black, new XRect(vPosL + 10, hPos, PageOne.Width, PageOne.Height), XStringFormats.TopLeft);
            gfx.DrawString("DETAILS", Headerft10Std, XBrushes.Black, new XRect(vPosL + 75, hPos, PageOne.Width, PageOne.Height), XStringFormats.TopLeft);
            gfx.DrawString("EINZELPREIS", Headerft10Std, XBrushes.Black, new XRect(vPosL + 275, hPos, PageOne.Width, PageOne.Height), XStringFormats.TopLeft);
            gfx.DrawString("POSITION GESAMT", Headerft10Std, XBrushes.Black, new XRect(vPosL + 375, hPos, PageOne.Width, PageOne.Height), XStringFormats.TopLeft);

            hPos += 15;
            gfx.DrawRectangle(new XSolidBrush(XColorLightBrown), vPosL, hPos - 0.5, vPosR - vPosL, 20);

            double LinehPos = hPos;
            bool drawRec = true;
            decimal totalPosGes = 0;
            foreach (Calculation calc in Calculations.Values)
            {
                if (drawRec)
                {
                    drawRec = false;
                    gfx.DrawRectangle(new XSolidBrush(XColorLightBrown), vPosL, hPos - 0.5, vPosR - vPosL, 20);
                }
                else
                {
                    drawRec = true;

                }

                hPos += 5;
                Product p = calc.SelectedProduct;
                gfx.DrawString(calc.Amount.ToString(), ft8Std, XBrushes.Black, new XRect(vPosL + 10, hPos, PageOne.Width, PageOne.Height), XStringFormats.TopLeft);

                String Description = p.getArtikelbezeichnung();
                if (Description.Length > 22)
                {
                    Description = Description.Substring(0, 22) + "...";
                }
                gfx.DrawString(Description, ft8Std, XBrushes.Black, new XRect(vPosL + 75, hPos, PageOne.Width, PageOne.Height), XStringFormats.TopLeft);
                gfx.DrawString(p.getPreis().ToFormatStringEuro(), ft8Std, XBrushes.Black, new XRect(vPosL + 275, hPos, PageOne.Width, PageOne.Height), XStringFormats.TopLeft);
                decimal posGes = RoundCurrency((calc.Amount * p.getPreis()));
                totalPosGes += posGes;
                gfx.DrawString(posGes.ToFormatStringEuro(), ft8Std, XBrushes.Black, new XRect(vPosL + 375, hPos, PageOne.Width, PageOne.Height), XStringFormats.TopLeft);
                hPos += 15;



                if (hPos >= 800)
                {
                    // Create an empty page
                    PdfPage NextPage = doc.AddPage();

                    // Get an XGraphics object for drawing
                    gfx = XGraphics.FromPdfPage(NextPage);
                    drawRec = true;
                    hPos = 115;
                }

            }

            gfx.DrawLine(new XPen(XColorLightBrownLine, 0.5), vPosL, LinehPos, vPosR, LinehPos);
            gfx.DrawLine(new XPen(XColorLightBrownLine, 0.5), vPosL, hPos, vPosR, hPos);

            totalPosGes.RoundCurrency();

            if (hPos > 750)
            {
                // Create an empty page
                PdfPage NextPage = doc.AddPage();

                // Get an XGraphics object for drawing
                gfx = XGraphics.FromPdfPage(NextPage);
                drawRec = true;
                hPos = 115;
            }
            else
            {
                hPos = gfx.PageSize.Height - 100;

            }

            hPos += 5;
            gfx.DrawString("Gesamt Netto", ft8Std, XBrushes.Black, new XRect(vPosL + 275, hPos, PageOne.Width, PageOne.Height), XStringFormats.TopLeft);
            gfx.DrawString(totalPosGes.ToFormatStringEuro(), ft8Std, XBrushes.Black, new XRect(vPosL + 375, hPos, PageOne.Width, PageOne.Height), XStringFormats.TopLeft);
            hPos += 15;
            decimal tax = totalPosGes * 0.19m;
            gfx.DrawString("MwSt.(19%)", ft8Std, XBrushes.Black, new XRect(vPosL + 275, hPos, PageOne.Width, PageOne.Height), XStringFormats.TopLeft);
            gfx.DrawString(tax.ToFormatStringEuro(), ft8Std, XBrushes.Black, new XRect(vPosL + 375, hPos, PageOne.Width, PageOne.Height), XStringFormats.TopLeft);

            hPos += 15;
            gfx.DrawLine(new XPen(XColorBlack, 0.5), vPosL, hPos, vPosR, hPos);

            hPos += 5;
            gfx.DrawString("EUR Gesamt Brutto", ft9Std, XBrushes.Orange, new XRect(vPosL + 275, hPos, PageOne.Width, PageOne.Height), XStringFormats.TopLeft);
            gfx.DrawString(RoundCurrency(totalPosGes + tax).ToFormatStringEuro(), ft9Std, XBrushes.Orange, new XRect(vPosL + 375, hPos, PageOne.Width, PageOne.Height), XStringFormats.TopLeft);

            hPos += 15;
            gfx.DrawLine(new XPen(XColorBlack), vPosL, hPos, vPosR, hPos);



            doc.Save(new FileStream(FilePath, FileMode.Create, FileAccess.Write));
        }

        private static decimal RoundCurrency(this decimal currencyDecimal)
        {
            return Math.Round(currencyDecimal, 2, MidpointRounding.AwayFromZero);
        }

        private static String ToFormatStringEuro(this decimal price)
        {
            return string.Format(CultureInfo.CreateSpecificCulture("de-DE"), "{0:C}", price);
        }

        public class CustomerPDFDocument
        {
            private FileInfo TemporaryFile;

            public CustomerPDFDocument(Customer Customer)
            {
                TemporaryFile = new FileInfo(System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".pdf");
                Customer.GenerateTotalCalculation(TemporaryFile.FullName);
                TemporaryFile.IsReadOnly = true;
            }

            public void savePDF(String Path, bool open)
            {
                FileStream fs = new FileStream(Path, FileMode.Create, FileAccess.Write);
                new FileStream(TemporaryFile.FullName, FileMode.Open, FileAccess.Read).CopyTo(fs);
                if (open)
                {
                    String FilePath = fs.Name;
                    fs.Close();
                    Process.Start(FilePath);
                }
            }
            public void savePDF(bool open)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Pdf Files|*.pdf";
                sfd.Title = "Gesamtkalkulation speichern";
                sfd.ShowDialog();


                if (!sfd.FileName.Equals(""))
                {
                    savePDF(sfd.FileName, open);
                }
            }

            public void showPDF()
            {
                Process.Start(TemporaryFile.FullName);
            }

            public void printPDF(bool fastCopy)
            {

                PrintDialog printDialog = new PrintDialog();
                PrinterSettings settings = new PrinterSettings();
                printDialog.PageRangeSelection = PageRangeSelection.AllPages;
                printDialog.UserPageRangeEnabled = true;
                String printerExtraParameters = "";

                if (printDialog.ShowDialog() == true)
                {
                    Process printJob = new Process();
                    printJob.StartInfo.FileName = TemporaryFile.FullName;
                    printJob.StartInfo.UseShellExecute = true;
                    printJob.StartInfo.Verb = "printto";
                    printJob.StartInfo.CreateNoWindow = true;
                    printJob.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    printJob.StartInfo.Arguments = "\"" + printDialog.PrintQueue.Name + "\"" + " " + printerExtraParameters;
                    printJob.StartInfo.WorkingDirectory = TemporaryFile.DirectoryName;
                    printJob.Start();

                }
            }

            private bool IsFileReady()
            {
                // If the file can be opened for exclusive access it means that the file
                // is no longer locked by another process.
                try
                {
                    using (FileStream inputStream = File.Open(TemporaryFile.FullName, FileMode.Open, FileAccess.Read, FileShare.None))
                    {
                        if (inputStream.Length > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }

                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }

            ~CustomerPDFDocument()
            {
                TemporaryFile.IsReadOnly = false;
                while (!IsFileReady())
                {

                }
                TemporaryFile.Delete();
            }
        }
    }
}
