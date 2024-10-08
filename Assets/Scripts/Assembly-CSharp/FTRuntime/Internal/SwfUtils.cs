using System.Collections.Generic;
using UnityEngine;

namespace FTRuntime.Internal
{
	internal static class SwfUtils
	{
		private static class ShaderPropsCache
		{
			public static int TintShaderProp;

			public static int MainTexShaderProp;

			public static int AlphaTexShaderProp;

			public static int ExternalAlphaShaderProp;

			private static bool _initialized;

			public static void LazyInitialize()
			{
				if (!_initialized)
				{
					_initialized = true;
					TintShaderProp = Shader.PropertyToID("_Tint");
					MainTexShaderProp = Shader.PropertyToID("_MainTex");
					AlphaTexShaderProp = Shader.PropertyToID("_AlphaTex");
					ExternalAlphaShaderProp = Shader.PropertyToID("_ExternalAlpha");
				}
			}
		}

		private static class GeneratedMeshCache
		{
			private const int PreallocatedVertices = 500;

			public static List<int> Indices = new List<int>(750);

			private static Vector3 Vertex = Vector3.zero;

			public static List<Vector3> Vertices = new List<Vector3>(500);

			private static Vector2 UV0 = Vector2.zero;

			private static Vector2 UV1 = Vector2.zero;

			private static Vector2 UV2 = Vector2.zero;

			private static Vector2 UV3 = Vector2.zero;

			public static List<Vector2> UVs = new List<Vector2>(500);

			private static Vector4 AddColor = Vector4.one;

			public static List<Vector4> AddColors = new List<Vector4>(500);

			private static Color MulColor = Color.white;

			public static List<Color> MulColors = new List<Color>(500);

			public static void FillTriangles(int start_vertex, int index_count)
			{
				Indices.Clear();
				if (Indices.Capacity < index_count)
				{
					Indices.Capacity = index_count * 2;
				}
				for (int i = 0; i < index_count; i += 6)
				{
					Indices.Add(start_vertex + 2);
					Indices.Add(start_vertex + 1);
					Indices.Add(start_vertex);
					Indices.Add(start_vertex);
					Indices.Add(start_vertex + 3);
					Indices.Add(start_vertex + 2);
					start_vertex += 4;
				}
			}

			public static void FillVertices(Vector2[] vertices)
			{
				Vertices.Clear();
				if (Vertices.Capacity < vertices.Length)
				{
					Vertices.Capacity = vertices.Length * 2;
				}
				int i = 0;
				for (int num = vertices.Length; i < num; i++)
				{
					Vector2 vector = vertices[i];
					Vertex.x = vector.x;
					Vertex.y = vector.y;
					Vertices.Add(Vertex);
				}
			}

			public static void FillUVs(uint[] uvs)
			{
				UVs.Clear();
				if (UVs.Capacity < uvs.Length * 2)
				{
					UVs.Capacity = uvs.Length * 2 * 2;
				}
				int i = 0;
				for (int num = uvs.Length; i < num; i += 2)
				{
					UnpackUV(uvs[i], out var u, out var v);
					UnpackUV(uvs[i + 1], out var u2, out var v2);
					UV0.x = u;
					UV0.y = v;
					UV1.x = u2;
					UV1.y = v;
					UV2.x = u2;
					UV2.y = v2;
					UV3.x = u;
					UV3.y = v2;
					UVs.Add(UV0);
					UVs.Add(UV1);
					UVs.Add(UV2);
					UVs.Add(UV3);
				}
			}

			public static void FillAddColors(uint[] colors)
			{
				AddColors.Clear();
				if (AddColors.Capacity < colors.Length * 2)
				{
					AddColors.Capacity = colors.Length * 2 * 2;
				}
				int i = 0;
				for (int num = colors.Length; i < num; i += 2)
				{
					UnpackFColorFromUInts(colors[i], colors[i + 1], out AddColor.x, out AddColor.y, out AddColor.z, out AddColor.w);
					AddColors.Add(AddColor);
					AddColors.Add(AddColor);
					AddColors.Add(AddColor);
					AddColors.Add(AddColor);
				}
			}

