using ADC.Managers;
using ADC.Screens.Error;
using ADC.Screens.SplashScreen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace ADC
{
    internal static class Program
    {
        public static MainScreen mainScreen { get; private set; }
        public static SplashScreen splashScreen { get; private set; }
        public static IniMaster iniFile { get; private set; }
        public static SQLMaster sqlMaster { get; private set; }
        public static CryptMaster cryptMaster { get; private set; }

        //static System.Windows.Forms.Timer programTime = new System.Windows.Forms.Timer();
        //public static double programTicks = 0;


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(ErrorHandler);


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            splashScreen = new SplashScreen();
            splashScreen.Show();

            //programTime.Tick += new EventHandler(TimerEventProcessor);
            //programTime.Interval = 1000;
            //programTime.Start();

            iniFile = new IniMaster();
            
            cryptMaster = new CryptMaster();

            if (iniFile.KeyExists("ConnectionString") == false)
            {
                InitialSetup();
            }

            sqlMaster = new SQLMaster();

            splashScreen.Hide();
            Application.Run(mainScreen = new MainScreen());
        }
        
        private static void InitialSetup()
        {
            iniFile.Write("ConnectionString", @"Data Source=.\SQLExpress;Initial Catalog=ADCDB;User ID=sa;Password=password");
        }

        static void ErrorHandler(object sender, UnhandledExceptionEventArgs args)
        {
            Exception e = (Exception)args.ExceptionObject;
            Console.WriteLine("MyHandler caught : " + e.Message);

            ErrorScreen err = new ErrorScreen(e);
            err.ShowDialog();
        }

        //private static void TimerEventProcessor(Object myObject, EventArgs myEventArgs)
        //{
        //    programTicks++;
        //}
    }
}
