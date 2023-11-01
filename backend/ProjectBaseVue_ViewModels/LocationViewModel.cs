using ProjectBaseVue_Data;
using ProjectBaseVue_Models;
using ProjectBaseVue_Models.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBaseVue_ViewModels
{
    public class LocationViewModel: LocationModel
    {

        public LocationViewModel() { }

        public LocationViewModel(DataEntities db, string id)
        {
            Location model = db.Location.Find(id);
       
            if (model == null) return;
            BaseProgram.CopyProperties(typeof(Location), model, typeof(LocationViewModel), this);

            //Details = new List<CompanyModel>();
            //var details = db.Companies.Where(r => r.Company_Location.Where(a => a.Location_Id == model.Id && a.Is_Deleted == "N").ToList().Count > 0 && r.Is_Deleted == "N").ToArray();

        }

        public LocationViewModel(Location model)
        {
            BaseProgram.CopyProperties(typeof(Location), model, typeof(LocationModel), this);
            mode = Constants.FORM_MODE_UNCHANGED;
        }
    }
}
