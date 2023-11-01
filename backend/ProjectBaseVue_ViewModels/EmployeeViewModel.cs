using ProjectBaseVue_Data;
using ProjectBaseVue_Models;
using ProjectBaseVue_Models.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBaseVue_ViewModels
{
    public class EmployeeViewModel:EmployeeModel
    { 
        public EmployeeViewModel() { }

        public EmployeeViewModel(DataEntities db, Guid id)
        {

            Employees model = db.Employees.Find(id);
            if (model == null) return;
            BaseProgram.CopyProperties(typeof(Employees), model, typeof(EmployeeModel), this);

        }

        public EmployeeViewModel(Company model)
        {
            BaseProgram.CopyProperties(typeof(Company), model, typeof(CompanyModel), this);
            mode = Constants.FORM_MODE_UNCHANGED;
        }

    }

}
