using UnityEngine;
using System.Collections.Generic;

public class Individual {
    public GameObject GameObject { get; set; }
    public DNA Dna { get; set; }
    public NeuralNetwork NeuralNet { get; set; }

    public GameObject[] Walls;
    public GameObject[] Foods;
    public List<GameObject> EatenFoods;

    public int Index { get; set; }
    public float Fitness = 0;
    public bool Finished = false;
    public bool Elite = false;

    public Individual(DNA dna) {
        Dna = dna;
        NeuralNet = new NeuralNetwork(Dna.weights);
        Walls = GameObject.FindGameObjectsWithTag("Wall");
        Foods = GameObject.FindGameObjectsWithTag("Food");
        EatenFoods = new List<GameObject>();
    }

    public void Advance() {
        List<float> inputs = GetInputs();
        List<float> outputs = NeuralNet.Compute(inputs);

        float minSpeed = ConfigManager.config.simulation.individualMinSpeed;
        MoveAndRotate((outputs[0] >= minSpeed ? outputs[0] : minSpeed), outputs[1], outputs[2]);
        CheckCollision();

        Fitness += (outputs[0] >= minSpeed ? outputs[0] : minSpeed);
    }

    public void SetFinished() {
        if (Finished) return;

        Finished = true;
        ColorHelper.Darkify(GameObject);
    }

    public void MoveAndRotate(float moveDistance, float rotationLeft, float rotationRight) {
        Vector3 newPos = GameObject.transform.position;
        Vector3 newRot = GameObject.transform.rotation.eulerAngles;

        //movement
        float angle = newRot.magnitude * Mathf.Deg2Rad;
        float speed = ConfigManager.config.simulation.individualSpeedModifier;

        newPos.x += (Mathf.Cos(angle) * speed * moveDistance) * Time.deltaTime;
        newPos.y += (Mathf.Sin(angle) * speed * moveDistance) * Time.deltaTime;

        //rotation
        float rotationSpeed = ConfigManager.config.simulation.individualRotationSpeedModifier;
        if (rotationRight > rotationLeft) {
            newRot.z -= rotationSpeed * rotationRight;
        } else {
            newRot.z += rotationSpeed * rotationLeft;
        }

        GameObject.transform.position = newPos;
        GameObject.transform.rotation = Quaternion.Euler(newRot);
    }

    public void CheckCollision() {
        foreach (var wall in Walls) {
            if (CollisionManager.Squares(GameObject, wall.gameObject, ConfigManager.config.simulation.individualWidth)) {
                SetFinished();
            }
        }

        foreach (var food in Foods) {
            bool eaten = false;

            foreach (var eatenFood in EatenFoods) {
                if (food == eatenFood) {
                    eaten = true;
                }
            }

            if (eaten) {
                continue;
            }

            if (CollisionManager.Squares(GameObject, food.gameObject, ConfigManager.config.simulation.individualWidth)) {
                Fitness += ConfigManager.config.simulation.foodFitness;
                EatenFoods.Add(food);
            }
        }
    }

    public List<float> GetInputs() {
        List<float> inputs = new List<float>();
        float angle = GameObject.transform.rotation.eulerAngles.magnitude * Mathf.Deg2Rad;

        for (int i = 0; i < ConfigManager.config.neuralNet.nInputNeurons; i++) {
            float startingRot = ConfigManager.config.simulation.individualRaycastStartingRotation;
            float rotationStep = ConfigManager.config.simulation.individualRaycastRotationStep;
            float realAngle = angle + startingRot + rotationStep * i;

            RaycastHit2D hit = Physics2D.Raycast(GameObject.transform.position, new Vector2(Mathf.Cos(realAngle), Mathf.Sin(realAngle)), 4);
            //Debug.DrawRay(GameObject.transform.position, new Vector3(Mathf.Cos(realAngle), Mathf.Sin(realAngle), 0), Color.red, 8);
            inputs.Add(hit.collider ? Sigmoid.Output(hit.distance / ConfigManager.config.neuralNet.inputEase) : 0);
        }

        return inputs;
    }

    public DNA CloneDNA() {
        DNA clone = new DNA(Dna.weights);
        return clone;
    }

    public void CreateGameObject() {
        float startingX = ConfigManager.config.simulation.individualStartingX;
        float startingY = ConfigManager.config.simulation.individualStartingY;

        GameObject = (GameObject)MonoBehaviour.Instantiate(Resources.Load("Prefabs/Individual"), new Vector3(startingX, startingY, 0), Quaternion.identity);

        if (Elite) {
            ColorHelper.Greenify(GameObject);
        }
    }

    public void DestroyGameObject() {
        GameObject.Destroy(GameObject);
    }
}