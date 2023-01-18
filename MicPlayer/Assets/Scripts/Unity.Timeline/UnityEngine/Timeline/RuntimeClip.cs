using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	internal class RuntimeClip : RuntimeClipBase
	{
		private TimelineClip m_Clip;

		private Playable m_Playable;

		private Playable m_ParentMixer;

		public override double start
		{
			get
			{
				return m_Clip.extrapolatedStart;
			}
		}

		public override double duration
		{
			get
			{
				return m_Clip.extrapolatedDuration;
			}
		}

		public TimelineClip clip
		{
			get
			{
				return m_Clip;
			}
		}

		public Playable mixer
		{
			get
			{
				return m_ParentMixer;
			}
		}

		public Playable playable
		{
			get
			{
				return m_Playable;
			}
		}

		public override bool enable
		{
			set
			{
				if (value && m_Playable.GetPlayState() != PlayState.Playing)
				{
					m_Playable.Play();
					SetTime(m_Clip.clipIn);
				}
				else if (!value && m_Playable.GetPlayState() != 0)
				{
					m_Playable.Pause();
					if (m_ParentMixer.IsValid())
					{
						m_ParentMixer.SetInputWeight(m_Playable, 0f);
					}
				}
			}
		}

		public RuntimeClip(TimelineClip clip, Playable clipPlayable, Playable parentMixer)
		{
			Create(clip, clipPlayable, parentMixer);
		}

		private void Create(TimelineClip clip, Playable clipPlayable, Playable parentMixer)
		{
			m_Clip = clip;
			m_Playable = clipPlayable;
			m_ParentMixer = parentMixer;
			clipPlayable.Pause();
		}

		public void SetTime(double time)
		{
			m_Playable.SetTime(time);
		}

		public void SetDuration(double duration)
		{
			m_Playable.SetDuration(duration);
		}

		public override void EvaluateAt(double localTime, FrameData frameData)
		{
			enable = true;
			float num = 1f;
			num = (clip.IsPreExtrapolatedTime(localTime) ? clip.EvaluateMixIn((float)clip.start) : ((!clip.IsPostExtrapolatedTime(localTime)) ? (clip.EvaluateMixIn(localTime) * clip.EvaluateMixOut(localTime)) : clip.EvaluateMixOut((float)clip.end)));
			if (mixer.IsValid())
			{
				mixer.SetInputWeight(playable, num);
			}
			double num2 = clip.ToLocalTime(localTime);
			if (num2 >= (0.0 - DiscreteTime.tickValue) / 2.0)
			{
				SetTime(num2);
			}
			SetDuration(clip.extrapolatedDuration);
		}

		public override void Reset()
		{
			SetTime(m_Clip.clipIn);
		}
	}
}
