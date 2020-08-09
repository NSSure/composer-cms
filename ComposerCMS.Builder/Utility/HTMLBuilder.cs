using System.Text;
using System.IO;
using System.Collections.Generic;
using CMSSure.Builder.Enums;
using CMSSure.Builder.Models;
using CMSSure.Builder.Utilities;

namespace CMSSure.Builder.Utilities
{
    public class HTMLBuilder
    {
        public HTMLNode Html { get; set; }
        public HTMLNode Head { get; set; }
        public HTMLNode Body { get; set; }

        public HTMLNode Create(params HTMLNode[] contentNodes)
        {
            this.Html = new HTMLNode(HTMLTag.Html);
            this.Head = new HTMLNode(HTMLTag.Head);
            this.Body = new HTMLNode(HTMLTag.Body);

            this.Body.Add(contentNodes);
            this.Html.Add(this.Head, this.Body);

            return this.Html;
        }
    }
}