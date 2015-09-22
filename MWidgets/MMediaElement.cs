using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MWidgets
{
	public class MMediaElement: System.Windows.Controls.MediaElement
	{
		public bool Paused;
		public bool Repeat;

		public void Load(string path)
		{
			this.Source = new Uri(path);
		}

		public void Load(Uri uri)
		{
			this.Source = uri;
		}

		public void Restart()
		{
			this.Stop();
			this.Play();
		}
	}
}
