namespace HeatBalance.ViewModels
{
	using HeatBalance.Models;
	using HeatBalance.Views;
	using System;
	using System.Threading;

	class Genetic
	{
		#region vars
		private Inputs[] population;
		private Inputs[] children;
		private Inputs[] wholePopulation;

		private Results[] popResults;
		private Results[] childrenResults;
		private Results[] wholePopResults;

		private int[] ParentsNumbers;
		private double sumFitness;
		private Random r = new Random();
		private bool stop = false;
		private int generationCounter = 0;
		private DateTime startTime;
		private Estimation est;
		private DataContexts dataContext;

		#endregion

		#region ctor
		public Genetic()
		{
			dataContext = DataContexts.Instance;

			population = new Inputs[dataContext.GAParams.PCount];
			popResults = new Results[dataContext.GAParams.PCount];
		}
		#endregion

		public void Optimize()
		{
			startTime = DateTime.Now;
			GenerateFirstPopulation();
			while (!stop)
			{
				dataContext.GAmodel.Generation = generationCounter;
				dataContext.GAmodel.Time = DateTime.Now - startTime;

				Breeding();
				generationCounter++;
			}

			//выведем результат
			Results();
		}

		private void Results()
		{
			dataContext.Inputs = population[0];
			dataContext.Results = popResults[0];
		}

		private void Breeding()
		{
			//посчитали сумму фитнесса
			sumFitness = 0;
			for (int i = 0; i < dataContext.GAParams.PCount; i++)
			{
				sumFitness += popResults[i].Fitness;
			}

			dataContext.GAmodel.AverageFitness = sumFitness / dataContext.GAParams.PCount;

			//определили долю фитнесса каждого 
			for (int i = 0; i < dataContext.GAParams.PCount; i++)
			{
				popResults[i].Crossingover = popResults[i].Fitness / sumFitness;
			}

			//выдали каждому диапазон вероятности для скрещивания (от 0 до 1)
			popResults[0].CrossChanceStart = 0;
			popResults[0].CrossChanceEnd = popResults[0].Crossingover;
			for (int i = 1; i < dataContext.GAParams.PCount; i++)
			{
				popResults[i].CrossChanceStart = popResults[i - 1].CrossChanceEnd;
				popResults[i].CrossChanceEnd = popResults[i].CrossChanceStart + popResults[i].Crossingover;
			}

			//отбираем родителей методом рулетки
			Roulette();

			//скрещиваем
			Crossingover();

			//мутация
			Mutation();

			//посчитали выходные параметры для детей
			childrenResults = new Results[dataContext.GAParams.ChildrenCount];
			for (int i = 0; i < dataContext.GAParams.ChildrenCount; i++)
			{
				est = new Estimation(children[i]);
				childrenResults[i] = new Results();
				childrenResults[i] = est.Estimate();
			}


			//считаем фитнесс для детей
			for (int i = 0; i < children.Length; i++)
			{
				FitnessCalc(children[i], childrenResults[i]);
			}

			//Формирование нового поколения
			wholePopulation = new Inputs[dataContext.GAParams.PCount + dataContext.GAParams.ChildrenCount];
			wholePopResults = new Results[dataContext.GAParams.PCount + dataContext.GAParams.ChildrenCount];
			NewGeneration();

			//Сортировка по фитнессу
			FitnessSort();
			dataContext.GAmodel.BestFitness = wholePopResults[0].Fitness;


			//умерщвление самых слабых
			//wholePopResults[0].Fitness = 0;
			population = new Inputs[dataContext.GAParams.PCount];
			popResults = new Results[dataContext.GAParams.PCount];
			Array.Copy(wholePopulation, population, dataContext.GAParams.PCount);
			Array.Copy(wholePopResults, popResults, dataContext.GAParams.PCount);


			int same = 0;
			for (int i = 1; i < dataContext.GAParams.PCount / 5 + 1; i++)
			{
				if (popResults[i].Fitness == popResults[i - 1].Fitness)
				{
					same++;
				}
			}

			//проверка условий остановки
			if ((generationCounter >= dataContext.GAParams.Generations) || (same >= dataContext.GAParams.PCount / 5))
			{
				stop = true;
			}
		}

