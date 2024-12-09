using System.Linq;
using System.Windows;
using System.Windows.Forms;

namespace Jw.MeetingCountdown.Extensions
{
    internal static class WindowExtensions
    {
        public static void MaximizeToPrimaryMonitor(this Window window)
        {
            var primaryScreen = Screen.AllScreens.FirstOrDefault(s => s.Primary);

            if (primaryScreen != null)
            {
                MaximizeWindow(window, primaryScreen);
            }
        }

        public static void MaximizeToSecondaryMonitor(this Window window)
        {
            var secondScreen = Screen.AllScreens.FirstOrDefault(s => !s.Primary);

            if (secondScreen != null)
            {
                MaximizeWindow(window, secondScreen);
            }
        }

        private static void MaximizeWindow(Window window, Screen screen)
        {
            if (!window.IsLoaded)
                window.WindowStartupLocation = WindowStartupLocation.Manual;

            var workingArea = screen.WorkingArea;
            window.Left = workingArea.Left;
            window.Top = workingArea.Top;
            window.Width = workingArea.Width;
            window.Height = workingArea.Height;

            if (window.IsLoaded)
                window.WindowState = WindowState.Maximized;
        }
    }
}
