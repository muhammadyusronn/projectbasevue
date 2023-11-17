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
    public class EstateController : ControllerBase
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
            int pageSize = 20;
            int skip = request != null ? request.start : 0;
            string orderBy = "a.id DESC";
            List<SqlParameter> parameters = new List<SqlParameter>(), parametert = new List<SqlParameter>();

            try
            {
                string baseQuery = @"
                SELECT * FROM
                ( 
	               {0}
                 ) as x 
                WHERE x.row_number BETWEEN {1} and {2}
                ";
                string query = @"SELECT 
                                    ROW_NUMBER() OVER(ORDER BY {1}) AS row_number,
                                    a.* 
                                    FROM M_Estate a 
                                    WHERE 1=1 {0} ";
                string whereQuery = "";
                string totalQuery = "SELECT COUNT(a.Id) From M_Estate a WHERE 1=1  {0}";
                if (request != null)
                {
                    if (request.filters != null && request.filters.Count > 0)
                    {
                        string[] fieldSpecial = { "IsDeleted" };
                        for (int i = 0; i < request.filters.Count; i++)
                        {
                            var filter = request.filters[i];

                            if (!string.IsNullOrEmpty(filter.value))
                            {
                                string columnName = filter.field;
                                string colName = columnName;
                                string tableAlias = "A.";
                                string filterValue = (fieldSpecial.Contains(colName)) ? (filter.value == "1") ? "Y" : (filter.value == "0") ? "N" : filter.value : filter.value;


                                if (columnName.Contains("Date") || columnName.Contains("date"))
                                {
                                    whereQuery += " AND FORMAT(" + tableAlias + columnName + ", 'yyyy-MM-dd') LIKE @" + colName;
                                    DateTime dt = Convert.ToDateTime(filter.value);
                                    parameters.Add(new SqlParameter("@" + colName, "%" + dt.ToString("yyyy-MM-dd") + "%"));

                                }
                                else
                                {
                                    whereQuery += " AND " + tableAlias + columnName + " LIKE @" + colName;
                                    parameters.Add(new SqlParameter("@" + colName, "%" + filterValue + "%"));
                                }
                            }
                        }
                    }

                    if (request.sorts != null && request.sorts.Count > 0)
                    {
                        List<string> sortList = new List<string>();
                        string tableAlias = "a.";
                        string sortBy = "DESC";
                        for (int i = 0; i < request.sorts.Count; i++)
                        {
                            var sort = request.sorts[i];
                            string columnName = sort.field;
                            sortBy = sort.order;

                            sortList.Add(tableAlias + columnName + " " + sortBy);
                        }
                        orderBy = String.Join(", ", sortList) + ", " + tableAlias + "Id " + sortBy;
                    }
                }
                string fQuery = string.Format(query, whereQuery, (string.IsNullOrEmpty(orderBy) ? "Id DESC" : orderBy));
                string qwery = string.Format(baseQuery, fQuery, skip + 1, skip + pageSize);
                var data = db.Database.SqlQuery<EstateModel>(qwery, parameters.ToArray()).ToList();

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
        public ResultData GetData(int id)
        {
            var result = new ResultData();

            try
            {
                var header = new EstateModel();

                if (id.ToString() != "")
                {
                    header = new EstateViewModel(db, id);
                }

                result.data = new EditorHelper()
                {
                    header = header
                };
            }
            catch (Exception ex)
            {
                result.success = false;
                result.message = ex.Message;
            }

            return result;
        }

        [HttpPost]
        public ResultData Save(EstateModel modelData)
        {
            var result = new ResultData();
            bool success = false;
            string message = "";

            try
            {
                var user = HttpContext.GetUserData().UserData();

                M_Estate model;
                if (modelData.mode == Constants.FORM_MODE_CREATE)
                {
                    var existedData = db.M_Estate.Where(r =>
                                            r.EstateCode == modelData.EstateCode
                                        ).ToArray();

                    if (existedData != null && existedData.Length > 0)
                    {
                        throw new Exception("Estate already exists!");
                    }

                    model = new M_Estate();

                    db.Entry(model).State = System.Data.Entity.EntityState.Added;
                }
                else
                {
                    model = db.M_Estate.Find(modelData.Id);

                    if (modelData.mode == Constants.FORM_MODE_DELETE)
                    {

                        if (model != null)
                        {
                            db.Entry(model).State = System.Data.Entity.EntityState.Deleted;
                        }
                    }
                    else if (modelData.mode == Constants.FORM_MODE_EDIT)
                    {
                        // EDIT PROSES
                        db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                    }

                }

                if (modelData.mode != Constants.FORM_MODE_DELETE)
                {
                    //modelData.Id = (modelData.mode == Constants.FORM_MODE_CREATE) ? new Guid() : modelData.Id;

                    model.RegionCode = modelData.RegionCode;
                    model.CompanyCode = modelData.CompanyCode;
                    model.EstateCode = modelData.EstateCode;
                    model.Name = modelData.Name;
                    model.Type = modelData.Type;
                    model.ProfileName = modelData.ProfileName;
                    model.PlantCode = modelData.PlantCode;
                    model.Storage = modelData.Storage;
                    model.NoophCode = modelData.NoophCode;
                    model.estateFingerCode = modelData.estateFingerCode;
                    model.templateCode = modelData.templateCode;
                    model.isActive = modelData.isActive;
                    model.estateAlias = modelData.estateAlias;
                }

                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {

                        db.SaveChanges();
                        transaction.Commit();
                        message = "Estate has been saved";
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

        public class EditorHelper
        {
            public EstateModel header { get; set; }
        }


    }
}
