using System;
using System.ComponentModel.DataAnnotations;

namespace Messenger.DAL.Entities
{
    public abstract class BaseEntity<TId> where TId : IComparable<TId>
    {
        [Key]
        public TId Id { get; set; }
    }
}
