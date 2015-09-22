using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyWindowsMediaPlayer;

namespace MyWindowsMediaPlayer
{
	[Serializable]
	public class MyPlaylist: IMyPlaylist
	{
		private bool _disposed;
		private int _current;
		private List<string> _playlist;

		public MyPlaylist()
		{
			this._playlist = new List<string>();
			this._disposed = false;
			this._current = 0;
		}

		public void PushPlaylist(string[] files)
		{
			foreach (string path in files)
			{
				if (File.Exists(path))
					this._playlist.Add(path);
				else
					this.PushPlaylist(Directory.GetFiles(path));
			}
			this._current = this._playlist.Count;
		}

		public string Current()
		{
			return (this._playlist[this._current]);
		}

		public string Next()
		{
			this._current++;
			if (this._current > this._playlist.Count)
				this._current = 0;
			return (this._playlist[this._current]);
		}

		public string Previous()
		{
			this._current--;
			if (this._current < 0)
				this._current = this._playlist.Count;
			return (this._playlist[this._current]);
		}

		public string[] GetList()
		{
			string[] list;
			int i;

			list = new string[this._playlist.Count];
			i = -1;
			while (this._playlist.Count > ++i)
				list[i] = this._playlist[i];
			return (list);
		}

		public void Remove(int elem)
		{
			this._playlist.RemoveAt(elem);
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			if (!this._disposed)
				if (disposing)
				{
					this._playlist.Clear();
					this._disposed = true;
				}
		}
	}
}
