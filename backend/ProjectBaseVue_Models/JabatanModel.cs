using ProjectBaseVue_Models.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBaseVue_Models
{
    public class JabatanModel
    {
        public JabatanModel() 
        {
           
        }

        public string JabatanCode { get; set; }
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string Seq { get; set; }
        public string Area { get; set; }
        public Nullable<int> Priority { get; set; }
        public string Granted { get; set; }
        public string mode { get; set; }

    }



}
