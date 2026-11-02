using System;
using System.Windows;
using BinarySearchTreeLab9.ViewModels;

namespace BinarySearchTreeLab9
{
    public partial class MainWindow : Window
    {
        private readonly MainViewModel viewModel;
        public MainWindow()
        {
            InitializeComponent();

            viewModel = new MainViewModel();
            DataContext = viewModel;
            viewModel.TreeChanged += UpdateTreeDisplay;
            UpdateTreeDisplay(this, EventArgs.Empty);
        }
        private void UpdateTreeDisplay(object? sender, EventArgs e)
        {
            TreeDisplay.Root = viewModel.Root;
            TreeDisplay.RefreshTree();
        }
    }
}