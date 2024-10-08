using System;
using System.Collections.Generic;
using FTRuntime.Internal;
using UnityEngine;
using UnityEngine.Rendering;

namespace FTRuntime
{
	[AddComponentMenu("FlashTools/SwfClip")]
	[ExecuteInEditMode]
	[DisallowMultipleComponent]
	[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(SortingGroup))]
	public class SwfClip : MonoBehaviour
	{
		private MeshFilter _meshFilter;

		private MeshRenderer _meshRenderer;

		private SortingGroup _sortingGroup;

		private bool _dirtyMesh = true;

		private SwfClipAsset.Sequence _curSequence;

		private MaterialPropertyBlock _curPropBlock;

		[Header("Sorting")]
		[SerializeField]
		[SwfSortingLayer]
		private string _sortingLayer = string.Empty;

		[SerializeField]
		private int _sortingOrder;

		[Header("Animation")]
		[SerializeField]
		private Color _tint = Color.white;

		[SerializeField]
		private SwfClipAsset _clip;

		[SerializeField]
		[HideInInspector]
		private string _sequence = string.Empty;

		[SerializeField]
		[HideInInspector]
		private int _currentFrame;

		public string sortingLayer
		{
			get
			{
				return _sortingLayer;
			}
			set
			{
				_sortingLayer = value;
				ChangeSortingProperties();
			}
		}

		public int sortingOrder
		{
			get
			{
				return _sortingOrder;
			}
			set
			{
				_sortingOrder = value;
				ChangeSortingProperties();
			}
		}

		public Color tint
		{
			get
			{
				return _tint;
			}
			set
			{
				_tint = value;
				ChangeTint();
			}
		}

		public SwfClipAsset clip
		{
			get
			{
				return _clip;
			}
			set
			{
				_clip = value;
				_sequence = string.Empty;
				_currentFrame = 0;
				ChangeClip();
				EmitChangeEvents(clip: true, sequence: true, current_frame: true);
			}
		}

		public string sequence
		{
			get
			{
				return _sequence;
			}
			set
			{
				_sequence = value;
				_currentFrame = 0;
				ChangeSequence();
				EmitChangeEvents(clip: false, sequence: true, current_frame: true);
			}
		}

		public int currentFrame
		{
			get
			{
				return _currentFrame;
			}
			set
			{
				_currentFrame = value;
				ChangeCurrentFrame();
				EmitChangeEvents(clip: false, sequence: false, current_frame: true);
			}
		}

		public int frameCount
		{
			get
			{
				if (_curSequence == null || _curSequence.Frames == null)
				{
					return 0;
				}
				return _curSequence.Frames.Count;
			}
		}

		public float frameRate
		{
			get
			{
				if (!clip)
				{
					return 0f;
				}
				return clip.FrameRate;
			}
		}

		public int currentLabelCount
		{
			get
			{
				string[] array = GetCurrentBakedFrame()?.Labels;
				if (array == null)
				{
					return 0;
				}
				return array.Length;
			}
		}

		public Bounds currentLocalBounds => GetCurrentBakedFrame()?.CachedMesh.bounds ?? default(Bounds);

		public Bounds currentWorldBounds
		{
			get
			{
				Internal_UpdateMesh();
				if (!_meshRenderer)
				{
					return default(Bounds);
				}
				return _meshRenderer.bounds;
			}
		}

		public event Action<SwfClip> OnChangeClipEvent;

		public event Action<SwfClip> OnChangeSequenceEvent;

		public event Action<SwfClip> OnChangeCurrentFrameEvent;

		public void ToBeginFrame()
		{
			currentFrame = 0;
		}

		public void ToEndFrame()
		{
			currentFrame = ((frameCount > 0) ? (frameCount - 1) : 0);
		}

		public bool ToPrevFrame()
		{
			if (currentFrame > 0)
			{
				int num = currentFrame - 1;
				currentFrame = num;
				return true;
			}
			return false;
		}

		public bool ToNextFrame()
		{
			if (currentFrame < frameCount - 1)
			{
				int num = currentFrame + 1;
				currentFrame = num;
				return true;
			}
			return false;
		}

		public string GetCurrentFrameLabel(int index)
		{
			string[] array = GetCurrentBakedFrame()?.Labels;
			if (array == null || index < 0 || index >= array.Length)
			{
				return string.Empty;
			}
			return array[index];
		}

		internal void Internal_UpdateMesh()
		{
			if ((bool)_meshFilter && (bool)_meshRenderer && _dirtyMesh)
			{
				SwfClipAsset.Frame currentBakedFrame = GetCurrentBakedFrame();
				if (currentBakedFrame != null)
				{
					_meshFilter.sharedMesh = currentBakedFrame.CachedMesh;
					_meshRenderer.sharedMaterials = currentBakedFrame.Materials;
				}
				else
				{
					_meshFilter.sharedMesh = null;
					_meshRenderer.sharedMaterials = new Material[0];
				}
				_dirtyMesh = false;
			}
		}

		public void Internal_UpdateAllProperties()
		{
			ClearCache(allow_to_create_components: false);
			ChangeTint();
			ChangeClip();
			ChangeSequence();
			ChangeCurrentFrame();
			ChangeSortingProperties();
		}

		private void ClearCache(bool allow_to_create_components)
		{
			_meshFilter = SwfUtils.GetComponent<MeshFilter>(base.gameObject, allow_to_create_components);
			_meshRenderer = SwfUtils.GetComponent<MeshRenderer>(base.gameObject, allow_to_create_components);
			_sortingGroup = SwfUtils.GetComponent<SortingGroup>(base.gameObject, allow_to_create_components);
			_dirtyMesh = true;
			_curSequence = null;
			_curPropBlock = null;
		}

		private void ChangeTint()
		{
			UpdatePropBlock();
		}

		private void ChangeClip()
		{
			if ((bool)_meshRenderer)
			{
				_meshRenderer.enabled = clip;
			}
			ChangeSequence();
			UpdatePropBlock();
		}

		private void ChangeSequence()
		{
			_curSequence = null;
			if ((bool)clip && clip.Sequences != null)
			{
				if (!string.IsNullOrEmpty(this.sequence))
				{
					int i = 0;
					for (int count = clip.Sequences.Count; i < count; i++)
					{
						SwfClipAsset.Sequence sequence = clip.Sequences[i];
						if (sequence != null && sequence.Name == this.sequence)
						{
							_curSequence = sequence;
							break;
						}
					}
					if (_curSequence == null)
					{
						Debug.LogWarningFormat(this, "<b>[FlashTools]</b> Sequence '{0}' not found", this.sequence);
					}
				}
				if (_curSequence == null)
				{
					int j = 0;
					for (int count2 = clip.Sequences.Count; j < count2; j++)
					{
						SwfClipAsset.Sequence sequence2 = clip.Sequences[j];
						if (sequence2 != null)
						{
							_sequence = sequence2.Name;
							_curSequence = sequence2;
							break;
						}
					}
				}
			}
			ChangeCurrentFrame();
		}

		private void ChangeCurrentFrame()
		{
			_dirtyMesh = true;
			_currentFrame = ((frameCount > 0) ? Mathf.Clamp(currentFrame, 0, frameCount - 1) : 0);
		}

		private void ChangeSortingProperties()
		{
			if ((bool)_meshRenderer)
			{
				_meshRenderer.sortingOrder = sortingOrder;
				_meshRenderer.sortingLayerName = sortingLayer;
			}
			if ((bool)_sortingGroup)
			{
				_sortingGroup.sortingOrder = sortingOrder;
				_sortingGroup.sortingLayerName = sortingLayer;
			}
		}

		private void UpdatePropBlock()
		{
			if ((bool)_meshRenderer)
			{
				if (_curPropBlock == null)
				{
					_curPropBlock = new MaterialPropertyBlock();
				}
				_meshRenderer.GetPropertyBlock(_curPropBlock);
				_curPropBlock.SetColor(SwfUtils.TintShaderProp, tint);
				Sprite sprite = (clip ? clip.Sprite : null);
				Texture2D texture2D = (((bool)sprite && (bool)sprite.texture) ? sprite.texture : Texture2D.whiteTexture);
				Texture2D texture2D2 = (sprite ? sprite.associatedAlphaSplitTexture : null);
				_curPropBlock.SetTexture(SwfUtils.MainTexShaderProp, texture2D ? texture2D : Texture2D.whiteTexture);
				if ((bool)texture2D2)
				{
					_curPropBlock.SetTexture(SwfUtils.AlphaTexShaderProp, texture2D2);
					_curPropBlock.SetFloat(SwfUtils.ExternalAlphaShaderProp, 1f);
				}
				else
				{
					_curPropBlock.SetTexture(SwfUtils.AlphaTexShaderProp, Texture2D.whiteTexture);
					_curPropBlock.SetFloat(SwfUtils.ExternalAlphaShaderProp, 0f);
				}
				_meshRenderer.SetPropertyBlock(_curPropBlock);
			}
		}

		private void EmitChangeEvents(bool clip, bool sequence, bool current_frame)
		{
			if (clip && this.OnChangeClipEvent != null)
			{
				this.OnChangeClipEvent(this);
			}
			if (sequence && this.OnChangeSequenceEvent != null)
			{
				this.OnChangeSequenceEvent(this);
			}
			if (current_frame && this.OnChangeCurrentFrameEvent != null)
			{
				this.OnChangeCurrentFrameEvent(this);
			}
		}

		private SwfClipAsset.Frame GetCurrentBakedFrame()
		{
			List<SwfClipAsset.Frame> list = ((_curSequence != null) ? _curSequence.Frames : null);
			if (list == null || currentFrame < 0 || currentFrame >= list.Count)
			{
				return null;
			}
			return list[currentFrame];
		}

		private void Awake()
		{
			ClearCache(allow_to_create_components: true);
			Internal_UpdateAllProperties();
		}

		private void Start()
		{
			EmitChangeEvents(clip: true, sequence: true, current_frame: true);
		}

		private void OnEnable()
		{
			SwfManager instance = SwfManager.GetInstance(allow_create: true);
			if ((bool)instance)
			{
				instance.AddClip(this);
			}
		}

		private void OnDisable()
		{
			SwfManager instance = SwfManager.GetInstance(allow_create: false);
			if ((bool)instance)
			{
				instance.RemoveClip(this);
			}
		}

		private void Reset()
		{
			Internal_UpdateAllProperties();
		}

		private void OnValidate()
		{
			Internal_UpdateAllProperties();
		}
	}
}
