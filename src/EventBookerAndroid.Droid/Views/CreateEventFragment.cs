using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using EventBookerAndroid.Core.ViewModels.Main;
using EventBookerAndroid.Core.ViewModels;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Binding.BindingContext;
using System.Globalization;
using Android.Graphics.Drawables;
using System.Drawing;

namespace EventBookerAndroid.Droid.Views
{
    [MvxFragmentPresentation(typeof(MainContainerViewModel), Resource.Id.content_frame, true)]
    public class CreateEventFragment : BaseFragment<CreateEventViewModel>, DatePickerDialog.IOnDateSetListener, TimePickerDialog.IOnTimeSetListener
    {
        public static readonly int PICK_IMAGE_ID = 1000;

        private EditText _datePickerText;
        private EditText _timePickerText;
        private Button _btnSelectImage;
        private Button _btnFinished;
        private ImageView _imgEvent;

        protected override int FragmentLayoutId => Resource.Layout.fragment_create_event;

        public CreateEventFragment()
        {
            RetainInstance = true;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            View view = this.BindingInflate(FragmentLayoutId, container, false);

            InitDatePicker(view);
            InitTimePicker(view);

            _btnSelectImage = view.FindViewById<Button>(Resource.Id.btn_select_img);
            _btnSelectImage.Click += SelectImageOnClick;

            //TODO: Disable/Hide this button when not all fields entered.
            //For now I'll just add dummy values if user presses with empty fields
            _btnFinished = view.FindViewById<Button>(Resource.Id.btn_finished);
            _btnFinished.Click += FinishedOnClick;

            _imgEvent = view.FindViewById<ImageView>(Resource.Id.img_event);

            return view;
        }

        public override void OnActivityResult(int requestCode, int resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if ((requestCode == PICK_IMAGE_ID) && (resultCode == (int)Result.Ok) && (data != null))
            {
                _imgEvent.SetImageURI(data.Data);
            }
        }

        private void InitDatePicker(View view)
        {
            _datePickerText = view.FindViewById<EditText>(Resource.Id.event_date_text);
            _datePickerText.Text = DateTime.Now.ToString("MMM dd, yyyy");
            _datePickerText.Focusable = false;
            _datePickerText.Click += delegate
            {
                var dialog = new DatePickerDialogFragment(Activity, this, Convert.ToDateTime(_datePickerText.Text));
                dialog.Show(FragmentManager, "date");
            };
        }

        private void InitTimePicker(View view)
        {
            _timePickerText = view.FindViewById<EditText>(Resource.Id.event_time_text);
            _timePickerText.Text = DateTime.Now.ToString("hh:mm tt", CultureInfo.InvariantCulture);
            _timePickerText.Focusable = false;
            _timePickerText.Click += delegate
            {
                var dialog = new TimePickerDialogFragment(Activity, this, Convert.ToDateTime(_timePickerText.Text));
                dialog.Show(FragmentManager, "time");
            };
        }

        public void OnDateSet(DatePicker view, int year, int month, int dayOfMonth)
        {
            ViewModel.UpdateEventDate(new DateTime(year, month + 1, dayOfMonth));
        }

        public void OnTimeSet(TimePicker view, int hourOfDay, int minute)
        {
            ViewModel.UpdateEventTime(hourOfDay, minute);
        }

        private void SelectImageOnClick(object sender, EventArgs args)
        {
            var intent = new Intent();
            intent.SetType("image/*");
            intent.SetAction(Intent.ActionGetContent);

            StartActivityForResult(Intent.CreateChooser(intent, "Select Image"), PICK_IMAGE_ID);
        }

        private Android.Graphics.Bitmap GetBitmapFromImageView()
        {
            var bitmapDrawable = (BitmapDrawable)_imgEvent.Drawable;

            return bitmapDrawable.Bitmap;
        }

        private void FinishedOnClick(object sender, EventArgs args)
        {
            var bitmap = GetBitmapFromImageView();

            ViewModel.CreateNewEvent(bitmap);
        }

        //Below are private dialog classes to enable date/time dialogs to appear when the EditTexts above are selected
        private class DatePickerDialogFragment : Android.Support.V4.App.DialogFragment
        {
            private readonly Context _context;
            private readonly DatePickerDialog.IOnDateSetListener _onDateSetListener;
            private DateTime _date;

            public DatePickerDialogFragment(Context context, DatePickerDialog.IOnDateSetListener onDateSetListener, DateTime date)
            {
                _context = context;
                _onDateSetListener = onDateSetListener;
                _date = date;
            }

            public override Dialog OnCreateDialog(Bundle savedInstanceState)
            {
                base.OnCreateDialog(savedInstanceState);

                return new DatePickerDialog(_context, _onDateSetListener, _date.Year, _date.Month-1, _date.Day);
            }
        }

        private class TimePickerDialogFragment : Android.Support.V4.App.DialogFragment
        {
            private readonly Context _context;
            private readonly TimePickerDialog.IOnTimeSetListener _onTimeSetListener;
            private DateTime _time;

            public TimePickerDialogFragment(Context context, TimePickerDialog.IOnTimeSetListener onTimeSetListener, DateTime time)
            {
                _context = context;
                _onTimeSetListener = onTimeSetListener;
                _time = time;
            }

            public override Dialog OnCreateDialog(Bundle savedInstanceState)
            {
                base.OnCreateDialog(savedInstanceState);

                return new TimePickerDialog(_context, _onTimeSetListener, _time.Hour, _time.Minute, false);
            }
        }
    }
}
