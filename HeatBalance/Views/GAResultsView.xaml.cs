namespace HeatBalance.Views
{
    using System.Threading;
    using System.Windows;

	/// <summary>
	/// Interaction logic for GAResultsView.xaml
	/// </summary>
	public partial class GAResultsView : Window
	{
		public GAResultsView()
		{
			InitializeComponent();
		}

        private void Window_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Thread.CurrentThread.Abort();
            Application.Current.Shutdown();
        }
	}
}
