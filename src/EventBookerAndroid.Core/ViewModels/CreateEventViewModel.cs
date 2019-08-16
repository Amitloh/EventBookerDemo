using System;
using System.Collections.Generic;
using System.Text;
using Android.Graphics;
using EventBookerAndroid.Core.Interfaces;
using EventBookerAndroid.Core.Models;
using MvvmCross.Navigation;

namespace EventBookerAndroid.Core.ViewModels
{
    public class CreateEventViewModel : BaseViewModel
    {
        private readonly IEventDataService _eventDataService;
        private readonly IMvxNavigationService _navigationService;


        private string _eventName;
        public string EventName
        {
            get => _eventName;

            set
            {
                _eventName = value;
                RaisePropertyChanged(() => EventName);
            }
        }

        private DateTime _eventDate;
        public DateTime EventDate
        {
            get
            {
                if(_eventDate != null)
                {
                    return _eventDate;
                }
                else
                {
                    return DateTime.Now;
                }
            }

            set
            {
                _eventDate = value;
                RaisePropertyChanged(() => EventDate);
            }
        }

        private DateTime _eventTime;
        public DateTime EventTime
        {
            get => _eventTime;

            set
            {
                _eventTime = value;
                RaisePropertyChanged(() => EventTime);
            }
        }

        private int _totalTickets;
        public int TotalTickets
        {
            get => _totalTickets;

            set
            {
                _totalTickets = value;
                RaisePropertyChanged(() => TotalTickets);
            }
        }

        public CreateEventViewModel(IEventDataService eventDataService, IMvxNavigationService navigationService)
        {
            _eventDataService = eventDataService;
            _navigationService = navigationService;
        }

        public void UpdateEventDate(DateTime date)
        {
            EventDate = date;
        }

        public void UpdateEventTime(int hours, int minutes)
        {
            EventTime = EventDate.Date + new TimeSpan(hours, minutes, 0);
        }

        public void CreateNewEvent(Bitmap bitmap)
        {
            DateTime eventDateTime = EventDate + new TimeSpan(EventTime.Hour, EventTime.Minute, EventTime.Second);

            _eventDataService.SaveNewEventAsync(EventName, eventDateTime, TotalTickets);

            _navigationService.Close(this); //might work?
        }
    }
}
