using UnityEngine;

namespace FTRuntime.Yields
{
	public class SwfWaitStopOrRewindPlaying : CustomYieldInstruction
	{
		private SwfClipController _waitCtrl;

		public override bool keepWaiting => _waitCtrl != null;

		public SwfWaitStopOrRewindPlaying(SwfClipController ctrl)
		{
			Subscribe(ctrl);
		}

		public SwfWaitStopOrRewindPlaying Reuse(SwfClipController ctrl)
		{
			return Subscribe(ctrl);
		}

		private SwfWaitStopOrRewindPlaying Subscribe(SwfClipController ctrl)
		{
			Unsubscribe();
			if ((bool)ctrl)
			{
				_waitCtrl = ctrl;
				ctrl.OnStopPlayingEvent += OnStopOrRewindPlaying;
				ctrl.OnRewindPlayingEvent += OnStopOrRewindPlaying;
			}
			return this;
		}

		private void Unsubscribe()
		{
			if (_waitCtrl != null)
			{
				_waitCtrl.OnStopPlayingEvent -= OnStopOrRewindPlaying;
				_waitCtrl.OnRewindPlayingEvent -= OnStopOrRewindPlaying;
				_waitCtrl = null;
			}
		}

		private void OnStopOrRewindPlaying(SwfClipController ctrl)
		{
			Unsubscribe();
		}
	}
}
