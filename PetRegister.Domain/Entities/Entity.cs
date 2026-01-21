using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PetRegister.Domain.Entities
{
    public class Entity
    {
        protected Entity() { }
        
        public Guid Id { get; set; }
    }
}
