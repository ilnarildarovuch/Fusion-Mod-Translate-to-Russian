namespace FTRuntime.Yields
{
	public static class SwfWaitExtensions
	{
		public static SwfWaitStopPlaying WaitForStopPlaying(this SwfClipController ctrl)
		{
			return new SwfWaitStopPlaying(ctrl);
		}

		public static SwfWaitRewindPlaying WaitForRewindPlaying(this SwfClipController ctrl)
		{
			return new SwfWaitRewindPlaying(ctrl);
		}

		public static SwfWaitStopOrRewindPlaying WaitForStopOrRewindPlaying(this SwfClipController ctrl)
		{
			return new SwfWaitStopOrRewindPlaying(ctrl);
		}

		public static SwfWaitPlayStopped WaitForPlayStopped(this SwfClipController ctrl)
		{
			return new SwfWaitPlayStopped(ctrl);
		}

		public static SwfWaitStopPlaying PlayAndWaitStop(this SwfClipController ctrl, bool rewind)
		{
			ctrl.Play(rewind);
			return ctrl.WaitForStopPlaying();
		}

		public static SwfWaitStopPlaying PlayAndWaitStop(this SwfClipController ctrl, string sequence)
		{
			ctrl.Play(sequence);
			return ctrl.WaitForStopPlaying();
		}

		public static SwfWaitRewindPlaying PlayAndWaitRewind(this SwfClipController ctrl, bool rewind)
		{
			ctrl.Play(rewind);
			return ctrl.WaitForRewindPlaying();
		}

		public static SwfWaitRewindPlaying PlayAndWaitRewind(this SwfClipController ctrl, string sequence)
		{
			ctrl.Play(sequence);
			return ctrl.WaitForRewindPlaying();
		}

		public static SwfWaitStopOrRewindPlaying PlayAndWaitStopOrRewind(this SwfClipController ctrl, bool rewind)
		{
			ctrl.Play(rewind);
			return ctrl.WaitForStopOrRewindPlaying();
		}

		public static SwfWaitStopOrRewindPlaying PlayAndWaitStopOrRewind(this SwfClipController ctrl, string sequence)
		{
			ctrl.Play(sequence);
			return ctrl.WaitForStopOrRewindPlaying();
		}

		public static SwfWaitStopPlaying GotoAndPlayAndWaitStop(this SwfClipController ctrl, int frame)
		{
			ctrl.GotoAndPlay(frame);
			return ctrl.WaitForStopPlaying();
		}

		public static SwfWaitStopPlaying GotoAndPlayAndWaitStop(this SwfClipController ctrl, string sequence, int frame)
		{
			ctrl.GotoAndPlay(sequence, frame);
			return ctrl.WaitForStopPlaying();
		}

		public static SwfWaitRewindPlaying GotoAndPlayAndWaitRewind(this SwfClipController ctrl, int frame)
		{
			ctrl.GotoAndPlay(frame);
			return ctrl.WaitForRewindPlaying();
		}

		public static SwfWaitRewindPlaying GotoAndPlayAndWaitRewind(this SwfClipController ctrl, string sequence, int frame)
		{
			ctrl.GotoAndPlay(sequence, frame);
			return ctrl.WaitForRewindPlaying();
		}

		public static SwfWaitStopOrRewindPlaying GotoAndPlayAndWaitStopOrRewind(this SwfClipController ctrl, int frame)
		{
			ctrl.GotoAndPlay(frame);
			return ctrl.WaitForStopOrRewindPlaying();
		}

		public static SwfWaitStopOrRewindPlaying GotoAndPlayAndWaitStopOrRewind(this SwfClipController ctrl, string sequence, int frame)
		{
			ctrl.GotoAndPlay(sequence, frame);
			return ctrl.WaitForStopOrRewindPlaying();
		}

		public static SwfWaitPlayStopped StopAndWaitPlay(this SwfClipController ctrl, bool rewind)
		{
			ctrl.Stop(rewind);
			return ctrl.WaitForPlayStopped();
		}

		public static SwfWaitPlayStopped StopAndWaitPlay(this SwfClipController ctrl, string sequence)
		{
			ctrl.Stop(sequence);
			return ctrl.WaitForPlayStopped();
		}

		public static SwfWaitPlayStopped GotoAndStopAndWaitPlay(this SwfClipController ctrl, int frame)
		{
			ctrl.GotoAndStop(frame);
			return ctrl.WaitForPlayStopped();
		}

		public static SwfWaitPlayStopped GotoAndStopAndWaitPlay(this SwfClipController ctrl, string sequence, int frame)
		{
			ctrl.GotoAndStop(sequence, frame);
			return ctrl.WaitForPlayStopped();
		}
	}
}
