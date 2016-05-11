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
using System.Windows.Shapes;
using Microsoft.Kinect;
using Microsoft.Kinect.Toolkit;

namespace ReconoceVoz
{
    /// <summary>
    /// Interaction logic for Prueba.xaml
    /// </summary>
    public partial class Prueba : Window
    {
          KinectSensorChooser miKinect;  //verifico  cuantos kinect  estan  conectados
        public Prueba()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            miKinect = new KinectSensorChooser();                   //creo objeto
            miKinect.KinectChanged += miKinect_KinectChanged;       //si se deconecto un kinect o s einicializo
            sensorChooserUI.KinectSensorChooser = miKinect; //verifica si esta conectado o desconectado en pantalla
            miKinect.Start();
        }

        void miKinect_KinectChanged(object sender, KinectChangedEventArgs e)
        {

            //se desconecto el kinect
            bool error = true;                                                          //verifico error
            if (e.OldSensor == null)
            {                                                      // se desconecto
                try
                {
                    e.OldSensor.DepthStream.Disable();                                      //desabilito los streams  que habia habilitado
                    e.OldSensor.SkeletonStream.Disable();
                }
                catch (Exception)
                {

                    error = true;                                                           //error  es igual a true
                }
            }

            if (e.NewSensor == null)
                return;
            try
            {
                e.NewSensor.DepthStream.Enable(DepthImageFormat.Resolution640x480Fps30);            //asigno el formato del strin de profundidad 640*480
                e.NewSensor.SkeletonStream.Enable();
                try
                {
                    e.NewSensor.SkeletonStream.TrackingMode = SkeletonTrackingMode.Seated;          //se habilita
                    e.NewSensor.DepthStream.Range = DepthRange.Near;                                  //  habilito el nearmode solo en string de profundidad mas rango de datos
                    e.NewSensor.SkeletonStream.EnableTrackingInNearRange = true;                      // se habilita y se hace verdadera  (solo para kinect de windows)
                }
                catch (InvalidOperationException)
                {

                    e.NewSensor.DepthStream.Range = DepthRange.Default;                                  //desactivo esos modos de range   
                    e.NewSensor.SkeletonStream.EnableTrackingInNearRange = false;
                }
            }
            catch (InvalidOperationException)
            {
                error = true;

            }

            ZonaCursor.KinectSensor = e.NewSensor;
        }

        private void KinectTileButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Muay Thai is the melody of my life");
        }

        private void KinectTileButton_Click_1(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(); //salir
        }


    }
}
