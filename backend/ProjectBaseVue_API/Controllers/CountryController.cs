﻿using ProjectBaseVue_API.Utilities;
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
    public class CountryController : ControllerBase
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
            int pageSize =20;
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
                                    FROM M_Country a 
                                    WHERE 1=1 {0} ";
                string whereQuery = "";
                string totalQuery = "SELECT COUNT(a.Id) From M_Country a WHERE 1=1  {0}";
                if (request != null)
                {
                    if (request.filters != null && request.filters.Count > 0)
                    {
                        string[] fieldSpecial = {"IsDeleted" };
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
                        orderBy = String.Join(", ", sortList)+", "+tableAlias+ "Id " + sortBy;
                    }
                }
                string fQuery = string.Format(query, whereQuery, (string.IsNullOrEmpty(orderBy) ? "Id DESC" : orderBy));
                string qwery = string.Format(baseQuery, fQuery, skip+1, skip+pageSize);
                var data = db.Database.SqlQuery<CountryModel>(qwery, parameters.ToArray()).ToList();

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
        public class EditorHelper
        {
            public CountryModel header { get; set; }
        }


    }
}
