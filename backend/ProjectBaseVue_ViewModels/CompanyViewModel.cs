using ProjectBaseVue_Data;
using ProjectBaseVue_Models;
using ProjectBaseVue_Models.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBaseVue_ViewModels
{
    public class CompanyViewModel:CompanyModel
    {
        public CompanyViewModel() { }

        public CompanyViewModel(DataEntities db, long id)
        {

            Company model = db.Company.Find(id);
            if (model == null) return;
            BaseProgram.CopyProperties(typeof(Company), model, typeof(CompanyModel), this);

            //var locations = db.Company_Location.Where(r => r.Company_Id == id && r.Is_Deleted != "Y").ToArray();
            //Locations = new List<CompanyLocationModel>();
            //foreach (var location in locations)
            //    Locations.Add(new CompanyLocationViewModel(location));

        }

        public CompanyViewModel(Company model)
        {
            BaseProgram.CopyProperties(typeof(Company), model, typeof(CompanyModel), this);
            mode = Constants.FORM_MODE_UNCHANGED;
        }

    }

    //public class CompanyLocationViewModel: CompanyLocationModel
    //{
    //    public CompanyLocationViewModel() { }

    //    public CompanyLocationViewModel(DataEntities db, long id)
    //    {
    //        CompanyLocation model = db.CompanyLocation.Find(id);
    //        if (model == null) return;
    //        BaseProgram.CopyProperties(typeof(CompanyLocation), model, typeof(CompanyLocationModel), this);

    //    }
        
    //    public CompanyLocationViewModel(CompanyLocation model)
    //    {
    //        BaseProgram.CopyProperties(typeof(CompanyLocation), model, typeof(CompanyLocationModel), this);
    //        mode = Constants.FORM_MODE_UNCHANGED;
    //    }
    //}
}
