using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Practice.MVVMModels;
using DatabaseConnector;
using System.Linq;
using Models;
using Practice.Commands;
using System.Collections;
using System.Windows.Controls;

namespace Practice.ViewModel
{
    class ApplicationCountryViewModel : MVVMModel
    {
        private CountryModel selectedCountry;
        private string selectedCountryName { get; set; }
        public ObservableCollection<CountryModel> Countries { get; set; } = new ObservableCollection<CountryModel>();

        private RelayCommand addCountryCommand;
        public RelayCommand AddCountryCommand
        {
            get
            {
                return addCountryCommand ??
                    (addCountryCommand = new RelayCommand(obj =>
                    {
                        Country country = new Country { CountryName = "Введите имя страны " + (Countries.Count + 1) };
                        CountryModel cm = new CountryModel(country);
                        CountryService.AddCountry(country);
                        Countries.Insert(0, cm);
                        selectedCountry = cm;
                    }, (obj) =>
                    {
                        if (selectedCountry != null)
                        {
                            IEnumerable<CountryModel> a = (from c in Countries where c.CountryName.Length == 0 select c);
                            if (a.Count() > 0)
                            {
                                Console.WriteLine("Заполните  пропуски");
                                return false;
                            }
                            else
                                return true;
                        }
                        return true;
                    }));
            }
        }
        
        private RelayCommand removeCountryCommand;
        public RelayCommand RemoveCountryCommand
        {
            get
            {
                Console.WriteLine("Попытка удаления");
                return removeCountryCommand ??
                  (removeCountryCommand = new RelayCommand(obj =>
                  {
                      CountryModel cm = obj as CountryModel;
                      if (cm != null)
                      {
                          Countries.Remove(cm);
                      }
                  },
                  (obj) => Countries.Count > 0));
            }
        }
        private RelayCommand findCommand;
        public RelayCommand FindCountryCommand
        {
            get
            {
                Console.WriteLine("Попытаемся найти");
                return findCommand ??
                    (findCommand = new RelayCommand(obj =>
                    {
                        if (obj != null && obj.ToString().Length > 0)
                        {
                            Console.WriteLine(obj.GetType() + " " + obj);
                            Countries = new ObservableCollection<CountryModel>(Countries.OrderByDescending(i => i.CountryName.Contains(obj.ToString())));
                            OnPropertyChanged("Countries");
                        }
                        else if (obj != null)
                        {
                            Countries = new ObservableCollection<CountryModel>(Countries.OrderBy(i => i.CountryName));
                            OnPropertyChanged("Countries");
                        }
                    }
                    ));
            }
        }
        public CountryModel SelectedCountry
        {
            get
            {
                return selectedCountry;
            }
            set
            {
                selectedCountry = value;
                selectedCountryName = selectedCountry.CountryName;
                Console.WriteLine(selectedCountryName);
                OnPropertyChanged("SelectedCountry");
            }
        }
        public ApplicationCountryViewModel()
        {
            List<Country> collection = CountryService.GetCountries();
            foreach (Country c in collection)
            {
                Countries.Add(new CountryModel(c));
            }

            Countries.CollectionChanged += (s, e) =>
            {
                if (e.Action.ToString().Equals("Add"))
                {
                    CountryModel countryModel = null;
                    foreach (CountryModel cm in e.NewItems)
                        countryModel = cm;

                    CountryService.AddCountry(countryModel.Country);
                    Console.WriteLine("Добавили элемент " + countryModel?.CountryName);
                }
                else if (e.Action.ToString().Equals("Remove"))
                {
                    CountryModel countryModel = null;
                    foreach (CountryModel cm in e.OldItems)
                        countryModel = cm;

                    CountryService.RemoveCountry(countryModel.Country);
                    Console.WriteLine("Удаление элемента " + countryModel?.CountryName);
                }
                //else if (e.Action.ToString().Equals("Replace"))
                //{
                //    CountryModel countryModel = null;
                //    foreach (CountryModel cm in e.OldItems)
                //        countryModel = cm;

                //    Console.WriteLine("Изменения элемента " + countryModel?.CountryName);
                //}
                OnPropertyChanged("Countries");
            };
        }
    }
}
