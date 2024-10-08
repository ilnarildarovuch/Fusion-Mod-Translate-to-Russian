using System;
using FTRuntime.Internal;
using UnityEngine;

namespace FTRuntime
{
	[Serializable]
	public struct SwfSettingsData
	{
		public enum AtlasFilter
		{
			Point = 0,
			Bilinear = 1,
			Trilinear = 2
		}

		public enum AtlasFormat
		{
			AutomaticCompressed = 0,
			AutomaticTruecolor = 2
		}

		[SwfPowerOfTwoIf(5, 13, "AtlasPowerOfTwo")]
		public int MaxAtlasSize;

		[SwfIntRange(0, int.MaxValue)]
		public int AtlasPadding;

		[SwfFloatRange(float.Epsilon, float.MaxValue)]
		public float PixelsPerUnit;

		public bool BitmapTrimming;

		public bool GenerateMipMaps;

		public bool AtlasPowerOfTwo;

		public bool AtlasForceSquare;

		public AtlasFilter AtlasTextureFilter;

		public AtlasFormat AtlasTextureFormat;

		public static SwfSettingsData identity
		{
			get
			{
				SwfSettingsData result = default(SwfSettingsData);
				result.MaxAtlasSize = 2048;
				result.AtlasPadding = 1;
				result.PixelsPerUnit = 100f;
				result.BitmapTrimming = true;
				result.GenerateMipMaps = false;
				result.AtlasPowerOfTwo = true;
				result.AtlasForceSquare = true;
				result.AtlasTextureFilter = AtlasFilter.Bilinear;
				result.AtlasTextureFormat = AtlasFormat.AutomaticTruecolor;
				return result;
			}
		}

		public bool CheckEquals(SwfSettingsData other)
		{
			if (MaxAtlasSize == other.MaxAtlasSize && AtlasPadding == other.AtlasPadding && Mathf.Approximately(PixelsPerUnit, other.PixelsPerUnit) && BitmapTrimming == other.BitmapTrimming && GenerateMipMaps == other.GenerateMipMaps && AtlasPowerOfTwo == other.AtlasPowerOfTwo && AtlasForceSquare == other.AtlasForceSquare && AtlasTextureFilter == other.AtlasTextureFilter)
			{
				return AtlasTextureFormat == other.AtlasTextureFormat;
			}
			return false;
		}
	}
}
