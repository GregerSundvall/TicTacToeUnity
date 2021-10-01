using System;
using UnityEngine.Events;

namespace FG {
	[Serializable]
	public class FloatEvent : UnityEvent<float> {
	}
	
	[Serializable]
	public class IntEvent : UnityEvent<int> {
	}
	
	[Serializable]
	public class PlayerEvent : UnityEvent<Player> {
	}
}
