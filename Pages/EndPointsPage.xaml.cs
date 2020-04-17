﻿using Notification_Forwarder.ConfigHelper;
using Notification_Forwarder.Controls;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace Notification_Forwarder.Pages
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class EndPointsPage : Page
    {
        public EndPointsPage()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var endPoint in Conf.CurrentConf.APIEndPoints)
            {
                ListView_EndPoints.Items.Add(endPoint);
            }
        }

        private void ToggleButton_Multiselect_Click(object sender, RoutedEventArgs e)
        {
            if (ToggleButton_Multiselect.IsChecked == true) ListView_EndPoints.SelectionMode = ListViewSelectionMode.Multiple;
            else ListView_EndPoints.SelectionMode = ListViewSelectionMode.Single;
        }

        private void Button_Delete_Click(object sender, RoutedEventArgs e)
        {
            var temp = new List<string>();
            temp.AddRange(ListView_EndPoints.SelectedItems.Select(s => (string)s));
            foreach (var item in temp)
            {
                Conf.CurrentConf.APIEndPoints.Remove(item);
                ListView_EndPoints.Items.Remove(item);
                Conf.Log($"deleted endpoint {item}.");
            }
        }

        private async void Button_Add_Click(object sender, RoutedEventArgs e)
        {
            var url = await AddAPIEndPointDialog.GetUrl();
            if (string.IsNullOrEmpty(url)) return;
            if (!Conf.IsUrl(url)) return;
            Conf.Log($"a new endpoint {url} has been added.");
            ListView_EndPoints.Items.Add(url);
            Conf.CurrentConf.APIEndPoints.Add(url);
        }
    }
}
