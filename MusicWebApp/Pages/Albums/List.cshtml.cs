using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MusicWebApp.Models;
using Newtonsoft.Json;

namespace MusicWebApp.Pages.Albums
{
    public class ListModel : PageModel
    {
        string _API;


        public ListModel()
        {
            var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            this._API = config["ApiUrl"] + "/api/Albums";
        }

        public IEnumerable<Album> albums { get; set; }
        public async Task OnGet()
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage? response = await httpClient.GetAsync($"{this._API}");
            string? content = await response.Content.ReadAsStringAsync();
            albums = JsonConvert.DeserializeObject<List<Album>>(content);
        }


        public async Task<IActionResult> OnPostBorrar(int id)
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage? response = await httpClient.DeleteAsync($"{this._API}/{id}");
            string? content = await response.Content.ReadAsStringAsync();
            return RedirectToPage("List");
        }
    }
}
