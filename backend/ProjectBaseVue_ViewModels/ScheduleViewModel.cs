using ProjectBaseVue_Data;
using ProjectBaseVue_Models;
using ProjectBaseVue_Models.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBaseVue_ViewModels
{
    public class ScheduleViewModel:ScheduleModel
    { 
        public ScheduleViewModel() { }

        public ScheduleViewModel(DataEntities db, int Id)
        {

            T_ScheduleData model = db.T_ScheduleData.Find(Id);
            if (model == null) return;
            BaseProgram.CopyProperties(typeof(T_ScheduleData), model, typeof(ScheduleModel), this);

        }

        public ScheduleViewModel(T_ScheduleData model)
        {
            BaseProgram.CopyProperties(typeof(T_ScheduleData), model, typeof(ScheduleModel), this);
            mode = Constants.FORM_MODE_UNCHANGED;
        }

    }

}
