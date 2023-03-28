using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCartWeb.Models
{
    public class DocumentModel
    {
        public string Id { get; set; }
        public string Filename { get; set; }
        public string Content { get; set; }

        internal void ExtractTextFromPdf(string filename)
        {
            throw new NotImplementedException();
        }
    }
}