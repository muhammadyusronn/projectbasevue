using ProjectBaseVue_Models.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBaseVue_Models
{
    public class EmployeeModel
    {
        public EmployeeModel() 
        {
           
        }

        public Guid Id { get; set; }
        public string mode { get; set; }
        public string name { get; set; }

        public string email { get; set; }
        public int phone { get; set; }
        public int salary { get; set; }
        public string departement { get; set; }

    }



}
