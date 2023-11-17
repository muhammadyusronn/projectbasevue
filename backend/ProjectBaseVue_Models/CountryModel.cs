using ProjectBaseVue_Models.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBaseVue_Models
{
    public class CountryModel
    {
        public CountryModel() 
        {
           
        }

        public Guid Id { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string EditedBy { get; set; }
        public Nullable<System.DateTime> EditedDate { get; set; }
        public Nullable<byte> isActive { get; set; }
        public string mode { get; set; }

    }



}
