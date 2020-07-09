# How to hide the line separator in Xamarin.Forms ListView with grouping (SfListView)

The Xamarin.Forms [SfListView](https://help.syncfusion.com/xamarin/listview/overview) does not contain line separator by default. You can load the StackLayout with height as 1 to separate the items.

You can hide the separator for any item in a group by changing the [IsVisible](https://docs.microsoft.com/en-us/dotnet/api/xamarin.forms.visualelement.isvisible) property of the **StackLayout** (Used as a Separator) loaded in the [SfListView.ItemTemplate](https://help.syncfusion.com/cr/cref_files/xamarin/Syncfusion.SfListView.XForms~Syncfusion.ListView.XForms.SfListView~ItemTemplate.html).

**XAML**

Converter bind to **Separator** to hide it based on the requirement.

``` xml
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:local="clr-namespace:ListViewXamarin"
             xmlns:syncfusion="clr-namespace:Syncfusion.ListView.XForms;assembly=Syncfusion.SfListView.XForms"
             x:Class="ListViewXamarin.MainPage" Padding="0,50,0,0">
    <ContentPage.Resources>
        <ResourceDictionary>
            <local:VisibilityConverter x:Key="VisibilityConverter"/>
        </ResourceDictionary>
    </ContentPage.Resources>
    <ContentPage.Content>
        <StackLayout>
            <syncfusion:SfListView x:Name="listView" ItemSize="60" ItemsSource="{Binding ContactsInfo}">
                <syncfusion:SfListView.ItemTemplate >
                    <DataTemplate>
                        <StackLayout>
                            <StackLayout IsVisible="{Binding .,Converter={StaticResource VisibilityConverter}, ConverterParameter={x:Reference Name=listView}}" BackgroundColor="Red" HeightRequest="1"/>
                            <Grid>
                                <Grid.ColumnDefinitions>
                                    <ColumnDefinition Width="70" />
                                    <ColumnDefinition Width="*" />
                                </Grid.ColumnDefinitions>
                                <Image Source="{Binding ContactImage}" VerticalOptions="Center" HorizontalOptions="Center" HeightRequest="50" WidthRequest="50"/>
                                <Grid Grid.Column="1" RowSpacing="1" Padding="10,0,0,0" VerticalOptions="Center">
                                    <Label LineBreakMode="NoWrap" TextColor="#474747" Text="{Binding ContactName}"/>
                                    <Label Grid.Row="1" Grid.Column="0" TextColor="#474747" LineBreakMode="NoWrap" Text="{Binding ContactNumber}"/>
                                </Grid>
                            </Grid>
                        </StackLayout>
                    </DataTemplate>
                </syncfusion:SfListView.ItemTemplate>
                <syncfusion:SfListView.GroupHeaderTemplate>
                    <DataTemplate>
                        <StackLayout BackgroundColor="#E4E4E4">
                            <Label Text="{Binding Key}" FontSize="22" FontAttributes="Bold" VerticalOptions="CenterAndExpand" HorizontalOptions="Start" Margin="20,0,0,0" />
                        </StackLayout>
                    </DataTemplate>
                </syncfusion:SfListView.GroupHeaderTemplate>
            </syncfusion:SfListView>
        </StackLayout>
    </ContentPage.Content>
</ContentPage> 
```

**C#**

In the converter, get the group result value and change the visibility of the element of first item in a group.

``` c#
public class VisibilityConverter : IValueConverter
{
    SfListView ListView;

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        ListView = parameter as SfListView;

        if (value == null || ListView.DataSource.Groups.Count == 0)
            return false;

        var groupresult = GetGroup(value);
        var list = groupresult.Items.ToList<object>().ToList();
        return list[0] != value;
    }

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
}
```

**Output**

![HideLineSeparator](https://github.com/SyncfusionExamples/hide-line-separator-listview-xamarin/blob/master/ScreenShot/HideLineSeparator.png)
