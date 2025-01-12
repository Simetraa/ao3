using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ao3.lib.exceptions
{
    public class WorkNotFoundException : Exception
    {
        public WorkNotFoundException() : base("Could not find work") { }
    }
}
