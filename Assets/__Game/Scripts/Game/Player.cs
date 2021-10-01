using System;
using UnityEngine;

namespace FG {
	public class Player : MonoBehaviour {
		public GameObject piecePrefab;
		public Color markerColor;
		
		[NonSerialized] public string displayName = string.Empty;
	}
}
