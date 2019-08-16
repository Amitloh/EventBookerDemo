using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using EventBookerAndroid.Core.Extensions;
using EventBookerAndroid.Core.Interfaces;
using EventBookerAndroid.Core.Models;
using EventBookerAndroid.Core.ViewModels;
using MvvmCross.Commands;
using MvvmCross.Navigation;

namespace EventBookerAndroid.Core.ViewModels.Main
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IEventDataService _eventDataService;
        private readonly IMvxNavigationService _navigationService;

        public IMvxAsyncCommand<Event> ShowEventDetailCommand { get; private set; }

        private ObservableCollection<Event> _allEvents;
        public ObservableCollection<Event> AllEvents
        {
            get => _allEvents;

            set
            {
                _allEvents = value;
                RaisePropertyChanged(() => AllEvents);
            }
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        private IMvxAsyncCommand _refreshCommand;
        public IMvxAsyncCommand RefreshCommand => _refreshCommand ?? (_refreshCommand = new MvxAsyncCommand(ExecuteRefreshCommandAsync));

        public MainViewModel(IEventDataService eventDataService, IMvxNavigationService navigationService) : base()
        {
            _eventDataService = eventDataService;
            _navigationService = navigationService;

            ShowEventDetailCommand = new MvxAsyncCommand<Event>(NavigateToEventDetail);
        }

        //TODO: Should potentialy implement auto refresh as well, for now pull to refresh works
        private async Task ExecuteRefreshCommandAsync()
        {
            IsLoading = true;

            await Initialize();

            IsLoading = false;
        }

        public override async void Start()
        {
            base.Start();

            await ReloadDataAsync();
        }

        public override async Task Initialize()
        {
            await base.Initialize();

            AllEvents = (await _eventDataService.GetAllEventsAsync()).ToObservableCollection();
        }

        private Task NavigateToEventDetail(Event selectedEvent)
        {
            return _navigationService.Navigate<EventDetailViewModel, Event>(selectedEvent);
        }
    }
}
