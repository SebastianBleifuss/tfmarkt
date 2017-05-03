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
    /// <summary>
    /// Extention class for Customer
    /// </summary>
    public static class PDFFactory
    {
        /// <summary>
        /// Extention method for creating a PDF from a customer and his calculations
        /// </summary>
        /// <param name="Customer">Customer</param>
        /// <param name="FilePath">Path where the PDF will be saved</param>
        private static void GenerateTotalCalculation(this Customer Customer, String FilePath)
        {
            //Create an set XPdfFontOptions
            XPdfFontOptions options = new XPdfFontOptions(PdfFontEncoding.Unicode, PdfFontEmbedding.Always);

            //Create a fonts
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



            //Create a new PDF document
            PdfDocument doc = new PdfDocument();

            //Create an empty page
            PdfPage PageOne = doc.AddPage();

            //Get an XGraphics object for drawing
            XGraphics gfx = XGraphics.FromPdfPage(PageOne);

            //Set CompanyLogo
            XImage CompanyLogo = XImage.FromFile("..\\datastorage\\CompanyLogo.png");
            int CompanyLogoWidth = CompanyLogo.PixelWidth / 2;
            int CompanyLogoHeight = CompanyLogo.PixelHeight / 2;

            //X,Y,Width,Height
            gfx.DrawImage(CompanyLogo, gfx.PageSize.Width - 50 - CompanyLogoWidth, 55, 250, 100);
            

            //Create XColors
            XColor XColorBlack = XColor.FromArgb(0, 0, 0);
            XColor XColorLightBrown = XColor.FromArgb(249, 245, 238);
            XColor XColorLightBrownLine = XColor.FromArgb(220, 197, 156);

            //Define positions for vertical and horizontal allignment
            double vPos = 120;
            double hPosR = gfx.PageSize.Width - 70;
            double hPosL = 60;

            //Create document header
            gfx.DrawString("tfMarkt GmbH & Co. KG.", Headerft20Fett, XBrushes.Black, new XRect(hPosL, vPos, PageOne.Width, PageOne.Height), XStringFormats.TopLeft);

            vPos += 25;
            gfx.DrawLine(new XPen(XColorBlack), hPosL, vPos, hPosR, vPos);//Draw black line

            vPos += 5;
            gfx.DrawString(DateTime.Now.ToShortDateString(), ft9Fett, XBrushes.Black, new XRect(hPosL, vPos, PageOne.Width, PageOne.Height), XStringFormats.TopLeft);//Write date

            vPos += 15;
            gfx.DrawLine(new XPen(XColorBlack), hPosL, vPos, hPosR, vPos);//Draw black line

            vPos += 30;//Write customer informations
            gfx.DrawString("Kundenname:", ft11Fett, XBrushes.Black, new XRect(hPosL, vPos, PageOne.Width, PageOne.Height), XStringFormats.TopLeft);
            gfx.DrawString(Customer.Name, ft9Std, XBrushes.Black, new XRect(hPosL + 120, vPos + 2, PageOne.Width, PageOne.Height), XStringFormats.TopLeft);

            vPos += 15;
            gfx.DrawString("Kundennummer:", ft11Fett, XBrushes.Black, new XRect(hPosL, vPos, PageOne.Width, PageOne.Height), XStringFormats.TopLeft);
            gfx.DrawString(Customer.Customernumber.ToString(), ft9Std, XBrushes.Black, new XRect(hPosL + 120, vPos + 3, PageOne.Width, PageOne.Height), XStringFormats.TopLeft);

            vPos += 50;
            gfx.DrawLine(new XPen(XColorBlack), hPosL, vPos, hPosR, vPos);

            // Calculations
            Dictionary<Guid, Calculation> Calculations = Customer.Calculations;

            vPos += 5;//Write column header
            gfx.DrawString("MENGE", Headerft10Std, XBrushes.Black, new XRect(hPosL + 10, vPos, PageOne.Width, PageOne.Height), XStringFormats.TopLeft);
            gfx.DrawString("DETAILS", Headerft10Std, XBrushes.Black, new XRect(hPosL + 75, vPos, PageOne.Width, PageOne.Height), XStringFormats.TopLeft);
            gfx.DrawString("EINZELPREIS", Headerft10Std, XBrushes.Black, new XRect(hPosL + 275, vPos, PageOne.Width, PageOne.Height), XStringFormats.TopLeft);
            gfx.DrawString("POSITION GESAMT", Headerft10Std, XBrushes.Black, new XRect(hPosL + 375, vPos, PageOne.Width, PageOne.Height), XStringFormats.TopLeft);

            vPos += 15;//Draw colored row
            gfx.DrawRectangle(new XSolidBrush(XColorLightBrown), hPosL, vPos - 0.5, hPosR - hPosL, 20);

            double LinehPos = vPos;
            bool drawRec = true;
            decimal totalPosGes = 0;
            foreach (Calculation calc in Calculations.Values)
            {
                if (drawRec)
                {
                    drawRec = false;
                    gfx.DrawRectangle(new XSolidBrush(XColorLightBrown), hPosL, vPos - 0.5, hPosR - hPosL, 20);//Draw colored row
                }
                else
                {
                    drawRec = true;

                }


                //Write product informations
                vPos += 5;
                Product p = calc.SelectedProduct;
                gfx.DrawString(calc.Amount.ToString(), ft8Std, XBrushes.Black, new XRect(hPosL + 10, vPos, PageOne.Width, PageOne.Height), XStringFormats.TopLeft);

                String Description = p.getArtikelbezeichnung();
                if (Description.Length > 22)
                {
                    Description = Description.Substring(0, 22) + "...";
                }
                gfx.DrawString(Description, ft8Std, XBrushes.Black, new XRect(hPosL + 75, vPos, PageOne.Width, PageOne.Height), XStringFormats.TopLeft);
                gfx.DrawString(p.getPreis().ToFormatStringEuro(), ft8Std, XBrushes.Black, new XRect(hPosL + 275, vPos, PageOne.Width, PageOne.Height), XStringFormats.TopLeft);//Write currency formated price
                decimal posGes = RoundCurrency((calc.Amount * p.getPreis()));//Calculate total price and round it away from zero
                totalPosGes += posGes;//Add total position price to global total position price
                gfx.DrawString(posGes.ToFormatStringEuro(), ft8Std, XBrushes.Black, new XRect(hPosL + 375, vPos, PageOne.Width, PageOne.Height), XStringFormats.TopLeft);
                vPos += 15;



                if (vPos >= 800)//Check if page end is reached
                {
                    // Create an empty page
                    PdfPage NextPage = doc.AddPage();

                    // Get an XGraphics object for drawing
                    gfx = XGraphics.FromPdfPage(NextPage);
                    drawRec = true;
                    vPos = 115;
                }

            }

            //Draw colored line at top and bottom of the table
            gfx.DrawLine(new XPen(XColorLightBrownLine, 0.5), hPosL, LinehPos, hPosR, LinehPos);
            gfx.DrawLine(new XPen(XColorLightBrownLine, 0.5), hPosL, vPos, hPosR, vPos);

            totalPosGes = totalPosGes.RoundCurrency();

            if (vPos > 750)
            {
                // Create an empty page
                PdfPage NextPage = doc.AddPage();

                // Get an XGraphics object for drawing
                gfx = XGraphics.FromPdfPage(NextPage);
                drawRec = true;
                vPos = 115;
            }
            else
            {
               // hPos = gfx.PageSize.Height - 100;

            }

            //Draw global total price and taxes
            vPos += 5;
            gfx.DrawString("Gesamt Netto", ft8Std, XBrushes.Black, new XRect(hPosL + 275, vPos, PageOne.Width, PageOne.Height), XStringFormats.TopLeft);
            gfx.DrawString(totalPosGes.ToFormatStringEuro(), ft8Std, XBrushes.Black, new XRect(hPosL + 375, vPos, PageOne.Width, PageOne.Height), XStringFormats.TopLeft);
            vPos += 15;
            decimal tax = totalPosGes * 0.19m;
            gfx.DrawString("MwSt.(19%)", ft8Std, XBrushes.Black, new XRect(hPosL + 275, vPos, PageOne.Width, PageOne.Height), XStringFormats.TopLeft);
            gfx.DrawString(tax.ToFormatStringEuro(), ft8Std, XBrushes.Black, new XRect(hPosL + 375, vPos, PageOne.Width, PageOne.Height), XStringFormats.TopLeft);

            vPos += 15;
            gfx.DrawLine(new XPen(XColorBlack, 0.5), hPosL, vPos, hPosR, vPos);

            vPos += 5;
            gfx.DrawString("EUR Gesamt Brutto", ft9Std, XBrushes.Orange, new XRect(hPosL + 275, vPos, PageOne.Width, PageOne.Height), XStringFormats.TopLeft);
            gfx.DrawString(RoundCurrency(totalPosGes + tax).ToFormatStringEuro(), ft9Std, XBrushes.Orange, new XRect(hPosL + 375, vPos, PageOne.Width, PageOne.Height), XStringFormats.TopLeft);

            vPos += 15;
            gfx.DrawLine(new XPen(XColorBlack), hPosL, vPos, hPosR, vPos);


            //Save PDF to FilePath with write-only-permissions
            doc.Save(new FileStream(FilePath, FileMode.Create, FileAccess.Write));
        }


        /// <summary>
        /// Extention method for round decimal at its secound position away from zero
        /// </summary>
        /// <param name="currencyDecimal">decimal for rounding</param>
        /// <returns>rounded decimal</returns>
        private static decimal RoundCurrency(this decimal currencyDecimal)
        {
            return Math.Round(currencyDecimal, 2, MidpointRounding.AwayFromZero);
        }


        /// <summary>
        /// Extention method for format decimal to string for currency euro
        /// </summary>
        /// <param name="price">decimal for formating</param>
        /// <returns>string as formated decimal</returns>
        private static String ToFormatStringEuro(this decimal price)
        {
            return string.Format(CultureInfo.CreateSpecificCulture("de-DE"), "{0:C}", price);
        }

        /// <summary>
        /// Class to manage PDF-generation, saving and printing
        /// </summary>
        public class CustomerPDFDocument
        {
            /// <summary>
            /// FileInfo of temporary file
            /// </summary>
            private FileInfo TemporaryFile;

            /// <summary>
            /// Constructor which will generate PDF from passed Customer
            /// </summary>
            /// <param name="Customer">Customer for PDF-generation</param>
            public CustomerPDFDocument(Customer Customer)
            {
                TemporaryFile = new FileInfo(System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".pdf");//Create temporary file and set FileInfo of it
                Customer.GenerateTotalCalculation(TemporaryFile.FullName);//Generate PDF and save to temporary file
                TemporaryFile.IsReadOnly = true;//Set temporary file read-only
            }


            /// <summary>
            /// Save PDF to passed Path
            /// </summary>
            /// <param name="Path">Path where the PDF will be saved</param>
            /// <param name="open">Defines if the PDF should be opened after saving</param>
            public void savePDF(String Path, bool open)
            {
                FileStream fs = new FileStream(Path, FileMode.Create, FileAccess.Write);
                new FileStream(TemporaryFile.FullName, FileMode.Open, FileAccess.Read).CopyTo(fs);//Copy data from temporary file to saving file via filestream
                if (open)
                {
                    String FilePath = fs.Name;
                    fs.Close();
                    Process.Start(FilePath);//Send PDF to Process to trigger windows opening sequence
                }
            }
            /// <summary>
            ///  Save PDF to Path selected in SaveFileDialog
            /// </summary>
            /// <param name="open">Defines if the PDF should be opened after saving</param>
            public void savePDF(bool open)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Pdf Files|*.pdf";//Set file filter
                sfd.Title = "Gesamtkalkulation speichern";
                sfd.ShowDialog();


                if (!sfd.FileName.Equals(""))
                {
                    savePDF(sfd.FileName, open);//Call other savePDF method with selected Path
                }
            }


            /// <summary>
            /// Display PDF file
            /// </summary>
            public void showPDF()
            {
                Process.Start(TemporaryFile.FullName);//Send PDF to Process to trigger windows opening sequence
            }


            /// <summary>
            /// print PDF file
            /// </summary>
            public void printPDF()
            {
                //Create PrintDialog and configure it
                PrintDialog printDialog = new PrintDialog();
                PrinterSettings settings = new PrinterSettings();
                printDialog.PageRangeSelection = PageRangeSelection.AllPages;
                printDialog.UserPageRangeEnabled = true;
                String printerExtraParameters = "";

                if (printDialog.ShowDialog() == true)
                {
                    //Create and execute print process
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


            /// <summary>
            /// Checks if temporary file isn't in use and ready for modifying
            /// </summary>
            /// <returns>If temporary file isn't in use and ready for modifying</returns>
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


            //Deconstructor which delete temporary file after usage
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
