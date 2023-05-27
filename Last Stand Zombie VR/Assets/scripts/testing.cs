using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testing : MonoBehaviour
{
    public float speed = 5f; // Speed of movement
    public GameObject objectToMove; // GameObject to move forward

    void Update()
    {
        // Move the object forward
        objectToMove.transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
