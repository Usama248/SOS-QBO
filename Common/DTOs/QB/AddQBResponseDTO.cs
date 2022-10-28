using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs.QB
{
    public class AddQBResponseDTO
    {
        public Guid? DeletedQBId { get; set; }
        public Guid? AddedId { get; set; }
    }
}
