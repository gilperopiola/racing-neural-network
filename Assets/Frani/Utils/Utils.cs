using UnityEngine;
using System.Collections.Generic;

public enum Direction {
    NONE,
    UP,
    UP_RIGHT,
    RIGHT,
    RIGHT_DOWN,
    DOWN,
    DOWN_LEFT,
    LEFT,
    LEFT_UP
}

public static class FileHandler {
    public static string Read(string fileName) {
        return System.IO.File.ReadAllText(fileName);
    }

    public static void WriteFloatList(string fileName, List<float> content, string delimiter) {
        using (System.IO.StreamWriter file = new System.IO.StreamWriter(fileName)) {
            foreach (float value in content) {
                file.Write(value.ToString() + delimiter);
            }
        }
    }
}

public static class RandomGenerator {
    public static int Int(int minInclusive, int maxInclusive) {
        return UnityEngine.Random.Range(minInclusive, maxInclusive + 1);
    }
    public static float Float(float minInclusive, float maxInclusive) {
        return UnityEngine.Random.Range(minInclusive, maxInclusive);
    }
}

public static class Instantiator {
    public static GameObject InstantiateGameObject(string fileName, Vector3 pos, Quaternion rot) {
        return (GameObject)MonoBehaviour.Instantiate(Resources.Load(fileName), pos, rot);
    }

    public static Sprite InstantiateSprite(string fileName, Vector3 pos, Quaternion rot) {
        return (Sprite)MonoBehaviour.Instantiate(Resources.Load(fileName), pos, rot);
    }
}

public static class Sigmoid {
    public static float Output(float x) {
        return x < -45.0 ? 0 : x > 45.0 ? 1 : (float)1.0 / (float)(1.0 + Mathf.Exp(-x));
    }

    public static float Derivative(float x) {
        return x * (1 - x);
    }
}