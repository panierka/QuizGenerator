using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using QuizGenerator.Model.Ciphers;
using QuizGenerator.Model.Game;

namespace QuizGenerator.Model.IO
{
    internal class FileSaver
    {
        private readonly IEncryptor encryptor;

        public FileSaver(IEncryptor encryptor = null)
        {
            this.encryptor = encryptor;
        }

        public void SaveToFile(Quiz quiz, string path)
        {
            string text = QuizToJson(quiz);

            if (encryptor is { })
            {
                text = encryptor.Encrypt(text);
            }
            
            File.WriteAllText(path, text);
        }

        private static string QuizToJson(Quiz quiz)
        {
            quiz = quiz.TrimmedCopy();
            return JsonSerializer.Serialize(quiz);
        }
    }
}
