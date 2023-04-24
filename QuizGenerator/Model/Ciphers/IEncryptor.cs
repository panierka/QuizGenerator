namespace QuizGenerator.Model.Ciphers
{
    internal interface IEncryptor
    {
        public string Encrypt(string text);
    }
}
