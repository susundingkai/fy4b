using System.Collections.ObjectModel;
using fy4b.Models;
using static System.Console;
namespace fy4b;
using System.IO;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Storage;

public partial class MainPage : ContentPage
{
	
	public MainPage()
	{
		InitializeComponent();
        using(var reader = new StreamReader(@"G:\0seasfog_watch\地面逐小时观测\22110102.000"))
        {
            var init = -2;
            //0,1,2,3,17,18
            List<int> station = new List<int>();
            List<int> lon = new List<int>();
            List<int> lat = new List<int>();
            List<int> high = new List<int>();
            List<int> vis = new List<int>();
            List<int> code = new List<int>();
            while (!reader.EndOfStream)
            {
                if (init < 0)
                {
                    init++;
                    continue;
                }
                var line = reader.ReadLine().TrimStart();
                var values = line.Split(' ');
                int cnt = -1;
                foreach(var v in values)
                {
                    if(v.Length>0) cnt++;
                    switch(cnt)
                    {
                        case 0:
                            station.Add(Convert.ToInt32(v)); break;
                        case 1:
                            lon.Add(Convert.ToInt32(v)); break;
                        case 2:
                            lat.Add(Convert.ToInt32(v)); break;
                        case 3:
                            high.Add(Convert.ToInt32(v)); break;
                        case 17:
                            vis.Add(Convert.ToInt32(v)); break;
                        case 18:
                            code.Add(Convert.ToInt32(v)); break;
                    }
                }

                listA.Add(values[0]);
                listB.Add(values[1]);
            }
        }

    }

    readonly ObservableCollection<RemoteSenseModel> RemoteSenseModels = new();
    protected override void OnAppearing()
    {
        base.OnAppearing();

    }
    public void PickFolder(object sender, EventArgs e)
    {
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        CancellationToken cancellationToken = cancellationTokenSource.Token;

        _ = PickFolder(cancellationToken);
    }
    public async Task PickFolder(CancellationToken cancellationToken)
    {
        try
        {
            var folder = await FolderPicker.Default.PickAsync(cancellationToken);
            LoadRemoteSenseList(folder.Path);
            await Toast.Make($"Folder picked: Name - {folder.Name}, Path - {folder.Path}", ToastDuration.Long).Show(cancellationToken);
        }
        catch (Exception ex)
        {
            await Toast.Make($"Folder is not picked, {ex.Message}").Show(cancellationToken);
        }
    }
    void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        RemoteSenseModel previous = e.PreviousSelection.FirstOrDefault() as RemoteSenseModel;
        RemoteSenseModel current = e.CurrentSelection.FirstOrDefault() as RemoteSenseModel;
        ImageRemoteSense.Source = current.Images[0].Source;
        ListViewChannels.ItemsSource = current.Images;
        WriteLine("select one");
    }
    void OnCollectionViewSelectionChanged2(object sender, SelectionChangedEventArgs e)
    {
        RemoteSenseImageModel previous = e.PreviousSelection.FirstOrDefault() as RemoteSenseImageModel;
        RemoteSenseImageModel current = e.CurrentSelection.FirstOrDefault() as RemoteSenseImageModel;
        ImageRemoteSense.Source = current.Source;
        WriteLine("select one");
    }
    void LoadRemoteSenseList(string directPath)
    {
        RemoteSenseModels.Clear();
        var DirectList = Directory.GetDirectories(directPath);
        foreach (var DirectPath in DirectList)
        {
            RemoteSenseModels.Add(new RemoteSenseModel(DirectPath));
        }
        ListViewRemoteSense.ItemsSource = RemoteSenseModels;
    }
}


