using ProjectBaseVue_Models.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBaseVue_Models
{
    public class UserModel
    {
        public UserModel() {
            Groups = new List<UserGroupModel>();
        }
        [DefaultValue(Constants.FORM_MODE_UNCHANGED)]
        public string mode { get; set; }

        public long Id { get; set; }
        public string Username { get; set; }
        public string Full_Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Location_Code { get; set; }
        public string IsAdmin { get; set; }
        public string IsActive { get; set; }
        public string Use_AD { get; set; }
   
        public Nullable<System.DateTime> LastAccessDate { get; set; }

        public string EstateCode { get; set; }
        public string Department { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> EditedDate { get; set; }
        public string EditedBy { get; set; }
        public string IsDeleted { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public string DeletedBy { get; set; }
        public List<UserGroupModel> Groups { get; set; }
    }

    public class UserGroupModel
    {
        public string mode { get; set; }
        public long Id { get; set; }
        public long UserId { get; set; }
        public long Group_Id { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> EditedDate { get; set; }
        public string EditedBy { get; set; }
        public string IsDeleted { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public string DeletedBy { get; set; }
    }


    //USER LOGIN LOG TOKEN

}