			public static void FillMulColors(uint[] colors)
			{
				MulColors.Clear();
				if (MulColors.Capacity < colors.Length * 2)
				{
					MulColors.Capacity = colors.Length * 2 * 2;
				}
				int i = 0;
				for (int num = colors.Length; i < num; i += 2)
				{
					UnpackFColorFromUInts(colors[i], colors[i + 1], out MulColor.r, out MulColor.g, out MulColor.b, out MulColor.a);
					MulColors.Add(MulColor);
					MulColors.Add(MulColor);
					MulColors.Add(MulColor);
					MulColors.Add(MulColor);
				}
			}
		}

		private const ushort UShortMax = ushort.MaxValue;

		private const float FColorPrecision = 0.001953125f;

		private const float InvFColorPrecision = 512f;

		public static int TintShaderProp
		{
			get
			{
				ShaderPropsCache.LazyInitialize();
				return ShaderPropsCache.TintShaderProp;
			}
		}

		public static int MainTexShaderProp
		{
			get
			{
				ShaderPropsCache.LazyInitialize();
				return ShaderPropsCache.MainTexShaderProp;
			}
		}

		public static int AlphaTexShaderProp
		{
			get
			{
				ShaderPropsCache.LazyInitialize();
				return ShaderPropsCache.AlphaTexShaderProp;
			}
		}

		public static int ExternalAlphaShaderProp
		{
			get
			{
				ShaderPropsCache.LazyInitialize();
				return ShaderPropsCache.ExternalAlphaShaderProp;
			}
		}

		public static void UnpackUV(uint pack, out float u, out float v)
		{
			u = (float)((pack >> 16) & 0xFFFFu) / 65535f;
			v = (float)(pack & 0xFFFFu) / 65535f;
		}

		public static void UnpackFColorFromUInts(uint pack0, uint pack1, out float c0, out float c1, out float c2, out float c3)
		{
			c0 = (float)(short)((pack0 >> 16) & 0xFFFF) / 512f;
			c1 = (float)(short)(pack0 & 0xFFFF) / 512f;
			c2 = (float)(short)((pack1 >> 16) & 0xFFFF) / 512f;
			c3 = (float)(short)(pack1 & 0xFFFF) / 512f;
		}

		public static T GetComponent<T>(GameObject obj, bool allow_create) where T : Component
		{
			T val = obj.GetComponent<T>();
			if (allow_create && !val)
			{
				val = obj.AddComponent<T>();
			}
			return val;
		}

		public static void FillGeneratedMesh(Mesh mesh, SwfClipAsset.MeshData mesh_data)
		{
			if (mesh_data.SubMeshes.Length != 0)
			{
				mesh.subMeshCount = mesh_data.SubMeshes.Length;
				GeneratedMeshCache.FillVertices(mesh_data.Vertices);
				mesh.SetVertices(GeneratedMeshCache.Vertices);
				int i = 0;
				for (int num = mesh_data.SubMeshes.Length; i < num; i++)
				{
					GeneratedMeshCache.FillTriangles(mesh_data.SubMeshes[i].StartVertex, mesh_data.SubMeshes[i].IndexCount);
					mesh.SetTriangles(GeneratedMeshCache.Indices, i);
				}
				GeneratedMeshCache.FillUVs(mesh_data.UVs);
				mesh.SetUVs(0, GeneratedMeshCache.UVs);
				GeneratedMeshCache.FillAddColors(mesh_data.AddColors);
				mesh.SetUVs(1, GeneratedMeshCache.AddColors);
				GeneratedMeshCache.FillMulColors(mesh_data.MulColors);
				mesh.SetColors(GeneratedMeshCache.MulColors);
			}
		}
	}
}
