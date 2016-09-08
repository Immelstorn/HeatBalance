namespace HeatBalance.Models
{
	using System.Collections.Generic;

	public class Materials
	{
		public Dictionary<string, double> JSB = new Dictionary<string, double>()
		{
			{"SiO2",7},
			{"CaO",7},
			{"FeO",30},
			{"Fe2O3",45.5},
			{"MgO",1},
			{"FeObsh",55.5}
		};

		public Dictionary<string, double> Okatishi = new Dictionary<string, double>()
		{
			{"SiO2",9.5},
			{"CaO",0.6},
			{"FeO",1.4},
			{"Fe2O3",87},
			{"MgO",0.8},
			{"FeObsh",62.5}
		};

		public Dictionary<string, double> Izvest = new Dictionary<string, double>()
		{
			{"SiO2",2},
			{"CaO",87},
			{"MgO",1}
		};

		public Dictionary<string, double> Izvestnyak = new Dictionary<string, double>()
		{
			{"SiO2",1.5},
			{"CaO",49},
			{"MgO",5}
		};

		public Dictionary<string, double> BriketMgO = new Dictionary<string, double>()
		{
			{"MgO",75}
		};

		public Dictionary<string, double> ScrapChugun = new Dictionary<string, double>()
		{
			{"SiO2",20},
			{"CaO",20},
			{"FeObsh",61}
		};

		public Dictionary<string, double> ScrapStal = new Dictionary<string, double>()
		{
			{"SiO2",15},
			{"CaO",20},
			{"FeObsh",65}
		};

		public Dictionary<string, double> Shlakometal = new Dictionary<string, double>()
		{
			{"SiO2",20},
			{"CaO",20},
			{"FeO",1},
			{"Fe2O3",85},
			{"FeObsh",60}
		};

		public Dictionary<string, double> PlShpat = new Dictionary<string, double>()
		{
			{"SiO2",7},
			{"CaO",8}
		};

		public Dictionary<string, double> Dolomit = new Dictionary<string, double>()
		{
			{"SiO2",12},
			{"CaO",35},
			{"MgO",19}
		};
	}
}
