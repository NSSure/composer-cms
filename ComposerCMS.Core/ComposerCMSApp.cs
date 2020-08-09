using CMSSure.Builder.Enums;
using CMSSure.Builder.Models;
using CMSSure.Builder.Utilities;
using ComposerCMS.Core.Entity;
using ComposerCMS.Core.Model;
using ComposerCMS.Core.Utility;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ComposerCMS.Core
{
    public class ComposerCMSApp
    {
        public static string Css { get; set; }
        public static string Js { get; set; }

        public static List<RouteSection> SystemRoutes { get; set; }
        public static List<Theme> Themes { get; set; }
        public static Settings Settings { get; set; }

        public static void LoadExternalResources()
        {
            //string[] paths = Directory.GetFiles("wwwroot/plugins", "*.css", searchOption: SearchOption.AllDirectories);

            //foreach (string path in paths)
            //{
            //    Css = string.Format("<link rel=\"stylesheet\" href=\"{0}\"", path);
            //}
        }

        public static string GetTitle(ViewDataDictionary dataDict)
        {
            string _title = string.Empty;

            if (dataDict.ContainsKey("Title"))
            {
                _title = dataDict["Title"].ToString();
            }

            if (Settings != null && !string.IsNullOrEmpty(Settings.Title))
            {
                if (string.IsNullOrEmpty(_title))
                {
                    _title = $"{Settings.Title}";
                }
                else
                {
                    _title = $"{_title} - {Settings.Title}";
                }
            }

            if (string.IsNullOrEmpty(_title))
            {
                _title = "Composer CMS";
            }

            return _title;
        }

        public static void Init()
        {
            ComposerCMSApp.EnsureContentDirectories();
            ComposerCMSApp.LoadConfiguration();
        }

        private static void LoadConfiguration()
        {
            ComposerCMSApp.SystemRoutes = Constants.Configuration.GetSection("Routes").Get<List<RouteSection>>();
            ComposerCMSApp.Themes = Constants.Configuration.GetSection("Themes").Get<List<Theme>>();
        }

        public static void EnsureContentDirectories()
        {
            if (!Directory.Exists(Constants.Path.CssDirectory))
            {
                Directory.CreateDirectory(Constants.Path.CssDirectory);
            }

            if (!Directory.Exists(Constants.Path.JsDirectory))
            {
                Directory.CreateDirectory(Constants.Path.JsDirectory);
            }

            if (!Directory.Exists(Constants.Path.MediaDirectory))
            {
                Directory.CreateDirectory(Constants.Path.MediaDirectory);
            }
        }

        public static HtmlString ApplyThemeCss()
        {
            Theme _appliedTheme = Themes.FirstOrDefault(a => a.Key == Settings.ThemeKey);

            if (_appliedTheme == null)
            {
                return HtmlString.Empty;
            }

            string _href = $"/{Constants.Href.ThemeDirectory}/{_appliedTheme.Name}/theme.css";

            var node = new HTMLNode(HTMLTag.Link);

            node.AddAttribute(HTMLAttribute.Rel, "stylesheet");
            node.AddAttribute(HTMLAttribute.Href, _href);

            string _compiledNode = NodeBuilder.Compile(node);

            return new HtmlString(_compiledNode);
        }
    }
}