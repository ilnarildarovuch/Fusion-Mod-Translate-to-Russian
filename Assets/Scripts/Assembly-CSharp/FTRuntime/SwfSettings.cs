using UnityEngine;

namespace FTRuntime
{
	[CreateAssetMenu(fileName = "SwfSettings", menuName = "FlashTools/SwfSettings", order = 100)]
	public class SwfSettings : ScriptableObject
	{
		public SwfSettingsData Settings;

		private void Reset()
		{
			Settings = SwfSettingsData.identity;
		}
	}
}
