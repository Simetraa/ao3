using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ao3.lib.exceptions
{
    public class AuthorNotFoundException : Exception
    {
        public AuthorNotFoundException() : base("Could not find author") { }
    }
}
