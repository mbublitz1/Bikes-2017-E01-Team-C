using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eBike.Data.DTOs;

#region Additional Namespaces
using eBike.Data.Entities;
using eBike.System.DAL;
using eBike.Data.POCOs;
#endregion

namespace eBike.System.BLL
{
    [DataObject]
    public class ReceivingOrderController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<ReceivingOrderListPOCO> Get_OutstandingOrders()
        {
            using (var context = new eBikesContext())
            {
                var results = context.PurchaseOrders.Where(od => od.Closed == false && !String.IsNullOrEmpty(od.PurchaseOrderNumber.ToString()) && od.OrderDate != null ).Select(o => new ReceivingOrderListPOCO
                {
                    PurchaseOrderId = o.PurchaseOrderID,
                    PurchaseOrderNumber = o.PurchaseOrderNumber,
                    OrderDate = o.OrderDate,
                    VendorName = o.Vendor.VendorName,
                    VendorPhone = o.Vendor.Phone
                }).ToList();

                return results;
            }
        } //eom

        public ReceivingVendorOrderDetails Get_OrderDetails(int purchaseOrderId)
        {
            using (var context = new eBikesContext())
            {
                ReceivingVendorOrderDetails results = null;

                List<ReceivingOrderDetailsPOCO> orderDetails = null;

                var vendor =
                    context.PurchaseOrders.Where(po => po.PurchaseOrderID == purchaseOrderId).Select(v => new
                    {
                        PONumber = v.PurchaseOrderNumber,
                        Phone = v.Vendor.Phone,
                        VendorName = v.Vendor.VendorName

                    }).FirstOrDefault();

                orderDetails =
                     context.PurchaseOrderDetails.Where(pod => pod.PurchaseOrderID == purchaseOrderId && (pod.Quantity - pod.ReceiveOrderDetails.Sum(rod => rod.QuantityReceived)) != 0)
                         .Select(pod => new ReceivingOrderDetailsPOCO
                         {
                             PurchaseOrderId = pod.PurchaseOrderID,
                             PurchaseOrderDetailId = pod.PurchaseOrderDetailID,
                             PartId = pod.PartID,
                             PartDescription = pod.Part.Description,
                             QtyOnOrder = pod.Quantity,
                             QtyOutstanding = pod.ReceiveOrderDetails.Select(rod => rod.QuantityReceived).Any() ? pod.Quantity - pod.ReceiveOrderDetails.Sum(rod => rod.QuantityReceived) : pod.Quantity
                         }).ToList();

                if (vendor != null)
                {
                    results = new ReceivingVendorOrderDetails()
                    {
                        PONumber = vendor.PONumber,
                        VendorName = vendor.VendorName,
                        Phone = vendor.Phone,
                        ReceivingOrderDetails = orderDetails
                    };
                }

                return results;
            }
        } //eom

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<UnorderedPurchaseItemCart> Get_UnorderedParts()
        {
            using (var context = new eBikesContext())
            {
                var results = context.UnorderedPurchaseItemCarts;

                return results.ToList();
            }
        } //eom

        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public void Add_UnorderedPart(UnorderedPurchaseItemCart item)
        {
            using (var context = new eBikesContext())
            {
                context.UnorderedPurchaseItemCarts.Add(item);
                context.SaveChanges();
            }
        } //eom     

        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public void Delete_UnOrderedParts(UnorderedPurchaseItemCart item)
        {
            Delete_UnorderedPart(item.CartID);
        }

        private void Delete_UnorderedPart(int cartId)
        {
            using (var context = new eBikesContext())
            {
                var existing = context.UnorderedPurchaseItemCarts.Find(cartId);

                if (existing == null)
                {
                    throw new Exception("Item does not exist in database");
                }

                context.UnorderedPurchaseItemCarts.Remove(existing);
                context.SaveChanges();
            }
        } //eom

        public void Update_ClosePO(PurchaseOrder purchaseOrder, List<ReceivingOrderDetailsPOCO> orderDetails)
        {
            using (var context = new eBikesContext())
            {
                PurchaseOrder poExists =
                    context.PurchaseOrders.Where(po => po.PurchaseOrderNumber == purchaseOrder.PurchaseOrderNumber).FirstOrDefault();

                if (poExists == null)
                {
                    throw new Exception("Purchase Order does not exist in the database");
                }
                else
                {
                    context.PurchaseOrders.Attach(poExists);
                    poExists.Closed = purchaseOrder.Closed;
                    poExists.Notes = purchaseOrder.Notes;

                    context.Entry(poExists).State = EntityState.Modified;
                    foreach (ReceivingOrderDetailsPOCO item in orderDetails)
                    {
                        Part partExists = context.Parts.Where(p => p.PartID == item.PartId).SingleOrDefault();

                        if (partExists != null)
                        {
                            if (partExists.QuantityOnOrder >= item.QtyOutstanding)
                            {
                                context.Parts.Attach(partExists);
                                partExists.QuantityOnOrder -= item.QtyOutstanding;
                                context.Entry(partExists).State = EntityState.Modified;
                            }
                            else
                            {
                                throw new Exception("There is an issue with Part Number " + partExists.PartID +
                                                        " - " + partExists.Description +
                                                        " the quanity on order (" + partExists.QuantityOnOrder + ") is less than that outsanding.");
                            }
                        }
                        else
                        {
                            throw new Exception("Part does not exist");
                        }
                    }

                    context.SaveChanges();
                }
            }
        } //eom

