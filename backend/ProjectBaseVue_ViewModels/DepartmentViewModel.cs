using ProjectBaseVue_Data;
using ProjectBaseVue_Models;
using ProjectBaseVue_Models.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBaseVue_ViewModels
{
    public class DepartmentViewModel:DepartmentModel
    { 
        public DepartmentViewModel() { }

        public DepartmentViewModel(DataEntities db, long Id)
        {

            M_Department model = db.M_Department.Find(Id);
            if (model == null) return;
            BaseProgram.CopyProperties(typeof(M_Department), model, typeof(DepartmentModel), this);

        }

        public DepartmentViewModel(M_Department model)
        {
            BaseProgram.CopyProperties(typeof(M_Department), model, typeof(DepartmentModel), this);
            mode = Constants.FORM_MODE_UNCHANGED;
        }

    }

}
