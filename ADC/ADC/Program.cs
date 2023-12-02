using ADC.Managers;
using ADC.Screens.ConnectionScreen;
using ADC.Screens.Error;
using ADC.Screens.SplashScreen;
using ADC.Screens.SystemScreens.Loading;
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
        public static Loading loadingScreen { get; private set; }
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
            
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(FatalErrorHandler);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.Automatic);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            splashScreen = new SplashScreen();
            splashScreen.Show();

            loadingScreen = new Loading();

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
            ConnectionScreen connectionScreen = new ConnectionScreen();
            connectionScreen.ShowDialog();
            
            
        }

        public static void ErrorHandler(Exception e)
        {
            ErrorScreen err = new ErrorScreen(e);
            err.ShowDialog();
        }

        static void FatalErrorHandler(object sender, UnhandledExceptionEventArgs args)
        {
            Exception e = (Exception)args.ExceptionObject;

            ErrorScreen err = new ErrorScreen(e, true);
            err.ShowDialog();
        }

        //private static void TimerEventProcessor(Object myObject, EventArgs myEventArgs)
        //{
        //    programTicks++;
        //}
    }
}
