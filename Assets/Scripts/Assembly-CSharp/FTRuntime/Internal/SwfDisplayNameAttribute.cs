using UnityEngine;

namespace FTRuntime.Internal
{
	public class SwfDisplayNameAttribute : PropertyAttribute
	{
		public string DisplayName;

		public SwfDisplayNameAttribute(string display_name)
		{
			DisplayName = display_name;
		}
	}
}
