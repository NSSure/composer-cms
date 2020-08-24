using System.Collections.Generic;
using CMSSure.Builder.Utilities;

namespace CMSSure.Builder.Models
{
    public static class HTMLNodeExtensions
    {
        public static string Compile(this HTMLNode node)
        {
            return NodeBuilder.Compile(node);
        }

        public static string Compile(this List<HTMLNode> nodes)
        {
            return NodeBuilder.Compile(nodes.ToArray());
        }

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