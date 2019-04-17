using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour {

    public float Speed { get; set; }
    public float RotationSpeed { get; set; }

    void Start() {
        Speed = 1.2f;
        RotationSpeed = 2;
    }

    void FixedUpdate() {
        Vector3 newPos = transform.position;
        Vector3 newRot = transform.rotation.eulerAngles;

        //movement
        float angle = newRot.magnitude * Mathf.Deg2Rad;
        if (Input.GetKey(KeyCode.UpArrow)) {
            newPos.x += (Mathf.Cos(angle) * Speed) * Time.deltaTime;
            newPos.y += (Mathf.Sin(angle) * Speed) * Time.deltaTime;
        }

        //rotation
        if (Input.GetKey(KeyCode.RightArrow)) {
            newRot.z -= RotationSpeed;
        }
        if (Input.GetKey(KeyCode.LeftArrow)) {
            newRot.z += RotationSpeed;
        }

        transform.position = newPos;
        transform.rotation = Quaternion.Euler(newRot);
    }
}
