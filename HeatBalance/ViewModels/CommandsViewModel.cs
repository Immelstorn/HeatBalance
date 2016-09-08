namespace HeatBalance.ViewModels
{
    using System.IO;
    using System.Threading;
    using HeatBalance.Models;
    using HeatBalance.Views;

    public class CommandsViewModel
    {
        private Estimation est;
        private DataContexts dataContext;
        private GAParameters gaView;
        private MainWindow mainWindow;
        private GAResultsView gar;

        public DelegateCommand MainWindow { get; private set; }
        public DelegateCommand GAParams { get; private set; }
        public DelegateCommand SaveFile { get; private set; }

        public CommandsViewModel(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            dataContext = DataContexts.Instance;

            MainWindow = new DelegateCommand(OnExecuteMainWindow, CanExecute);
            GAParams = new DelegateCommand(OnExecuteGAParams, CanExecute);
            SaveFile = new DelegateCommand(OnSaveFile, CanExecute);


        }

        private void OnExecuteMainWindow()
        {
            est = new Estimation(dataContext.Inputs);

            if (dataContext.Inputs.IsOptimizing)
            {
                gaView = new GAParameters();
                gaView.DataContext = dataContext;
                gaView.Show();
            }
            else
            {
                ResultsView resView = new ResultsView(est.Estimate());
                resView.Show();
            }
        }

        private void OnExecuteGAParams()
        {
            gar = new GAResultsView();
            gar.DataContext = dataContext;
            gar.Show();

            Genetic gen = new Genetic();

            Thread thr = new Thread(gen.Optimize);
            thr.Start();

            gaView.Close();
            mainWindow.Close();

        }

        private void OnSaveFile()
        {
            string filename = dataContext.Results.Fitness + ".txt";
            string content =
                "Входные параметры" +
                "\nМасса чугуна: " + dataContext.Inputs.MChuguna +
                "\nМасса боя чугуна: " + dataContext.Inputs.MBoyChuguna +
                "\nМасса чушкового чугуна: " + dataContext.Inputs.MChushkovogoChuguna +
                "\nМасса лома (10% Кре): " + dataContext.Inputs.MLoma10PercentKre +
                "\nМасса лома (стального): " + dataContext.Inputs.MLomaSt +
                "\nМасса скрапа (чугун): " + dataContext.Inputs.MScrapChugun +
                "\nМасса скрапа (сталь): " + dataContext.Inputs.MScrapStal +
                "\nМасса ЖСБ: " + dataContext.Inputs.MJSB +

                "\n\nРезультаты оптимизации" +

                "\nФитнесс: " + dataContext.Results.Fitness +
                "\nОбщая цена: " + dataContext.Results.FinalPrice +
                "\nЗавалка: " + dataContext.Results.Zavalka +
                "\nМасса стали: " + dataContext.Results.Mst +
                "\nМасса лома (коррекция): " + dataContext.Results.MLomaKorr +
                "\nПоколений: " + dataContext.GAmodel.Generation +
                "\nЛучший фитнесс: " + dataContext.GAmodel.BestFitness +
                "\nСредний показатель фитнесса: " + dataContext.GAmodel.AverageFitness;

            File.WriteAllText(filename, content);
            gar.bntSave.Content = "Файл сохранен в " + filename;
        }

        private bool CanExecute(object parameter)
        {
            return true;
        }
    }
}
