using System.Text;
using System.Collections.Generic;
using CMSSure.Builder.Enums;
using CMSSure.Builder.Models;

namespace CMSSure.Builder.Utilities
{
    public static class NodeBuilder
    {
        public static string Compile(params HTMLNode[] nodes)
        {
            string template = string.Empty;

            foreach (HTMLNode node in nodes)
            {
                node.IndentationLevel = 0;
                template += GenerateMarkup(node);
            }

            return template;
        }

        private static string GenerateMarkup(HTMLNode node)
        {
            string tag = node.Tag.ToString().ToLower();

            StringBuilder nodeBuilder = new StringBuilder();

            StringBuilder openingTag = new StringBuilder();
            
            if (node.IsSelfClosing)
            {
                openingTag.AppendLine($"{GenerateTabification(node.IndentationLevel)}<{tag} {GenerateAttributes(node.Attributes)} />");
            }
            else
            {
                openingTag.AppendLine($"{GenerateTabification(node.IndentationLevel)}<{tag} {GenerateAttributes(node.Attributes)}>");
            }

            // openingTag.AppendLine(node.InnerHTML);

            nodeBuilder.Append(openingTag);

            foreach(HTMLNode childNode in node.Children)
            {
                childNode.IndentationLevel = node.IndentationLevel + 1;
                nodeBuilder.Append(GenerateMarkup(childNode));
            }

            if(!node.IsSelfClosing)
            {
                StringBuilder closingTag = new StringBuilder();
                closingTag.AppendLine($"{GenerateTabification(node.IndentationLevel)}</{tag}>");

                nodeBuilder.Append(closingTag);
            }

            return nodeBuilder.ToString();
        }

        private static string GenerateAttributes(Dictionary<HTMLAttribute, string> attributes)
        {
            string nodeAttributes = string.Empty;

            foreach(KeyValuePair<HTMLAttribute, string> attribute in attributes)
            {
                nodeAttributes += $"{attribute.Key.ToString().ToLower()}=\"{attribute.Value}\"";
            }

            return nodeAttributes;
        }

        private static string GenerateTabification(int indentationLevel)
        {
            StringBuilder builder = new StringBuilder();

            for(int i = 0; i < indentationLevel; i++)
            {
                builder.Append(HTMLCharacter.Tab);
            }

            return builder.ToString();
        }
    }
}