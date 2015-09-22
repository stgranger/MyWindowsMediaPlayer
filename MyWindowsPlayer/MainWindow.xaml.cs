using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Timers;
using System.Globalization;
using System.Resources;
using System.Windows.Threading;
using MyWindowsMediaPlayer.Properties;
using MWidgets;
using MyWindowsMediaPlayer.Resources;

namespace MyWindowsMediaPlayer
{
	public partial class MainWindow : Window
	{
		#region [DoubleClick]
		//[System.Runtime.InteropServices.DllImport("user32.dll")]
		//static extern uint GetDoubleClickTime();

		//System.Timers.Timer timeClick = new System.Timers.Timer((int)GetDoubleClickTime()) { AutoReset = false };
		#endregion

		#region [VARS]
		private MyPlaylist _PlayList;
		private int _current;
        private bool _fullscreen = false;
        private bool _isDraging = false;
        private timerTick _tick;
		private DispatcherTimer _timer;
		private bool _doubleclicked;
		private Timer _doubleclick;
		private delegate void timerTick();
		private GridLength _ControlsHeight;
        #endregion

        #region [INIT]
        public MainWindow()
		{
			InitializeComponent();

            this.Loaded += new RoutedEventHandler(MainWindow_Loaded);
		}

		private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
			this._Media.Paused = false;
			this._Media.Repeat = false;
			this._ControlsHeight = this._ControlsRow.Height;
            this._current = -1;
            this._Media.MediaFailed += _FailedToLoad;
            this._PlayList = new MyPlaylist();
            this._Media.Volume = (double)_SliderVolume.Value;
            this._timer = new DispatcherTimer();
            this._timer.Interval = TimeSpan.FromMilliseconds(1);
            this._timer.Tick += new EventHandler(_timer_Ticker);
            this._tick = new timerTick(_changeDurationStatus);

            this._SliderVolume.Value = this._Media.Volume;

			this._doubleclicked = false;
			this._doubleclick = new Timer(750);
			this._doubleclick.Elapsed += new ElapsedEventHandler(_Elapsed);
			
			this._ComboBoxLanguage.ItemsSource = CultureInfo.GetCultures(CultureTypes.NeutralCultures);
			this._ComboBoxLanguage.SelectedIndex = 0;
            this._SwitchCulture();
        }
        #endregion

        private void _SwitchCulture()
		{
			this._ButtonNext.Content = SR._NEXT_;
			this._ButtonPrevious.Content = SR._PREVIOUS_;
			this._ButtonPlay.Content = SR._PLAY_;
			this._ButtonStop.Content = SR._STOP_;

			this._MenuItemFile.Header = SR._FILE_;
			this._MenuItemQuit.Header = SR._QUIT_;
			this._MenuItemOpen.Header = SR._OPEN_;
		}

		private void _SwitchLanguage(object sender, SelectionChangedEventArgs e)
		{
			SR.Culture = (CultureInfo)this._ComboBoxLanguage.SelectedItem;
			this._SwitchCulture();
		}

		private void _FailedToLoad(object o, EventArgs e)
		{
			this._LabelStatus.Content = SR._FAILEDTOLOAD_;
		}

		private void _LaunchMedia()
		{
			try
			{
				if (this._current != this._ListPlayList.SelectedIndex)
				{
					this._Media.Load(this._ListPlayList.SelectedItem.ToString());
					this._current = this._ListPlayList.SelectedIndex;
				}
                if (this._Media.NaturalDuration.HasTimeSpan)
                {
                    this._SliderDuration.Maximum = this._Media.NaturalDuration.TimeSpan.TotalMilliseconds;
                    this._timer.Start();
                }
                else
                {
                    _SliderDuration.Maximum = 5000;
                    this._timer.Start();
                }

				this._Media.Play();
				this._Media.Paused = false;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
				this._LabelDuration.ContentStringFormat = ex.Message;
			}
		}

		private void _DragNDrop(object sender, DragEventArgs e)
		{
			this._PlayList.PushPlaylist((string[])e.Data.GetData(DataFormats.FileDrop));
			this._ListPlayList.ItemsSource = this._PlayList.GetList();
		}

