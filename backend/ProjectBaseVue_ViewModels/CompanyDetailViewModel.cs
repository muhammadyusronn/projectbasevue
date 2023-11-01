using ProjectBaseVue_Data;
using ProjectBaseVue_Models;
using ProjectBaseVue_Models.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBaseVue_ViewModels
{
    public class CompanyDetailViewModel:CompanyDetailModel
    {
        public CompanyDetailViewModel() { }

        public CompanyDetailViewModel(DataEntities db, long id)
        {
            var model = new CompanyDetail();

            var details = db.CompanyDetail.Where(r => r.Company_Id == id).ToArray();
            if (details == null) return;
            var det = new List<CompanyDetailModel>();
            foreach (var detail in details)
                det.Add(new CompanyDetailViewModel(detail));

        }

        public CompanyDetailViewModel(CompanyDetail model)
        {
            BaseProgram.CopyProperties(typeof(CompanyDetail), model, typeof(CompanyDetailModel), this);
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
