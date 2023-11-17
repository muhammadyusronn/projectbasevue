using ProjectBaseVue_Data;
using ProjectBaseVue_Models;
using ProjectBaseVue_Models.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBaseVue_ViewModels
{
    public class RegionViewModel:RegionModel
    { 
        public RegionViewModel() { }

        public RegionViewModel(DataEntities db, string RegionCode)
        {

            M_Region model = db.M_Region.Find(RegionCode);
            if (model == null) return;
            BaseProgram.CopyProperties(typeof(M_Region), model, typeof(RegionModel), this);
        }

        public RegionViewModel(M_Region model)
        {
            BaseProgram.CopyProperties(typeof(M_Region), model, typeof(RegionModel), this);
            mode = Constants.FORM_MODE_UNCHANGED;
        }

    }

}
