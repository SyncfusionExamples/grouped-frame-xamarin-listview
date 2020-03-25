# How to show group and grouped items within a frame Xamarin.Forms ListView (SfListView)

You can show the frame around each group and group items with the help of [converter](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/data-binding/converters) by changing the border [thickness](https://docs.microsoft.com/en-us/dotnet/api/xamarin.forms.thickness?view=xamarin-forms) of the [GroupHeaderItem](https://help.syncfusion.com/cr/cref_files/xamarin/Syncfusion.SfListView.XForms~Syncfusion.ListView.XForms.GroupHeaderItem.html?) and [ListViewItem](https://help.syncfusion.com/cr/cref_files/xamarin/Syncfusion.SfListView.XForms~Syncfusion.ListView.XForms.ListViewItem.html?) in Xamarin.Forms [SfListView](https://help.syncfusion.com/xamarin/listview/overview?).

**XAML**

[ItemTemplate](https://help.syncfusion.com/cr/cref_files/xamarin/Syncfusion.SfListView.XForms~Syncfusion.ListView.XForms.SfListView~ItemTemplate.html?) and [GroupHeaderTemplate](https://help.syncfusion.com/cr/cref_files/xamarin/Syncfusion.SfListView.XForms~Syncfusion.ListView.XForms.SfListView~GroupHeaderTemplate.html?) are defined with the converter that defines the border thickness.
``` xml
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:local="clr-namespace:ListViewXamarin"
             xmlns:syncfusion="clr-namespace:Syncfusion.ListView.XForms;assembly=Syncfusion.SfListView.XForms"
             x:Class="ListViewXamarin.MainPage">
    <ContentPage.BindingContext>
        <local:ContactsViewModel/>
    </ContentPage.BindingContext>
    
    <ContentPage.Resources>
        <local:ThicknessConverter x:Key="converter"/>
    </ContentPage.Resources>
 
    <ContentPage.Behaviors>
        <local:Behavior/>
    </ContentPage.Behaviors>
    <ContentPage.Content>
        <syncfusion:SfListView x:Name="listView" GroupHeaderSize="50" ItemSize="60" Margin="10" ItemsSource="{Binding contactsinfo}">
            <syncfusion:SfListView.ItemTemplate>
                <DataTemplate>
                    <Grid BackgroundColor="Gray">
                        <Grid Padding="5,0,0,0" BackgroundColor="White" x:Name="grid" Margin="{Binding Converter={StaticResource converter}, ConverterParameter={x:Reference listView}}" >
                            <Grid.ColumnDefinitions>
                                <ColumnDefinition Width="50" />
                                <ColumnDefinition Width="*" />
                            </Grid.ColumnDefinitions>
                            <Image Source="{Binding ContactImage}" VerticalOptions="Center" HorizontalOptions="Center" HeightRequest="50"/>
                            <Grid Grid.Column="1" Padding="10,0,0,0" VerticalOptions="Center">
                                <Label LineBreakMode="WordWrap" TextColor="#474747" Text="{Binding ContactName}"/>
                                <Label Grid.Row="1" Grid.Column="0" TextColor="#474747" Text="{Binding ContactNumber}"/>
                            </Grid>
                        </Grid>
                    </Grid>
                </DataTemplate>
            </syncfusion:SfListView.ItemTemplate>
 
            <syncfusion:SfListView.GroupHeaderTemplate>
                <DataTemplate>
                    <Grid BackgroundColor="Gray" Margin="0,10,0,0">
                        <Grid BackgroundColor="White" Margin="{Binding Converter={StaticResource converter}, ConverterParameter={x:Reference listView}}">
                            <Grid Margin="5">
                                <Frame Padding="5,0,0,0" HasShadow="False" OutlineColor="LightGray" BackgroundColor="AliceBlue">
                                    <Label x:Name="label" Text="{Binding Key}" FontSize="20" FontAttributes="Bold" VerticalOptions="Center" HorizontalOptions="Start"/>
                                </Frame>
                            </Grid>
                        </Grid>
                    </Grid>
                </DataTemplate>
            </syncfusion:SfListView.GroupHeaderTemplate>
        </syncfusion:SfListView>
    </ContentPage.Content>
</ContentPage>
```
**C#**

Thickness returned based on the ListView item,  GroupHeader item and the last item of the group.

``` c#
namespace ListViewXamarin
{
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
}
```
**Output**

![GroupedFrame](https://github.com/SyncfusionExamples/grouped-frame-xamarin-listview/blob/master/ScreenShots/GroupedItemFrame.jpeg)
