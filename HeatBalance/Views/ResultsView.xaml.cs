namespace HeatBalance.Views
{
    using System.Windows;
    using HeatBalance.Models;

    /// <summary>
    /// Interaction logic for ResultsView.xaml
    /// </summary>
    public partial class ResultsView : Window
    {
        private Results results;

        public ResultsView(Results results)
        {
            this.results = results;
            this.DataContext = results;
            
            InitializeComponent();
        }
    }
}