		private void _DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
				e.Effects = DragDropEffects.Copy;
			else
				e.Effects = DragDropEffects.None;
		}

		private void _Play(object sender, RoutedEventArgs e)
		{
			if (this._ListPlayList.Items.Count == 0)
				return;
			else if (this._ListPlayList.SelectedIndex == -1)
				this._ListPlayList.SelectedIndex = 0;
			this._LaunchMedia();
		}

		private void _Next(object sender, RoutedEventArgs e)
		{
			if (this._ListPlayList.Items.Count == 0)
				return;
			if ((this._ListPlayList.SelectedIndex + 1) == this._ListPlayList.Items.Count)
				this._ListPlayList.SelectedIndex = 0;
			else
				this._ListPlayList.SelectedIndex++;
			this._LaunchMedia();
		}

		private void _Previous(object sender, RoutedEventArgs e)
		{
			if (this._ListPlayList.Items.Count == 0)
				return;
			if (this._ListPlayList.SelectedIndex <= 0)
				this._ListPlayList.SelectedIndex = (this._ListPlayList.Items.Count - 1);
			else
				this._ListPlayList.SelectedIndex--;
			this._LaunchMedia();
		}

		private void _Pause(object sender, RoutedEventArgs e)
		{
			this._Media.Pause();
 		}

		private void _Stop(object sender, RoutedEventArgs e)
		{
			this._Media.Stop();
 		}

		private void _ListKeyDown(object sender, KeyEventArgs e)
		{
			if (this._ListPlayList.SelectedIndex == -1)
				return;
			if (e.Key == Key.Delete || e.Key == Key.Back)
			{
				this._Media.Stop();
				this._PlayList.Remove(this._ListPlayList.SelectedIndex);
				this._ListPlayList.ItemsSource = this._PlayList.GetList();
				if (this._ListPlayList.Items.Count == 0)
					this._current = -1;
			}
			else if (e.Key == Key.Enter)
			{
				this._Play(null, null);
			}
		}

		private void _ListMouseDown(object sender, MouseButtonEventArgs e)
		{
			if (this._ListPlayList.SelectedIndex < -1)
				return;
			if (this._current != this._ListPlayList.SelectedIndex)
				this._Play(null, null);
			else
				this._Media.Restart();
		}

		private void _PauseUnpause(object sender, MouseButtonEventArgs e)
		{
			if (this._ListPlayList.SelectedIndex == -1)
				return;
			if (!this._Media.Paused)
			{
				this._Media.Pause();
				this._Media.Paused = true;
			}
			else
			{
				this._Media.Play();
				this._Media.Paused = false;
            }
		}

		private void _CheckedFrench(object sender, RoutedEventArgs e)
		{
			this._SwitchCulture();
		}

		private void _MenuOpen(object sender, RoutedEventArgs e)
		{
			System.Windows.Forms.OpenFileDialog browser;

			browser = new System.Windows.Forms.OpenFileDialog();
			browser.Multiselect = true;
			if (browser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				this._PlayList.PushPlaylist(browser.FileNames);
			this._ListPlayList.ItemsSource = this._PlayList.GetList();
		}

		private void _ClickQuit(object sender, RoutedEventArgs e)
		{
			this._PlayList.Dispose();
			this.Close();
		}

		#region [FULLSCREEN]
		private void _Elapsed(object source, ElapsedEventArgs e)
		{
			this._doubleclick.Stop();
			this._doubleclicked = false;
		}

        private void mainMediaElement_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!_doubleclicked)
            {
				this._doubleclicked = true;
				this._doubleclick.Start();
                return;
            }
			if (_doubleclicked)
            {
                if (this._fullscreen)
                {
                    this.WindowStyle = WindowStyle.None;
                    this.WindowState = WindowState.Maximized;
					this._ControlsRow.Height = new GridLength(0);
                    this._ListPlayList.Visibility = System.Windows.Visibility.Hidden;
                    this._Menu.Visibility = System.Windows.Visibility.Hidden;
                    this._Menu.Height = 0;
                    this._Menu.Width = 0;
                    this._Media.SetValue(Grid.RowSpanProperty, 2);
                    this._Media.SetValue(Grid.RowProperty, 0);
                }
                else
                {
                    this.WindowStyle = WindowStyle.SingleBorderWindow;
                    this.WindowState = WindowState.Normal;
					this._ControlsRow.Height = this._ControlsHeight;
                    this._ListPlayList.Visibility = System.Windows.Visibility.Visible;
                    this._Menu.Visibility = System.Windows.Visibility.Visible;
                    this._Menu.Height = 28;
                    this._Menu.Width = 524;
                    this._Media.SetValue(Grid.RowSpanProperty, 1);
                    this._Media.SetValue(Grid.RowProperty, 1);

                }
                this._fullscreen = !this._fullscreen;
            }
        }
        #endregion

        private void SliderVolumeChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this._Media.Volume = (double)_SliderVolume.Value;
        }

        private void _timer_Ticker(object sender, EventArgs e)
        {
            Dispatcher.Invoke(this._tick);
        }

        private void _changeDurationStatus()
        {
            Dispatcher.Invoke(new Action(() =>
            {
                this._LabelDuration.Content = this._Media.Position.ToString(@"hh\:mm\:ss");
                if (this._Media.NaturalDuration.HasTimeSpan)
                    this._LabelStatus.Content = this._Media.NaturalDuration.TimeSpan.ToString(@"hh\:mm\:ss");
            }));

            if (!this._isDraging)
            {
                if (this._Media.NaturalDuration.HasTimeSpan)
                {
                    this._SliderDuration.Maximum = this._Media.NaturalDuration.TimeSpan.TotalMilliseconds;
                }
                _SliderDuration.Value = this._Media.Position.TotalMilliseconds;
            }
        }

        private void _positionChanged(TimeSpan ts)
        {
            this._Media.Position = ts;
        }

		private void _SliderMouseDown(object sender, MouseButtonEventArgs e)
		{
            this._isDraging = true;
		}

		private void _SliderMouseUp(object sender, MouseButtonEventArgs e)
		{
            if (this._isDraging)
            {
                TimeSpan ts = new TimeSpan(0, 0, 0, 0, (int)_SliderDuration.Value);
                _positionChanged(ts);
            }
            this._isDraging = false;
		}

		private void _MediaEnded(object sender, RoutedEventArgs e)
		{
			this._Media.Stop();
			this._Next(null, null);
        }
	}
}
