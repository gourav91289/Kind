using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OmniPot.Models.StateRegistrationViewModels
{
    public class OrderSummaryViewModel
    {
        public Guid PersonId { get; set; }
        public Guid OrderId { get; set; }
        public string ConfirmationNumber { get; set; }
        public DateTime OrderDateUtc { get; set; }
        public List<OrderItemViewModel> OrderItems { get; set; } = new List<OrderItemViewModel>();
    }

    public class PendingOrdersViewModel
    {
        public Guid PersonId { get; set; }
        public Guid OrderId { get; set; }
        public int ApplicationStatus { get; set; }
        public string ConfirmationNumber { get; set; }
        public DateTime OrderDateUtc { get; set; }
        public int OrderQty { get; set; }
        public List<OrderItemViewModel> OrderItems { get; set; } = new List<OrderItemViewModel>();
    }

    public class OrderItemViewModel
    {
        public Guid OrderItemId { get; set; }
        public int TagCount { get; set; }
        public decimal Price { get; set; }
        public decimal LineTotal { get; set; }
        public string TagType { get; set; }

    }

    public class GetOrderSummaryViewModel
    {
        [Required]
        public Guid PersonId { get; set; }
        [Required]
        public Guid OrderId { get; set; }
    }

    public class GetPendingOrdersViewModel
    {
        [Required]
        public Guid UserId { get; set; }
    }

}
