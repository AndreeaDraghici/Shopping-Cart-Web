using Lucene.Net.Analysis.Standard;
using Lucene.Net.Analysis;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Store;
using ShoppingCartWeb.Models;
using System.IO;


namespace ShoppingCartWeb.Logic.Service
{
    public class SearchIndex
    {
        private readonly Lucene.Net.Store.Directory _indexDirectory;
        private readonly Analyzer _analyzer;
        private readonly IndexWriterConfig _writerConfig;
        private readonly IndexWriter _indexWriter;

        public SearchIndex(string indexDirectoryPath)
        {
            _indexDirectory = FSDirectory.Open(indexDirectoryPath);
            _analyzer = new StandardAnalyzer(Lucene.Net.Util.LuceneVersion.LUCENE_48);

            _writerConfig = new IndexWriterConfig(Lucene.Net.Util.LuceneVersion.LUCENE_48, _analyzer);
            _indexWriter = new IndexWriter(_indexDirectory, _writerConfig);
        }

        public void AddDocument(DocumentModel document)
        {
            if (Path.GetExtension(document.Filename) == ".pdf")
            {
                document.ExtractTextFromPdf(document.Filename);
            }

            var luceneDoc = new Document
            {
                 new TextField("content", document.Content, Field.Store.YES)
            };

            _indexWriter.AddDocument(luceneDoc);
        }

        public void Commit()
        {
            _indexWriter.Commit();
        }

        public void Close()
        {
            _indexWriter.Dispose();
            _analyzer.Dispose();
            _indexDirectory.Dispose();
        }
    }

}