namespace HeatBalance.Models
{
	using HeatBalance.ViewModels;
	using System.ComponentModel;

	public class DataContexts : INotifyPropertyChanged
	{
		#region singleton
		private static DataContexts instance;

		public static DataContexts Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new DataContexts();
				}
				return instance;
			}
		}
		#endregion

		private DataContexts() { }

		private Inputs inputs;
		private Results results;

		public Inputs Inputs
		{
			get { return inputs; }
			set
			{
				inputs = value;
				if (PropertyChanged != null)
				{
					PropertyChanged(this, new PropertyChangedEventArgs("inputs"));
				}
			}
		}


		public Results Results
		{
			get { return results; }
			set
			{
				results = value;
				if (PropertyChanged != null)
				{
					PropertyChanged(this, new PropertyChangedEventArgs("results"));
				}
			}
		}

		public CommandsViewModel CommandsViewModel { get; set; }
		public GAParams GAParams { get; set; }
		public GAmodel GAmodel { get; set; }


		public event PropertyChangedEventHandler PropertyChanged;
	}
}
