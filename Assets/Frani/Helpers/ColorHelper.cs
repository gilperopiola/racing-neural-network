using UnityEngine;

public static class ColorHelper {
    public static void Darkify(GameObject gameObject) {
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();

        if (sr != null) {
            sr.color = new Color(sr.color.r - 0.6f, sr.color.g - 0.6f, sr.color.b - 0.6f, sr.color.a);
        }
    }

    public static void Greenify(GameObject gameObject) {
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();

        if (sr != null) {
            sr.color = new Color(0, 1, 0, sr.color.a);
        }
    }
}