        public void Add_ReceivedOrders(int purchaseOrderId, int poNumber, List<ReceiveNewOrders> newOrders)
        {
            using (var context = new eBikesContext())
            {
                int idValue = 0;
                ReceiveOrder receiveNewOrder = null;
                ReceiveOrderDetail receiveNewOrderDetail = null;
                ReturnedOrderDetail returnedOrder = null;

                //Add new received order
                receiveNewOrder = new ReceiveOrder();
                receiveNewOrder.PurchaseOrderID = purchaseOrderId;
                receiveNewOrder.ReceiveDate = DateTime.Now;
                receiveNewOrder = context.ReceiveOrders.Add(receiveNewOrder);
                idValue = receiveNewOrder.ReceiveOrderDetails.Count() + 1;

                //Loop through the details of order
                foreach (ReceiveNewOrders item in newOrders)
                {
                    if (item.QtyReceived != 0)
                    {
                        if (item.QtyReceived <= item.Outstanding)
                        {
                            receiveNewOrderDetail = new ReceiveOrderDetail();
                            receiveNewOrderDetail.PurchaseOrderDetailID = item.PurchaseOrderDetailId;
                            receiveNewOrderDetail.ReceiveOrderID = idValue;
                            receiveNewOrderDetail.QuantityReceived = item.QtyReceived;

                            receiveNewOrder.ReceiveOrderDetails.Add(receiveNewOrderDetail);

                            //Update quantities in parts table
                            var partExists = context.Parts.Where(p => p.PartID == item.PartId).SingleOrDefault();

                            if (partExists != null)
                            {
                                if (partExists.QuantityOnOrder >= item.Outstanding)
                                {
                                    context.Parts.Attach(partExists);
                                    partExists.QuantityOnHand += item.QtyReceived;
                                    partExists.QuantityOnOrder -= item.QtyReceived;
                                    context.Entry(partExists).State = EntityState.Modified;
                                }
                                else
                                {
                                    throw new Exception("There is an issue with Part Number " + partExists.PartID +
                                                        " - " + partExists.Description +
                                                        " the quanity on order  (" + partExists.QuantityOnOrder + ") is less than that outsanding.");
                                }
                            }
                            else
                            {
                                throw new Exception("Part does not exist in database or there is no quantity on order");
                            }
                        }
                        else
                        {
                            throw new Exception("Receive Quantity can not be more than Outstanding quantity");
                        }
                    }
                    //Process returned items
                    if (!string.IsNullOrEmpty(item.QtyReturned.ToString()) && !string.IsNullOrEmpty(item.Notes))
                    {
                        returnedOrder = new ReturnedOrderDetail();

                        returnedOrder.ReceiveOrderID = idValue;
                        returnedOrder.PurchaseOrderDetailID = item.PurchaseOrderDetailId;
                        returnedOrder.ItemDescription = item.PartDescription;
                        returnedOrder.Quantity = item.QtyReturned;
                        returnedOrder.Reason = item.Notes;
                        returnedOrder.VendorPartNumber = item.PartId.ToString();

                        receiveNewOrder.ReturnedOrderDetails.Add(returnedOrder);
                    } 
                }

                //Process items in unorder cart
                var unordered =
                        context.UnorderedPurchaseItemCarts.Where(up => up.PurchaseOrderNumber == 0);

                if (unordered.Count() > 0)
                {
                    foreach (var unorderedItem in unordered)
                    {
                        ReturnedOrderDetail unorderedReturn = new ReturnedOrderDetail();

                        unorderedReturn.ReceiveOrderID = idValue;
                        unorderedReturn.Quantity = unorderedItem.Quantity;
                        unorderedReturn.Reason = unorderedItem.Description;
                        unorderedReturn.VendorPartNumber = unorderedItem.VendorPartNumber;

                        receiveNewOrder.ReturnedOrderDetails.Add(unorderedReturn);
                        context.UnorderedPurchaseItemCarts.Remove(unorderedItem);
                    }
                }

                //Get count of outstanding items
                int outstandingSum = newOrders.Sum(item => item.Outstanding);
                int receivedSum = newOrders.Sum(rs => rs.QtyReceived);

                if ((outstandingSum - receivedSum) == 0)
                {
                    PurchaseOrder po = context.PurchaseOrders.Find(purchaseOrderId);

                    if (po != null)
                    {
                        context.PurchaseOrders.Attach(po);
                        po.Closed = true;
                        context.Entry(po).State = EntityState.Modified;
                    }
                }

                context.SaveChanges();
            }
        }

    }
}
