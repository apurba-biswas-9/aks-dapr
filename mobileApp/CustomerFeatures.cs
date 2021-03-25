using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace mobileApp
{
    public class CustomerFeatures
    {
        /// <summary>  
        /// This dictionary will hold a record for each customer Id and the Feature class object.   
        /// </summary>  
        public Dictionary<int, List<string>> EnabledFeatures = new Dictionary<int, List<string>>();
    }
}
