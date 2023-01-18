using System.Runtime.CompilerServices;

namespace UnityEngine.Timeline
{
	internal class TimeFieldAttribute : PropertyAttribute
	{
		public enum UseEditMode
		{
			None = 0,
			ApplyEditMode = 1
		}

		[CompilerGenerated]
		private readonly UseEditMode _003CuseEditMode_003Ek__BackingField;

		public UseEditMode useEditMode
		{
			[CompilerGenerated]
			get
			{
				return _003CuseEditMode_003Ek__BackingField;
			}
		}

		public TimeFieldAttribute(UseEditMode useEditMode = UseEditMode.ApplyEditMode)
		{
			_003CuseEditMode_003Ek__BackingField = useEditMode;
		}
	}
}
