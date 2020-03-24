using Xamarin.Forms;
using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using Syncfusion.ListView.XForms;
using Syncfusion.DataSource.Extensions;
using Syncfusion.DataSource;

namespace ListViewXamarin
{
    #region ThicknessConverter
    public class ThicknessConverter : IValueConverter
    {
        Thickness groupBorderThickness;
        Thickness lastItemThickness;
        Thickness defaultThickness;
        public ThicknessConverter()
        {
            groupBorderThickness = new Thickness(1, 1, 1, 0);
            defaultThickness = new Thickness(1, 0, 1, 0);
            lastItemThickness = new Thickness(1, 0, 1, 1);
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var listView = parameter as SfListView;
            var itemData = value as Contacts;
            object key = null;
            GroupResult actualGroup = null;
            var descriptor = listView.DataSource.GroupDescriptors[0];

            if (value == null)
                return defaultThickness;

            if (itemData == null)
                return groupBorderThickness;
            else
            {
                key = descriptor.KeySelector(itemData);
                for (int i = 0; i < listView.DataSource.Groups.Count; i++)
                {
                    var group = listView.DataSource.Groups[i];
                    if ((group.Key != null && group.Key.Equals(key)) || group.Key == key)
                    {
                        actualGroup = group;
                        break;
                    }
                }
                var lastItem = actualGroup.GetGroupLastItem();

                if (lastItem == itemData)
                    return lastItemThickness;

                return defaultThickness;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
    #endregion
}
