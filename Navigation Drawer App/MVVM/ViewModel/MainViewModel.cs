using Navigation_Drawer_App.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Navigation_Drawer_App.MVVM.ViewModel
{
    class MainViewModel: ObservableObject
    {
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand ContactViewCommand { get; set; }
        public RelayCommand MessageCommand { get; set; }
        public RelayCommand SettingCommand { get; set; }
        public RelayCommand SignOutCommand { get; set; }


        public HomeViewModel HomeVM { get; set; }
        public ContactViewModel ContactVM { get; set; }
        public MessageViewModel MessageVM { get; set; }
        public SettingViewModel SettingVM { get; set; }

        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { 
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            HomeVM = new HomeViewModel();
            ContactVM = new ContactViewModel();
            MessageVM = new MessageViewModel();
            SettingVM = new SettingViewModel();

            CurrentView = HomeVM;

            HomeViewCommand = new RelayCommand(o => {
                CurrentView = HomeVM;
            });

            ContactViewCommand = new RelayCommand(o => {
                CurrentView = ContactVM;
            });

            MessageCommand = new RelayCommand(o => {
                CurrentView = MessageVM;
            });


            SettingCommand = new RelayCommand(o => {
                CurrentView = SettingVM;
            });

            SignOutCommand = new RelayCommand(o => {
                CurrentView = HomeVM;
            });

        }
    }
}
