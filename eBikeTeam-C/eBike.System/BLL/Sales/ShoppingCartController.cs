using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using eBike.Data.Entities;
using eBike.Data.POCOs;
using eBike.Data.DTOs;
using eBike.System.DAL;
using System.ComponentModel;
#endregion

namespace eBike.System.BLL.Sales
{
    [DataObject]
    public class ShoppingCartController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<ShoppingCartLineItemPOCO> List_ShoppingCartItems(string username)
        {

            using (var context = new eBikesContext())
            {
                var results = (from x in context.ShoppingCartItems
                               orderby x.Part.Description
                              where x.ShoppingCart.OnlineCustomer.UserName.Equals(username)
                              select new ShoppingCartLineItemPOCO
                              {
                                  ShoppingCartItemId = x.ShoppingCartItemID,
                                  Description = x.Part.Description,
                                  Qty = x.Quantity,
                                  UnitPrice = x.Part.SellingPrice,
                                  ItemTotal = x.Part.SellingPrice * x.Quantity,
                                  Backorder = (x.Part.QuantityOnHand - x.Quantity) >= 0 ? "" : "On Back-Order"                               
                              }).ToList();
                return results;
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public ShoppingCartCheckoutDTO GetCurrentCheckoutInfo(string username) 
        {
            using (var context = new eBikesContext())
            {
                //THIS IS CRASHING WHEN THERE ARE NO ITEMS IN A CART NEED SOME CHECKING HERE
                ShoppingCartCheckoutDTO result = (from x in context.ShoppingCarts
                              where x.OnlineCustomer.UserName.Equals(username)
                              select new ShoppingCartCheckoutDTO
                              {
                                  SubTotal = x.ShoppingCartItems.Sum(y => y.Quantity * y.Part.SellingPrice),
                                  Total = x.ShoppingCartItems.Sum(y => y.Quantity * y.Part.SellingPrice),
                                  ShoppingCartItems = (from y in x.ShoppingCartItems
                                                       orderby y.Part.Description
                                                       select new ShoppingCartLineItemPOCO
                                                       {
                                                           Description = y.Part.Description,
                                                           Qty = y.Quantity,
                                                           UnitPrice = y.Part.SellingPrice,
                                                           ItemTotal = y.Quantity * y.Part.SellingPrice,
                                                           ShoppingCartItemId = y.ShoppingCartItemID
                                                       }).ToList()
                              }).FirstOrDefault();
                return result;
            }
        }

        public decimal GetShoppingCartTotal(string username)
        {
            using (var context = new eBikesContext())
            {
                decimal total = 0;
                int exists = (from x in context.ShoppingCarts
                                where x.OnlineCustomer.UserName.Equals(username)
                                select x.ShoppingCartItems.Count(y => y.ShoppingCartItemID > 0)).SingleOrDefault();
                   
                if (exists > 0)
                {
                    total = (from x in context.ShoppingCarts
                                     where x.OnlineCustomer.UserName.Equals(username)
                                     select x.ShoppingCartItems.Sum(y => y.Quantity * y.Part.SellingPrice)
                                ).FirstOrDefault();
                }
                return total;
            }
        }

        public string ShowQtyCartAmount(string username)
        {
            using (var context = new eBikesContext())
            {
                string count = "";
                int exists = (from x in context.ShoppingCarts
                              where x.OnlineCustomer.UserName.Equals(username)
                              select x.ShoppingCartItems.Count(y => y.ShoppingCartItemID > 0)).SingleOrDefault();
             
                if (exists > 0)
                {
                    int currentCount = (from x in context.ShoppingCarts
                                        where x.OnlineCustomer.UserName.Equals(username)
                                        select x.ShoppingCartItems.Sum(y => y.Quantity)).SingleOrDefault();
                    count = currentCount.ToString();
                }
                return count;
            }
        }

        public void ChangeQtyOfPart(string username, int shoppingcartitemid, string  direction)
        {
            using (var context = new eBikesContext())
            {
                ShoppingCart currentCart = (from x in context.ShoppingCartItems
                                            where x.ShoppingCart.OnlineCustomer.UserName.Equals(username)
                                            select x.ShoppingCart).FirstOrDefault();
                if (currentCart == null)
                {
                    throw new Exception("The shopping cart was removed from the database");
                }
                else
                {
                    ShoppingCartItem currentItem = currentCart.ShoppingCartItems.Where(i => i.ShoppingCartItemID == shoppingcartitemid).FirstOrDefault();
                    if (currentItem == null)
                    {
                        throw new Exception("Could not find that part in the database");
                    }
                    else
                    {
                        if(direction.Equals("Add"))
                        {
                            currentItem.Quantity += 1;
                        } 
                        else if(direction.Equals("Subtract"))
                        {
                            currentItem.Quantity -= 1;
                            if (currentItem.Quantity <= 0)
                            {
                                context.ShoppingCartItems.Remove(currentItem);
                            }
                        }
                        context.SaveChanges();
                    }
                }
            }
        }

        public void DeleteItemFromCart(string username, int shoppingcartitemid)
        {
            using (var context = new eBikesContext())
            {
                ShoppingCart currentCart = (from x in context.ShoppingCartItems
                                            where x.ShoppingCart.OnlineCustomer.UserName.Equals(username)
                                            select x.ShoppingCart).FirstOrDefault();
                if(currentCart == null)
                {
                    throw new Exception("The shopping cart was removed from the database");
                }
                else
                {
                    ShoppingCartItem deleteItem = currentCart.ShoppingCartItems.Where(i => i.ShoppingCartItemID == shoppingcartitemid).FirstOrDefault();
                    if(deleteItem == null)
                    {
                        throw new Exception("Could not find that part in the database");
                    }
                    else
                    {
                        currentCart.UpdatedOn = DateTime.Now;
                        context.ShoppingCartItems.Remove(deleteItem);
                        context.SaveChanges();
                    }
                }   
            }
        }
    }
}
