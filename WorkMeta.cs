using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;

namespace ao3
{
    class WorkMeta : HtmlElement
    {
        public WorkMeta(Document owner, string localName, string? prefix = null, NodeFlags flags = NodeFlags.None) : base(owner, localName, prefix, flags)
        {
        }
    }
}
