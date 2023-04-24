using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using QuizGenerator.Model.Ciphers;
using QuizGenerator.ViewModel.Base;
using QuizGenerator.Model.Game;
using QuizGenerator.Model.IO;

namespace QuizGenerator.ViewModel
{
    internal class FileManager : Base.ViewModel
    {
        private readonly MainViewModel viewModel;
        private string savePath;

        private readonly FileSaver fileSaver;
        private readonly FileLoader fileLoader;


        private RelayCommand _newFile;
        public RelayCommand NewFile => _newFile ??= new(
                _ => true,
                _ =>
                {
                    var saveDecision = MessageBox.Show(
                        "Czy chcesz zapisać aktualny quiz?",
                        viewModel.QuizEditor.Quiz.Name,
                        MessageBoxButton.YesNoCancel);

                    if (saveDecision == MessageBoxResult.Cancel)
                    {
                        return;
                    }

                    if (saveDecision == MessageBoxResult.Yes)
                    {
                        SaveFile.Execute(null);
                    }

                    viewModel.QuizEditor = new();
                    savePath = null;
                }
            );

        private RelayCommand _openFile;
        public RelayCommand OpenFile => _openFile ??= new(
                _ => true,
                _ =>
                {
                    OpenFileDialog openDialog = new()
                    {
                        DefaultExt = ".json",
                        Filter = "Pliki Json (.json)|*.json"
                    };

                    bool? succeeded = openDialog.ShowDialog();

                    if (!succeeded.GetValueOrDefault())
                    {
                        MessageBox.Show("Błąd przy otwieraniu pliku");
                        return;
                    }

                    string path = openDialog.FileName;

                    try
                    {
                        Quiz quiz = fileLoader.LoadFromFile(path);
                        viewModel.QuizEditor = new(quiz);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Błąd przy wczytywaniu pliku. Wybierz poprawny plik.");
                        return;
                    }

                    savePath = path;
                }
            );

        private RelayCommand _saveFile;
        public RelayCommand SaveFile => _saveFile ??= new(
                _ => true,
                _ =>
                {
                    if (savePath is null)
                    {
                        SaveFileDialog saveDialog = new()
                        {
                            FileName = "Quiz",
                            DefaultExt = ".json",
                            Filter = "Pliki Json (.json)|*.json"
                        };

                        bool? succeeded = saveDialog.ShowDialog();

                        if (!succeeded.GetValueOrDefault())
                        {
                            MessageBox.Show("Niepoprawna ścieżka zapisu");
                            return;
                        }

                        savePath = saveDialog.FileName;
                    }

                    fileSaver.SaveToFile(viewModel.QuizEditor.Quiz, savePath);
                }
            );

        public FileManager(MainViewModel viewModel, ICipher cipher = null)
        {
            this.viewModel = viewModel;
            fileSaver = new(cipher);
            fileLoader = new(cipher);
        }
    }
}
