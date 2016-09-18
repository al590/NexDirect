﻿using NexDirectLib;
using NexDirectLib.Structures;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NexDirect.Dialogs
{
    /// <summary>
    /// Interaction logic for DirectDownload.xaml
    /// </summary>
    public partial class DirectDownload : Window
    {
        BeatmapSet set;
        MainWindow _mw;

        public DirectDownload(MainWindow mw, BeatmapSet bs)
        {
            InitializeComponent();

            set = bs;
            _mw = mw;

            Title = $"{set.Artist} - {set.Title} <{set.Mapper}> :: NexDirect Download";
            previewImage.Source = new BitmapImage(bs.PreviewImage);
            songInfoLabel.Content = $"{set.Artist} - {set.Title}";
            mapperInfoLabel.Content = $"(mapped by {set.Mapper})";

            if (_mw.overlayMode)
                initOverlayMode();
        }

        private void initOverlayMode()
        {
            Opacity = 0.88;
            Topmost = true;
            WindowStyle = WindowStyle.None;
            Height = SystemParameters.PrimaryScreenHeight;
            AllowsTransparency = true;

            // Reposition to very right - wpf only has left/top properties so calculate.
            Left = SystemParameters.PrimaryScreenWidth - Width;
            Top = 0;
        }

        private void downloadBtn_Click(object sender, RoutedEventArgs e)
            => onDownloadClick();

        private void downloadViewBtn_Click(object sender, RoutedEventArgs e)
            => onDownloadClick(true);

        private void previewBtn_Click(object sender, RoutedEventArgs e)
            => Osu.PlayPreviewAudio(set);

        private void button_Click(object sender, RoutedEventArgs e)
            => Process.Start($"https://osu.ppy.sh/s/{set.Id}");

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
            => AudioManager.ForceStopPreview();

        private void Window_Loaded(object sender, RoutedEventArgs e)
            => Activate();

        private void onDownloadClick(bool restore = false)
        {
            AudioManager.ForceStopPreview();
            _mw.DownloadBeatmapSet(set);
            if (restore)
                _mw.RestoreWindow();
            Close();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
            => Close();
    }
}