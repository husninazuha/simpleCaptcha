using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace twoModelsInOneView.Models
{
    public class Customer
    {
        public int CustId { get; set; }
        public string CustName { get; set; }
        public string EmailAdd { get; set; }
        public DateTime DteCreated { get; set; }
    }
}
