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
using Microsoft.Win32;

namespace Vistas
{
    /// <summary>
    /// Interaction logic for FormPresentacion.xaml
    /// </summary>
    public partial class FormPresentacion : Window
    {
        public FormPresentacion()
        {
            InitializeComponent();
            myAudio.LoadedBehavior = MediaState.Manual;
        }

        private void btnLoadSong_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileMp3 = new OpenFileDialog();
            fileMp3.Filter = "Musica mp3(*.mp3)|*.mp3";
            string pathMp3;

            if (fileMp3.ShowDialog()==true) 
            {
                pathMp3 = fileMp3.FileName;
                lblPathSong.Content += pathMp3;
                myAudio.LoadedBehavior = MediaState.Manual;
                myAudio.Source = new Uri(pathMp3);
            }
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            myAudio.Play();
        }

        private void btnPausa_Click(object sender, RoutedEventArgs e)
        {
            myAudio.Pause();
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            myAudio.Stop();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            FormLogin login = new FormLogin();
            login.Show();
            this.Close();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
    }
}
