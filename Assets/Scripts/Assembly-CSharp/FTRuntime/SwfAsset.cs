using FTRuntime.Internal;
using UnityEngine;

namespace FTRuntime
{
	[PreferBinarySerialization]
	public class SwfAsset : ScriptableObject
	{
		[HideInInspector]
		public byte[] Data;

		[HideInInspector]
		public string Hash;

		[SwfReadOnly]
		public Texture2D Atlas;

		[HideInInspector]
		public SwfSettingsData Settings;

		[SwfDisplayName("Settings")]
		public SwfSettingsData Overridden;

		private void Reset()
		{
			Data = new byte[0];
			Hash = string.Empty;
			Atlas = null;
			Settings = SwfSettingsData.identity;
			Overridden = SwfSettingsData.identity;
		}
	}
}
