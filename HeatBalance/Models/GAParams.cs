namespace HeatBalance.Models
{
	using System.ComponentModel;

	public class GAParams : INotifyPropertyChanged
	{
		private int pCount = 1000;
		private int childrenCount = 100;
		private double mutationChance = 0.25;
		private int generations = 1000;

		public int Generations
		{
			get { return generations; }
			set
			{
				generations = value;
				if (PropertyChanged != null)
				{
					PropertyChanged(this, new PropertyChangedEventArgs("generations"));
				}
			}
		}

		public double MutationChance
		{
			get { return mutationChance; }
			set
			{
				mutationChance = value;
				if (PropertyChanged != null)
				{
					PropertyChanged(this, new PropertyChangedEventArgs("mutationChance"));
				}
			}
		}

		public int ChildrenCount
		{
			get { return childrenCount; }
			set
			{
				childrenCount = value;
				if (PropertyChanged != null)
				{
					PropertyChanged(this, new PropertyChangedEventArgs("childrenCount"));
				}
			}
		}

		public int PCount
		{
			get { return pCount; }
			set
			{
				pCount = value;
				if (PropertyChanged != null)
				{
					PropertyChanged(this, new PropertyChangedEventArgs("pCount"));
				}
			}
		}


		public event PropertyChangedEventHandler PropertyChanged;
	}
}
