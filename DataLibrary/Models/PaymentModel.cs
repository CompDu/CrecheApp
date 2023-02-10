using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class PaymentModel
    {
        public int Id { get; set; }

        public int ParentId { get; set; }

        public decimal Amount{ get; set; }

        public Guid Ref { get; set; }
    }
}
