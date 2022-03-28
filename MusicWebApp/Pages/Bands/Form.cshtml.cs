using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MusicWebApp.Models;
using Newtonsoft.Json;

namespace MusicWebApp.Pages.Bands
{
    public class FormModel : PageModel
    {
        [BindProperty]
        public Band band{ get; set; }

        string _API;

        public FormModel()
        {
            var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                                   .AddJsonFile("appsettings.json")
                                                   .Build();
            _API = config["ApiUrl"];
        }
        public async Task OnGet(int? id)
        {
            await GetBand(id);
        }

        public async Task GetBand(int? id)
        {

            if (id != null || id == 0)
            {
                HttpClient httpClient = new HttpClient();
                HttpResponseMessage? response = await httpClient.GetAsync($"{_API}/api/Bands/{id}");
                string? content = await response.Content.ReadAsStringAsync();
                band = JsonConvert.DeserializeObject<Band>(content) ?? new Band();
            }
            else
            {
                band = new Band();
            }


        }

        public Task<IActionResult> OnPostGuardar(int? id)
        {
            if (id == null || id == 0) { return Create(); }
            else { return Update(id); };
        }

        public async Task<IActionResult> Create()
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage? response = await httpClient.PostAsJsonAsync($"{_API}/api/Bands", band);
            string? returnValue = await response.Content.ReadAsStringAsync();
            return RedirectToPage("List");
        }


        public async Task<IActionResult> Update(int? id)
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage? response = await httpClient.PutAsJsonAsync($"{_API}/api/Bands/{id}", band);
            string? content = await response.Content.ReadAsStringAsync();
            return RedirectToPage("List");
        }
    }
}
