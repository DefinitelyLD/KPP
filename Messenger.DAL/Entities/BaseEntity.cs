using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.DAL.Entities
{
    public abstract class BaseEntity<TId> where TId : IComparable<TId>
    {
        [Key]
        public TId Id { get; set; }
    }
}
