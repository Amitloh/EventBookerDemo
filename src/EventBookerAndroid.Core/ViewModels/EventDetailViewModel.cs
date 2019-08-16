using System;
using System.Collections.Generic;
using System.Text;
using EventBookerAndroid.Core.Models;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace EventBookerAndroid.Core.ViewModels
{
    public class EventDetailViewModel : BaseViewModel<Event>
    {
        private readonly IMvxNavigationService _navigationService;

        private Event _selectedEvent;
        public Event SelectedEvent
        {
            get => _selectedEvent;

            set
            {
                _selectedEvent = value;
                RaisePropertyChanged(() => SelectedEvent);
            }
        }

        public EventDetailViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public override void Prepare(Event selectedEvent)
        {
            base.Prepare();

            SelectedEvent = selectedEvent;
            //test to see what this does
        }
    }
}
