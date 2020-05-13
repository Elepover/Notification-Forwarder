﻿using Notification_Forwarder.ConfigHelper;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace Notification_Forwarder.Pages
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            this.InitializeComponent();
        }

        private bool _isToggleSwitchReady = false;

        private void ToggleSwitch_DisplayPackageName_Toggled(object sender, RoutedEventArgs e)
        {
            if (!_isToggleSwitchReady) return;
            Conf.CurrentConf.DisplayPackageName = ToggleSwitch_DisplayPackageName.IsOn;
        }

        private void ToggleSwitch_EnableForwarder_Toggled(object sender, RoutedEventArgs e)
        {
            if (!_isToggleSwitchReady) return;
            Conf.CurrentConf.EnableForwarding = ToggleSwitch_EnableForwarder.IsOn;
            if (ToggleSwitch_EnableForwarder.IsOn && !MainPage.IsUploadWorkerActive)
                MainPage.StartUploadWorker();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ToggleSwitch_DisplayPackageName.IsOn = Conf.CurrentConf.DisplayPackageName;
            ToggleSwitch_EnableForwarder.IsOn = Conf.CurrentConf.EnableForwarding;
            ToggleSwitch_MuteNewApps.IsOn = Conf.CurrentConf.MuteNewApps;
            _isToggleSwitchReady = true;
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            Conf.MainPageInstance.ToggleBackButton(true);
            Frame.Navigate(typeof(AppsPage), null, new DrillInNavigationTransitionInfo());
        }

        private void HyperlinkButton_Click_1(object sender, RoutedEventArgs e)
        {
            Conf.MainPageInstance.ToggleBackButton(true);
            Frame.Navigate(typeof(EndPointsPage), null, new DrillInNavigationTransitionInfo());
        }

        private void ToggleSwitch_MuteNewApps_Toggled(object sender, RoutedEventArgs e)
        {
            if (!_isToggleSwitchReady) return;
            Conf.CurrentConf.MuteNewApps = ToggleSwitch_MuteNewApps.IsOn;
        }
    }
}
