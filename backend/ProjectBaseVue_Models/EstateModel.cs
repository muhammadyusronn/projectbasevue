using ProjectBaseVue_Models.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBaseVue_Models
{
    public class EstateModel
    {
        public EstateModel() 
        {
           
        }

        public int Id { get; set; }
        public string RegionCode { get; set; }
        public string CompanyCode { get; set; }
        public string EstateCode { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string ProfileName { get; set; }
        public string PlantCode { get; set; }
        public string Storage { get; set; }
        public string NoophCode { get; set; }
        public Nullable<int> estateFingerCode { get; set; }
        public string templateCode { get; set; }
        public Nullable<byte> isActive { get; set; }
        public string estateAlias { get; set; }
        public string mode { get; set; }

    }



}
