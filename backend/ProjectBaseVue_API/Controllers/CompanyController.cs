using ProjectBaseVue_API.Utilities;
using ProjectBaseVue_Data;
using ProjectBaseVue_Models;
using ProjectBaseVue_Models.Resources;
using ProjectBaseVue_Models.Utilities;
using ProjectBaseVue_ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBaseVue_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiAuthorize]
    public class CompanyController : ControllerBase
    {
        DataEntities db = new DataEntities();

        [HttpPost]
        [Route("list")]
        public ResultData List(IndexParams request = null)
        {
            var response = new ResultData();
            bool success = false;
            string message = "";
            int totalRecords = 0;
            int pageSize = 10;
            int skip = request != null ? request.start : 0;
            string orderBy = "A.Id DESC";
            List<SqlParameter> parameters = new List<SqlParameter>(), parametert = new List<SqlParameter>();

            try
            {
                string query = @"SELECT * 
                                    FROM Company A 
                                    WHERE 1=1 {0}
                                    ORDER BY {3} {2} {1}";
                string whereQuery = "";
                string totalQuery = "SELECT COUNT(A.Id) From Company A WHERE 1=1  {0}";

                if (request != null)
                {
                    if (request.filters != null && request.filters.Count > 0)
                    {
                        for (int i = 0; i < request.filters.Count; i++)
                        {
                            var filter = request.filters[i];

                            if (!string.IsNullOrEmpty(filter.value))
                            {
                                string columnName = filter.field;
                                string colName = columnName;
                                string tableAlias = "A.";
                                string filterValue = filter.value;


                                if (columnName.Contains("Date") || columnName.Contains("date"))
                                {
                                    whereQuery += " AND FORMAT(" + tableAlias + columnName + ", 'yyyy-MM-dd') LIKE @" + colName;
                                    DateTime dt = Convert.ToDateTime(filter.value);
                                    parameters.Add(new SqlParameter("@" + colName, "%" + dt.ToString("yyyy-MM-dd") + "%"));

                                }
                                else
                                {
                                    whereQuery += " AND " + tableAlias + columnName + " LIKE @" + colName;
                                    parameters.Add(new SqlParameter("@" + colName, "%" + filter.value + "%"));
                                }
                            }
                        }
                    }

                    if (request.sorts != null && request.sorts.Count > 0)
                    {
                        List<string> sortList = new List<string>();
                        for (int i = 0; i < request.sorts.Count; i++)
                        {
                            var sort = request.sorts[i];
                            string columnName = sort.field;
                            string tableAlias = "A.";
                            string sortBy = sort.order;

                            sortList.Add(tableAlias + columnName + " " + sortBy);
                        }
                        orderBy = String.Join(", ", sortList);
                    }
                }

                string fQuery = string.Format(query,
                                        whereQuery,
                                        (pageSize > 0 ? "FETCH NEXT " + pageSize.ToString() + " ROWS ONLY" : ""),
                                        (skip > -1 ? "OFFSET " + skip.ToString() + " ROWS" : ""),
                                        (string.IsNullOrEmpty(orderBy) ? "CreatedDate DESC" : orderBy)
                                    );

                var data = db.Database.SqlQuery<CompanyModel>(fQuery, parameters.ToArray()).ToList();

                foreach (SqlParameter prm in parameters)
                    parametert.Add(new SqlParameter(prm.ParameterName, prm.Value));

                var qt = db.Database.SqlQuery<Int32>(string.Format(totalQuery, whereQuery), parametert.ToArray()).ToArray();
                totalRecords = qt.Length > 0 ? qt[0] : 0;

                success = true;
                response.data = data;
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            response.success = success;
            response.message = message;
            response.totalRecords = totalRecords;

            return response;

        }

        [HttpGet("{id}")]
        public ResultData GetData(long id)
        {
            var result = new ResultData();

            try
            {
                var header = new CompanyModel();
                var location = new List<CompanyDetailModel>();

                if (id != 0)
                {
                    header = new CompanyViewModel(db, id);
                    var details = db.CompanyDetail.Where(r => r.Company_Id == id).ToArray();
                    foreach (var detail in details)
                        location.Add(new CompanyDetailViewModel(detail));
                }

                result.data = new EditorHelper()
                {
                    header = header,
                    location = location
                };
            }
            catch(Exception ex)
            {
                result.success = false;
                result.message = ex.Message;
            }

            return result;
        }

        [HttpPost]
        public ResultData Save(CompanyModel modelData)
        {
            var result = new ResultData();
            bool success = false;
            string message = "";

            try
            {
                var user = HttpContext.GetUserData().UserData();

                Company model;
                if (modelData.mode == Constants.FORM_MODE_CREATE)
                {
                    var existedData = db.Company.Where(r =>
                                            r.Code == modelData.Code
                                        ).ToArray();

                    if (existedData != null && existedData.Length > 0)
                    {
                        throw new Exception("Company code already exists!");
                    }

                    model = new Company();
                    model.CreatedDate = DateTime.Now;
                    model.CreatedBy = user.GetDisplayName();
                    db.Entry(model).State = System.Data.Entity.EntityState.Added;
                }
                else
                {
                    model = db.Company.Find(modelData.Id);

                    if (modelData.mode == Constants.FORM_MODE_DELETE)
                    {
                        model.DeletedDate = DateTime.Now;
                        model.DeletedBy = user.GetDisplayName();
                        model.IsDeleted = "Y";
                    }
                    else if (modelData.mode == Constants.FORM_MODE_EDIT)
                    {
                        model.EditedDate = DateTime.Now;
                        model.EditedBy = user.GetDisplayName();
                    }
                    db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                }

                if (modelData.mode != Constants.FORM_MODE_DELETE)
                {
                    model.Code = modelData.Code;
                    model.Description = modelData.Description;
                    model.IsDeleted = "N";
                }

                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        //if (modelData.Locations != null)
                        //{
                        //    foreach (var Detail in modelData.Locations)
                        //    {
                        //        if (modelData.mode == Constants.FORM_MODE_DELETE)
                        //        {
                        //            Detail.mode = modelData.mode;
                        //        }

                        //        if (Detail.mode != Constants.FORM_MODE_UNCHANGED)
                        //        {
                        //            bool saved = SaveLocation(db, Detail, modelData.Locations, model, user, out message);
                        //            if (!saved)
                        //                throw new Exception(message);
                        //        }

                        //    }
                        //}


                        db.SaveChanges();
                        transaction.Commit();

                        message = "Company has been saved";
                        success = true;

                    }
                    catch (Exception exc)
                    {
                        message = exc.Message;
                        //result = "Failed to save data.<br/>Error Message : " + exc_.Message.Replace(Environment.NewLine, " ").Replace("'", "");
                        transaction.Rollback();
                    }
                }
            }
            catch (Exception ex)
            {
                success = false;
                message = ex.Message;
            }

            result.success = success;
            result.message = message;

            return result;
        }

        //[NonAction]
        //public bool SaveLocation(DataEntities db, CompanyLocationModel currentDetail, List<CompanyLocationModel> allDetails, Company group, UserData currentUser, out string message)
        //{
        //    bool result = false;
        //    message = "";
        //    Company_Location model;

        //    try
        //    {
        //        if (currentDetail.mode == Constants.FORM_MODE_CREATE)
        //        {
        //            model = new Company_Location();
        //            model.Created_By = currentUser.GetDisplayName();
        //            model.Created_Date = DateTime.Now;
        //            //model.Is_Deleted = false;
        //            db.Entry(model).State = System.Data.Entity.EntityState.Added;
        //        }
        //        else
        //        {
        //            model = db.Company_Location.Find(currentDetail.Id);

        //            if (currentDetail.mode == Constants.FORM_MODE_DELETE)
        //            {
        //                model.Is_Deleted = "Y";
        //                model.Deleted_Date = DateTime.Now;
        //                model.Deleted_By = currentUser.GetDisplayName();
        //                //db.Entry(model).State = System.Data.Entity.EntityState.Deleted;
        //            }
        //            else if (currentDetail.mode == Constants.FORM_MODE_EDIT)
        //            {
        //                //model.Is_Deleted = false;
        //                model.Edited_Date = DateTime.Now;
        //                model.Edited_By = currentUser.GetDisplayName();
        //            }

        //            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
        //        }

        //        if (currentDetail.mode != Constants.FORM_MODE_DELETE)
        //        {
        //            model.Company = group;

        //            var location = db.Locations.Where(r => r.Id == currentDetail.Location_Id && r.Is_Deleted != "Y").FirstOrDefault();
        //            if (location == null)
        //                throw new Exception(Resources.LOCATION_INVALID);

        //            var conflictingTime = allDetails.Where(r =>
        //                                                    r.Id != currentDetail.Id &&
        //                                                    r.Location_Id == currentDetail.Location_Id
        //                                                    && r.mode != Constants.FORM_MODE_DELETE
        //                                                ).FirstOrDefault();

        //            if (conflictingTime != null)
        //                throw new Exception("Duplicate Location " + location.Code + " found");

        //            //model.Time_From = this.Time_From;
        //            //model.Time_To = this.Time_To;
        //            //model.Slot_Name = this.Slot_Name;
        //            //model.Max_Slot = this.Max_Slot;

        //            if (currentDetail.mode == Constants.FORM_MODE_CREATE)
        //            {
        //                var existing = db.Company_Location.Where(r =>
        //                                    r.Company_Id == model.Company_Id &&
        //                                    r.Location_Id == currentDetail.Location_Id
        //                                //&& !r.Is_Deleted
        //                                ).FirstOrDefault();
        //                if (existing != null)
        //                    throw new Exception("Location " + location.Code + " has been added previously for this group");
        //            }

        //            model.Location = location;
        //            model.Address = currentDetail.Address;
        //            model.Is_Deleted = "N";

        //            //businessSegmentName = businessSegment.Description;
        //        }

        //        result = true;
        //        message = "OK";
        //    }
        //    catch (Exception ex)
        //    {
        //        message = ex.Message;
        //    }

        //    return result;
        //}


        public class EditorHelper
        {
            public CompanyModel header { get; set; }
            public List<CompanyDetailModel> location { get; set; }
        }


    }
}
