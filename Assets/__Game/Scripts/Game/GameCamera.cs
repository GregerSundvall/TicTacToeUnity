using UnityEngine;

namespace FG {
    public class GameCamera : MonoBehaviour {
        public int borderSize;

        private void SetupCamera() {
            Camera camera = GetComponent<Camera>();
            
            float aspectRatio = (float) Screen.width / Screen.height;
            float verticalSize = PlaySettings.BoardSize * 0.5f + borderSize;
            float horizontalSize = (PlaySettings.BoardSize * 0.5f + borderSize) / aspectRatio;
            camera.orthographicSize = Mathf.Max(verticalSize, horizontalSize);

            transform.position = new Vector3((float) (PlaySettings.BoardSize - 1) / 2f,
                (float) (PlaySettings.BoardSize - 1) / -2f, -10f);
        }

        private void Awake() {
            SetupCamera();
        }
    }
}