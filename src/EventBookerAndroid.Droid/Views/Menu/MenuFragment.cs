using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using EventBookerAndroid.Core.ViewModels.Main;
using EventBookerAndroid.Core.ViewModels.Menu;
using EventBookerAndroid.Droid.Views.Helpers;

namespace EventBookerAndroid.Droid.Views.Menu
{
    [MvxFragmentPresentation(typeof(MainContainerViewModel), Resource.Id.navigation_frame)]
    public class MenuFragment : BaseFragment<MenuViewModel>, NavigationView.IOnNavigationItemSelectedListener
    {
        protected override int FragmentLayoutId => Resource.Layout.fragment_navigation;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = base.OnCreateView(inflater, container, savedInstanceState);

            NavigationView navigationView = view.FindViewById<NavigationView>(Resource.Id.navigation_view);
            navigationView.SetNavigationItemSelectedListener(this);

            return view;
        }

        public bool OnNavigationItemSelected(IMenuItem menuItem)
        {
            switch (menuItem.ItemId)
            {
                case Resource.Id.nav_home:
                    ViewModel.ShowHomeCommand.Execute();
                    break;
                case Resource.Id.nav_create:
                    ViewModel.ShowEventCreateCommand.Execute();
                    break;
            }

            (Activity as IDrawerActivity)?.DrawerLayout.CloseDrawers();
            return true;
        }
    }
}
