using System.Collections.ObjectModel;
using fy4b.Models;
using static System.Console;
namespace fy4b;
using System.IO;

public partial class MainPage : ContentPage
{
	
	public MainPage()
	{
		InitializeComponent();
        var DirectList = Directory.GetDirectories("/Volumes/TOSHIBA EXT/fy4b/L1/proj");
        foreach(var DirectPath in DirectList)
        {
            RemoteSenseModels.Add(new RemoteSenseModel(DirectPath));
        }
		
        RemoteSenseModels.Add(new RemoteSenseModel());
        RemoteSenseModels.Add(new RemoteSenseModel());
        ListViewRemoteSense.ItemsSource = RemoteSenseModels;
    }
    ObservableCollection<RemoteSenseModel> RemoteSenseModels = new ObservableCollection<RemoteSenseModel>();
    protected override void OnAppearing()
    {
        base.OnAppearing();

    }

	void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        RemoteSenseModel previous = (e.PreviousSelection.FirstOrDefault() as RemoteSenseModel);
        RemoteSenseModel current = (e.CurrentSelection.FirstOrDefault() as RemoteSenseModel);
        ImageRemoteSense.Source = current.RemoteImageSource;
        WriteLine("select one");
    }

}


