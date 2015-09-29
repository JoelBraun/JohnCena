using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Devices.Gpio;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace JohnCena3
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private DispatcherTimer timer;
        private const int PB_PIN = 5;
        private GpioPin pin;
        private GpioPin pushbutton;
        private GpioPinValue pushButtonValue;
        public MainPage()
        {
            this.InitializeComponent();
            Uri uri = new Uri("ms-appx:///Assets/jc.wav");
            MElement.Source = uri;

            var gpio = GpioController.GetDefault();
            if (gpio == null)
            {
                pin = null;
                return;
            }
            pushbutton = gpio.OpenPin(PB_PIN);
            pushbutton.SetDriveMode(GpioPinDriveMode.Input);
            pushButtonValue = pushbutton.Read();
            pushbutton.ValueChanged += JOHNCENA; 



        }



        void JOHNCENA(GpioPin p, GpioPinValueChangedEventArgs e)
        {

            MElement.Play();

        }

    }
}