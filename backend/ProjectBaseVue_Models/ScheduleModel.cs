using ProjectBaseVue_Models.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBaseVue_Models
{
    public class ScheduleModel
    {
        public ScheduleModel() 
        {
           
        }
        public Nullable<System.DateTime> ScheduleDate { get; set; }
        public Nullable<System.TimeSpan> TimeStart { get; set; }
        public Nullable<System.TimeSpan> TimeEnd { get; set; }
        public string CreatedBy { get; set; }
        public string Name { get; set; }
        public string Full_Name { get; set; }
        public string mode { get; set; }

    }



}
