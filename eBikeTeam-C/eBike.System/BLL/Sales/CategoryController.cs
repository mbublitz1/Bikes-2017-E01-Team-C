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
    [DataObject]
    public class CategoryController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<CategoryFilterPOCO> List_Categories_For_Filter()
        {
            using (var context = new eBikesContext())
            {
                var results = (from x in context.Categories
                               orderby x.Description
                              select new CategoryFilterPOCO
                              {
                                  CategoryID = x.CategoryID,
                                  Description = x.Description,
                                  PartCount = x.Parts.Count(p => p.Discontinued == false)
                              }).ToList();

                return results;
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public CategoryFilterPOCO List_All_Count()
        {
            using (var context = new eBikesContext())
            {
                var result = (from x in context.Categories
                              select new CategoryFilterPOCO
                              {
                                  CategoryID = 0,
                                  Description = "All Categories",
                                  PartCount = context.Parts.Count(p => p.Discontinued == false)
                              }).FirstOrDefault();
                return result;
            }
           
        }
    }
}
