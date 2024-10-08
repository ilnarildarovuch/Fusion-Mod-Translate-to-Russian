using UnityEngine;

namespace FTRuntime.Internal
{
	public class SwfIntRangeAttribute : PropertyAttribute
	{
		public int Min;

		public int Max;

		public SwfIntRangeAttribute(int min, int max)
		{
			Min = min;
			Max = max;
		}
	}
}
