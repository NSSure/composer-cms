using ComposerCMS.Core.Entity;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ComposerCMS.Core.Utility
{
    public class FileUtility
    {
        public const string HandleRequestsDirectlyFlag = "@page";

        public async Task WriteFile(Page page)
        {
            string _normalizedName = string.Join('-', page.Name.Split(Constants.Environment.EmptySpace).Select(a => a.ToLower()));

            if (string.IsNullOrEmpty(page.Path))
            {
                page.Path = string.Format("pages/{0}.cshtml", _normalizedName);
                page.Content = string.Format("{0}{1}{2}", HandleRequestsDirectlyFlag, Environment.NewLine, page.Content);
            }

            using (FileStream fileStream = File.Open(page.Path, FileMode.Create))
            {
                using (StreamWriter streamWriter = new StreamWriter(fileStream))
                {
                    await streamWriter.WriteLineAsync(page.Content);
                }
            }
        }

        public void Bundle()
        {
            //List<Guid> _externalResourceIDs = _pageScripts.Select(a => a.ExternalResourceID).ToList();
            //List<ExternalResource> _scriptResources = await this._externalResourceUtil.Table.Where(a => _externalResourceIDs.Contains(a.ID)).ToListAsync();

            //StringBuilder _resources = new StringBuilder();

            //using (FileStream fileStream = File.Open($"{Constants.Path.CssDirectory}/composer-css.min.css", FileMode.CreateNew))
            //{
            //    foreach (PageScript pageScript in _pageScripts)
            //    {
            //        ExternalResource _scriptResource = _scriptResources.FirstOrDefault(a => a.ID == pageScript.ExternalResourceID);

            //        if (File.Exists(_scriptResource.Path))
            //        {
            //            string _content = File.ReadAllText(_scriptResource.Path);
            //            fileStream.Write(Encoding.ASCII.GetBytes(_content));
            //        }
            //    }
            //}
        }
    }
}