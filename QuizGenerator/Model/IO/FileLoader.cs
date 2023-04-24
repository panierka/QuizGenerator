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
    internal class FileLoader
    {
        private readonly IDecryptor decryptor;

        public FileLoader(IDecryptor decryptor = null)
        {
            this.decryptor = decryptor;
        }

        public Quiz LoadFromFile(string path)
        {
            string content = File.ReadAllText(path);

            if(decryptor is { })
            {
                content = decryptor.Decrypt(content);
            }

            Quiz quiz = JsonToQuiz(content);
            InitializeEvents(quiz);

            return quiz;           
        }

        private static void InitializeEvents(Quiz quiz)
        {
            foreach(var question in quiz.Questions)
            {
                question.BindOnChangeEventToAnswers();
            }
        }

        private static Quiz JsonToQuiz(string json)
        {
            Quiz quiz = JsonSerializer.Deserialize<Quiz>(json);
            return quiz;
        }
    }
}
