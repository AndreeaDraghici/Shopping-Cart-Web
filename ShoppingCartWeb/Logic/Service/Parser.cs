using ikvm.io;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using org.apache.pdfbox.pdmodel;
using org.apache.pdfbox.util;
using System;
using System.IO;
using System.util;

namespace ShoppingCartWeb.Logic.Service
{
    public class Parser
    {
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
                    if (pdfReader != null)
                    {
                        pdfReader.Close();
                    }
                }
            }
            finally
            {
                if (pdDocument != null)
                {
                    pdDocument.close();
                }

                if (inputStreamWrapper != null)
                {
                    inputStreamWrapper.close();
                }
            }
        }
       
    }

    public class MediaItem
    {
        public string Name { get; internal set; }
        public string filePath { get; private set; }

        internal Stream GetMediaStream()
        {
            var fileContent = File.ReadAllBytes("C:\\Users\\user\\OneDrive\\Documents\\doc1.pdf"); 
            return new MemoryStream(fileContent);
        }
    }
}