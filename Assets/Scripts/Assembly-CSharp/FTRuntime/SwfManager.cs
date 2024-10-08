using System.Collections.Generic;
using FTRuntime.Internal;
using UnityEngine;

namespace FTRuntime
{
	[AddComponentMenu("FlashTools/SwfManager")]
	[ExecuteInEditMode]
	[DisallowMultipleComponent]
	public class SwfManager : MonoBehaviour
	{
		private SwfAssocList<SwfClip> _clips = new SwfAssocList<SwfClip>();

		private SwfAssocList<SwfClipController> _controllers = new SwfAssocList<SwfClipController>();

		private SwfList<SwfClipController> _safeUpdates = new SwfList<SwfClipController>();

		private bool _isPaused;

		private bool _useUnscaledDt;

		private float _rateScale = 1f;

		private HashSet<string> _groupPauses = new HashSet<string>();

		private HashSet<string> _groupUnscales = new HashSet<string>();

		private Dictionary<string, float> _groupRateScales = new Dictionary<string, float>();

		private static SwfManager _instance;

		public int clipCount => _clips.Count;

		public int controllerCount => _controllers.Count;

		public bool isPaused
		{
			get
			{
				return _isPaused;
			}
			set
			{
				_isPaused = value;
			}
		}

		public bool isPlaying
		{
			get
			{
				return !_isPaused;
			}
			set
			{
				_isPaused = !value;
			}
		}

		public bool useUnscaledDt
		{
			get
			{
				return _useUnscaledDt;
			}
			set
			{
				_useUnscaledDt = value;
			}
		}

		public float rateScale
		{
			get
			{
				return _rateScale;
			}
			set
			{
				_rateScale = Mathf.Clamp(value, 0f, float.MaxValue);
			}
		}

		public static SwfManager GetInstance(bool allow_create)
		{
			if (!_instance)
			{
				_instance = Object.FindObjectOfType<SwfManager>();
				if (allow_create && !_instance)
				{
					_instance = new GameObject("[SwfManager]").AddComponent<SwfManager>();
				}
			}
			return _instance;
		}

		public void Pause()
		{
			isPaused = true;
		}

		public void Resume()
		{
			isPlaying = true;
		}

		public void PauseGroup(string group_name)
		{
			if (!string.IsNullOrEmpty(group_name))
			{
				_groupPauses.Add(group_name);
			}
		}

		public void ResumeGroup(string group_name)
		{
			if (!string.IsNullOrEmpty(group_name))
			{
				_groupPauses.Remove(group_name);
			}
		}

		public bool IsGroupPaused(string group_name)
		{
			return _groupPauses.Contains(group_name);
		}

		public bool IsGroupPlaying(string group_name)
		{
			return !IsGroupPaused(group_name);
		}

		public void SetGroupUseUnscaledDt(string group_name, bool yesno)
		{
			if (!string.IsNullOrEmpty(group_name))
			{
				if (yesno)
				{
					_groupUnscales.Add(group_name);
				}
				else
				{
					_groupUnscales.Remove(group_name);
				}
			}
		}

		public bool IsGroupUseUnscaledDt(string group_name)
		{
			return _groupUnscales.Contains(group_name);
		}

		public void SetGroupRateScale(string group_name, float rate_scale)
		{
			if (!string.IsNullOrEmpty(group_name))
			{
				_groupRateScales[group_name] = Mathf.Clamp(rate_scale, 0f, float.MaxValue);
			}
		}

		public float GetGroupRateScale(string group_name)
		{
			if (!_groupRateScales.TryGetValue(group_name, out var value))
			{
				return 1f;
			}
			return value;
		}

		internal void AddClip(SwfClip clip)
		{
			_clips.Add(clip);
		}

		internal void RemoveClip(SwfClip clip)
		{
			_clips.Remove(clip);
		}

		internal void GetAllClips(List<SwfClip> clips)
		{
			_clips.AssignTo(clips);
		}

		internal void AddController(SwfClipController controller)
		{
			_controllers.Add(controller);
		}

		internal void RemoveController(SwfClipController controller)
		{
			_controllers.Remove(controller);
		}

		private void GrabEnabledClips()
		{
			SwfClip[] array = Object.FindObjectsOfType<SwfClip>();
			int i = 0;
			for (int num = array.Length; i < num; i++)
			{
				SwfClip swfClip = array[i];
				if (swfClip.enabled)
				{
					_clips.Add(swfClip);
				}
			}
		}

		private void GrabEnabledControllers()
		{
			SwfClipController[] array = Object.FindObjectsOfType<SwfClipController>();
			int i = 0;
			for (int num = array.Length; i < num; i++)
			{
				SwfClipController swfClipController = array[i];
				if (swfClipController.enabled)
				{
					_controllers.Add(swfClipController);
				}
			}
		}

		private void DropClips()
		{
			_clips.Clear();
		}

		private void DropControllers()
		{
			_controllers.Clear();
		}

		private void LateUpdateClips()
		{
			int i = 0;
			for (int count = _clips.Count; i < count; i++)
			{
				SwfClip swfClip = _clips[i];
				if ((bool)swfClip)
				{
					swfClip.Internal_UpdateMesh();
				}
			}
		}

		private void LateUpdateControllers(float scaled_dt, float unscaled_dt)
		{
			_controllers.AssignTo(_safeUpdates);
			int i = 0;
			for (int count = _safeUpdates.Count; i < count; i++)
			{
				SwfClipController swfClipController = _safeUpdates[i];
				if ((bool)swfClipController)
				{
					string groupName = swfClipController.groupName;
					if (string.IsNullOrEmpty(groupName))
					{
						swfClipController.Internal_Update(scaled_dt, unscaled_dt);
					}
					else if (IsGroupPlaying(groupName))
					{
						float groupRateScale = GetGroupRateScale(groupName);
						swfClipController.Internal_Update(groupRateScale * (IsGroupUseUnscaledDt(groupName) ? unscaled_dt : scaled_dt), groupRateScale * unscaled_dt);
					}
				}
			}
			_safeUpdates.Clear();
		}

		private void OnEnable()
		{
			GrabEnabledClips();
			GrabEnabledControllers();
		}

		private void OnDisable()
		{
			DropClips();
			DropControllers();
		}

		private void LateUpdate()
		{
			if (isPlaying)
			{
				LateUpdateControllers(rateScale * (useUnscaledDt ? Time.unscaledDeltaTime : Time.deltaTime), rateScale * Time.unscaledDeltaTime);
			}
			LateUpdateClips();
		}
	}
}
