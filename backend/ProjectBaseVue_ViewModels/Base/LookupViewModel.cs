﻿using ProjectBaseVue_Data;
using ProjectBaseVue_Models;
using ProjectBaseVue_Models.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBaseVue_ViewModels
{
    public class LookupViewModel: LookupModel
    {
        public LookupViewModel() { }

        public LookupViewModel(DataEntities db, long id)
        {
            Lookup model = db.Lookup.Find(id);
            if (model == null) return;
            BaseProgram.CopyProperties(typeof(Lookup), model, typeof(LookupModel), this);
        }

        public LookupViewModel(Lookup model)
        {
            BaseProgram.CopyProperties(typeof(Lookup), model, typeof(LookupModel), this);
            mode = Constants.FORM_MODE_UNCHANGED;
        }
    }
}
