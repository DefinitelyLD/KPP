using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.BLL.Models
{
    public class BaseModel<TId> where TId : IComparable<TId>
    {
        public TId Id { get; set; }
    }
}
