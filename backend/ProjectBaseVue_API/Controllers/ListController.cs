using ProjectBaseVue_API.Utilities;
using ProjectBaseVue_Data;
using ProjectBaseVue_Models;
using ProjectBaseVue_Models.Utilities;
using ProjectBaseVue_ViewModels.Utilities;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBaseVue_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiAuthorize]
    public class ListController : ControllerBase
    {
        DataEntities db = new DataEntities();

        #region Company
        [HttpGet]
        [Route("company_id")]
        public object CompanyId(string q = "", int page = 0, bool init = false)
        {
            int fromIdx = page > 0 ? (page * 10) : 0;
            int toIdx = 10;
            var listData = db.Company.Where(r => 1 == 2);

            if (init)
            {
                listData = db.Company
                .Where
                (
                    r =>
                    (string.IsNullOrEmpty(q) || r.Id.ToString() == q)
                )
                .OrderBy(r => r.Id);
            }
            else
            {
                listData = db.Company
                .Where
                (
                    r =>
                    (string.IsNullOrEmpty(q) || ("[" + r.Code + "] " + r.Description).ToUpper().Contains(q.ToUpper()))
                    && r.IsDeleted != "Y"
                )
                .OrderBy(r => r.Id);
            }
            var data = listData.Skip(fromIdx)
               .Take(toIdx)
               .Select(r => new { id = r.Id, text = "[" + r.Code + "] " + r.Description })
               .ToList();

            return new { items = data };
        }

        [HttpGet]
        [Route("company_code")]
        public object CompanyByCode(string q = "", int page = 0, bool init = false, string exclude_ids = "")
        {
            int fromIdx = page > 0 ? (page * 10) : 0;
            int toIdx = 10;
            var listData = db.Company.Where(r => 1 == 2);

            if (init)
            {
                listData = db.Company
                .Where
                (
                    r =>
                    (string.IsNullOrEmpty(q) || (q == r.Code.ToString()))
                )
                .OrderBy(r => r.Id);
            }
            else
            {
                bool isAdmin = HttpContext.GetUserIsAdmin();
                bool allCompany = false;
                //bool allSegment = false;

                var companies = new List<string>();
                //var businessSegments = new List<long>();
                if (!isAdmin)
                {
                    //companies = HttpContext.GetUserCompanyCode(out allCompany);
                    //businessSegments = HttpContext.GetUserBusinessSegment(out allSegment);
                }

                List<String> excludeList = new List<string>();
                if (!string.IsNullOrEmpty(exclude_ids))
                {
                    excludeList = exclude_ids.Split(';').ToList();
                }
                int excludeCount = excludeList.Count;

                listData = db.Company
                .Where
                (
                    r =>
                    (string.IsNullOrEmpty(q) || ("[" + r.Code + "] " + r.Description).ToUpper().Contains(q.ToUpper()))
                    && r.IsDeleted != "Y"
                )
                .OrderBy(r => r.Id);
            }
            var data = listData.Skip(fromIdx)
                .Take(toIdx)
                .Select(r => new { id = r.Code, text = "[" + r.Code + "] " + r.Description })
                .ToList();

            return new { items = data };
        }
        #endregion

        [HttpGet]
        [Route("location_code")]
        public object LocationCode(string q = "", int page = 0, bool init = false)
        {
            int fromIdx = page > 0 ? (page * 10) : 0;
            int toIdx = 10;
            var listData = db.Location.Where(r => 1 == 2);

            if (init)
            {
                var listCode = string.IsNullOrEmpty(q) ? new string[] { q } : q.Split(';');

                listData = db.Location
                .Where
                (
                    r =>
                    (string.IsNullOrEmpty(q) || listCode.Contains(r.Code.ToString()))
                )
                .OrderBy(r => r.Id);
            }
            else
            {
                listData = db.Location
                .Where
                (
                    r =>
                    (string.IsNullOrEmpty(q) || ("[" + r.Code + "] " + r.Description).ToUpper().Contains(q.ToUpper())) &&
                    r.IsDeleted != "Y"
                )
                .OrderBy(r => r.Id);
            }
            var data = listData.Skip(fromIdx)
                .Take(toIdx)
                .Select(r => new { id = r.Code, text = "[" + r.Code + "] " + r.Description, nama = r.Description })
                .ToList();

            return new { items = data };
        }

        [HttpGet]
        [Route("location_id")]
        public object LocationId(string q = "", int page = 0, bool init = false)
        {
            int fromIdx = page > 0 ? (page * 10) : 0;
            int toIdx = 10;
            var listData = db.Location.Where(r => 1 == 2);

            if (init)
            {
                listData = db.Location
                .Where
                (
                    r =>
                    (string.IsNullOrEmpty(q) || r.Id.ToString() == q)
                )
                .OrderBy(r => r.Id);
            }
            else
            {
                listData = db.Location
                .Where
                (
                    r =>
                    (string.IsNullOrEmpty(q) || ("[" + r.Code + "] " + r.Description).ToUpper().Contains(q.ToUpper())) &&
                    r.IsDeleted != "Y"
                )
                .OrderBy(r => r.Id);
            }
            var data = listData.Skip(fromIdx)
                .Take(toIdx)
                .Select(r => new { id = r.Id, text = "[" + r.Code + "] " + r.Description, nama = r.Description })
                .ToList();

            return new { items = data };
        }

        //[HttpGet]
        //[Route("department_code")]
        //public object DepartmentCode(string q = "", int page = 0, bool init = false)
        //{
        //    int fromIdx = page > 0 ? (page * 10) : 0;
        //    int toIdx = 10;
        //    var listData = db.Department.Where(r => 1 == 2);

        //    if (init)
        //    {
        //        var listCode = string.IsNullOrEmpty(q) ? new string[] { q } : q.Split(';');

        //        listData = db.Departments
        //        .Where
        //        (
        //            r =>
        //            (string.IsNullOrEmpty(q) || listCode.Contains(r.Code.ToString()))
        //        )
        //        .OrderBy(r => r.Id);
        //    }
        //    else
        //    {
        //        listData = db.Departments
        //        .Where
        //        (
        //            r =>
        //            (string.IsNullOrEmpty(q) || ("[" + r.Code + "] " + r.Description).ToUpper().Contains(q.ToUpper())) &&
        //            r.Is_Deleted != "Y"
        //        )
        //        .OrderBy(r => r.Id);
        //    }
        //    var data = listData.Skip(fromIdx)
        //        .Take(toIdx)
        //        .Select(r => new { id = r.Code, text = "[" + r.Code + "] " + r.Description, nama = r.Description })
        //        .ToList();

        //    return new { items = data };
        //}

        [HttpGet]
        [Route("department_id")]
        public object DepartmentId(string q = "", int page = 0, bool init = false)
        {
            int fromIdx = page > 0 ? (page * 10) : 0;
            int toIdx = 10;
            var listData = db.Department.Where(r => 1 == 2);

            if (init)
            {
                listData = db.Department
                .Where
                (
                    r =>
                    (string.IsNullOrEmpty(q) || r.Id.ToString() == q)
                )
                .OrderBy(r => r.Id);
            }
            else
            {
                listData = db.Department
                .Where
                (
                    r =>
                    (string.IsNullOrEmpty(q) || ("[" + r.Code + "] " + r.Description).ToUpper().Contains(q.ToUpper())) &&
                    r.IsDeleted != "Y"
                )
                .OrderBy(r => r.Id);
            }
            var data = listData.Skip(fromIdx)
                .Take(toIdx)
                .Select(r => new { id = r.Id, text = "[" + r.Code + "] " + r.Description, nama = r.Description })
                .ToList();

            return new { items = data };
        }

        [HttpGet]
        [Route("group")]
        public object Group(string q = "", int page = 0, bool init = false)
        {
            int fromIdx = page > 0 ? (page * 10) : 0;
            int toIdx = 10;
            var listData = db.Group.Where(r => 1 == 2);

            if (init)
            {
                listData = db.Group
                           .Where
                           (
                               r => r.Id.ToString() == q
                           )
                           .OrderBy(r => r.Group_Code);
            }
            else
            {
                listData = db.Group
                           .Where
                           (
                               r =>
                               (string.IsNullOrEmpty(q) || ("[" + r.Group_Code + "] " + r.Group_Description).ToUpper().Contains(q.ToUpper()))
                           )
                           .OrderBy(r => r.Group_Code);
            }
            var data = listData.Skip(fromIdx)
                .Take(toIdx)
                .Select(r => new { id = r.Id, text = "[" + r.Group_Code + "] " + r.Group_Description })
                .ToList();

            return new { items = data };
        }

        [HttpGet]
        [Route("release_status_all")]
        public object ReleaseStatusListGeneral(string q="", string initValue = "", int page = 0)
        {
            int fromIdx = page > 0 ? (page * 10) : 0;
            int toIdx = 10;
            var listStatus = new List<String>()
            {
                Constants.ReleaseStatus.FULL,
                Constants.ReleaseStatus.PARTIAL,
                Constants.ReleaseStatus.UNRELEASED
            };

            var listData = listStatus;

            if (!string.IsNullOrEmpty(initValue))
            {
                listData = listStatus
                .Where
                (
                    r =>
                    (string.IsNullOrEmpty(initValue) || (initValue == r.ToString()))
                ).ToList();
            }
            else
            {
                listData = listStatus
                    .Where
                    (
                        r =>
                        (string.IsNullOrEmpty(q) || (r).ToUpper().Contains(q.ToUpper()))
                    ).ToList();

            }
            var data = listData.Skip(fromIdx)
                .Take(toIdx)
                .Select(r => new { id = r, text = r })
                .ToList();

            return new { items = data };
        }


        [HttpGet]
        [Route("menu")]
        public object MenuList(string q = "", int page = 0, bool init = false)
        {
            int fromIdx = page > 0 ? (page * 10) : 0;
            int toIdx = 10;
            var listData = db.Menu.Where(r => 1 == 2);

            if (init)
            {
                listData = db.Menu
                .Where
                (
                    r =>
                    (string.IsNullOrEmpty(q) || (q == r.Id.ToString()))
                )
                .OrderBy(r => r.Id);
            }
            else
            {
                listData = db.Menu
                .Where
                (
                    r =>
                    (string.IsNullOrEmpty(q) || (r.MenuName).ToUpper().Contains(q.ToUpper()))
                   
                )
                .OrderBy(r => r.Id);
            }

            var data = listData.Skip(fromIdx)
                .Take(toIdx)
                .Select(r => new { id = r.Id, text = r.MenuName })
                .Distinct()
                .ToList();

            return new { items = data };
        }


        [HttpGet]
        [Route("user")]
        public object UserList(string q = "", int page = 0, bool init = false)
        {
            int fromIdx = page > 0 ? (page * 10) : 0;
            int toIdx = 10;
            var listData = db.User.Where(r => 1 == 2).OrderBy(r => r.Id);

            if (init)
            {
                listData = db.User
                .Where
                (
                    r => r.Username.ToString() == q
                )
                .OrderBy(r => r.Username);
            }
            else
            {
                listData = db.User
                    .Where
                    (
                        r =>
                        (string.IsNullOrEmpty(q) || ("[" + r.Username + "] " + r.Full_Name).ToUpper().Contains(q.ToUpper()))
                        && r.IsDeleted != "Y"
                    )
                    .OrderBy(r => r.Username);

            }
            var data = listData.Skip(fromIdx)
                .Take(toIdx)
                .Select(r => new { id = r.Username, text = "[" + r.Username + "] " + r.Full_Name, Email = r.Email})
                .ToList();

            return new { items = data };
        }
    }

}
