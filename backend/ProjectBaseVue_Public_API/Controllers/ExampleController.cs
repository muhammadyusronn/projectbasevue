using ProjectBaseVue_Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace ProjectBaseVue_Public_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExampleController : ControllerBase
    {

        [HttpGet]
        [Route("get_sample")]
        public object GetSample()
        {
            var headerModel = new ExampleModel();
            headerModel.Details = new List<ExampleDetailModel>();

            //var detail = new ExampleDetailModel();
            //detail.files_data = new List<object>();
            //detail.Image = new List<IFormFileWrapper>();

            return null;
        }



        [HttpPost]
        [Route("save")]
        public ResultData Save([FromForm] ExampleModel model)
        {
            var result = new ResultData();

            try
            {

            }
            catch(Exception ex)
            {

            }

            return result;
        }


    }
}
