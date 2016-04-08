﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace AppStudio.Uwp.Samples.Pages.HtmlBlock
{
    [SamplePage(Category = "FoundationControls", Name = "HtmlBlock", Order = 30)]
    sealed partial class HtmlBlockPage : SamplePage
    {
        public HtmlBlockViewModel ViewModel { get; set; } = new HtmlBlockViewModel();

        public HtmlBlockPage()
        {
            this.InitializeComponent();

            DataContext = ViewModel;
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            await ViewModel.LoadStateAsync();
        }

        public override string Caption
        {
            get { return "HtmlBlock Sample"; }
        }
    }
}