using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Launcher
{
	class Program
	{
		[STAThread]
		static void Main(string[] args)
		{
			MyWindowsMediaPlayer.MainWindow MainWin;

			MainWin = new MyWindowsMediaPlayer.MainWindow();
			MainWin.ShowDialog();
		}
	}
}
