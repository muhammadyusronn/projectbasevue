﻿using ProjectBaseVue_Models;
using ProjectBaseVue_Models.Utilities;
using ProjectBaseVue_Public_API.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;

namespace ProjectBaseVue_Public_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [CustomAuthorize]
    public class LookupController : ControllerBase
    {
        string url = "lookup";

        [Route("list")]
        [HttpPost]
        public ResultData List(IndexParams model = null)
        {
            var result = new ResultData();

            try
            {
                //https://localhost:44332/
                var userHeaders = HttpContext.GetMiddlewareAuth();
                result = UUtils.CallMiddlewareAPI($"{url}/list", userHeaders, JsonConvert.SerializeObject(model));
                //result.data = JsonConvert.DeserializeObject<List<CompanyModel>>(result.data.ToString());
            }
            catch (Exception ex)
            {
                result.success = false;
                result.message = ex.Message;
            }

            return result;
        }

        [HttpGet("{id}/{mode}")]
        public ResultData GetData(long id, string mode)
        {
            var result = new ResultData();

            try
            {
                var userHeaders = HttpContext.GetMiddlewareAuth(mode);
                result = UUtils.CallMiddlewareAPI($"{url}/" + id.ToString(), userHeaders, "", "GET");
            }
            catch (Exception ex)
            {
                result.success = false;
                result.message = ex.Message;
            }

            return result;
        }

        [HttpPost]
        public ResultData SaveData(LookupModel model)
        {
            var result = new ResultData();

            try
            {
                var userHeaders = HttpContext.GetMiddlewareAuth(model.mode);
                result = UUtils.CallMiddlewareAPI($"{url}", userHeaders, JsonConvert.SerializeObject(model));
            }
            catch (Exception ex)
            {
                result.success = false;
                result.message = ex.Message;
            }

            return result;
        }
    }
}
