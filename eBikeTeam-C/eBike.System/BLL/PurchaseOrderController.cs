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
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
#endregion

namespace eBike.System.BLL
{
    [DataObject]
    public class PurchaseOrderController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<PurchaseOrderList> Get_Order(int vendorid)
        {
            if (vendorid == 0)
            {
                return null;
            }
            using (var context = new eBikesContext())
            {
                var purhcaseorderid = from x in context.PurchaseOrders
                                      where x.VendorID == vendorid && x.OrderDate.Equals(null) && x.PurchaseOrderNumber.Equals(null)
                                      select x.PurchaseOrderID;
                List<PurchaseOrderList> Details = null;
                if (purhcaseorderid.Count() > 0)
                {
                    Details =
                         context.PurchaseOrderDetails.Where(x => x.PurchaseOrderID == purhcaseorderid.FirstOrDefault())
                             .Select(x => new PurchaseOrderList
                             {
                                 itemid = x.PartID,
                                 Description = x.Part.Description,
                                 QOH = x.Part.QuantityOnHand,
                                 QOO = x.Part.QuantityOnOrder,
                                 ROL = x.Part.ReorderLevel,
                                 Quantity = x.Quantity,
                                 Price = x.Part.PurchasePrice
                             }).ToList();
                    return Details;
                }
                else
                {
                    Details = context.Parts.Where(x => x.VendorID == vendorid && (x.ReorderLevel > x.QuantityOnHand + x.QuantityOnOrder))
                                .Select(x => new PurchaseOrderList
                                {
                                    itemid = x.PartID,
                                    Description = x.Description,
                                    QOH = x.QuantityOnHand,
                                    QOO = x.QuantityOnOrder,
                                    ROL = x.ReorderLevel,
                                    Quantity = 0,
                                    Price = x.PurchasePrice
                                }).ToList();

                    PurchaseOrder newOrder = new PurchaseOrder();
                    newOrder.VendorID = vendorid;
                    newOrder.TaxAmount = 0;
                    newOrder.SubTotal = 0;
                    newOrder.PurchaseOrderNumber = null;
                    newOrder.OrderDate = null;
                    newOrder.Notes = "none";
                    newOrder.EmployeeID = 5;

                    newOrder = context.PurchaseOrders.Add(newOrder);
                    PurchaseOrder GenerateOrder = context.PurchaseOrders.Find(newOrder.PurchaseOrderID);

                    foreach (PurchaseOrderList item in Details)
                    {
                        PurchaseOrderDetail order = new PurchaseOrderDetail();
                        order.PurchaseOrderID = GenerateOrder.PurchaseOrderID;
                        order.Quantity = 1;
                        order.PurchasePrice = item.Price;
                        order.PartID = item.itemid;
                        order.VendorPartNumber = "";

                        context.PurchaseOrderDetails.Add(order);
                    }
                    context.SaveChanges();
                    return Details;
                }
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<PurchaseOrderList> Get_currentInventory(int vendorid)
        {
            using (var context = new eBikesContext())
            {
                var purhcaseorderid = from x in context.PurchaseOrders
                                      where x.VendorID == vendorid && x.OrderDate.Equals(null) && x.PurchaseOrderNumber.Equals(null)
                                      select x.PurchaseOrderID;

                var results = from y in context.Parts
                              where y.VendorID == vendorid && !(from x in context.PurchaseOrderDetails
                                                                where x.PurchaseOrderID == purhcaseorderid.FirstOrDefault()
                                                                select x.PartID).Contains(y.PartID)
                              select new PurchaseOrderList
                              {
                                  itemid = y.PartID,
                                  Description = y.Description,
                                  QOH = y.QuantityOnHand,
                                  ROL = y.ReorderLevel,
                                  QOO = y.QuantityOnOrder,
                                  Price = y.PurchasePrice
                              };

                return results.ToList();

            }
        }

        public string transaction(List<PurchaseOrderList> incoming, int selectionvalue, int vendorid, decimal subtotal, decimal tax)
        {
            using (var context = new eBikesContext())
            {
                var purhcaseorderid = (from x in context.PurchaseOrders
                                       where x.VendorID == vendorid && x.OrderDate.Equals(null) && x.PurchaseOrderNumber.Equals(null)
                                       select x.PurchaseOrderID).ToList();

                if (selectionvalue == 1)
                {
                    List<PurchaseOrderList> existing = (from x in context.PurchaseOrderDetails
                                                        where x.PurchaseOrderID == purhcaseorderid.FirstOrDefault()
                                                        select new PurchaseOrderList
                                                        {
                                                            itemid = x.PartID,
                                                            Description = x.Part.Description,
                                                            QOH = x.Part.QuantityOnHand,
                                                            ROL = x.Part.ReorderLevel,
                                                            QOO = x.Part.QuantityOnOrder,
                                                            Quantity = x.Quantity,
                                                            Price = x.PurchasePrice
                                                        }).ToList();

                    var insertincomingdata = incoming.Where(x => !existing.Any(y => y.itemid == x.itemid));
                    var updateincomingdata = incoming.Where(x => existing.Any(y => y.itemid == x.itemid));

                    PurchaseOrderDetail Detail = new PurchaseOrderDetail();

                    foreach (var functioninsert in insertincomingdata)
                    {
                        Detail = new PurchaseOrderDetail();
                        Detail.PartID = functioninsert.itemid;
                        Detail.PurchasePrice = functioninsert.Price;
                        Detail.Quantity = functioninsert.Quantity;
                        Detail.PurchaseOrderID = purhcaseorderid.FirstOrDefault();
                        context.PurchaseOrderDetails.Add(Detail);
                    }

                    foreach (var functionupdate in updateincomingdata)
                    {
                        int id = (from x in context.PurchaseOrderDetails
                                  where x.PurchaseOrderID == purhcaseorderid.FirstOrDefault() && x.PartID == functionupdate.itemid
                                  select x.PurchaseOrderDetailID).Single();

                        PurchaseOrderDetail updation = context.PurchaseOrderDetails.Find(id);

                        updation.PurchasePrice = functionupdate.Price;
                        updation.Quantity = functionupdate.Quantity;

                        var newlyadded = context.PurchaseOrderDetails.Attach(updation);
                        var updated = context.Entry(newlyadded);

                        updated.State = EntityState.Modified;
                    }
                    context.SaveChanges();

                    return "Ordernumber " + purhcaseorderid.Single() + " Updated to refect recent changes";
                }
                else if (selectionvalue == 2)
                {
                    PurchaseOrder newOrder = new PurchaseOrder();
                    newOrder.PurchaseOrderID = purhcaseorderid.FirstOrDefault();
                    newOrder.VendorID = vendorid;
                    newOrder.EmployeeID = 5;
                    newOrder.SubTotal = subtotal;
                    newOrder.TaxAmount = tax;
                    newOrder.OrderDate = DateTime.Now;
                    newOrder.PurchaseOrderNumber = (from x in context.PurchaseOrders select x.PurchaseOrderNumber).Max() + 1;
                    newOrder.Closed = false;
                    newOrder.Notes = "";

                    var attachmenttopo = context.PurchaseOrders.Attach(newOrder);
                    var theOrderpur = context.Entry(attachmenttopo);
                    theOrderpur.State = EntityState.Modified;

                    List<PurchaseOrderList> existing = (from x in context.PurchaseOrderDetails
                                                        where x.PurchaseOrderID == purhcaseorderid.FirstOrDefault()
                                                        select new PurchaseOrderList
                                                        {
                                                            itemid = x.PartID,
                                                            Description = x.Part.Description,
                                                            QOH = x.Part.QuantityOnHand,
                                                            ROL = x.Part.ReorderLevel,
                                                            QOO = x.Part.QuantityOnOrder,
                                                            Quantity = x.Quantity,
                                                            Price = x.PurchasePrice
                                                        }).ToList();

                    var insertincomingdata = incoming.Where(x => !existing.Any(y => y.itemid == x.itemid));
                    var updateincomingdata = incoming.Where(x => existing.Any(y => y.itemid == x.itemid));


                    PurchaseOrderDetail Detail = new PurchaseOrderDetail();

                    foreach (var functioninsert in insertincomingdata)
                    {
                        Detail = new PurchaseOrderDetail();
                        Detail.PartID = functioninsert.itemid;
                        Detail.PurchasePrice = functioninsert.Price;
                        Detail.Quantity = functioninsert.Quantity;
                        Detail.PurchaseOrderID = purhcaseorderid.FirstOrDefault();
                        context.PurchaseOrderDetails.Add(Detail);
                    }

                    foreach (var functionupdate in updateincomingdata)
                    {
                        int id = (from x in context.PurchaseOrderDetails where x.PurchaseOrderID == purhcaseorderid.FirstOrDefault() && x.PartID == functionupdate.itemid select x.PurchaseOrderDetailID).Single();
                        PurchaseOrderDetail updation = context.PurchaseOrderDetails.Find(id);
                        updation.PartID = functionupdate.itemid;
                        updation.PurchasePrice = functionupdate.Price;
                        updation.Quantity = functionupdate.Quantity;
                        updation.PurchaseOrderID = purhcaseorderid.FirstOrDefault();
                        var newlyadded = context.PurchaseOrderDetails.Attach(updation);
                        var updated = context.Entry(newlyadded);
                        updated.State = EntityState.Modified;
                    }
                    context.SaveChanges();
                    return "Ordernumber" + purhcaseorderid.Single() + "Placed";
                }
                else if (selectionvalue == 3)
                {
                    var results = (from x in context.PurchaseOrderDetails
                                   where x.PurchaseOrderID == purhcaseorderid.FirstOrDefault()
                                   select x).ToList();

                    foreach (PurchaseOrderDetail item in results)
                    {
                        context.PurchaseOrderDetails.Remove(item);
                    }
                    PurchaseOrder ordertobedeleted = context.PurchaseOrders.Find(purhcaseorderid.FirstOrDefault());
                    context.PurchaseOrders.Remove(ordertobedeleted);
                    context.SaveChanges();
                    return "Order Succefully Deleted from the database";
                }
                else
                {
                    return "Screen Cleared";
                }
            }
        }
    }
}
