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
using System.Speech.Recognition;

namespace ReconoceVoz
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private SpeechRecognitionEngine recorder = new SpeechRecognitionEngine();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Grabar_Click(object sender, RoutedEventArgs e)
        {
            //boton garbar:

            //Declaro el dispositivo
            recorder.SetInputToDefaultAudioDevice();
            //carga la gramatica
            recorder.LoadGrammar(new DictationGrammar());
            //
            recorder.SpeechRecognized += recorder_SpeechRecognized; //revisar aca
            recorder.RecognizeAsync(RecognizeMode.Single);




        }

        void recorder_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            foreach (RecognizedWordUnit word in e.Result.Words)
            {
                Resultados.Items.Add(word.Text);

                if (word.Text == "siguiente")
                {

                    Prueba p = new Prueba();
                    p.Show();
                    this.Close();
                    //MessageBox.Show("Esatas adentro, sos bienvenido");
                    //corder.RecognizeAsyncCancel();
                    //corder.SetInputToNull();

                }
                else
                {
                    MessageBox.Show("Usted no puede ingresar a la aplicacion");
                }

            }
        }
    }
}