		private void NewGeneration()
		{
			Array.Copy(population, wholePopulation, dataContext.GAParams.PCount);
			Array.Copy(children, 0, wholePopulation, dataContext.GAParams.PCount, dataContext.GAParams.ChildrenCount);

			Array.Copy(popResults, wholePopResults, dataContext.GAParams.PCount);
			Array.Copy(childrenResults, 0, wholePopResults, dataContext.GAParams.PCount, dataContext.GAParams.ChildrenCount);
		}

		private void Mutation()
		{
			for (int i = 0; i < dataContext.GAParams.ChildrenCount; i++)
			{
				if (r.NextDouble() <= dataContext.GAParams.MutationChance)
				{
					int point = r.Next(7);
					int counter = 0;

					//мутируем ребенку одну пропертю
					foreach (var item in children[i].GetType().GetProperties())
					{
						if ((item.Name != "IsOptimizing") && (!item.Name.Contains("Price")) && (item.Name != "MShlakometal"))
						{
							if (counter == point)
							{
								item.SetValue(children[i], (double)GetMutValue(item), null);
								break;
							}

							counter++;
						}
					}
				}
			}
		}

		private void Crossingover()
		{
			children = new Inputs[dataContext.GAParams.ChildrenCount];
			for (int i = 0; i < ParentsNumbers.Length; i += 2)
			{
				//выбрали точку скрещивания
				int point = r.Next(7);
				int counter = 0;
				children[i / 2] = new Inputs();

				//отдаем ребенку все проперти первого родителя числом меньше либо равно point, остальные от второго родителя
				foreach (var item in population[ParentsNumbers[i]].GetType().GetProperties())
				{
					if ((item.Name != "IsOptimizing") && (!item.Name.Contains("Price")) && (item.Name != "MShlakometal"))
					{
						if (counter <= point)
						{
							item.SetValue(children[i / 2], (double)item.GetValue(population[ParentsNumbers[i]], null), null);
						}
						else
						{
							item.SetValue(children[i / 2], (double)item.GetValue(population[ParentsNumbers[i + 1]], null), null);
						}
						counter++;
					}
				}
			}
		}

		private void Roulette()
		{
			ParentsNumbers = new int[dataContext.GAParams.ChildrenCount * 2];
			double temp = 0;
			for (int i = 0; i < dataContext.GAParams.ChildrenCount * 2; i++)
			{
				//кинули шарик
				temp = r.NextDouble();
				//нашли в кого он попал
				for (int j = 0; j < dataContext.GAParams.PCount; j++)
				{
					if ((temp >= popResults[j].CrossChanceStart) && (temp <= popResults[j].CrossChanceEnd))
					{
						//записали номер счастливого родителя с проверкой на повторения
						if (Array.IndexOf(ParentsNumbers, j) <= -1)
						{
							ParentsNumbers[i] = j;
							break;
						}
						else
						{
							i--;
							break;
						}
					}
				}
			}
		}

		private object GetMutValue(System.Reflection.PropertyInfo item)
		{
			switch (item.Name)
			{
				case "MChuguna": return 85 + r.NextDouble() * 8;
				case "MBoyChuguna": return r.NextDouble() * 1.5;
				case "MChushkovogoChuguna": return r.NextDouble() * 5;
				case "MLoma10PercentKre": return r.NextDouble() * 2;
				case "MLomaSt": return 10 + r.NextDouble() * 15;
				case "MScrapChugun": return r.NextDouble() * 5;
				case "MScrapStal": return r.NextDouble() * 5;
				case "MJSB": return r.NextDouble() * 1.5;

				default: return null;
			}
		}

