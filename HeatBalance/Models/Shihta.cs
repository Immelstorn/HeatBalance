namespace HeatBalance.Models
{
	using System.Collections.Generic;

	public class Shihta
	{
		public const double Temp = 1306;

		public Dictionary<string, double> Chugun = new Dictionary<string, double>()
		{
			{"C",4.3},
			{"Si",0.8},
			{"Mn",0.7},
			{"S",0.035},
			{"P",0.05}
		};

		public Dictionary<string, double> ChushkoviyChugun = new Dictionary<string, double>()
		{
			{"C",4.3},
			{"Si",0.8},
			{"Mn",0.7},
			{"S",0.035},
			{"P",0.05}
		};
		public Dictionary<string, double> TverdiyChugun = new Dictionary<string, double>()
		{
			{"C",4.3},
			{"Si",0.8},
			{"Mn",0.7},
			{"S",0.035},
			{"P",0.05}
		};
		public Dictionary<string, double> Lom10percentKre = new Dictionary<string, double>()
		{
			{"C",0.2},
			{"Si",11},
			{"Mn",0.5},
			{"S",0.2},
			{"P",0.05}
		};
		public Dictionary<string, double> Lom = new Dictionary<string, double>()
		{
			{"C",0.2},
			{"Si",0.1},
			{"Mn",0.5},
			{"S",0.04},
			{"P",0.025}
		};
        private Inputs inp;

		public double C { get; set; }
		public double Si { get; set; }
		public double Mn { get; set; }
		public double S { get; set; }
		public double P { get; set; }

        public Shihta(Inputs inp)
		{
            this.inp = inp;

			C = (inp.MChuguna * Chugun["C"] / 100) + (inp.MLomaSt * Lom["C"] / 100) + (inp.MBoyChuguna * TverdiyChugun["C"] / 100) + (inp.MChushkovogoChuguna * ChushkoviyChugun["C"] / 100) + (inp.MLoma10PercentKre * Lom10percentKre["C"] / 100);
			Si = (inp.MChuguna * Chugun["Si"] / 100) + (inp.MLomaSt * Lom["Si"] / 100) + (inp.MBoyChuguna * TverdiyChugun["Si"] / 100) + (inp.MChushkovogoChuguna * ChushkoviyChugun["Si"] / 100) + (inp.MLoma10PercentKre * Lom10percentKre["Si"] / 100);
			Mn = (inp.MChuguna * Chugun["Mn"] / 100) + (inp.MLomaSt * Lom["Mn"] / 100) + (inp.MBoyChuguna * TverdiyChugun["Mn"] / 100) + (inp.MChushkovogoChuguna * ChushkoviyChugun["Mn"] / 100) + (inp.MLoma10PercentKre * Lom10percentKre["Mn"] / 100);
			P = (inp.MChuguna * Chugun["P"] / 100) + (inp.MLomaSt * Lom["P"] / 100) + (inp.MBoyChuguna * TverdiyChugun["P"] / 100) + (inp.MChushkovogoChuguna * ChushkoviyChugun["P"] / 100) + (inp.MLoma10PercentKre * Lom10percentKre["P"] / 100);
			S = (inp.MChuguna * Chugun["S"] / 100) + (inp.MLomaSt * Lom["S"] / 100) + (inp.MBoyChuguna * TverdiyChugun["S"] / 100) + (inp.MChushkovogoChuguna * ChushkoviyChugun["S"] / 100) + (inp.MLoma10PercentKre * Lom10percentKre["S"] / 100) + inp.MScrapChugun * 0.05 / 100 + inp.MScrapStal * 0.05 / 100 + inp.MShlakometal * 1.5 / 100 + inp.MJSB * 0.2 / 100;
		}

       
	}
}
