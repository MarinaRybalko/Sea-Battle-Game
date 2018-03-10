using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows;

namespace LocalizatorHelper
{
    public static class DesignHelpers
    {
        /// <summary>
        /// Checks whether the application is in design mode
        /// </summary>
        public static bool IsInDesignMode => (bool)(DesignerProperties.IsInDesignModeProperty
            .GetMetadata(typeof(DependencyObject))
            .DefaultValue);
    }
}
