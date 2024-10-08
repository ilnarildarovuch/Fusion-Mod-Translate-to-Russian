using UnityEngine;

namespace FTRuntime.Yields
{
	public class SwfWaitStopPlaying : CustomYieldInstruction
	{
		private SwfClipController _waitCtrl;

		public override bool keepWaiting => _waitCtrl != null;

		public SwfWaitStopPlaying(SwfClipController ctrl)
		{
			Subscribe(ctrl);
		}

		public SwfWaitStopPlaying Reuse(SwfClipController ctrl)
		{
			return Subscribe(ctrl);
		}

		private SwfWaitStopPlaying Subscribe(SwfClipController ctrl)
		{
			Unsubscribe();
			if ((bool)ctrl)
			{
				_waitCtrl = ctrl;
				ctrl.OnStopPlayingEvent += OnStopPlaying;
			}
			return this;
		}

		private void Unsubscribe()
		{
			if (_waitCtrl != null)
			{
				_waitCtrl.OnStopPlayingEvent -= OnStopPlaying;
				_waitCtrl = null;
			}
		}

		private void OnStopPlaying(SwfClipController ctrl)
		{
			Unsubscribe();
		}
	}
}
