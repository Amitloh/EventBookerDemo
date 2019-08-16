using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross.ViewModels;

namespace EventBookerAndroid.Core.ViewModels
{
    //BaseViewModel to handle ViewModels that need to be passed parameters
    public abstract class BaseViewModel<TParameter> : BaseViewModel, IMvxViewModel<TParameter>
    {
        public abstract void Prepare(TParameter parameter);
    }
}
