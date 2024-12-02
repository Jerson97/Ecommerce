using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Models.Payment
{
    public class StripeSettings
    {
        public string? Publishblekey { get; set; }
        public string? SecretKey { get; set; }
    }
}
