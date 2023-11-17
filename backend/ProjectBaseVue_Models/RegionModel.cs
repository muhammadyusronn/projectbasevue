using ProjectBaseVue_Models.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBaseVue_Models
{
    public class RegionModel
    {
        public RegionModel() 
        {
           
        }

        public string RegionCode { get; set; }
        public string Name { get; set; }
        public Nullable<byte> isActive { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string mode { get; set; }

    }



}
