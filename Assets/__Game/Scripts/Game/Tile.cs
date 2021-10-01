using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FG {
	[RequireComponent(typeof(SpriteRenderer))]
	public class Tile : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {
		[SerializeField] private Color _disabledColor;
		
		[NonSerialized] public Vector2Int gridPosition;
		[NonSerialized] public Board board;

		private SpriteRenderer _renderer;
		private Color _startColor;

		public bool IsMarkerPlacedOnTile { get; private set; }
		
		public void OnPointerClick(PointerEventData eventData) {
			if (IsMarkerPlacedOnTile || eventData.button != PointerEventData.InputButton.Left) {
				return;
			}

			if (board.PlaceMarkerOnTile(this)) {
				IsMarkerPlacedOnTile = true;
				_renderer.color = _disabledColor;
			}
		}

		public void OnPointerEnter(PointerEventData eventData) {
			if (!ReferenceEquals(board.CurrentPlayer, null) && !IsMarkerPlacedOnTile) {
				_renderer.color = board.CurrentPlayer.markerColor;
			}
		}

		public void OnPointerExit(PointerEventData eventData) {
			if (!IsMarkerPlacedOnTile) {
				_renderer.color = _startColor;
			}
		}

		private void Awake() {
			_renderer = GetComponent<SpriteRenderer>();
			_startColor = _renderer.color;
		}
	}
}
