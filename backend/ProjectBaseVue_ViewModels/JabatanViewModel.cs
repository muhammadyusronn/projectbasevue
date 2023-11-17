using ProjectBaseVue_Data;
using ProjectBaseVue_Models;
using ProjectBaseVue_Models.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBaseVue_ViewModels
{
    public class JabatanViewModel:JabatanModel
    { 
        public JabatanViewModel() { }

        public JabatanViewModel(DataEntities db, string JabatanCode)
        {

            M_Jabatan model = db.M_Jabatan.Find(JabatanCode);
            if (model == null) return;
            BaseProgram.CopyProperties(typeof(M_Jabatan), model, typeof(JabatanModel), this);

        }

        public JabatanViewModel(M_Jabatan model)
        {
            BaseProgram.CopyProperties(typeof(M_Jabatan), model, typeof(JabatanModel), this);
            mode = Constants.FORM_MODE_UNCHANGED;
        }

    }

}
