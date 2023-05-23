using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Models
{
    
    public class OrderHeaderViewModel
    {
        public string OrderDesc { get; set; }
        public int Supplier{ get; set; }
        public int Salesman { get; set; }
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }
        public List<OrderItemViewModel> OrderItems { get; set; }
    }


    public class OrderItemViewModel
    {
        public int DrugId { get; set; }
        public int Quantity { get; set; }



    }
}