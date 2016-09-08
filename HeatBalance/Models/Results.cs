using System.ComponentModel;
namespace HeatBalance.Models
{
	public class Results : INotifyPropertyChanged
	{
		private double zavalka;
		private double mst;
		private double mLomaKorr;

		public double Zavalka
		{
			get { return zavalka; }
			set
			{
				zavalka = value;
				if (PropertyChanged != null)
				{
					PropertyChanged(this, new PropertyChangedEventArgs("zavalka"));
				}
			}
		}

		public double Mst
		{
			get { return mst; }
			set
			{
				mst = value;
				if (PropertyChanged != null)
				{
					PropertyChanged(this, new PropertyChangedEventArgs("mst"));
				}
			}
		}

		public double MLomaKorr
		{
			get { return mLomaKorr; }
			set
			{
				mLomaKorr = value;
				if (PropertyChanged != null)
				{
					PropertyChanged(this, new PropertyChangedEventArgs("mLomaKorr"));
				}
			}
		}


		#region GA
		private double fitness;
		private double finalPrice;

		public double Fitness
		{
			get { return fitness; }
			set
			{
				fitness = value;
				if (PropertyChanged != null)
				{
					PropertyChanged(this, new PropertyChangedEventArgs("fitness"));
				}
			}
		}

		public double FinalPrice
		{
			get { return finalPrice; }
			set
			{
				finalPrice = value;
				if (PropertyChanged != null)
				{
					PropertyChanged(this, new PropertyChangedEventArgs("finalPrice"));
				}
			}
		}

		public double Crossingover { get; set; }
		public double CrossChanceStart { get; set; }
		public double CrossChanceEnd { get; set; }

		#endregion

		public event PropertyChangedEventHandler PropertyChanged;
	}
}
