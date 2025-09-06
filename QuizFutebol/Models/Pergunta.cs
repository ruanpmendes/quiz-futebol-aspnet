namespace QuizFutebol.Models
{
    public class Pergunta
    {
        public int Id { get; set; }
        public string Texto { get; set; } = string.Empty;
        public string OpcaoA { get; set; } = string.Empty;
        public string OpcaoB { get; set; } = string.Empty;
        public string OpcaoC { get; set; } = string.Empty;
        public string OpcaoD { get; set; } = string.Empty;
        public string RespostaCorreta { get; set; } = string.Empty;
    }
}