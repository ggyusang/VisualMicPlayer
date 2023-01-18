using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	internal class InfiniteRuntimeClip : RuntimeElement
	{
		private Playable m_Playable;

		private static readonly long kIntervalEnd = DiscreteTime.GetNearestTick(TimelineClip.kMaxTimeValue);

		public override long intervalStart
		{
			get
			{
				return 0L;
			}
		}

		public override long intervalEnd
		{
			get
			{
				return kIntervalEnd;
			}
		}

		public override bool enable
		{
			set
			{
				if (value)
				{
					m_Playable.Play();
				}
				else
				{
					m_Playable.Pause();
				}
			}
		}

		public InfiniteRuntimeClip(Playable playable)
		{
			m_Playable = playable;
		}

		public override void EvaluateAt(double localTime, FrameData frameData)
		{
			m_Playable.SetTime(localTime);
		}
	}
}
