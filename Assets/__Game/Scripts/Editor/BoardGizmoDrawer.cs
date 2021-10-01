using UnityEditor;
using UnityEngine;

namespace FG {
    public class MyScriptGizmoDrawer {
        [DrawGizmo(GizmoType.Selected | GizmoType.Active)]
        static void DrawGizmoForMyScript(Board board, GizmoType gizmoType) {
            if (!board.CurrentPlayer) {
                return;
            }

            GUIStyle style = new GUIStyle();
            style.normal.textColor = Color.magenta;

            for (int row = 0; row < PlaySettings.BoardSize; row++) {
                for (int column = 0; column < PlaySettings.BoardSize; column++) {
                    // Handles.Label(board[row, column].transform.position, board[row, column].gridPosition.ToString(),
                    //     style);
                    SpriteRenderer renderer = board[row, column].GetComponent<SpriteRenderer>();
                    Handles.Label(new Vector3(renderer.bounds.min.x, renderer.bounds.max.y, 0f),
                        board[row, column].gridPosition.ToString(),
                        style);
                }
            }
        }
    }
}