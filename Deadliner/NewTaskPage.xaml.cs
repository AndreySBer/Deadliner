using Deadliner.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Deadliner
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NewTaskPage : Page
    {
        TodoItem item;
        public NewTaskPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            item = (TodoItem)e.Parameter;
            base.OnNavigatedTo(e);
        }

        private async void SaveNew(object sender, RoutedEventArgs e)
        {
            //TodoItem item = new TodoItem
            if (DatePicker.Date != null)
            {
                item.Title = TitleTextBox.Text;
                item.Complete = false;
                item.Text = TextTextBox.Text;
                item.DueTo = ((DateTimeOffset)DatePicker.Date).Date.Add(TimePicker.Time);
                Frame.Navigate(typeof(MainPage), item);
            }
            else
            {
                await new MessageDialog("Выберите день").ShowAsync();
            }
            
            
        }
    }
}
