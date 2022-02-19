using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ShoppingCartWeb.Models;
using ShoppingCartWeb.Logic;

namespace ShoppingCartWeb
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            using (ShoppingCartActions usersShoppingCart = new ShoppingCartActions())
            {
                string cartStr = string.Format("Cart ({0})", usersShoppingCart.GetCount());
                cartCount.InnerText = cartStr;
            }
        }
        public IQueryable<Category> GetCategories()
        {
            var _db = new ShoppingCartWeb.Models.ProductContext();
            IQueryable<Category> query = _db.Categories;
            return query;
        }
    }
}