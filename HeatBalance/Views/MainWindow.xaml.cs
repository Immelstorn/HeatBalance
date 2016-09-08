namespace HeatBalance
{

    using System;
    using System.Windows;
    using HeatBalance.Models;
    using HeatBalance.ViewModels;

	public partial class MainWindow : Window
	{
		private DataContexts dataContext;

		public MainWindow()
		{
			dataContext = DataContexts.Instance;
			dataContext.Inputs = new Inputs();
			dataContext.Results = new Results();
			dataContext.CommandsViewModel = new CommandsViewModel(this);
			dataContext.GAParams = new GAParams();
			dataContext.GAmodel = new GAmodel();

			this.DataContext = dataContext;
			dataContext.Inputs.IsOptimizing = false;

			dataContext.Inputs.MBoyChuguna = 1;
			dataContext.Inputs.MChuguna = 90.11;
			dataContext.Inputs.MChushkovogoChuguna = 0;
			dataContext.Inputs.MJSB = 1.46;
			dataContext.Inputs.MLoma10PercentKre = 1.99;
			dataContext.Inputs.MLomaSt = 14.78;
			dataContext.Inputs.MScrapChugun = 1.33;
			dataContext.Inputs.MShlakometal = 0.45;
			dataContext.Inputs.MScrapStal = 2.46;

			dataContext.Inputs.MBoyChugunaPrice = 3700;
			dataContext.Inputs.MChugunaPrice = 4000;
			dataContext.Inputs.MChushkovogoChugunaPrice = 4200;
			dataContext.Inputs.MJSBPrice = 950;
			dataContext.Inputs.MLoma10PercentKrePrice = 2900;
			dataContext.Inputs.MLomaStPrice = 3200;
			dataContext.Inputs.MScrapChugunPrice = 1600;
			dataContext.Inputs.MScrapStalPrice = 1700;

		

			InitializeComponent();
		}

		private void chkOptimize_Checked(object sender, EventArgs e)
		{
			dataContext.Inputs.IsOptimizing = true;
		}

		private void chkOptimize_Unchecked(object sender, EventArgs e)
		{
			dataContext.Inputs.IsOptimizing = false;
		}
	}
}
