using System;
using System.Text;
using CMSSure.Builder.Enums;
using CMSSure.Builder.Models;
using CMSSure.Builder.Utilities;

namespace CMSSure.Builder.Models
{
    public static class HTMLNodeExtensions
    {
        public static HTMLNode Add(this HTMLNode node, HTMLNode child)
        {
            node.Children.Add(child);
            return node;
        }

        public static HTMLNode Add(this HTMLNode node, params HTMLNode[] childNodes)
        {
            foreach(HTMLNode childNode in childNodes)
            {
                Add(node, childNode);
            }

            return node;
        }
    }
}