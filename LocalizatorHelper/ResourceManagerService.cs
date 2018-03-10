using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Resources;
using System.Globalization;
using System.Threading;

namespace LocalizatorHelper
{
    public static class ResourceManagerService
    {
        private static Dictionary<string, ResourceManager> _managers;
        /// <summary>
        /// Occurs when culture changed
        /// </summary>
        public static event LocaleChangedEventHander LocaleChanged;

        private static void RaiseLocaleChanged(CultureInfo newLocale)
        {
            var evt = LocaleChanged;

            if (evt != null)
            {
                evt.Invoke(null, new LocaleChangedEventArgs(newLocale));
            }
        }

        /// <summary>
        /// Current application culture
        /// </summary>
        public static CultureInfo CurrentLocale { get; private set; }
        /// <summary>
        /// Initialize static data members
        /// </summary>
        static ResourceManagerService()
        {
            _managers = new Dictionary<string, ResourceManager>();

            ChangeLocale(CultureInfo.CurrentCulture.Name);
        }

        /// <summary>
        /// Receives a string resource  for the specified key from the specified ResourceManager
        /// 
        /// </summary>
        /// <param name="managerName">Name of ResourceManager</param>
        /// <param name="resourceKey">Name of resource for searching</param>
        /// <returns></returns>
        public static string GetResourceString(string managerName, string resourceKey)
        {
            ResourceManager manager = null;
            string resource = String.Empty;

            if (_managers.TryGetValue(managerName, out manager))
            {
                resource = manager.GetString(resourceKey);
            }
            return resource;
        }

        /// <summary>
        /// Changes current culture
        /// </summary>
        /// <param name="newLocaleName">Culture name (en-US, en-GB)</param>
        public static void ChangeLocale(string newLocaleName)
        {
            CultureInfo newCultureInfo = new CultureInfo(newLocaleName);
            Thread.CurrentThread.CurrentCulture = newCultureInfo;
            Thread.CurrentThread.CurrentUICulture = newCultureInfo;

            CurrentLocale = newCultureInfo;

            RaiseLocaleChanged(newCultureInfo);
        }

        /// <summary>
        /// Raise event LocaleChanged for update bindings
        /// </summary>
        public static void Refresh()
        {
            ChangeLocale(CultureInfo.CurrentCulture.IetfLanguageTag);
        }

        /// <summary>
        ///  Registers of the resource manager without updating the interface
        /// </summary>
        public static void RegisterManager(string managerName, ResourceManager manager)
        {
            RegisterManager(managerName, manager, false);
        }

        /// <summary>
        /// Registers a new resource manager and update the interface
        /// </summary>
        public static void RegisterManager(string managerName, ResourceManager manager, bool refresh)
        {
            ResourceManager _manager = null;

            _managers.TryGetValue(managerName, out _manager);

            if (_manager == null)
            {
                _managers.Add(managerName, manager);
            }

            if (refresh)
            {
                Refresh();
            }
        }

        /// <summary>
        /// Deletes resource manager
        /// </summary>
        public static void UnregisterManager(string name)
        {
            ResourceManager manager = null;

            _managers.TryGetValue(name, out manager);

            if (manager != null)
            {
                _managers.Remove(name);
            }
        }
    }
}


