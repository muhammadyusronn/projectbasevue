using ProjectBaseVue_Models.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBaseVue_Models
{
    public class DepartmentModel
    {
        public DepartmentModel() 
        {
           
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string EditedBy { get; set; }
        public Nullable<System.DateTime> EditedDate { get; set; }
        public string IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public string mode { get; set; }

    }



}
