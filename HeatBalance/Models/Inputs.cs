namespace HeatBalance.Models
{
	using System.ComponentModel;

    public class Inputs : INotifyPropertyChanged
	{
		private bool isOptimizing;

        #region values
        public double MShlakometal { get; set; }
        public double MChuguna { get; set; }
        public double MBoyChuguna { get; set; }
        public double MChushkovogoChuguna { get; set; }
        public double MLoma10PercentKre { get; set; }
        public double MLomaSt { get; set; }
        public double MScrapChugun { get; set; }
        public double MScrapStal { get; set; }
        public double MJSB { get; set; }
        #endregion

        #region prices
        public double MShlakometalPrice { get; set; }
        public double MChugunaPrice { get; set; }
        public double MBoyChugunaPrice { get; set; }
        public double MChushkovogoChugunaPrice { get; set; }
        public double MLoma10PercentKrePrice { get; set; }
        public double MLomaStPrice { get; set; }
        public double MScrapChugunPrice { get; set; }
        public double MScrapStalPrice { get; set; }
        public double MJSBPrice { get; set; }

        #endregion


        public bool IsOptimizing
        {
            get
            {
                return isOptimizing;
            }
            set
            {
                isOptimizing = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("isOptimizing"));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
