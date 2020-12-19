using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_SERVER.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace API_SERVER.Pages
{
    public class DownloadLatestVersionModel : PageModel
    {
        public string url { get; set; }

        public string updateContent { get; set; }
        public string updateVersion { get; set; }
        private UpdateService service { get; set; }
        public DownloadLatestVersionModel(UpdateService updateService)
        {
            service = updateService;
        }
        public void OnGet()
        {
            var tuple = service.downloadContent();
            url = tuple.Item1;
            updateVersion = tuple.Item2;
            updateContent = tuple.Item3;
        }
    }
}
