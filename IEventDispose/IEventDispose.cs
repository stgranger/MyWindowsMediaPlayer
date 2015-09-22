using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEventDispose
{
	public interface IEventDispose: IDisposable
	{

		Boolean IsDisposed { get; }
		event EventHandler OnDispose;
		event EventHandler OnExpliciteDispose;
		event EventHandler GCDispose;
	}
}
