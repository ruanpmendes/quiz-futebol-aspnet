using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuizFutebol.Models;
using System.Text.Json;

namespace QuizFutebol.Pages
{
    public class IndexModel : PageModel
    {
        public Quiz QuizAtual { get; set; } = new Quiz();

        [BindProperty]
        public RespostaModel Resposta { get; set; } = new RespostaModel();

        public void OnGet()
        {
            // Verificar se já existe um quiz na sessão
            var quizJson = HttpContext.Session.GetString("Quiz");

            if (string.IsNullOrEmpty(quizJson))
            {
                // Criar novo quiz
                QuizAtual = CriarQuiz();
                HttpContext.Session.SetString("Quiz", JsonSerializer.Serialize(QuizAtual));
            }
            else
            {
                // Recuperar quiz existente
                QuizAtual = JsonSerializer.Deserialize<Quiz>(quizJson) ?? new Quiz();
            }
        }

        public IActionResult OnPost()
        {
            var quizJson = HttpContext.Session.GetString("Quiz");
            if (string.IsNullOrEmpty(quizJson))
            {
                return RedirectToPage();
            }

            QuizAtual = JsonSerializer.Deserialize<Quiz>(quizJson) ?? new Quiz();

            // Verificar resposta
            if (QuizAtual.PerguntaAtual < QuizAtual.Perguntas.Count)
            {
                var perguntaAtual = QuizAtual.Perguntas[QuizAtual.PerguntaAtual];
                if (perguntaAtual.RespostaCorreta == Resposta.RespostaSelecionada)
                {
                    QuizAtual.Pontuacao++;
                }

                QuizAtual.PerguntaAtual++;

                // Verificar se terminou
                if (QuizAtual.PerguntaAtual >= QuizAtual.Perguntas.Count)
                {
                    QuizAtual.Finalizado = true;
                    HttpContext.Session.SetString("Quiz", JsonSerializer.Serialize(QuizAtual));
                    return RedirectToPage("Resultado");
                }

                HttpContext.Session.SetString("Quiz", JsonSerializer.Serialize(QuizAtual));
            }

            return RedirectToPage();
        }

        public IActionResult OnPostReiniciar()
        {
            HttpContext.Session.Remove("Quiz");
            return RedirectToPage();
        }

        private Quiz CriarQuiz()
        {
            var perguntas = new List<Pergunta>
            {
                new Pergunta
                {
                    Id = 1,
                    Texto = "Qual país ganhou a Copa do Mundo de 2018?",
                    OpcaoA = "Brasil",
                    OpcaoB = "França",
                    OpcaoC = "Alemanha",
                    OpcaoD = "Argentina",
                    RespostaCorreta = "B"
                },
                new Pergunta
                {
                    Id = 2,
                    Texto = "Quantos jogadores tem cada time em campo durante uma partida?",
                    OpcaoA = "10",
                    OpcaoB = "11",
                    OpcaoC = "12",
                    OpcaoD = "9",
                    RespostaCorreta = "B"
                },
                new Pergunta
                {
                    Id = 3,
                    Texto = "Qual é o maior estádio do Brasil?",
                    OpcaoA = "Morumbi",
                    OpcaoB = "Mineirão",
                    OpcaoC = "Maracanã",
                    OpcaoD = "Arena Corinthians",
                    RespostaCorreta = "C"
                },
                new Pergunta
                {
                    Id = 4,
                    Texto = "Quantas Copas do Mundo o Brasil já ganhou?",
                    OpcaoA = "4",
                    OpcaoB = "5",
                    OpcaoC = "6",
                    OpcaoD = "3",
                    RespostaCorreta = "B"
                },
                new Pergunta
                {
                    Id = 5,
                    Texto = "Qual jogador é conhecido como 'Rei do Futebol'?",
                    OpcaoA = "Ronaldinho",
                    OpcaoB = "Ronaldo",
                    OpcaoC = "Pelé",
                    OpcaoD = "Kaká",
                    RespostaCorreta = "C"
                }
            };

            return new Quiz { Perguntas = perguntas };
        }
    }
}