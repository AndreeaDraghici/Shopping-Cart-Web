<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductList.aspx.cs" Inherits="ShoppingCartWeb.ProductList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

              <script>
                  $(document).ready(function () {
                      $("#myInput").on("keyup", function () {
                          var value = $(this).val().toLowerCase();
                          $("#myTable tr").filter(function () {
                              $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
                          });
                      });
                  });
              </script>

               <input id="myInput" type="text" placeholder=" Search a product... " style="
                      padding: 12px 20px;
                      margin: 8px 0;
                      box-sizing: border-box;
                      border: 3px solid red;
                      border-radius: 4px;
                      background-color: aquamarine;
                      color: black;
              ">
           
              <head>
                  <link rel="stylesheet" href="//code.jquery.com/ui/1.13.1/themes/smoothness/jquery-ui.css">
                  <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
                  <script src="https://code.jquery.com/ui/1.13.1/jquery-ui.min.js"></script>
              </head>

             <input type="text" id="search-box" placeholder="Predictive search..."style="
                      padding: 12px 20px;
                      margin: 8px 0;
                      box-sizing: border-box;
                      border: 3px solid yellow;
                      border-radius: 4px;
                      background-color: aquamarine;
                      color: black;
              ">

            <script>
                $(function () {
                    // Create an array of product names to be used as the source for autocomplete.
                    var productNames = [];
                    $('#myTable tr span:first-child').each(function () {
                        productNames.push($(this).text());
                    });
                    // Initialize the autocomplete functionality.
                    $('#search-box').autocomplete({
                        source: productNames,
                        select: function (event, ui) {
                            // Find the rows in the table that match the selected product name.
                            var searchTerm = ui.item.value;
                            $('#myTable tr').hide();
                            $('#myTable tr span:first-child').filter(function () {
                                return $(this).text() === searchTerm;
                            }).closest('tr').show();
                        }
                    });
                });

            </script>

            <section>
                <div>
                    <hgroup>
                        <h2><%: Page.Title %></h2>
                    </hgroup>

                    <asp:ListView ID="productList" runat="server" 
                        DataKeyNames="ProductID" GroupItemCount="4"
                        ItemType="ShoppingCartWeb.Models.Product" SelectMethod="GetProducts">
                        <EmptyDataTemplate>
                            <table >
                                <tr>
                                    <td>No data was returned.</td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                        <EmptyItemTemplate>
                            <td/>
                        </EmptyItemTemplate>
                        <GroupTemplate>
                            <tr id="itemPlaceholderContainer" runat="server">
                                <td id="itemPlaceholder" runat="server"></td>
                            </tr>
                        </GroupTemplate>

                        <ItemTemplate>
                            <td runat="server">
                                <table id="myTable">
                                    <tr>
                                        <td>
                                            <a href="ProductDetails.aspx?productID=<%#:Item.ProductID%>">
                                                <img src="/Images/<%#:Item.ImagePath%>"
                                                    width="100" height="75" style="border: solid" /></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <a href="ProductDetails.aspx?productID=<%#:Item.ProductID%>">
                                                <span>
                                                    <%#:Item.ProductName%>
                                                </span>
                                            </a>
                                            <br />
                                            <span>
                                                <b>Price: </b><%#:String.Format("{0:c}", Item.UnitPrice)%>
                                            </span>
                                            <br />
                                            <a href="/AddToCart.aspx?productID=<%#:Item.ProductID %>">               
                                                <span class="ProductListItem">
                                                    <b>Add To Cart<b>
                                                </span>           
                                            </a>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                    </tr>
                                </table>
                                </p>
                            </td>
                        </ItemTemplate>
                        <LayoutTemplate>
                            <table style="width:100%;">
                                <tbody>
                                    <tr>
                                        <td>
                                            <table id="groupPlaceholderContainer" runat="server" style="width:100%">
                                                <tr id="groupPlaceholder"></tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                    </tr>
                                    <tr></tr>
                                </tbody>
                            </table>
                        </LayoutTemplate>
                    </asp:ListView>
                </div>
            </section>
</asp:Content>