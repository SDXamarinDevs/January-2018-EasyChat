using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace EasyChat.Helpers
{
    public class Settings : IAppSettings
    {
        private static Lazy<Settings> lazyLoader = new Lazy<Settings>(() => new Settings());
        private static Settings _current;

        public static IAppSettings Current
        {
            get
            {
                if (!lazyLoader.IsValueCreated)
                {
                    _current = lazyLoader.Value;
                }

                return _current;
            }
        }

        private static ISettings AppSettings => CrossSettings.Current;

        private Settings()
        {
        }

        public string UserName
        {
            get => GetProperty("");
            set => SetProperty(value);
        }
        public string IncommingColor
        {
            get => GetProperty("#03A9F4");
            set => SetProperty(value);
        }
        public string OutgoingColor
        {
            get => GetProperty("#F5F5F5");
            set => SetProperty(value);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private string GetProperty(string defaultValue = default(string), [CallerMemberName]string propertyName = null) =>
            AppSettings.GetValueOrDefault(propertyName, defaultValue);

        private bool GetProperty(bool defaultValue = default(bool), [CallerMemberName]string propertyName = null) =>
            AppSettings.GetValueOrDefault(propertyName, defaultValue);

        private DateTime GetProperty(DateTime defaultValue = default(DateTime), [CallerMemberName]string propertyName = null) =>
            AppSettings.GetValueOrDefault(propertyName, defaultValue);

        private void SetProperty(object value, [CallerMemberName]string propertyName = null)
        {
            bool updated = false;
            switch (value)
            {
                case string str:
                    updated = AppSettings.AddOrUpdateValue(propertyName, str);
                    break;
                case bool boolean:
                    updated = AppSettings.AddOrUpdateValue(propertyName, boolean);
                    break;
                case DateTime dateTime:
                    updated = AppSettings.AddOrUpdateValue(propertyName, dateTime);
                    break;
            }

            if (updated)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
