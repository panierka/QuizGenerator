using System;
using System.Collections.Generic;
using System.Text;
using QuizGenerator.Model.Game;
using QuizGenerator.Model.Ciphers;

namespace QuizGenerator.ViewModel
{
    internal class MainViewModel : Base.ViewModel
    {
        private QuizEditor _quizEditor;

        public QuizEditor QuizEditor
        {
            get => _quizEditor;
            set
            {
                _quizEditor = value;
                OnPropertyChanged(nameof(QuizEditor));
            }
        }
        public FileManager FileManager { get; set; }

        public MainViewModel()
        {
            QuizEditor = new();

            CaesarsCipher cipher = new(1);
            FileManager = new(this, cipher);
        }
    }
}
