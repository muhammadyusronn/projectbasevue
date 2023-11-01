using ProjectBaseVue_Data;
using ProjectBaseVue_Models.Resources;
using ProjectBaseVue_Models.Utilities;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBaseVue_ViewModels.Utilities
{
    public class UAuth
    {


        public static string Login(DataEntities db, string username, string password, string platform, string fcm_token, string token, out UserPrincipal userData, out User user, out bool correctPass)
        {
            string result = "";
            userData = null;
            user = null;
            correctPass = false;

            try
            {
                user = db.User.Where(r => r.Username == username).FirstOrDefault();
                if (user == null)
                    throw new Exception(Resources.USER_PASS_INVALID);

                if (user.IsDeleted == "Y")
                    throw new Exception(Resources.USER_PASS_INVALID);

                if (user.Use_AD == "Y")
                {
                    using (PrincipalContext ctx = new PrincipalContext(ContextType.Domain, Constants.AD_DOMAIN, Constants.AD_SA_USER, Constants.AD_SA_PASSWORD))
                    {
                        userData = UserPrincipal.FindByIdentity(ctx, username);
                        if (userData == null)
                            return Resources.USER_INVALID;
                        if (userData.IsAccountLockedOut())
                            return "Account is locked. Please wait for 15 minutes or contact local IT";
                        if (userData.AccountExpirationDate.HasValue)
                        {
                            if (userData.AccountExpirationDate.Value >= DateTime.Now)
                                return "Password expired. <a href='https://adpass.wilmar.co.id/RDWeb/Pages/en-US/password.aspx' target='_blank' style='text-decoration:underline'>Click here to change password</a>";
                        }
                        if (userData.LastPasswordSet == null)
                        {
                            return "Password must be change at first login. <a href='https://adpass.wilmar.co.id/RDWeb/Pages/en-US/password.aspx' target='_blank' style='text-decoration:underline'>Click here to change password</a>.";
                        }

                        //else if ((DateTime.Now - userData.LastPasswordSet.Value.AddHours(7)).TotalHours < 24)
                        //    return "Password has been changed today at " + userData.LastPasswordSet.Value.AddHours(7).ToString("HH:mm") + ", please try again tomorrow.";
                        if (userData.Enabled.HasValue)
                            if (userData.Enabled.Value == false) return "Account has been terminated. Please contact local IT";

                        if (!ctx.ValidateCredentials(username, password))
                            return Resources.USER_PASS_INVALID;
                        else
                        {
                            user.LastAccessDate = DateTime.Now;
                            db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                            //db.SaveChanges();

                            //UUtils.CreateUserLog(db, username, platform, "LOGIN");
                            correctPass = true;
                            return "OK";
                        }
                    }
                }
                else
                {
                    var a = UEncryption.ComputeSha256Hash(password);
                    if (user.Password == UEncryption.ComputeSha256Hash(password))
                    {
                        correctPass = true;
                        if (user.FirstLogin == "Y")
                        {
                            return "Please change your password on first login";
                        }

                        user.LastAccessDate = DateTime.Now;
                        db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                        //db.SaveChanges();

                        //UUtils.CreateUserLog(db, username, platform, "LOGIN");

                        return "OK";
                    }
                    else
                    {
                        return Resources.USER_PASS_INVALID;
                    }
                }

            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return result;
        }


        public static bool CheckPassword(string password, out string message, out string newPassword)
        {
            var complexity = false;
            message = "";
            newPassword = "";

            try
            {
                if (string.IsNullOrEmpty(password))
                {
                    newPassword = "Wilmar@2023";
                    complexity = true;
                }
                else
                {
                    complexity = CheckPasswordComplexity(password);
                    if (!complexity)
                        throw new Exception("Password does not meet minimum security requirements. It must be at least 8(eight) characters long, minimum of 1 capital letter and number.");

                    newPassword = password;
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            return complexity;
        }

        public static bool CheckPasswordComplexity(string passWord)
        {
            int validConditions = 0;
            //foreach (char c in passWord)
            //{
            //    if (c >= 'a' && c <= 'z')
            //    {
            //        validConditions++;
            //        break;
            //    }
            //}
            if (passWord.Length < 8) return false;

            foreach (char c in passWord)
            {
                if (c >= 'A' && c <= 'Z')
                {
                    validConditions++;
                    break;
                }
            }
            if (validConditions == 0) return false;
            foreach (char c in passWord)
            {
                if (c >= '0' && c <= '9')
                {
                    validConditions++;
                    break;
                }
            }
            if (validConditions == 1) return false;
            //if (validConditions == 2)
            //{
            //    char[] special = { '@', '#', '$', '%', '^', '&', '+', '=' }; // or whatever    
            //    if (passWord.IndexOfAny(special) == -1) return false;
            //}
            return true;
        }

        //public static List<string> GetUserLocation(string username,out bool allLocation)
        //{
        //    DMSEntities db = new DMSEntities();
        //    allLocation = false;

        //    var user = db.Users.Where(r => r.Employee_Number == username && r.Is_Deleted != "Y").FirstOrDefault();
        //    if (user == null)
        //        return new List<string>();

        //    if (user.Is_Admin)
        //    {
        //        allLocation = true;
        //        return new List<string>();
        //    }

        //    if (user.Location_All)
        //    {
        //        allLocation = true;
        //        return new List<string>();
        //    }

        //    var userPOrg = db.User_Location.Where(r => r.User_Id == username && !r.Is_Deleted).ToArray();
        //    if (userPOrg.Length > 0)
        //    {
        //        return userPOrg.Select(r => r.Location_Code).ToList();
        //    }
        //    else return new List<string>();
        //}

        //public static List<String> GetTransporterLocation(string username)
        //{
        //    DMSEntities db = new DMSEntities();
        //    var userPOrg = db.User_Location.Where(r => r.User_Id == username && !r.Is_Deleted).ToArray();
        //    if (userPOrg.Length > 0)
        //    {
        //        return userPOrg.Select(r => r.Location_Code).ToList();
        //    }
        //    else return new List<string>();
        //}
    }
}
