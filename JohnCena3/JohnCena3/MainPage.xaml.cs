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
using Windows.UI.Core;

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
                return;
            }
            pushbutton = gpio.OpenPin(PB_PIN);
            pushbutton.SetDriveMode(GpioPinDriveMode.Input);
            pushbutton.DebounceTimeout = TimeSpan.FromMilliseconds(50);

            pushbutton.ValueChanged += buttonPin_ValueChanged;
            /*
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += Timer_Tick;
            timer.Start();
            */
        }

    private void buttonPin_ValueChanged(GpioPin sender, GpioPinValueChangedEventArgs e)
    {
        // toggle the state of the LED every time the button is pressed
        if (e.Edge == GpioPinEdge.FallingEdge)
        {

        }

        // need to invoke UI updates on the UI thread because this event
        // handler gets invoked on a separate thread.
        var task = Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => {
            if (e.Edge == GpioPinEdge.FallingEdge)
            {
                MElement.Play();
            }

        });
    }


    private void Timer_Tick(object sender, object e)
        {
            pushButtonValue = pushbutton.Read();
            try {
                if (pushButtonValue == GpioPinValue.High)
                {
                    MElement.Play();
                }
            } catch (Exception ex)
            {

            }
        }

    }
}