using ProjectBaseVue_Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ProjectBaseVue_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExampleController : ControllerBase
    {

        public void List()
        {
            string fruit = "apple";

            string[] fruits = new string[]
            {
                "apple", 
                "orange", 
                "melon", 
                "banana", 
                "durian", 
                "raspberry"
            };

            string[] fruits2 = new string[5];

            for (int i = 0; i < fruits.Length; i++)
            {
                fruits2[i] = fruits[i];
            }
            




        }
    }
}
