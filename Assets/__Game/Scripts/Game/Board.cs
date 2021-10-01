using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace FG {
    public class Board : MonoBehaviour {
        [SerializeField] private GameObject _tilePrefab;
        public Player playerOne;
        public Player playerTwo;

        [Header("Events")] public PlayerEvent switchPlayerEvent;

        public UnityEvent didPlaceEvent;

        private int _boardSize;
        private Tile[,] _tiles;
        private GamePiece[,] _pieces;

        private Transform _tilesTransform;
        private Transform _piecesTransform;
        
        private const float _timeBetweenMarkingWinningTiles = 0.5f;
        private const float _timeToFadeWinningTiles = 0.5f;
        public Player CurrentPlayer { get; private set; }

        public Tile this[int row, int column] => _tiles[row, column];

        public bool win = false;
        public TextMeshProUGUI winBox;
        
        public bool PlaceMarkerOnTile(Tile tile) {
            if (ReferenceEquals(CurrentPlayer, null)) {
                return false;
            }
            
            if (ReferenceEquals(_pieces[tile.gridPosition.x, tile.gridPosition.y], null)) {
                GamePiece piece = Instantiate(CurrentPlayer.piecePrefab,
                    new Vector3(tile.gridPosition.x, -tile.gridPosition.y),
                    Quaternion.identity, _piecesTransform)?.GetComponent<GamePiece>();
                if (!ReferenceEquals(piece, null)) {
                    piece.Owner = CurrentPlayer;
                    _pieces[tile.gridPosition.x, tile.gridPosition.y] = piece;
                }

                didPlaceEvent.Invoke();

                win = CheckForWin(tile);

                if (win)
                {
                    winBox.enabled = true;
                }
                else
                {
                    SwitchPlayer();
                }
                    
                return true;
            }

            return false;
        }

        private IEnumerator MarkWinningTiles(List<Vector2Int> winningTiles, Color color) {
            foreach (Vector2Int tile in winningTiles) {
                StartCoroutine(FadeTile(_tiles[tile.x, tile.y], color));
                yield return new WaitForSeconds(_timeBetweenMarkingWinningTiles);
            }
        }

        private IEnumerator FadeTile(Tile tile, Color targetColor) {
            SpriteRenderer tileRenderer = tile.GetComponent<SpriteRenderer>();
            float elapsedTime = 0f;
            Color startColor = tileRenderer.color;
            float fadeTime = _timeToFadeWinningTiles;
            
            while (elapsedTime < fadeTime) {
                elapsedTime += Time.deltaTime;
                float blend = Mathf.Clamp01(elapsedTime / fadeTime);
                tileRenderer.color = Color.Lerp(startColor, targetColor, blend);
                yield return null;
            }

            tileRenderer.color = targetColor;
        }

        private void SwitchPlayer()
        {
            if (win)
            {
                Debug.Log("Win");
            };
            CurrentPlayer = ReferenceEquals(CurrentPlayer, playerOne) ? playerTwo : playerOne;
            switchPlayerEvent.Invoke(CurrentPlayer);

        }

        private void SetupTiles() {
            for (int x = 0; x < _boardSize; x++) {
                for (int y = 0; y < _boardSize; y++) {
                    GameObject tileGo = Instantiate(_tilePrefab, new Vector3(x, -y, 0f), Quaternion.identity,
                        _tilesTransform);
                    tileGo.name = $"Tile_({x},{y})";

                    Tile tile = tileGo.GetComponent<Tile>();
                    tile.board = this;
                    tile.gridPosition = new Vector2Int(x, y);

                    _tiles[x, y] = tile;
                }
            }
        }

        private void SetCurrentPlayer() {
            CurrentPlayer = Random.Range(0, 2) == 0 ? playerOne : playerTwo;
            switchPlayerEvent.Invoke(CurrentPlayer);
        }

        public void Awake()
        {
            winBox.enabled = false;
            _tilesTransform = transform.GetChild(0);
            _piecesTransform = transform.GetChild(1);
            _boardSize = PlaySettings.BoardSize;

            _tiles = new Tile[_boardSize, _boardSize];
            _pieces = new GamePiece[_boardSize, _boardSize];

            SetupTiles();

            playerOne.displayName = PlaySettings.PlayerOneName;
            playerTwo.displayName = PlaySettings.PlayerTwoName;

            SetCurrentPlayer();
        }

        private bool CheckForWin(Tile tile)
        {
            var x = tile.gridPosition.x;
            var y = tile.gridPosition.y;

            bool CheckPos(int x, int y)
            {
                if (x < 0 || x > (_boardSize - 1) || y < 0 || y > (_boardSize - 1)) return false;
                if (_pieces[x, y] == null) return false;
                return _pieces[x, y].Owner == CurrentPlayer;
            }

            if (CheckPos(x+1, y) && CheckPos(x+2, y) ||
                CheckPos(x+1, y) && CheckPos(x-1, y) ||
                CheckPos(x-1, y) && CheckPos(x-2, y))
            {
                return true;
            }
            if (CheckPos(x, y+1) && CheckPos(x, y+2) ||
                CheckPos(x, y+1) && CheckPos(x, y-1) ||
                CheckPos(x, y-1) && CheckPos(x, y-2))
            {
                return true;
            }
            if (CheckPos(x-1, y+1) && CheckPos(x+1, y-1) ||
                CheckPos(x-1, y+1) && CheckPos(x-2, y+2) ||
                CheckPos(x+1, y-1) && CheckPos(x+2, y-2))
            {
                return true;
            }
            if (CheckPos(x+1, y+1) && CheckPos(x-1, y-1) ||
                CheckPos(x+1, y+1) && CheckPos(x+2, y+2) ||
                CheckPos(x-1, y-1) && CheckPos(x-2, y-2))
            {
                return true;
            }
            
            Debug.Log($"win = {win}");
            return false;
            
            
            
        }

        
        
        
    }
}