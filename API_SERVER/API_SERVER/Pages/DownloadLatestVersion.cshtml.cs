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
        private UpdateService service { get; set; }
        public DownloadLatestVersionModel(UpdateService updateService)
        {
            url = updateService.downloadPage();
            service = updateService;
        }
        public void OnGet()
        {
            url = service.downloadPage();
        }
    }
}
