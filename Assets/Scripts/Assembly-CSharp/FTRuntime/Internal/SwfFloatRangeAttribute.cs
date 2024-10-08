using UnityEngine;

namespace FTRuntime.Internal
{
	public class SwfFloatRangeAttribute : PropertyAttribute
	{
		public float Min;

		public float Max;

		public SwfFloatRangeAttribute(float min, float max)
		{
			Min = min;
			Max = max;
		}
	}
}
