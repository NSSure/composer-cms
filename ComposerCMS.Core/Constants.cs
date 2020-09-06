using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace ComposerCMS.Core
{
    public class Constants
    {
        public static IConfiguration Configuration { get; set; }

        /// <summary>
        /// Used when referencing physical file system paths.
        /// </summary>
        public class Path
        {
            public static string ContentDirectory = @"wwwroot\composer-cms";
            public static string PackageDirectory = @$"{ContentDirectory}\package";
            public static string CssDirectory = @$"{ContentDirectory}\css";
            public static string JsDirectory = @$"{ContentDirectory}\js";
            public static string MediaDirectory = @$"{ContentDirectory}\media";
            public static string ThemeDirectory = @$"{ContentDirectory}\themes";
        }

        public class Href
        {
            public static string ContentDirectory = "composer-cms";
            public static string PackageDirectory = @$"{ContentDirectory}/package";
            public static string CssDirectory = $"{ContentDirectory}/css";
            public static string ThemeDirectory = $"{ContentDirectory}/themes";
            public static string JsDirectory = $"{ContentDirectory}/js";
            public static string MediaDirectory = $"{ContentDirectory}/media";
        }

        public class Route
        {
            public static string BlogBaseUrl = "blog";
            public static string ProductBaseUrl = "product";
            public static string ProductCategoryBaseUrl = "product/categories";
        }

        public class Settings
        {
            public static Guid ID = Guid.Parse("62366de5-9661-4bdd-a42e-5dfdd06bf03a");
        }

        public class File
        {
            public static List<string> SupportedImageExtensions = new List<string> { ".jpg", ".png" };
        }

        public class Environment
        {
            public static string EmptySpace = " ";
        }

        public class Permission
        {
            public class Role
            {
                public static Guid AdminID = Guid.Parse("993ab932-df4d-47ba-902f-2ec313dc4e73");
                public static Guid EditorID = Guid.Parse("4d323c3f-d805-4932-bb1e-02cc2d0f58b5");
                public static Guid AuthorID = Guid.Parse("de6f9df8-d8ef-4dc4-b0b0-fc2dc2c51aed");
                public static Guid ContributorID = Guid.Parse("a7f52a41-4c4c-45e0-9088-a89cb25dea92");
                public static Guid UserID = Guid.Parse("e3c7b0e0-88f7-4bd0-b846-66c63db1f614");
            }
        }

        public class Theme
        {
            public static Guid DefaultID = Guid.Parse("B48EED67-62BF-4C47-889A-EA1B90218B86");
        }
    }
}
