using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Enums.QB
{
    public enum QBOActionTypeEnum : int
    {
        [Description("Add")]
        Add = 1,
        [Description("Update")]
        Update = 2,
        [Description("Delete")]
        Delete = 3
    }
}
