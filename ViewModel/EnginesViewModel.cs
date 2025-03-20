using GIBDD.Model;
using GIBDD.View;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIBDD.ViewModel
{
	public class EnginesViewModel : ViewModels
	{
		public bool IsAdmin
		{
			get => App.IsAdmin;
			set
			{
				App.IsAdmin = value;
				OnPropertyChanged(nameof(IsAdmin));
			}
		}

		private ObservableCollection<EngineNumber> engines = new();
		public ObservableCollection<EngineNumber> Engines
		{
			get => engines;
			set
			{
				engines = value;
				OnPropertyChanged(nameof(Engines));
			}
		}

		public EnginesViewModel()
		{
			App.context.VINNumbers.Load();
			foreach(var engine in App.context.EngineNumbers)
			{
				Engines.Add(engine);
			}
		}

		private RelayCommand add;
		public RelayCommand Add
		{
			get
			{
				return add ?? (add = new(obj =>
				{
					EngineNumber engineNumber = new();
					AddEngine addEngineWindow = new()
					{
						DataContext = engineNumber
					};
					if(addEngineWindow.ShowDialog() == true)
					{
						App.context.Add(engineNumber);
						Engines.Add(engineNumber);
					}
				},
				obj => IsAdmin));
			}
		}
	}
}
