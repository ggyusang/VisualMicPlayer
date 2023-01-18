using System;
using System.Collections.Generic;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	internal static class NotificationUtilities
	{
		public static ScriptPlayable<TimeNotificationBehaviour> CreateNotificationsPlayable(PlayableGraph graph, IEnumerable<IMarker> markers, GameObject go)
		{
			ScriptPlayable<TimeNotificationBehaviour> result = ScriptPlayable<TimeNotificationBehaviour>.Null;
			PlayableDirector component = go.GetComponent<PlayableDirector>();
			foreach (IMarker marker in markers)
			{
				INotification notification = marker as INotification;
				if (notification != null)
				{
					if (result.Equals(ScriptPlayable<TimeNotificationBehaviour>.Null))
					{
						result = TimeNotificationBehaviour.Create(graph, component.playableAsset.duration, component.extrapolationMode);
					}
					DiscreteTime discreteTime = (DiscreteTime)marker.time;
					DiscreteTime discreteTime2 = (DiscreteTime)component.playableAsset.duration;
					if (discreteTime >= discreteTime2 && discreteTime <= discreteTime2.OneTickAfter() && discreteTime2 != 0)
					{
						discreteTime = discreteTime2.OneTickBefore();
					}
					INotificationOptionProvider notificationOptionProvider = marker as INotificationOptionProvider;
					if (notificationOptionProvider != null)
					{
						result.GetBehaviour().AddNotification((double)discreteTime, notification, notificationOptionProvider.flags);
					}
					else
					{
						result.GetBehaviour().AddNotification((double)discreteTime, notification);
					}
				}
			}
			return result;
		}

		public static bool TrackTypeSupportsNotifications(Type type)
		{
			TrackBindingTypeAttribute trackBindingTypeAttribute = (TrackBindingTypeAttribute)Attribute.GetCustomAttribute(type, typeof(TrackBindingTypeAttribute));
			if (trackBindingTypeAttribute != null)
			{
				if (!typeof(Component).IsAssignableFrom(trackBindingTypeAttribute.type))
				{
					return typeof(GameObject).IsAssignableFrom(trackBindingTypeAttribute.type);
				}
				return true;
			}
			return false;
		}
	}
}
