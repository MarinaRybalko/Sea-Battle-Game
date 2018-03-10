using System;
using System.ComponentModel;


namespace LocalizatorHelper
{
    public class LocalisationHelper : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            var evt = PropertyChanged;

            evt?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        ///Initialize a new instance of the <see cref="LocalisationHelper"/> class
        /// </summary>
        public LocalisationHelper()
        {
            if (!DesignHelpers.IsInDesignMode)
            {
                ResourceManagerService.LocaleChanged += (s, e) =>
                {
                    RaisePropertyChanged(string.Empty);
                };
            }
        }

        /// <summary>
        /// Gets a row from resources using ResourceManager
        /// 
        /// {Binding Source={StaticResource localisation}, Path=.[MainScreenResources.IntroTextLine1]}
        /// </summary>
        /// <param name="key">The key to extract from the resources in the format [ManagerName].[ResourceKey]</param>
        /// <returns></returns>
        public string this[string key]
        {
            get
            {
                if (!ValidateKey(key))
                {
                    throw new ArgumentException(@"Is the correct string format specified [ManagerName].[ResourceKey]");
                }
                if (DesignHelpers.IsInDesignMode)
                {
                    return "[res]";
                }

                return ResourceManagerService.GetResourceString(GetManagerKey(key), GetResourceKey(key));
            }
        }

        #region Private Key Methods

        private bool ValidateKey(string input)
        {
            return input.Contains(".");
        }

        private string GetManagerKey(string input)
        {
            return input.Split('.')[0];
        }

        private string GetResourceKey(string input)
        {
            return input.Substring(input.IndexOf('.') + 1);
        }

        #endregion
    }
}


