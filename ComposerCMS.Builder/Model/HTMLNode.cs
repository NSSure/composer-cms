using System;
using System.Collections.Generic;
using System.Text;
using CMSSure.Builder.Enums;

namespace CMSSure.Builder.Models
{
    public class HTMLNode
    {
        public HTMLNode(HTMLTag tag)
        {
            this.Tag = tag;

            this.Attributes = new Dictionary<HTMLAttribute, string>();
            this.Children = new List<HTMLNode>();

            if (tag == HTMLTag.Link)
            {
                this.IsSelfClosing = true;
            }
        }

        public HTMLTag Tag { get; set; }
        public int IndentationLevel { get; set; }
        public string InnerText { get; set; }
        public string InnerHTML { get; set; }
        public List<HTMLNode> Children { get; set; }
        public Dictionary<HTMLAttribute, string> Attributes { get; set; }

        public readonly bool IsSelfClosing;

        public void AddAttribute(HTMLAttribute attribute, string value)
        {
            if(!Attributes.ContainsKey(attribute))
            {
                Attributes.Add(attribute, value);
            }
        }
    }
}