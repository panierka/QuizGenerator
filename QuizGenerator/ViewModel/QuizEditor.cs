using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using QuizGenerator.Model.Game;
using QuizGenerator.ViewModel.Base;

namespace QuizGenerator.ViewModel
{
    internal class QuizEditor : Base.ViewModel
    {
        public Quiz Quiz { get; private set; }

        private Question _currentQuestion;
        public Question CurrentQuestion
        {
            get => _currentQuestion;
            set
            {
                if (value is { } && CurrentQuestion is { } && !CurrentQuestion.IsValid)
                {
                    return;
                }

                if (CurrentQuestion is { })
                {
                    CurrentQuestion.PropertyChanged -= UpdateWarning;
                }

                _currentQuestion = value;
                OnPropertyChanged(nameof(CurrentQuestion),
                    nameof(Quiz),
                    nameof(Quiz.Questions),
                    nameof(WarningVisibility));

                if (CurrentQuestion is { })
                {
                    CurrentQuestion.PropertyChanged += UpdateWarning;
                }
            }
        }

        private RelayCommand _addQuestion;
        public RelayCommand AddQuestion => _addQuestion ??= new(
                _ => Quiz.Questions.All(x => x.IsValid),
                _ =>
                {
                    CurrentQuestion = Quiz.AddNewQuestion();
                    OnPropertyChanged(nameof(Quiz.Questions));
                }
            );

        private RelayCommand _removeQuestion;
        public RelayCommand RemoveQuestion => _removeQuestion ??= new(
                _ => Quiz.Questions.Count > 1,
                _ =>
                {
                    Quiz.Questions.Remove(CurrentQuestion);
                    CurrentQuestion = null;
                    CurrentQuestion = Quiz.Questions[0];
                }
            );

        public Visibility WarningVisibility
        {
            get
            {
                if(CurrentQuestion is null)
                {
                    return Visibility.Visible;
                }   

                return CurrentQuestion.IsValid? Visibility.Hidden: Visibility.Visible;
            }
        }

        private void UpdateWarning(object _, PropertyChangedEventArgs _1)
        {
            OnPropertyChanged(nameof(WarningVisibility));
        }
        
        public QuizEditor()
        {
            Quiz = new();
            CurrentQuestion = Quiz.AddNewQuestion();
        }

        public QuizEditor(Quiz quiz)
        {
            Quiz = quiz;

            if (Quiz.Questions.Count == 0)
            {
                _ = Quiz.AddNewQuestion();
            }

            CurrentQuestion = Quiz.Questions[0];
        }
    }
}
