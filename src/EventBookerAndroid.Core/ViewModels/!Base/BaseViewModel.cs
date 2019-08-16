using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.ViewModels;

namespace EventBookerAndroid.Core.ViewModels
{
    public abstract class BaseViewModel : MvxViewModel
    {
        protected async Task ReloadDataAsync()
        {
            try
            {
                await InitializeAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        protected virtual Task InitializeAsync()
        {
            //dummy init method to be overloaded
            return Task.FromResult(0);
        }
    }
}
