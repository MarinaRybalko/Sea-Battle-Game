using System;
using System.Globalization;

namespace LocalizatorHelper
{
    public delegate void LocaleChangedEventHander(object sender, LocaleChangedEventArgs e);

    public class LocaleChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Returns or sets culture
        /// </summary>
        public CultureInfo NewLocale { get; set; }
        /// <summary>
        /// Initialize a new instance of the <see cref="LocaleChangedEventArgs"/> class
        /// </summary>
        /// <param name="newLocale"></param>
        public LocaleChangedEventArgs(CultureInfo newLocale)
        {
            NewLocale = newLocale;
        }
    }
}
