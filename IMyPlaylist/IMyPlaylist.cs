using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWindowsMediaPlayer
{
	public interface IMyPlaylist: IDisposable
	{
		void PushPlaylist(string[] files);
		string Current();
		string Next();
		string Previous();
		void Remove(int elem);
	}
}
