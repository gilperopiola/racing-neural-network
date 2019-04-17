using UnityEngine;

public static class CameraManager {
    public static Camera camera;

    public static void SetCamera(Camera _camera) {
        camera = _camera;
    }

    public static void SetBackgroundColor(Color color) {
        camera.backgroundColor = color;
    }

    public static void SetZoom(int zoom) {
        camera.orthographicSize = zoom;
    }

    public static void CenterOnGameObject(GameObject obj) {
        camera.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, -10);
    }
}
