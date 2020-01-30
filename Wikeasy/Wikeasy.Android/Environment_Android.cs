using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Plugin.CurrentActivity;
using Wikeasy.Droid;
using Wikeasy.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(Environment_Android))]
namespace Wikeasy.Droid
{
    public class Environment_Android : IEnvironment
    {
        public Task<Theme> GetOperatingSystemThemeAsync() =>
            Task.FromResult(GetOperatingSystemTheme());

        public Theme GetOperatingSystemTheme()
        {
            //Ensure the device is running Android Froyo or higher because UIMode was added in Android Froyo, API 8.0
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Froyo)
            {
                var uiModeFlags = CrossCurrentActivity.Current.AppContext.Resources.Configuration.UiMode & UiMode.NightMask;

                switch (uiModeFlags)
                {
                    case UiMode.NightYes:
                        return Theme.Dark;

                    case UiMode.NightNo:
                        return Theme.Light;

                    default:
                        throw new NotSupportedException($"UiMode {uiModeFlags} not supported");
                }
            }
            else
            {
                return Theme.Light;
            }
        }
    }
}