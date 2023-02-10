using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CrecheApp.Models
{
    public class PaymentModel
    {
        [Key]
        [HiddenInput]
        public int Id { get; set; }
        [HiddenInput]
        public int ParentId { get; set; }
        [DisplayName("Amount Owed")]
        public decimal Amount { get; set; }
        [DisplayName("Payment Reference")]
        public Guid Ref { get; set; }
    }
}
