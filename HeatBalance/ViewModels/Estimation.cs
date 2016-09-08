namespace HeatBalance.ViewModels
{
	using System;
	using System.Collections.Generic;
	using HeatBalance.Models;

    public class Estimation
    {
        #region constants

        public const double MIzvOhl = 0;
        public const double MIzvZav = 7.8;
        public const double Mmgo = 0.52;
        public const double Mpsh = 0.17;
        public const double Dolomit = 0;
        public const double MizvRasch = 7.8;
        public const double Fe2Oshl = 15;
        public const double Fe2O3shl = 6;

        #endregion

        #region vars
        private Materials mat = new Materials();
        private Shihta shihta;
        private Shlak shlak = new Shlak();
        private Gases gas = new Gases();
        private PrihodTepla prihod = new PrihodTepla();
        private RashodTepla rashod = new RashodTepla();
        Results res = new Results();
        private double MLom;
        private double MIzvSum;
        private double Fvap;
        private double CaOsum;
        private double SiO2sum;
        private double MorSt;
        private double MorShl;
        private double MshlRasch;
        private double lgKmn;
        private double Kmn;
        private double MnPov;
        private double Ppov;
        private double Spov;
        private double MSiO2;
        private double MAl2O3;
        private double MMnO;
        private double MCaO;
        private double MMgO;
        private double MP2O5;
        private double MS;
        private double MShl;
        private double Mst;
        private double MKislRash;
        private double MKislPrih;
        private double MDutya;
        private double VDutya;
        private double MCO2;
        private double VCO2;
        private double MCO;
        private double VCO;
        private double MN2;
        private double VN2;
        private double MO2;
        private double VO2;
        private double PoluchStaliBezRaskis;
        private double MLomaKorr;
        private double Zavalka;
        private double FinalSteel;

        public Dictionary<string, double> Povalka = new Dictionary<string, double>()
		{
			{"C",0.18},
			{"t",1645}
		};
        private Inputs inp;
        #endregion

        public Estimation(Inputs inp)
        {
            this.inp = inp;
            shihta = new Shihta(inp);
        }

        public Results Estimate()
        {
			//MLom = inp.MBoyChuguna + inp.MChushkovogoChuguna + inp.MLoma10PercentKre + inp.MLomaSt;
            MIzvSum = MIzvOhl + MIzvZav;
            Fvap = 0.9 * (mat.Izvest["CaO"] / 100 - (mat.Izvest["SiO2"] / 100 * Pollution.B));
            CaOsum = 0.01 * (Mpsh * mat.PlShpat["CaO"] + inp.MChuguna * Pollution.KolMiksShlaka / 100 * 35 + mat.JSB["CaO"] * inp.MJSB + mat.Shlakometal["CaO"] * inp.MShlakometal + inp.MScrapChugun * mat.ScrapChugun["CaO"] + inp.MScrapStal * mat.ScrapStal["CaO"]);
            SiO2sum = shihta.Si * 2.14 + 0.01 * (Mpsh * mat.PlShpat["SiO2"] + inp.MChuguna * Pollution.KolMiksShlaka * 45 / 400 + MLom * 70 * Pollution.ZagrayzLoma / 100 + inp.MJSB * mat.JSB["SiO2"] + inp.MShlakometal * mat.Shlakometal["SiO2"] + inp.MScrapChugun * mat.ScrapChugun["SiO2"] + inp.MScrapStal * mat.ScrapStal["SiO2"]);
            MorSt = (inp.MChuguna + inp.MBoyChuguna + inp.MChushkovogoChuguna) * ((100 - shihta.Chugun["C"] - shihta.Chugun["Si"] - shihta.Chugun["Mn"] - shihta.Chugun["S"] - shihta.Chugun["P"]) / 100) + inp.MLoma10PercentKre * ((100 - shihta.Lom10percentKre["C"] - shihta.Lom10percentKre["Si"] - shihta.Lom10percentKre["Mn"] - shihta.Lom10percentKre["S"] - shihta.Lom10percentKre["P"]) / 100) + inp.MLomaSt * 1 + inp.MScrapChugun * (mat.ScrapChugun["FeObsh"] / 100) + inp.MScrapStal * (mat.ScrapStal["FeObsh"] / 100) + inp.MShlakometal * (mat.Shlakometal["FeObsh"] / 100) + inp.MJSB * (mat.JSB["FeObsh"] / 100) - (shihta.C - Povalka["C"] + shihta.Si + shihta.Mn * 0.76 + 0.9 * shihta.P + Pollution.DeltaFPoter);
            MorShl = (100 / (100 - (Fe2Oshl + Fe2O3shl))) * (shihta.Si * 2.14 + 0.8 * shihta.Mn * 1.29 + 0.9 * shihta.P * 2.29 + 0.01 * (MizvRasch * (100 - 7) + inp.MScrapChugun * (mat.Okatishi["SiO2"] + mat.Okatishi["CaO"] + mat.Okatishi["FeO"] + mat.Okatishi["Fe2O3"] + mat.Okatishi["MgO"]) + inp.MJSB * (mat.JSB["SiO2"] + mat.JSB["CaO"] + mat.JSB["FeO"] + mat.JSB["MgO"]) + Mmgo * mat.BriketMgO["MgO"] + Mpsh * 100 + Pollution.KolMiksShlaka * inp.MChuguna * (100 - 2) / 100 + Pollution.ZagrayzLoma * MLom));
            MshlRasch = (100 / (100 - (Fe2Oshl + Fe2O3shl))) * (shihta.Si * 2.14 + 0.8 * shihta.Mn * 1.29 + 0.9 * shihta.P * 2.29 + 0.01 * (MIzvSum * (100 - 7) + inp.MScrapChugun * (mat.Okatishi["SiO2"] + mat.Okatishi["CaO"] + mat.Okatishi["FeO"] + mat.Okatishi["Fe2O3"] + mat.Okatishi["MgO"]) + inp.MJSB * (mat.JSB["SiO2"] + mat.JSB["CaO"] + mat.JSB["FeO"] + mat.JSB["MgO"] + mat.JSB["Fe2O3"]) + Mmgo * mat.BriketMgO["MgO"] + Mpsh * 100 + Pollution.KolMiksShlaka * inp.MChuguna * (100 - 2) / 100 + Pollution.ZagrayzLoma * MLom));
            lgKmn = (-6234 / (Povalka["t"] + 273)) + 3.026;
            Kmn = Math.Pow(10, lgKmn);
            MnPov = (shihta.Mn + (Pollution.KolMiksShlaka / 100 * inp.MChuguna * 0.05 * 0.77)) / (0.01 * (MorSt + MorShl * (Fe2Oshl + Fe2O3shl) / Kmn)) + 0.03;
            Ppov = (100 * shihta.P) / (MorSt + Pollution.itaP * MorShl * 62 / 142);
            Spov = (shihta.S + (MizvRasch * 0.075 / 100) + Pollution.KolMiksShlaka / 100 * inp.MChuguna * 1.25 / 100) / (0.01 * (MorSt + Pollution.itaS * MorShl));
            MSiO2 = shihta.Si * 2.14 + 0.01 * (MizvRasch * mat.Izvest["SiO2"] + Mpsh * mat.PlShpat["SiO2"] + Pollution.KolMiksShlaka / 100 * inp.MChuguna * 45 + Pollution.ZagrayzLoma / 100 * MLom * 70 + inp.MScrapChugun * mat.ScrapChugun["SiO2"] + inp.MJSB * mat.JSB["SiO2"] + inp.MScrapStal * mat.ScrapStal["SiO2"] + inp.MShlakometal * mat.Shlakometal["SiO2"]);
            MAl2O3 = 0.01 * (MizvRasch * 1.5 + Pollution.KolMiksShlaka / 100 * inp.MChuguna * 6 + Pollution.ZagrayzLoma / 100 * MLom * 30);
            MMnO = (shihta.Mn - MnPov * MorSt / 100) * 1.29 + 0.01 * Pollution.KolMiksShlaka / 100 * inp.MChuguna * 5;
            MCaO = 0.01 * (MizvRasch * mat.Izvest["CaO"] + Mpsh * mat.PlShpat["CaO"] + Pollution.KolMiksShlaka / 100 * inp.MChuguna * 35 + inp.MScrapChugun * mat.ScrapChugun["CaO"] + inp.MJSB * mat.JSB["CaO"] + inp.MScrapStal * mat.ScrapStal["CaO"] + inp.MShlakometal * mat.Shlakometal["CaO"]);
            MMgO = 0.01 * (MizvRasch * mat.Izvest["MgO"] + Pollution.KolMiksShlaka / 100 * inp.MChuguna * 3 + Mmgo * mat.BriketMgO["MgO"] + mat.Okatishi["MgO"] * inp.MScrapChugun + inp.MJSB * mat.JSB["MgO"]);
            MP2O5 = (shihta.S - Ppov * MorSt / 100) * 2.29;
            MS = shihta.S - Spov * MorSt / 100 + 0.01 * (MizvRasch * 0.1 + Pollution.KolMiksShlaka / 100 * inp.MChuguna * 1.5);
            MShl = (MSiO2 + MAl2O3 + MMnO + MCaO + MMgO + MP2O5 + MS) / (1 - 0.01 * (Fe2Oshl + Fe2O3shl)); ;
            shlak.SiO2 = MSiO2 / (0.01 * MShl);
            shlak.Al2O3 = MAl2O3 / (0.01 * MShl);
            shlak.MnO = MMnO / (0.01 * MShl);
            shlak.CaO = MCaO / (0.01 * MShl);
            shlak.MgO = MMgO / (0.01 * MShl);
            shlak.P2O5 = MP2O5 / (0.01 * MShl);
            shlak.S = MS / (0.01 * MShl);
            shlak.FeO = Fe2Oshl;
            shlak.Fe2O3 = Fe2O3shl;
            Mst = ((inp.MChuguna + inp.MBoyChuguna + inp.MChushkovogoChuguna) * ((100 - shihta.Chugun["C"] - shihta.Chugun["Si"] - shihta.Chugun["Mn"] - shihta.Chugun["S"] - shihta.Chugun["P"]) / 100) + (inp.MLoma10PercentKre * ((100 - shihta.Lom10percentKre["C"] - shihta.Lom10percentKre["Si"] - shihta.Lom10percentKre["Mn"] - shihta.Lom10percentKre["S"] - shihta.Lom10percentKre["P"]) / 100)) + (inp.MLomaSt * 1) + (inp.MScrapChugun * (mat.ScrapChugun["FeObsh"] / 100) + inp.MScrapStal * (mat.ScrapStal["FeObsh"] / 100) + inp.MShlakometal * (mat.Shlakometal["FeObsh"] / 100) + inp.MJSB * (mat.JSB["FeObsh"] / 100))) - ((0.01 * MorSt * (Povalka["C"] + MnPov + Ppov + Spov)) + Pollution.KolMiksShlaka / 100 * inp.MChuguna + (Pollution.ZagrayzLoma + Pollution.OkalinaLoma) / 100 * MLom + MShl * 0.01 * (shlak.FeO * 0.78 + shlak.Fe2O3 * 0.7) + Pollution.DeltaFPoter) + 0.01 * 0.78 * (Pollution.KolMiksShlaka / 100 * inp.MLomaSt * 2 + Pollution.OkalinaLoma / 100 * MLom * 30 + mat.Shlakometal["FeO"] / 100 * inp.MShlakometal + mat.JSB["FeO"] / 100 * inp.MJSB) + 0.01 * 0.7 * (Pollution.OkalinaLoma / 100 * MLom * 70 + mat.Shlakometal["Fe2O3"] / 100 * inp.MShlakometal + mat.JSB["Fe2O3"] / 100 * inp.MJSB);
            MKislRash = (shihta.C - 0.01 * Mst * Povalka["C"]) * ((1 - Pollution.DozhiganieCOdoCO2) * 1.33 + Pollution.DozhiganieCOdoCO2 * 2.67) + shihta.Si * 1.14 + (shihta.Mn - 0.01 * Mst * MnPov) * 0.29 + (shihta.P - 0.01 * Mst * Ppov) * 1.29 + 0.01 * MShl * (shlak.Fe2O3 * 0.3 + shlak.FeO * 0.22) + Pollution.RashFeIspzr * 0.43;
            MKislPrih = 0.01 * inp.MChuguna * Pollution.KolMiksShlaka / 100 * 2 * 0.22 + 0.01 * MLom * Pollution.OkalinaLoma / 100 * (70 * 0.3 + 30 * 0.22) + 0.01 * MizvRasch * 7 * (1 - Pollution.DozhiganieCOdoCO2) * 0.36;
            MDutya = (MKislRash - MKislPrih) * 10000 / (Pollution.k * Pollution.n);
			//VDutya = MDutya * 22.4 / 32;
            MCO2 = 3.67 * (shihta.C - 0.01 * Mst * Povalka["C"]) * Pollution.DozhiganieCOdoCO2 + 0.01 * MizvRasch * 7 * Pollution.DozhiganieCOdoCO2;
            VCO2 = MCO2 * 0.51;
            MCO = 2.33 * (shihta.C - 0.01 * Mst * Povalka["C"]) * (1 - Pollution.DozhiganieCOdoCO2) + 0.01 * MizvRasch * 7 * (1 - Pollution.DozhiganieCOdoCO2);
            VCO = MCO * 0.8;
            MN2 = MDutya * (100 - Pollution.n) / 100;
            VN2 = MN2 * 0.8;
            MO2 = (MKislRash - MKislPrih) * (100 - Pollution.k) / 100;
            VO2 = MO2 * 0.7;
			//PoluchStaliBezRaskis = Mst * 59 / 100;

            gas.CO = VCO;
            gas.CO2 = VCO2;
            gas.N2 = VN2;
            gas.O2 = VO2;
			//gas.Sum = gas.CO + gas.CO2 + gas.N2 + gas.O2;

            prihod.Chugun = inp.MChuguna * (61.9 + 0.88 * Shihta.Temp);
            prihod.MiksShl = (1.53 * Shihta.Temp - 710) * Pollution.KolMiksShlaka * inp.MChuguna / 100;
            prihod.Primesi = (11380 * (1 - Pollution.DozhiganieCOdoCO2) + 35300 * Pollution.DozhiganieCOdoCO2) * (shihta.C - 0.01 * MorSt * Povalka["C"]) + 26930 * shihta.Si + 7035 * (shihta.Mn - 0.01 * lgKmn * MnPov) + 19755 * (shihta.P - 0.01 * lgKmn * Ppov);
            prihod.OkFe = 0.01 * MShl * (3600 * Fe2Oshl + 5110 * Fe2O3shl + 5110 * Pollution.RashFeIspzr);
            prihod.ShlObr = 0.01 * MShl * (2300 * 17 + 0.8 * 4.866);

            prihod.Sum = prihod.Chugun + prihod.MiksShl + prihod.Primesi + prihod.OkFe + prihod.ShlObr;

            rashod.Stal = Mst * (54.8 + 0.84 * Povalka["t"]);
            rashod.Shl = MShl * (2.09 * Povalka["t"] - 1380);
            rashod.Gas = (Shihta.Temp + Povalka["t"]) / 2 * (1.466 * gas.CO + 2.353 * gas.CO2 + 1.44 * gas.N2 + 1.528 * gas.O2);
            rashod.DisOk = 3600 * 0.01 * (Pollution.KolMiksShlaka / 100 * inp.MChuguna * 2 + Pollution.OkalinaLoma / 100 * MLom * 30) + 5110 * 0.01 * (Pollution.OkalinaLoma / 100 * MLom * 70);
            rashod.DisIzv = 4040 * 0.01 * MizvRasch * 7;
            rashod.Pil = Pollution.RashFeIspzr * (23.05 + 0.69 * (Povalka["t"] + Shihta.Temp) / 2);
            rashod.Vibr = (Pollution.DeltaFPoter - Pollution.RashFeIspzr) * (54.9 + 0.838 * Povalka["t"]);
            rashod.PotKonv = 0.01 * prihod.Sum * Pollution.PoteriTeplaKonvPercent;

            rashod.Sum = rashod.Stal + rashod.Shl + rashod.Gas + rashod.DisOk + rashod.DisIzv + rashod.Pil + rashod.Vibr + rashod.PotKonv;

            MLomaKorr = (prihod.Sum - rashod.Sum) / (0.84 * Povalka["t"] + 54.9);

            Zavalka = (inp.MBoyChuguna + inp.MChuguna + inp.MChushkovogoChuguna + inp.MJSB + inp.MLoma10PercentKre + inp.MLomaSt + inp.MScrapChugun + inp.MScrapStal + inp.MShlakometal) * 59 / 100;

            FinalSteel = Mst * 59 / 100 + (Mst * 59 / 100) * 7.4 / 1000 * 0.7;

            res.Mst = FinalSteel;
            res.Zavalka = Zavalka;
            res.MLomaKorr = MLomaKorr;

            return res;
        }


    }
}
