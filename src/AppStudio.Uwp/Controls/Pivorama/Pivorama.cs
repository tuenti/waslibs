﻿using System;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Controls;
using Windows.Foundation;

namespace AppStudio.Uwp.Controls
{
    public partial class Pivorama : Control
    {
        private Panel _frame = null;

        private Panel _headerContainer = null;
        private PivoramaPanel _header = null;

        private Panel _tabsContainer = null;
        private PivoramaTabs _tabs = null;

        private Panel _panelContainer = null;
        private PivoramaPanel _panel = null;

        private ScrollViewer _scrollViewer = null;

        private RectangleGeometry _clip = null;

        private bool _isInitialized = false;

        public Pivorama()
        {
            this.DefaultStyleKey = typeof(Pivorama);
        }

        protected override void OnApplyTemplate()
        {
            _frame = base.GetTemplateChild("frame") as Panel;

            _headerContainer = base.GetTemplateChild("headerContainer") as Panel;
            _header = base.GetTemplateChild("header") as PivoramaPanel;
            _header.SelectedIndexChanged += OnSelectedIndexChanged;

            _tabsContainer = base.GetTemplateChild("tabsContainer") as Panel;
            _tabs = base.GetTemplateChild("tabs") as PivoramaTabs;
            _tabs.SelectedIndexChanged += OnSelectedIndexChanged;

            _panelContainer = base.GetTemplateChild("panelContainer") as Panel;
            _panel = base.GetTemplateChild("panel") as PivoramaPanel;

            _scrollViewer = base.GetTemplateChild("scrollViewer") as ScrollViewer;

            _clip = base.GetTemplateChild("clip") as RectangleGeometry;

            _frame.ManipulationDelta += OnManipulationDelta;
            _frame.ManipulationCompleted += OnManipulationCompleted;
            _frame.ManipulationMode = ManipulationModes.TranslateX | ManipulationModes.TranslateInertia | ManipulationModes.System;
            _frame.PointerWheelChanged += OnPointerWheelChanged;

            _panelContainer.ManipulationDelta += OnManipulationDelta;
            _panelContainer.ManipulationCompleted += OnManipulationCompleted;
            _panelContainer.ManipulationMode = ManipulationModes.TranslateX | ManipulationModes.TranslateInertia | ManipulationModes.System;
            _panelContainer.PointerWheelChanged += OnPointerWheelChanged;

            _isInitialized = true;

            this.ItemWidthEx = this.ItemWidth;

            this.SizeChanged += OnSizeChanged;

            base.OnApplyTemplate();
        }

        private void OnSelectedIndexChanged(object sender, int index)
        {
            if (this.Index != index)
            {
                this.Index = index - 1;
                this.AnimateNext(100);
            }
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            RefreshLayout();
            _clip.Rect = new Rect(0, 0, e.NewSize.Width, e.NewSize.Height);
        }

        private void RefreshLayout()
        {
            if (this.FitToScreen)
            {
                this.ItemWidthEx = Math.Round(this.ActualWidth);
                _headerContainer.Visibility = Visibility.Collapsed;
                _tabsContainer.Visibility = Visibility.Visible;
            }
            else
            {
                if (this.ItemWidthEx * 2 < this.ActualWidth)
                {
                    _headerContainer.Visibility = Visibility.Visible;
                    _tabsContainer.Visibility = Visibility.Collapsed;
                }
                else
                {
                    _headerContainer.Visibility = Visibility.Collapsed;
                    _tabsContainer.Visibility = Visibility.Visible;
                }
            }
        }
    }
}
