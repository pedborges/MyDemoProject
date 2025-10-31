using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public sealed class CustomerEntity: BaseEntity
    {        
        public string CustomerName { get; set; }=string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public string CustomerRole{ get; set; } = string.Empty;
        public string CustomerPassword { get; set; } = string.Empty;
        public int CustomerAge { get; set; }
        public string CustomerCity { get; set; } = string.Empty;
        public string CustomerCountry { get; set; } = string.Empty;
        public DateTime CustomerBirthday { get; set; }
        public ICollection<SellEntity> Sells { get; set; } = new List<SellEntity>();
    }

}
