using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using BinarySearchTreeLab9.Models;
using BinarySearchTreeLab9.Services;

namespace BinarySearchTreeLab9.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly BinarySearchTree tree = new BinarySearchTree();
        private readonly TreeComparisonService comparisonService = new TreeComparisonService();

        private string numbersText = "";
        private string searchText = "";
        private string status = "Введіть інвентарні номери.";
        private string searchPath = "";
        private string preorderText = "";
        private string inorderText = "";
        private string postorderText = "";
        private string comparisonText = "";
        public string NumbersText
        {
            get { return numbersText; }
            set
            {
                numbersText = value;
                OnPropertyChanged(nameof(NumbersText));
            }
        }
        public string SearchText
        {
            get { return searchText; }
            set
            {
                searchText = value;
                OnPropertyChanged(nameof(SearchText));
            }
        }
        public string Status
        {
            get { return status; }
            set
            {
                status = value;
                OnPropertyChanged(nameof(Status));
            }
        }
        public string SearchPath
        {
            get { return searchPath; }
            set
            {
                searchPath = value;
                OnPropertyChanged(nameof(SearchPath));
            }
        }
        public string PreorderText
        {
            get { return preorderText; }
            set
            {
                preorderText = value;
                OnPropertyChanged(nameof(PreorderText));
            }
        }
        public string InorderText
        {
            get { return inorderText; }
            set
            {
                inorderText = value;
                OnPropertyChanged(nameof(InorderText));
            }
        }
        public string PostorderText
        {
            get { return postorderText; }
            set
            {
                postorderText = value;
                OnPropertyChanged(nameof(PostorderText));
            }
        }
        public string ComparisonText
        {
            get { return comparisonText; }
            set
            {
                comparisonText = value;
                OnPropertyChanged(nameof(ComparisonText));
            }
        }
        public int Count
        {
            get { return tree.Count; }
        }
        public int Height
        {
            get { return tree.Height; }
        }
        public string MinText
        {
            get { return tree.Min?.ToString() ?? "немає даних"; }
        }
        public string MaxText
        {
            get { return tree.Max?.ToString() ?? "немає даних"; }
        }
        public Node? Root
        {
            get { return tree.Root; }
        }
        public ICommand AddCommand { get; }
        public ICommand FindCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand ClearCommand { get; }
        public ICommand DemoCommand { get; }
        public ICommand CompareCommand { get; }
        public event PropertyChangedEventHandler? PropertyChanged;
        public event EventHandler? TreeChanged;
        public MainViewModel()
        {
            AddCommand = new RelayCommand(AddNumbers);
            FindCommand = new RelayCommand(FindNumber);
            DeleteCommand = new RelayCommand(DeleteNumber);
            ClearCommand = new RelayCommand(ClearTree);
            DemoCommand = new RelayCommand(LoadDemo);
            CompareCommand = new RelayCommand(CompareHeights);

            UpdateResults();
        }
        private void AddNumbers(object? parameter)
        {
            string[] parts = NumbersText.Split(new[] { ' ', ',', ';', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 0)
            {
                Status = "Введіть хоча б один номер.";
                return;
            }
            int added = 0;
            int duplicates = 0;
            int invalid = 0;

            foreach (string part in parts)
            {
                if (!int.TryParse(part, out int value) || value < 1 || value > 999999)
                {
                    invalid++;
                    continue;
                }

                if (tree.Add(value))
                {
                    added++;
                }
                else
                {
                    duplicates++;
                }
            }
            Status = "Додано: " + added + ", дублікати: " + duplicates + ", некоректні: " + invalid + ".";
            UpdateResults();
        }
        private void FindNumber(object? parameter)
        {
            if (!int.TryParse(SearchText, out int value) || value < 1 || value > 999999)
            {
                Status = "Введіть коректний номер від 1 до 999999.";
                SearchPath = "";
                return;
            }

            bool found = tree.Contains(value, out List<int> visited);
            SearchPath = string.Join(" → ", visited);
            Status = found ? "Номер " + value + " знайдено." : "Номер " + value + " не знайдено.";

            OnPropertyChanged(nameof(SearchPath));
        }
        private void DeleteNumber(object? parameter)
        {
            if (!int.TryParse(SearchText, out int value) ||
                value < 1 || value > 999999)
            {
                Status = "Введіть коректний номер для видалення.";
                return;
            }
            Status = tree.Delete(value) ? "Номер " + value + " видалено." : "Номер " + value + " відсутній у дереві.";
            UpdateResults();
        }
        private void ClearTree(object? parameter)
        {
            tree.Clear();
            SearchPath = "";
            Status = "Дерево очищено.";
            UpdateResults();
        }
        private void LoadDemo(object? parameter)
        {
            tree.Clear();

            foreach (int value in new[] { 50, 30, 70, 20, 40, 60, 80 })
            {
                tree.Add(value);
            }
            NumbersText = "50, 30, 70, 20, 40, 60, 80";
            Status = "Демонстраційне дерево завантажено.";
            UpdateResults();
        }
        private void CompareHeights(object? parameter)
        {
            var result = comparisonService.CompareHeights(20);

            ComparisonText = "Кількість номерів: 20" + Environment.NewLine + "Випадкова послідовність — висота: " + result.RandomHeight + Environment.NewLine + "Відсортована послідовність — висота: " + result.SortedHeight;
        }
        private void UpdateResults()
        {
            PreorderText = string.Join(", ", tree.Preorder());
            InorderText = string.Join(", ", tree.Inorder());
            PostorderText = string.Join(", ", tree.Postorder());

            OnPropertyChanged(nameof(Count));
            OnPropertyChanged(nameof(Height));
            OnPropertyChanged(nameof(MinText));
            OnPropertyChanged(nameof(MaxText));
            OnPropertyChanged(nameof(Root));

            TreeChanged?.Invoke(this, EventArgs.Empty);
        }
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}