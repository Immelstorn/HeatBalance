namespace HeatBalance.Models
{
	using System;
	using System.ComponentModel;

	public class GAmodel : INotifyPropertyChanged
	{
		private int generation;
		private double bestFitness;
		private double averageFitness;
		private TimeSpan time;

		public int Generation
		{
			get { return generation; }
			set
			{
				generation = value;
				if (PropertyChanged != null)
				{
					PropertyChanged(this, new PropertyChangedEventArgs("Generation"));
				}
			}
		}

		public double BestFitness
		{
			get { return bestFitness; }
			set
			{
				bestFitness = value;
				if (PropertyChanged != null)
				{
					PropertyChanged(this, new PropertyChangedEventArgs("BestFitness"));
				}
			}
		}

		public double AverageFitness
		{
			get { return averageFitness; }
			set
			{
				averageFitness = value;
				if (PropertyChanged != null)
				{
					PropertyChanged(this, new PropertyChangedEventArgs("AverageFitness"));
				}
			}
		}

		public TimeSpan Time
		{
			get { return time; }
			set
			{
				time = value;
				if (PropertyChanged != null)
				{
					PropertyChanged(this, new PropertyChangedEventArgs("Time"));
				}
			}
		}


		public event PropertyChangedEventHandler PropertyChanged;
	}
}
