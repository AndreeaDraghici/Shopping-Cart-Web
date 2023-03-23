using Lucene.Net.Analysis.Standard;
using Lucene.Net.Analysis;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.Store;
using ShoppingCartWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTextSharp.text.pdf.parser;
using java.nio.file;
using iTextSharp.text.pdf;
using System.Text;
using Remotion.Linq.Parsing.Structure;

namespace ShoppingCartWeb.Logic.Service
{
    public class Searcher
    {
        private readonly Directory _indexDirectory;
        private readonly Analyzer _analyzer;
        private readonly IndexReader _indexReader;
        private readonly IndexSearcher _indexSearcher;

        public Searcher(string indexDirectoryPath)
        {
            _indexDirectory = FSDirectory.Open(indexDirectoryPath);
            _analyzer = new StandardAnalyzer(Lucene.Net.Util.LuceneVersion.LUCENE_48);

            _indexReader = DirectoryReader.Open(_indexDirectory);
            _indexSearcher = new IndexSearcher(_indexReader);
        }

        public IList<DocumentModel> Search(string queryText)
        {
            var queryParser = new QueryParser(Lucene.Net.Util.LuceneVersion.LUCENE_30, "content", _analyzer);
            var query = queryParser.Parse(queryText);

            var topDocs = _indexSearcher.Search(query, 10);
            var documentModels = new List<DocumentModel>();

            
            foreach (var scoreDoc in topDocs.ScoreDocs)
            {
                var document = _indexSearcher.Doc(scoreDoc.Doc);
                var content = document.Get("content");
                var filename = document.Get("filename");

                // Check if the file is a PDF and extract its contents
                if (System.IO.Path.GetExtension(filename).Equals(".pdf", StringComparison.OrdinalIgnoreCase))
                {
                    var pdfReader = new PdfReader(filename);
                    var stringBuilder = new StringBuilder();

                    for (var page = 1; page <= pdfReader.NumberOfPages; page++)
                    {
                        var strategy = new SimpleTextExtractionStrategy();
                        var text = PdfTextExtractor.GetTextFromPage(pdfReader, page, strategy);
                        stringBuilder.AppendLine(text);
                    }

                    pdfReader.Close();
                    content = stringBuilder.ToString();
                }

                documentModels.Add(new DocumentModel
                {
                    Id = document.Get("id"),
                    Filename = filename,
                    Content = content
                });
            }

            return documentModels;
        }
            
        public void Close()
        {
            _indexReader.Dispose();
            _analyzer.Dispose();
            _indexDirectory.Close();
        }

    }
}