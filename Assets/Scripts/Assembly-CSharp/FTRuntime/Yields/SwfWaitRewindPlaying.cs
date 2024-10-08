using UnityEngine;

namespace FTRuntime.Yields
{
	public class SwfWaitRewindPlaying : CustomYieldInstruction
	{
		private SwfClipController _waitCtrl;

		public override bool keepWaiting => _waitCtrl != null;

		public SwfWaitRewindPlaying(SwfClipController ctrl)
		{
			Subscribe(ctrl);
		}

		public SwfWaitRewindPlaying Reuse(SwfClipController ctrl)
		{
			return Subscribe(ctrl);
		}

		private SwfWaitRewindPlaying Subscribe(SwfClipController ctrl)
		{
			Unsubscribe();
			if ((bool)ctrl)
			{
				_waitCtrl = ctrl;
				ctrl.OnRewindPlayingEvent += OnRewindPlaying;
			}
			return this;
		}

		private void Unsubscribe()
		{
			if (_waitCtrl != null)
			{
				_waitCtrl.OnRewindPlayingEvent -= OnRewindPlaying;
				_waitCtrl = null;
			}
		}

		private void OnRewindPlaying(SwfClipController ctrl)
		{
			Unsubscribe();
		}
	}
}
