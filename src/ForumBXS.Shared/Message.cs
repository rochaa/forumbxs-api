namespace ForumBXS.Shared
{
    public static class Message
    {
        // PostCommand
        public static string PostTextIsNull = "Necessário informar o texto";
        public static string PostTextMinChar = "Minímo de 10 caracteres para o texto";
        public static string PostTextMaxChar = "Maximo de 500 caracteres para o texto";
        public static string PostUserIsNull = "Necessário informar o usuário";
        public static string PostUserMinChar = "Minímo de 3 caracteres para o nome do usuário";
        public static string PostUserMaxChar = "Maximo de 50 caracteres para o nome do usuário";

        // PostHandler
        public static string NewQuestionInvalidCommand = "Dados para criar a pergunta inválidos";
        public static string NewQuestionInsertedSucess = "Pergunta criada com sucesso.";
        public static string NewAnswerInvalidCommand = "Dados para criar a resposta inválidos";
        public static string NewAnswerInsertedSucess = "Resposta criada com sucesso.";
        public static string QuestionNotFound = "Pergunta não encontrada.";
        public static string AnswerNotFound = "Resposta não encontrada.";
        public static string LikeInvalidCommand = "Dados para criar a curtida inválidos.";
        public static string LikedSucess = "Curtiu com sucesso. rs";
    }
}