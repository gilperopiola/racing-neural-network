using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarTest : MonoBehaviour {

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


        if (Input.GetKeyDown(KeyCode.Space)) {
            RaycastHit2D hit = CheckRaycast(new Vector2(Mathf.Cos(angle - 0.6f), Mathf.Sin(angle - 0.6f)));
            hit = CheckRaycast(new Vector2(Mathf.Cos(angle - 0.3f), Mathf.Sin(angle - 0.3f)));
            hit = CheckRaycast(new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)));
            hit = CheckRaycast(new Vector2(Mathf.Cos(angle + 0.3f), Mathf.Sin(angle + 0.3f)));
            hit = CheckRaycast(new Vector2(Mathf.Cos(angle + 0.6f), Mathf.Sin(angle + 0.6f)));

            Debug.DrawRay(transform.position, new Vector3(Mathf.Cos(angle - 0.6f), Mathf.Sin(angle - 0.6f), 0), Color.red, 8);
            Debug.DrawRay(transform.position, new Vector3(Mathf.Cos(angle - 0.3f), Mathf.Sin(angle - 0.3f), 0), Color.red, 8);
            Debug.DrawRay(transform.position, new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0), Color.red, 8);
            Debug.DrawRay(transform.position, new Vector3(Mathf.Cos(angle + 0.3f), Mathf.Sin(angle + 0.3f), 0), Color.red, 8);
            Debug.DrawRay(transform.position, new Vector3(Mathf.Cos(angle + 0.6f), Mathf.Sin(angle + 0.6f), 0), Color.red, 8);

            if (hit.collider) {
                Debug.Log(hit.distance);
            }
        }
    }

    public RaycastHit2D CheckRaycast(Vector2 direction) {
        Vector2 startingPosition = new Vector2(transform.position.x, transform.position.y);
        return Physics2D.Raycast(startingPosition, direction, 10);
    }
}
