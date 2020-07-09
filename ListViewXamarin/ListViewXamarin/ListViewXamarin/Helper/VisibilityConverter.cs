using Syncfusion.DataSource.Extensions;
using Syncfusion.GridCommon.ScrollAxis;
using Syncfusion.ListView.XForms;
using Syncfusion.ListView.XForms.Control.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ListViewXamarin
{
    public class VisibilityConverter : IValueConverter
    {
        #region Fields
        SfListView ListView;
        #endregion

        #region Convert

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ListView = parameter as SfListView;

            if (value == null || ListView.DataSource.Groups.Count == 0)
                return false;

            var groupresult = GetGroup(value);
            var list = groupresult.Items.ToList<object>().ToList();
            return list[0] != value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private method

        private GroupResult GetGroup(object itemData)
        {
            GroupResult itemGroup = null;

            foreach (var item in this.ListView.DataSource.DisplayItems)
            {
                if (item == itemData)
                    break;

                if (item is GroupResult)
                    itemGroup = item as GroupResult;
            }
            return itemGroup;
        }
        #endregion
    }
}