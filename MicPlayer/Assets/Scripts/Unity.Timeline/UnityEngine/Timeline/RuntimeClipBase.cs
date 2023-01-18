namespace UnityEngine.Timeline
{
	internal abstract class RuntimeClipBase : RuntimeElement
	{
		public abstract double start { get; }

		public abstract double duration { get; }

		public override long intervalStart
		{
			get
			{
				return DiscreteTime.GetNearestTick(start);
			}
		}

		public override long intervalEnd
		{
			get
			{
				return DiscreteTime.GetNearestTick(start + duration);
			}
		}
	}
}
