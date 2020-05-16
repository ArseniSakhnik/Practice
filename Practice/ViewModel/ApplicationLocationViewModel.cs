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
    public class ApplicationLocationViewModel : MVVMModel
    {
        private LocationModel selectedLocation;
        private string SelectedLocationName { get; set; }
        public ObservableCollection<LocationModel> Locations { get; set; } = new ObservableCollection<LocationModel>();

        private RelayCommand addLocationCommand;
        public RelayCommand AddLocationCommand
        {
            get
            {
                Console.WriteLine("Пытаемся найти локацию");
                return addLocationCommand ??
                    (addLocationCommand = new RelayCommand(obj =>
                    {
                        Location location = new Location
                        {
                            LocationName = "Название локации " + (Locations.Count + 1),
                            LocationDescription = "Описание локации " + (Locations.Count + 1)
                        };
                        LocationModel lm = new LocationModel(location);
                        LocationService.AddLocation(location);
                        Locations.Insert(0, lm);
                        selectedLocation = lm;
                    },
                    (obj) =>
                    {
                        if (selectedLocation != null)
                        {
                            IEnumerable<LocationModel> a = (from l in Locations where l.LocationName.Length == 0 select l);
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

        private RelayCommand removeLocationCommand;
        public RelayCommand RemoveLocationCommand
        {
            get
            {
                Console.WriteLine("Удаляем локацию ");
                return removeLocationCommand ??
                    (removeLocationCommand = new RelayCommand(obj =>
                    {
                        LocationModel lm = obj as LocationModel;
                        if (lm != null)
                        {
                            Locations.Remove(lm);
                        }
                    },
                    (obj) => Locations.Count > 0));
            }
        }

        private RelayCommand findLocationCommand;
        public RelayCommand FindLocationCommand
        {
            get
            {
                Console.WriteLine("Пытаемся найти локацию ");
                return findLocationCommand ??
                    (findLocationCommand = new RelayCommand(obj =>
                    {
                        if (obj != null && obj.ToString().Length > 0)
                        {
                            Console.WriteLine(obj.GetType() + " " + obj);
                            Locations = new ObservableCollection<LocationModel>(Locations.OrderByDescending(i => i.LocationName.Contains(obj.ToString())));
                            OnPropertyChanged("Locations");
                        }
                        else if (obj != null)
                        {
                            Locations = new ObservableCollection<LocationModel>(Locations.OrderBy(i => i.LocationName));
                            OnPropertyChanged("Locations");
                        }
                    }));
            }
        }

        private RelayCommand locationAddCountry;
        public RelayCommand LocationAddCountry
        {
            get
            {
                return locationAddCountry ??
                    (locationAddCountry = new RelayCommand(obj =>
                {
                    if (selectedLocation != null)
                    {
                        LocationAddCountryPage locationAddCountryPage = new LocationAddCountryPage(selectedLocation);
                        locationAddCountryPage.Show();
                    }
                }, 
                (obj) => selectedLocation != null));
            }
        }
        public LocationModel SelectedLocation
        {
            get
            {
                return selectedLocation;
            }
            set
            {
                selectedLocation = value;
                SelectedLocationName = selectedLocation.LocationName;
                Console.WriteLine(SelectedLocationName);
                OnPropertyChanged("SelectedLocation");
            }
        }
        public ApplicationLocationViewModel()
        {
            List<Location> locations = LocationService.GetLocations();
            foreach (Location l in locations)
            {
                Locations.Add(new LocationModel(l));
            }

            Locations.CollectionChanged += (o, e) =>
            {
                if (e.Action.ToString().Equals("Add"))
                {
                    LocationModel locationModel = null;
                    foreach (LocationModel lm in e.NewItems)
                        locationModel = lm;

                    LocationService.AddLocation(locationModel.Location);
                    Console.WriteLine("Добавили элемент " + locationModel?.LocationName);
                }
                else if (e.Action.ToString().Equals("Remove"))
                {
                    LocationModel locationModel = null;
                    foreach (LocationModel lm in e.OldItems)
                        locationModel = lm;

                    LocationService.RemoveLocation(locationModel.Location);
                    Console.WriteLine("Удаление элемента " + locationModel?.LocationName);
                }
                OnPropertyChanged("Locations");
            };
        }

    }
}
