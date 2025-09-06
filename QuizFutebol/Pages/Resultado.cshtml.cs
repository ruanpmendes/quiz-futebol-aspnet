using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuizFutebol.Models;
using System.Text.Json;

namespace QuizFutebol.Pages
{
    public class ResultadoModel : PageModel
    {
        public Quiz QuizFinalizado { get; set; } = new Quiz();

        public IActionResult OnGet()
        {
            var quizJson = HttpContext.Session.GetString("Quiz");
            if (string.IsNullOrEmpty(quizJson))
            {
                return RedirectToPage("Index");
            }

            QuizFinalizado = JsonSerializer.Deserialize<Quiz>(quizJson) ?? new Quiz();
            return Page();
        }

        public IActionResult OnPost()
        {
            HttpContext.Session.Remove("Quiz");
            return RedirectToPage("Index");
        }
    }
}