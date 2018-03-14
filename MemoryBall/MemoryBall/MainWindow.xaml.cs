using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using static MemoryBall.SafeNativeMethods;

namespace MemoryBall
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private System.Timers.Timer infoUpdatetimer;
        private MEMORYSTATUSEX mEMORYSTATUSEX;
        private MemoryInfo memoryInfo;

        public MainWindow()
        {
            InitializeComponent();
            mEMORYSTATUSEX = new MEMORYSTATUSEX();
            mEMORYSTATUSEX.dwLength = (uint)Marshal.SizeOf(mEMORYSTATUSEX);
            memoryInfo = new MemoryInfo();
            MainGrid.DataContext = memoryInfo;
            infoUpdatetimer = new System.Timers.Timer(1000);
            infoUpdatetimer.Elapsed += InfoUpdatetimer_Elapsed;
            infoUpdatetimer.Start();
        }

        private void InfoUpdatetimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            GlobalMemoryStatusEx(out mEMORYSTATUSEX);
            memoryInfo.MemLoad = mEMORYSTATUSEX.dwMemoryLoad;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => DragMove();
    }
}
