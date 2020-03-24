using Syncfusion.DataSource;
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
    public class Behavior : Behavior<ContentPage>
    {
        SfListView ListView;
        protected override void OnAttachedTo(ContentPage bindable)
        {
            ListView = bindable.FindByName<SfListView>("listView");

            ListView.DataSource.GroupDescriptors.Add(new GroupDescriptor()
            {
                PropertyName = "ContactName",
                KeySelector = (object obj1) =>
                {
                    var item = (obj1 as Contacts);
                    return item.ContactName[0].ToString();
                }
            });

            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(ContentPage bindable)
        {
            ListView = null;
            base.OnDetachingFrom(bindable);
        }
    }
}