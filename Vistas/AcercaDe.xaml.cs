using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.IO;

namespace Vistas
{
    /// <summary>
    /// Interaction logic for AcercaDe.xaml
    /// </summary>
    public partial class AcercaDe : Window
    {
        DispatcherTimer timer;
        bool isDragging;

        public AcercaDe()
        {
            InitializeComponent();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new EventHandler(timer_Tick);

            isDragging = false;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (!isDragging)
            {
                sldVideo.Value = video.Position.TotalSeconds;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            video.LoadedBehavior = MediaState.Manual;
            video.UnloadedBehavior = MediaState.Stop;
            var CurrentDirectory = Directory.GetCurrentDirectory();
            var VistasDirectory = CurrentDirectory.Substring(0, CurrentDirectory.Length - 9);
            video.Source = new Uri(VistasDirectory + "\\media\\Wildlife.wmv");
            //video.Source = new Uri(@"C:\Users\admin\Documents\LPOOII_GRUPO05\Vistas\media\Wildlife.wmv");
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            video.Play();
        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            video.Pause();
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            video.Stop();
            sldVideo.Value = 0;
        }

        private void video_MediaOpened(object sender, RoutedEventArgs e)
        {
            if (video.NaturalDuration.HasTimeSpan)
            {
                TimeSpan ts = video.NaturalDuration.TimeSpan;
                sldVideo.Maximum = ts.TotalSeconds;
                sldVideo.SmallChange = 1;
            }

            timer.Start();
        }

        private void video_MediaEnded(object sender, RoutedEventArgs e)
        {
            sldVideo.Value = 0;
        }

        private void sldVideo_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            isDragging = false;
            video.Position = TimeSpan.FromSeconds(sldVideo.Value);
        }

        private void sldVideo_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            isDragging = true;
        }

        private void sldVideo_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            video.Position = TimeSpan.FromSeconds(sldVideo.Value);
        }
    }
}
