using ProjectBaseVue_Data;
using ProjectBaseVue_Models;
using ProjectBaseVue_Models.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBaseVue_ViewModels
{
    public class RunningNumberViewModel : RunningNumberModel
    {
        public RunningNumberViewModel()
        {
            Detail = new List<RunningNumberDetailModel>();
        }

        public RunningNumberViewModel(DataEntities db, long id)
        {
            RunningNumber model = db.RunningNumber.Where(r => r.Id == id).FirstOrDefault();
            if (model == null) return;
            BaseProgram.CopyProperties(typeof(RunningNumber), model, typeof(RunningNumberModel), this);    
        }

        public RunningNumberViewModel(RunningNumber model)
        {
            BaseProgram.CopyProperties(typeof(RunningNumber), model, typeof(RunningNumberModel), this);
            mode = Constants.FORM_MODE_UNCHANGED;
        }
    }

    public class RunningNumberDetailViewModel : RunningNumberDetailModel
    {
        public RunningNumberDetailViewModel() { }

        public RunningNumberDetailViewModel(DataEntities db, long id)
        {
            RunningNumber model = db.RunningNumber.Find(id);
            if (model == null) return;
            BaseProgram.CopyProperties(typeof(RunningNumber), model, typeof(RunningNumberDetailModel), this);
        }

        public RunningNumberDetailViewModel(RunningNumber model)
        {
            BaseProgram.CopyProperties(typeof(RunningNumber), model, typeof(RunningNumberDetailModel), this);
            mode = Constants.FORM_MODE_UNCHANGED;
        }
    }
}