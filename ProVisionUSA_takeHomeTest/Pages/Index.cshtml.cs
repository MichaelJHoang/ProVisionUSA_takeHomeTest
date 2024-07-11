using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ProVisionUSA_takeHomeTest.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        // create a public instance of the program lister to be accesssed in the corresponding HTML file
        public ProgramLister _programLister;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            _programLister = new ProgramLister();
        }

        // make a call to FetchAllVersions() to get the program versions as soon as this callback is called
        public async Task OnGet()
        {
            await _programLister.FetchAllVersions();
        }
    }
}
