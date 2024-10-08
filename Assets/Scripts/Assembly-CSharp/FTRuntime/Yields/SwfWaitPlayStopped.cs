using UnityEngine;

namespace FTRuntime.Yields
{
	public class SwfWaitPlayStopped : CustomYieldInstruction
	{
		private SwfClipController _waitCtrl;

		public override bool keepWaiting => _waitCtrl != null;

		public SwfWaitPlayStopped(SwfClipController ctrl)
		{
			Subscribe(ctrl);
		}

		public SwfWaitPlayStopped Reuse(SwfClipController ctrl)
		{
			return Subscribe(ctrl);
		}

		private SwfWaitPlayStopped Subscribe(SwfClipController ctrl)
		{
			Unsubscribe();
			if ((bool)ctrl)
			{
				_waitCtrl = ctrl;
				ctrl.OnPlayStoppedEvent += OnPlayStopped;
			}
			return this;
		}

		private void Unsubscribe()
		{
			if (_waitCtrl != null)
			{
				_waitCtrl.OnPlayStoppedEvent -= OnPlayStopped;
				_waitCtrl = null;
			}
		}

		private void OnPlayStopped(SwfClipController ctrl)
		{
			Unsubscribe();
		}
	}
}