		//сортировка по значению фитнесса
		private void FitnessSort()
		{
			Inputs tempI = new Inputs();
			Results tempR = new Results();

			for (int i = 0; i < dataContext.GAParams.PCount + dataContext.GAParams.ChildrenCount - 1; i++)
			{
				for (int j = i + 1; j < dataContext.GAParams.PCount + dataContext.GAParams.ChildrenCount; j++)
				{
					if (wholePopResults[i].Fitness < wholePopResults[j].Fitness)
					{
						tempI = wholePopulation[i];
						tempR = wholePopResults[i];
						wholePopulation[i] = wholePopulation[j];
						wholePopResults[i] = wholePopResults[j];
						wholePopulation[j] = tempI;
						wholePopResults[j] = tempR;
					}
				}
			}
		}

		//генерация первой популяции
		private void GenerateFirstPopulation()
		{
			for (int i = 0; i < dataContext.GAParams.PCount; i++)
			{
				//забили входные параметры
				population[i] = Randomize();

				//посчитали выходные параметры
				est = new Estimation(population[i]);
				popResults[i] = new Results();
				popResults[i] = est.Estimate();

				//посчитали фитнесс
				FitnessCalc(population[i], popResults[i]);
			}
		}

		//рассчет фитнеса и общей цены
		private void FitnessCalc(Inputs inputs, Results results)
		{
			results.FinalPrice =
				inputs.MChuguna * 59 * dataContext.Inputs.MChugunaPrice / 100 +
				inputs.MBoyChuguna * 59 * dataContext.Inputs.MBoyChugunaPrice / 100 +
				inputs.MChushkovogoChuguna * 59 * dataContext.Inputs.MChushkovogoChugunaPrice / 100 +
				inputs.MLoma10PercentKre * 59 * dataContext.Inputs.MLoma10PercentKrePrice / 100 +
				inputs.MLomaSt * 59 * dataContext.Inputs.MLomaStPrice / 100 +
				inputs.MScrapChugun * 59 * dataContext.Inputs.MScrapChugunPrice / 100 +
				inputs.MScrapStal * 59 * dataContext.Inputs.MScrapStalPrice / 100 +
				inputs.MJSB * 59 * dataContext.Inputs.MJSBPrice / 100;

			//общий фитнесс
			results.Fitness = (300000 - results.FinalPrice) / 76863;

			//ограничение по завалке
			if (results.Zavalka > 67.2)
			{
				results.Fitness -= ((results.Zavalka - 67.2) / 14.5) * results.Fitness;
			}
			else if (results.Zavalka < 66.8)
			{
				results.Fitness -= ((66.8 - results.Zavalka) / 10.5) * results.Fitness;
			}

			//ограничение по плавке
			if (results.Mst > 59.2)
			{
				results.Fitness -= ((results.Mst - 59.2) / 12) * results.Fitness;
			}
			else if (results.Mst < 58.8)
			{
				results.Fitness -= ((58.8 - results.Mst) / 10.5) * results.Fitness;
			}

			//ограничение по массе лома
			if (results.MLomaKorr != 0)
			{
				results.Fitness -= results.Fitness * Math.Abs(results.MLomaKorr) / 19;
			}

			if (results.Fitness < 0)
			{
				Console.WriteLine();
			}
		}

		//рандомизация начальных параметров
		private Inputs Randomize()
		{
			Inputs tempInputs = new Inputs();

			tempInputs.MChuguna = 85 + r.NextDouble() * 8;
			tempInputs.MBoyChuguna = r.NextDouble() * 1.5;
			tempInputs.MChushkovogoChuguna = r.NextDouble() * 5;
			tempInputs.MLoma10PercentKre = r.NextDouble() * 2;
			tempInputs.MLomaSt = 10 + r.NextDouble() * 15;
			tempInputs.MScrapChugun = r.NextDouble() * 5;
			tempInputs.MScrapStal = r.NextDouble() * 5;
			tempInputs.MJSB = r.NextDouble() * 1.5;

			return tempInputs;
		}
	}
}
