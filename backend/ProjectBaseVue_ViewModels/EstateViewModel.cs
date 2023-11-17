using ProjectBaseVue_Data;
using ProjectBaseVue_Models;
using ProjectBaseVue_Models.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBaseVue_ViewModels
{
    public class EstateViewModel:EstateModel
    { 
        public EstateViewModel() { }

        public EstateViewModel(DataEntities db, int Id)
        {

            M_Estate model = db.M_Estate.Find(Id);
            if (model == null) return;
            BaseProgram.CopyProperties(typeof(M_Estate), model, typeof(EstateModel), this);

        }

        public EstateViewModel(M_Estate model)
        {
            BaseProgram.CopyProperties(typeof(M_Estate), model, typeof(EstateModel), this);
            mode = Constants.FORM_MODE_UNCHANGED;
        }

    }

}
