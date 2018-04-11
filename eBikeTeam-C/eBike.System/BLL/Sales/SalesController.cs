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
using System.Data.Entity;
#endregion

namespace eBike.System.BLL.Sales
{
    [DataObject]
    public class SalesController
    {
        public int CreatSaleFromCart(string username, decimal subtotal, int couponId, string paymentType)
        {
            using (var context = new eBikesContext())
            {
                var exists = (from x in context.ShoppingCarts
                             where x.OnlineCustomer.UserName.Equals(username)
                             select x).FirstOrDefault();
                if (exists == null)
                {
                    throw new Exception("Shopping Cart for user no longer exsists in the database");
                }
                else
                {
                    var itemsExist = (from x in exists.ShoppingCartItems
                                      select x).ToList();
                    if (itemsExist == null)
                    {
                        throw new Exception("There are no valid items in this shopping cart");
                    }
                    else
                    {
                        Sale newSale = new Sale();
                        newSale.SaleDate = DateTime.Now;
                        newSale.UserName = username;
                        newSale.EmployeeID = 10;
                        newSale.TaxAmount = 0;
                        newSale.SubTotal = subtotal;
                        if (couponId == 0)
                        {
                            newSale.CouponID = null;
                        }
                        else
                        {
                            newSale.CouponID = couponId;
                        }
                        newSale.PaymentType = paymentType;
                        newSale.PaymentToken = null;

                        newSale = context.Sales.Add(newSale);

                        foreach (ShoppingCartItem item in itemsExist)
                        {
                            SaleDetail newSaleItem = new SaleDetail();
                            newSaleItem.PartID = item.PartID;
                            newSaleItem.Quantity = item.Quantity;
                            newSaleItem.SellingPrice = item.Part.SellingPrice;

                            int partQty = (from x in context.Parts
                                           where x.PartID == item.PartID
                                           select x.QuantityOnHand).FirstOrDefault();
                            if (item.Quantity > partQty)
                            {
                                newSaleItem.Backordered = true;
                                newSaleItem.ShippedDate = null;
                            }
                            else
                            {
                                newSaleItem.Backordered = false;
                                newSaleItem.ShippedDate = DateTime.Now;

                                Part currentPart = context.Parts.Find(item.PartID);
                                currentPart.QuantityOnHand -= item.Quantity;

                                context.Parts.Attach(currentPart);

                                context.Entry(currentPart).State = EntityState.Modified;
                            }

                            newSale.SaleDetails.Add(newSaleItem);
                        }

                        foreach (ShoppingCartItem item in itemsExist)
                        {
                            context.ShoppingCartItems.Remove(item);
                        }

                        context.ShoppingCarts.Remove(exists);

                        context.SaveChanges();

                        return newSale.SaleID;
                    }
                }
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public Sale GetSaleConfirmation(int saleid)
        {
            using (var context = new eBikesContext())
            {
                var result = context.Sales.Find(saleid);
                return result;
            }
        }

    }
}
