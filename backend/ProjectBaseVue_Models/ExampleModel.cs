using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ProjectBaseVue_Models
{
    public class ExampleModel
    {
        public ExampleModel() 
        {
        
        }

        public string DocumentNo { get; set; }
        public string CompanyCode { get; set; }
        public string LocationCode { get; set; }
        public string DocumentType { get; set; }

        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> EditedDate { get; set; }
        public string EditedBy { get; set; }
        public string IsDeleted { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public string DeletedBy { get; set; }

        public List<ExampleDetailModel> Details { get; set; }

        #region CustomAttributes
        public string mode { get; set; }
        #endregion



    }

    public class ExampleDetailModel
    {
        public ExampleDetailModel() { }

        public long Id { get; set; }
        public long ExampleId { get; set; }
        public string MaterialCode { get; set; }
        public decimal Qty { get; set; }
        public string UOM { get; set; }


        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> EditedDate { get; set; }
        public string EditedBy { get; set; }
        public string IsDeleted { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public string DeletedBy { get; set; }

        #region CustomAttributes
        public string mode { get; set; }
        #endregion
    }
}
