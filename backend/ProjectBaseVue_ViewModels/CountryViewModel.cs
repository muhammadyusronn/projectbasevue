using ProjectBaseVue_Data;
using ProjectBaseVue_Models;
using ProjectBaseVue_Models.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBaseVue_ViewModels
{
    public class CountryViewModel:CountryModel
    { 
        public CountryViewModel() { }

        public CountryViewModel(DataEntities db, Guid Id)
        {

            M_Country model = db.M_Country.Find(Id);
            if (model == null) return;
            BaseProgram.CopyProperties(typeof(M_Country), model, typeof(CountryModel), this);

        }

        public CountryViewModel(M_Country model)
        {
            BaseProgram.CopyProperties(typeof(M_Country), model, typeof(CountryModel), this);
            mode = Constants.FORM_MODE_UNCHANGED;
        }

    }

}
