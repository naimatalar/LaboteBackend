using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labote.Core.Entities
{
    public class Laboratory:BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public UserTopic UserTopic { get; set; }
        public Guid UserTopicId { get; set; }
        public virtual ICollection<LaboratoryUser> LaboratoryUsers { get; set; }
        public virtual ICollection<LaboratoryDevice> LaboratoryDevice { get; set; }
        public virtual ICollection<Chemical> Chemicals { get; set; }
        public virtual ICollection<SampleAccept> SampleAccepts { get; set; }



    }
}
