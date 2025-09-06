namespace QuizFutebol.Models
{
    public class Quiz
    {
        public List<Pergunta> Perguntas { get; set; } = new List<Pergunta>();
        public int PerguntaAtual { get; set; } = 0;
        public int Pontuacao { get; set; } = 0;
        public bool Finalizado { get; set; } = false;
    }
}