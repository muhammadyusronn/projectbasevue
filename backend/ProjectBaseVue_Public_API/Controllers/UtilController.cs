using ProjectBaseVue_Models.Utilities;
using ProjectBaseVue_Public_API.Utilities;
using ProjectBaseVue_Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ProjectBaseVue_Public_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [CustomAuthorize]
    public class UtilController : ControllerBase
    {
        string mode = Constants.FORM_MODE_UTIL;

        [HttpGet]
        [Route("transporter_exceed_quota")]
        public ResultData TransporterExceedQuota(string transporter_code, string order_ids)
        {
            var result = new ResultData();

            try
            {
                var userHeaders = HttpContext.GetMiddlewareAuth(mode);
                result = UUtils.CallMiddlewareAPI($"util/transporter_exceed_quota?transporter_code={transporter_code}&order_ids={order_ids}", userHeaders, "", "GET");
            }
            catch (Exception ex)
            {
                result.success = false;
                result.message = ex.Message;
            }

            return result;
        }

        [HttpGet]
        [Route("customer_vendor_address")]
        public ResultData CustomerVendorAddress(string code, string type, bool hide = true)
        {
            var result = new ResultData();

            try
            {
                var userHeaders = HttpContext.GetMiddlewareAuth(mode);
                result = UUtils.CallMiddlewareAPI($"util/customer_vendor_address?code={code}&type={type}&hide={hide}", userHeaders, "", "GET");
            }
            catch (Exception ex)
            {
                result.success = false;
                result.message = ex.Message;
            }

            return result;
        }

        [HttpGet]
        [Route("order_type_fields")]
        public ResultData OrderTypeFields(long id)
        {
            var result = new ResultData();

            try
            {
                var userHeaders = HttpContext.GetMiddlewareAuth(mode);
                result = UUtils.CallMiddlewareAPI($"util/order_type_fields?id={id}", userHeaders, "", "GET");
            }
            catch (Exception ex)
            {
                result.success = false;
                result.message = ex.Message;
            }

            return result;
        }

        [HttpGet]
        [Route("order_type_fields_code")]
        public ResultData OrderTypeFieldsCode(string code)
        {
            var result = new ResultData();

            try
            {
                var userHeaders = HttpContext.GetMiddlewareAuth(mode);
                result = UUtils.CallMiddlewareAPI($"util/order_type_fields_code?code={code}", userHeaders, "", "GET");
            }
            catch (Exception ex)
            {
                result.success = false;
                result.message = ex.Message;
            }

            return result;
        }

        [HttpGet]
        [Route("order_is_relation")]
        public ResultData OrderIsRelation(string incoterm, string order_type)
        {
            var result = new ResultData();

            try
            {
                var userHeaders = HttpContext.GetMiddlewareAuth(mode);
                result = UUtils.CallMiddlewareAPI($"util/order_is_relation?incoterm={incoterm}&order_type={order_type}", userHeaders, "", "GET");
            }
            catch (Exception ex)
            {
                result.success = false;
                result.message = ex.Message;
            }

            return result;
        }

        [HttpPost]
        [Route("refresh_freight_cost")]
        public ResultData RefreshFreightCost(TransporterFreightModel model)
        {
            var result = new ResultData();
            bool success = false;
            string message = "";

            try
            {
                var userHeaders = HttpContext.GetMiddlewareAuth(mode);
                result = UUtils.CallMiddlewareAPI("util/refresh_freight_cost", userHeaders, JsonConvert.SerializeObject(model));
            }
            catch (Exception ex)
            {
                result.success = success;
                result.message = ex.Message;

            }

            return result;
        }


        [HttpGet]
        [Route("lookup_sap_sync")]
        public ResultData LookupSAPSync()
        {
            var result = new ResultData();

            try
            {
                var userHeaders = HttpContext.GetMiddlewareAuth(mode);
                result = UUtils.CallMiddlewareAPI($"util/lookup_sap_sync", userHeaders, "", "GET");
            }
            catch (Exception ex)
            {
                result.success = false;
                result.message = ex.Message;
            }

            return result;
        }


        [HttpGet]
        [Route("version")]
        public ResultData GetVersion()
        {
            var result = new ResultData();

            try
            {
                var userHeaders = HttpContext.GetMiddlewareAuth(mode);
                result = UUtils.CallMiddlewareAPI($"util/version", userHeaders, "", "GET");
            }
            catch (Exception ex)
            {
                result.success = false;
                result.message = ex.Message;
            }

            return result;
        }

        [HttpGet]
        [Route("ping")] //For Probes
        public string Ping()
        {
            return "OK";
        }

        [HttpGet]
        [Route("changelog")]
        public ResultData GetChangelog()
        {
            var result = new ResultData();

            try
            {
                var userHeaders = HttpContext.GetMiddlewareAuth(mode);
                result = UUtils.CallMiddlewareAPI($"util/changelog", userHeaders, "", "GET");
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
