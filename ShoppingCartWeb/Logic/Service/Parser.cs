using ikvm.io;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using org.apache.pdfbox.pdmodel;
using org.apache.pdfbox.util;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.util;
using System.Web;
using System.Web.UI.WebControls;

namespace ShoppingCartWeb.Logic.Service
{
    public class Parser
    {
        /*
        public static string PdfParser(MediaItem mediaItem)
        {
            if (mediaItem is null)
            {
                throw new ArgumentNullException("Media " + nameof(mediaItem) + " item cannot be null!");
            }

            PDDocument pdDocument = null;
            InputStreamWrapper inputStreamWrapper = null;
            Stream stream = mediaItem.GetMediaStream();

            try
            {
                inputStreamWrapper = new InputStreamWrapper(stream);
                pdDocument = PDDocument.load(inputStreamWrapper);
                PDFTextStripper pdfTextStripper = new PDFTextStripper();
                string parsedPdf = pdfTextStripper.getText(pdDocument);
                Console.WriteLine(mediaItem.Name + " was parsed by pdfbox...", typeof(Util));
                return parsedPdf;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not parse pdf due to: " + mediaItem.Name, ex, typeof(Util));
                Console.WriteLine("We have to try to index the pdf with itextCharp then...", typeof(Util));

                PdfReader pdfReader = null;

                try
                {
                    Stream pdfReaderStream = mediaItem.GetMediaStream();
                    pdfReader = new PdfReader(pdfReaderStream);

                    StringWriter output = new StringWriter();
                    for (int i = 1; i <= pdfReader.NumberOfPages; i++)
                        output.WriteLine(PdfTextExtractor.GetTextFromPage(pdfReader, i,
                        new SimpleTextExtractionStrategy()));

                    Console.WriteLine(mediaItem.Name + " was parsed by itextCharp... ", typeof(Util));
                    return output.ToString();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(" Could not parse pdf with itextCharp either " + mediaItem.Name, exception, typeof(Util));
                    return String.Empty;
                }
                finally
                {
#pragma warning disable IDE0031 // Use null propagation
                    if (pdfReader != null)
                    {
                        pdfReader.Close();
                    }
#pragma warning restore IDE0031 // Use null propagation
                }
            }
            finally
            {
#pragma warning disable IDE0031 // Use null propagation
                if (pdDocument != null)
                {
                    pdDocument.close();
                }
#pragma warning restore IDE0031 // Use null propagation

#pragma warning disable IDE0031 // Use null propagation
                if (inputStreamWrapper != null)
                {
                    inputStreamWrapper.close();
                }
#pragma warning restore IDE0031 // Use null propagation
            }
        }
        */
    }
}