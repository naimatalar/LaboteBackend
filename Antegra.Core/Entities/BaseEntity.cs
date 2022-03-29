using System;
using System.ComponentModel.DataAnnotations;

namespace Labote.Core.Entities
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public bool IsDelete { get; set; } = false;
        public bool IsActive { get; set; } = true;
        public Guid? CreatorUserId { get; set; }

    }
}
