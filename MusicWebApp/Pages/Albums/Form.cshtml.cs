using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MusicWebApp.Models;
using Newtonsoft.Json;

namespace MusicWebApp.Pages.Albums
{
    public class FormModel : PageModel
    {
        string _API;

        [BindProperty]
        public Album album { get; set; }

        [BindProperty]
        public List<Band> bands { get; set; }


        public FormModel()
        {
            var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                                   .AddJsonFile("appsettings.json")
                                                   .Build();
            _API = config["ApiUrl"];
        }

        public async Task OnGet(int? id)
        {
            await GetAlbum(id);
            await GetBands();
        }


        public async Task GetAlbum(int? id)
        {
            if (id != null || id == 0)
            {
                HttpClient httpClient = new HttpClient();
                HttpResponseMessage? response = await httpClient.GetAsync($"{_API}/api/Albums/{id}");
                string? content = await response.Content.ReadAsStringAsync();
                album = JsonConvert.DeserializeObject<Album>(content) ?? new Album();
            }
            else
            {
                album = new Album();
            }
        }

        public async Task GetBands()
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage? response = await httpClient.GetAsync($"{_API}/api/Bands");
            string? content = await response.Content.ReadAsStringAsync();
            bands = JsonConvert.DeserializeObject<List<Band>>(content) ?? new List<Band>();
        }

        public Task<IActionResult> OnPostGuardar(int? id)
        {
            if (id == null || id == 0) { return Create(); }
            else { return Update(id); };
        }

        public async Task<IActionResult> Create()
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage? response = await httpClient.PostAsJsonAsync($"{_API}/api/Albums", album);
            string? returnValue = await response.Content.ReadAsStringAsync();
            return RedirectToPage("List");
        }


        public async Task<IActionResult> Update(int? id)
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage? response = await httpClient.PutAsJsonAsync($"{_API}/api/Albums/{id}", album);
            string? content = await response.Content.ReadAsStringAsync();
            return RedirectToPage("List");
        }


    }
}
