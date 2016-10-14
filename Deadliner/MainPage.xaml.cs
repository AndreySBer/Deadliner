using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Deadliner.Services;
using Deadliner.Models;
using System.Globalization;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Controls.Primitives;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Deadliner
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        ObservableCollection<TodoItem> DeadList = new ObservableCollection<TodoItem>();
        TodoItem NewDead;
        public MainPage()
        {
            this.InitializeComponent();
            ShowData();
            TileService.UpdatePrimaryTile(this, null);
            UpdateBadge();
        }
        
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            {
                TodoItem item = (TodoItem)e.Parameter;
                await App.MobileService.GetTable<TodoItem>().InsertAsync(item);
                ReLoadItems();
            }
            catch { }
                base.OnNavigatedTo(e);
            
        }

        private async void Log(string v)
        {
            try
            {
                Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
                Windows.Storage.StorageFile sampleFile = await storageFolder.CreateFileAsync("Log.csv",
                        Windows.Storage.CreationCollisionOption.OpenIfExists);

                await Windows.Storage.FileIO.WriteTextAsync(sampleFile, v);

            }
            catch (Exception)
            { }
        }

        private void ShowData()
        {
            //DeadList.Add(new Deadline("В Питере - пить") { Name = "В Питере - пить" });
            //DeadList.Add(new Deadline("ЗОЖ") { Name = "ЗОЖ" });
            LoadDeads();
            dataTable.ItemsSource = DeadList;
        }

        int _count = 1;
        
        private void UpdateBadge()
        {
            TileService.SetBadgeCountOnTile(_count++);
        }

       
        string GetMonth
        {
            get
            {
                return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month).ToString().Substring(0, 3);
            }
        }
        string GetYear
        {
            get
            {
                return ((int)DateTime.Now.Year % 100).ToString();
            }
        }

        /*private void ChangeEvent(object sender, RoutedEventArgs e)
        {
            
            string temp = DateTime.Now.ToString();
            DeadList.Add(new TodoItem());
            dataTable.Items.Add(temp);
            _count++;
            PrimaryTile.IdealMessage = (Enum.GetName(typeof(Second), ((int)(DateTime.Now.Second)) % 2).ToString());
            TileService.UpdatePrimaryTile(this, null);
        }*/
        enum Second
        {
            Odd, Even
        }
        
        private void NewEvent(object sender, RoutedEventArgs e)
        {
            NewDead = new TodoItem();
            Frame.Navigate(typeof(NewTaskPage),NewDead);
        }

        private async void SaveEvents(object sender, RoutedEventArgs e)
        {
            try
            {
                Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
                Windows.Storage.StorageFile sampleFile = await storageFolder.CreateFileAsync("DataFile.csv",
                        Windows.Storage.CreationCollisionOption.OpenIfExists);
                string strres = "";
                foreach (TodoItem d in DeadList)
                    strres += d.ToString() + '\n';
                await Windows.Storage.FileIO.WriteTextAsync(sampleFile, strres);

            }
            catch (Exception)
            { }
        }
        private void LoadDeads()
        {
            /*Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile sampleFile = await storageFolder.GetFileAsync("DataFile.csv");
            var lines = await Windows.Storage.FileIO.ReadLinesAsync(sampleFile);
            foreach (String s in lines)
            {
                DeadList.Add(new TodoItem() { Title = s, Text="Шнур",DueTo=new DateTime() });
            }*/
            ReLoadItems();
        }

        private void AzureEvent(object sender, RoutedEventArgs e)
        {
            
            
            ReLoadItems();
        }

        private async void ReLoadItems()
        {
            var todoTable = App.MobileService.GetTable<TodoItem>();
            List<TodoItem> items = await todoTable
                .Where(todoItem => todoItem.Complete == false)
                .OrderBy(todoItem => todoItem.DueTo)
                .ToListAsync();
            DeadList.Clear();
            foreach (var v in items)
            {
                DeadList.Add(v/*new TodoItem() {Title=v.Title, Text=v.Text, DueTo=v.DueTo}*/ );
            }
            if (DeadList.Count > 0)
            {
                PrimaryTile.IdealTime = DeadList[0].Title;
                PrimaryTile.IdealMessage = DeadList[0].Text;
            }
            else
            {
                PrimaryTile.IdealTime = "You have no current tasks";
                PrimaryTile.IdealMessage = "Why don\'t you run city quests from Quest-City.ru";
            }
            TileService.UpdatePrimaryTile(this, null);
            TileService.SetBadgeCountOnTile(DeadList.Count);
        }
    }
}
