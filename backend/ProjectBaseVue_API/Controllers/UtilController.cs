using ProjectBaseVue_API.Utilities;
using ProjectBaseVue_Data;
using ProjectBaseVue_Models;
using ProjectBaseVue_Models.Resources;
using ProjectBaseVue_Models.Utilities;
using ProjectBaseVue_ViewModels;
using ProjectBaseVue_ViewModels.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
//using RabbitTester.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBaseVue_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiAuthorize]
    public class UtilController : ControllerBase
    {
        DataEntities db = new DataEntities();

        //[HttpGet]
        //[Route("transporter_exceed_quota")]
        //public ResultData TransporterExceedQuota(string transporter_code, string order_ids)
        //{
        //    var result = new ResultData();
        //    var success = false;
        //    var message = "";

        //    try
        //    {
        //        var exceedQuota = false;
        //        var exceedMessage = "";

        //        var orderIds = order_ids.Split(",").Select(r=> Int64.Parse(r)).ToArray();

        //        success = UUtils.CheckOrdersExceedTransporterQuota(db, transporter_code, orderIds, out exceedQuota, out exceedMessage);

        //        result.data = new
        //        {
        //            exceed = exceedQuota,
        //            message = exceedMessage
        //        };
        //    }
        //    catch(Exception ex)
        //    {
        //        message = ex.Message;
        //    }

        //    result.success = success;
        //    result.message = message;

        //    return result;
        //}

        //[HttpGet]
        //[Route("customer_vendor_address")]
        //public ResultData CustomerVendorAddress(string code, string type, bool hide = true)
        //{
        //    var result = new ResultData();
        //    var success = false;
        //    var address = "";
        //    var message = "";

        //    try
        //    {
        //        if(type == Constants.Roles.CUSTOMER)
        //        {
        //            var customer = db.Customers.Where(r => r.Customer_Code == code).FirstOrDefault();
        //            address = UUtils.GetCustomerAddress(customer);
        //        }
        //        else if(type == Constants.Roles.VENDOR)
        //        {
        //            var vendor = db.Vendors.Where(r => r.Code == code).FirstOrDefault();
        //            address = UUtils.GetVendorAddress(vendor, hide);
        //        }

        //        success = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        message = ex.Message;
        //    }

        //    result.success = success;
        //    result.message = message;
        //    result.data = address;

        //    return result;
        //}

        //[HttpGet]
        //[Route("order_type_fields")]
        //public ResultData OrderTypeFields(long id)
        //{
        //    var result = new ResultData();
        //    bool success = false;
        //    string message = "";

        //    try
        //    {
        //        var orderType = db.Order_Type.Where(r => r.Id == id).FirstOrDefault();
        //        result.data = new OrderTypeViewModel(orderType);
        //        success = true;
        //    }
        //    catch(Exception ex)
        //    {
        //        message = ex.Message;
        //    }

        //    result.success = success;
        //    result.message = message;

        //    return result;
        //}

        //[HttpGet]
        //[Route("order_type_fields_code")]
        //public ResultData OrderTypeFieldsCode(string code)
        //{
        //    var result = new ResultData();
        //    bool success = false;
        //    string message = "";

        //    try
        //    {
        //        var orderType = db.Order_Type.Where(r => r.Code == code).FirstOrDefault();
        //        result.data = new OrderTypeViewModel(orderType);
        //        success = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        message = ex.Message;
        //    }

        //    result.success = success;
        //    result.message = message;

        //    return result;
        //}

        //[HttpGet]
        //[Route("order_is_relation")]
        //public ResultData OrderIsRelation(string incoterm, string order_type)
        //{
        //    var result = new ResultData();
        //    bool success = false;
        //    string message = "";

        //    try
        //    {
        //        var orderType = db.Order_Type.Where(r => r.Code == order_type && !r.Is_Deleted).FirstOrDefault();
        //        if (orderType == null)
        //            throw new Exception(Resources.ORDER_TYPE_INVALID);

        //        var lookup = db.Lookups.Where(r => r.Lookup_Group == Constants.LookupGroup.RELATION_SETTING && r.Lookup_Info1 == orderType.WB_Trans_Type && r.Lookup_Info2 == incoterm && !r.Is_Deleted).FirstOrDefault();

        //        result.data = lookup != null;
        //        success = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        message = ex.Message;
        //    }

        //    result.success = success;
        //    result.message = message;

        //    return result;
        //}


        //[HttpPost]
        //[Route("refresh_freight_cost")]
        //public ResultData RefreshFreightCost(TransporterFreightModel model)
        //{
        //    var result = new ResultData();
        //    bool success = false;
        //    string message = "";

        //    try
        //    {            
        //        var loading = model.loading;
        //        var destination = model.destination;
        //        //var excludeTransporters = model.exclude_transporters;
        //        var materials = model.materials;
        //        var container = model.container;
        //        var chargeOACompany = model.charge_oa_company;
        //        var documentType = model.document_type;
        //        var warehouseCode = model.warehouse_code;
        //        var freightDate = model.freight_date;
        //        var sapOrderIds = model.sap_order_ids;
        //        var selectedTransporters = model.selected_transporters;
        //        //var excludeTransporters = model.exclude_transporters;

        //        var listData = UUtils.GetTransporters(db, loading, destination, warehouseCode, documentType, freightDate, null, materials, container, chargeOACompany, sapOrderIds, selectedTransporters);

        //        success = true;
        //        message = "OK";
        //        result.data = listData;
        //    }
        //    catch(Exception ex)
        //    {
        //        message = ex.Message;
        //    }

        //    result.success = success;
        //    result.message = message;

        //    return result;
        //}

        //[HttpPost]
        //[Route("refresh_freight_cost_order")]
        //public ResultData RefreshFreightCostOrder(ReassignDOModel model)
        //{
        //    var result = new ResultData();
        //    bool success = false;
        //    var message = "";

        //    try
        //    {
        //        var orderId = model.order_id;
        //        var details = model.details;

        //        var order = db.Orders.Where(r => r.Id == orderId && !r.Is_Deleted).FirstOrDefault();
        //        if (order == null)
        //            throw new Exception(Resources.ORDER_INVALID);

        //        //Total Biaya Pengiriman untuk per kg
        //        var lookup = db.Lookups.Where(r => r.Lookup_Group == Constants.LookupGroup.SHIPMENT_TYPE && r.Lookup_Key == order.Shipment_Type).FirstOrDefault();
        //        if (lookup == null)
        //            throw new Exception(Resources.SHIPMENT_TYPE_INVALID);

        //        var includedDetails = details.Where(r => r.include_merge).ToArray();
        //        if (includedDetails.Length == 0)
        //            throw new Exception("At least 1(one) or more item(s) must be included in Order");

        //        var editedOrderDetails = includedDetails.Where(r=> r.include_merge).Select(r => new Order_Detail()
        //        {
        //            Material_Code = r.Material_Code,
        //            Qty = r.Qty,
        //            UOM = r.UOM,
        //            Item_No = r.Item_No
        //        }).ToArray();

        //        if (lookup.Lookup_Key != Constants.ShipmentTypeCategory.RELATION)
        //        {
        //            if (!order.SAP_Transporter.HasValue || (order.SAP_Transporter.HasValue && !order.SAP_Transporter.Value))
        //            {
        //                if (!order.Freight_Cost_Value.HasValue || (order.Freight_Cost_Value.HasValue && !order.Freight_Cost_Value.Value))
        //                {
        //                    var transporterData = new TransporterSelectModel();
        //                    var getTransporterMessage = "";
        //                    var getTransporterData = UUtils.GetOrderTransporterData(db, order, editedOrderDetails.ToArray(), out getTransporterMessage, out transporterData);

        //                    if (getTransporterData)
        //                    {
        //                        result.data = transporterData;
        //                    }
        //                    else
        //                    {
        //                        throw new Exception(getTransporterMessage);
        //                    }
        //                }
        //            }
        //        }

        //        success = true;
        //        message = "OK";
        //    }
        //    catch(Exception ex)
        //    {
        //        message = ex.Message;   
        //    }

        //    result.success = success;
        //    result.message = message;

        //    return result;
        //}

        [HttpGet]
        [Route("version")]
        public ResultData GetWebVersion()
        {
            var result = new ResultData();
            result.success = true;
            result.message = Constants.OK;

            try
            {
                DataEntities db = new DataEntities();

                var environment = "";
                var version = "";

                //environment = ConfigurationManager.AppSettings["Environment"].ToString();

                //var changeLog = db.Changelogs.Where(r => r.Active == "Y" && !r.Is_Deleted).FirstOrDefault();
                //if (changeLog != null)
                //    version = changeLog.Version;

                var setting = db.Setting.Where(r => r.Code == Constants.SettingsCode.WEB_VERSION).FirstOrDefault();
                if (setting != null)
                {
                    version = setting.Value;
                }

                result.data = $"[{environment}] v.{version}";
            }
            catch (Exception ex)
            {
                result.success = false;
                result.message = ex.Message;
            }

            return result;
        }

        [HttpGet]
        [Route("ping")]
        public string PingBackend()
        {
            var result = new ResultData();
            bool success = false;
            string message = "";

            try
            {
                //var user = HttpContext.GetUserData();
                var rpc = new RpcClient();

                var res = rpc.CallPing(BaseProgram.CreateRoutingKey("util", "ping"), "", false);

                result = JsonConvert.DeserializeObject<ResultData>(res);

                message = result.message;
            }
            catch(Exception ex)
            {
                message = ex.Message;
            }

            return message;
        }



    }
}
