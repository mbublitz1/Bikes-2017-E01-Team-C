using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using eBike.Data.Entities;
using eBike.Data.POCOs;
using eBike.System.DAL;
using System.ComponentModel;
#endregion

namespace eBike.System.BLL.Sales
{
    public class ShoppingCartItemController
    {
        public void Add_Product_To_ShoppingCart(string username, int productId, int quantity)
        {
            using (var context = new eBikesContext())
            {
                ShoppingCart exists = (from x in context.ShoppingCarts
                                       where x.OnlineCustomer.UserName.Equals(username)
                                       select x).FirstOrDefault();
                ShoppingCartItem newItem = null;
                if (exists == null)
                {
                    exists = new ShoppingCart();
                    exists.OnlineCustomerID = (from x in context.OnlineCustomer
                                               where x.UserName == username
                                               select x.OnlineCustomerID).SingleOrDefault();
                    exists.CreatedOn = DateTime.Now;
                    exists = context.ShoppingCarts.Add(exists);
                } 
                
                newItem = exists.ShoppingCartItems.SingleOrDefault(x => x.PartID == productId);
                if (newItem != null)
                {
                    newItem.Quantity += quantity;
                } else
                {
                    newItem = new ShoppingCartItem();
                    newItem.PartID = productId;
                    newItem.Quantity = quantity;

                    exists.ShoppingCartItems.Add(newItem);
                }

                context.SaveChanges();
            }
        }

        public bool CheckForShoppingCartItems(string username)
        {
            using (var context = new eBikesContext())
            {
                bool check = true;
                var result = (from x in context.ShoppingCartItems
                             where x.ShoppingCart.OnlineCustomer.UserName.Equals(username)
                             select x).FirstOrDefault();
                if(result == null)
                {
                    check = false;
                }
                return check;

            }
        }
    }
}
