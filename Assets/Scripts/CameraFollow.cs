using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform objectToFollow;

    [Range(0, 1)]
    public float smooth;

    private Transform currentPosition;
    private Vector3 newPosition;

    private void Start()
    {
        currentPosition = GetComponent<Transform>();
    }

    private void Update()
    {
        newPosition = objectToFollow.position;
        newPosition.z = currentPosition.position.z;
    }

    private void FixedUpdate()
    {
        currentPosition.position = Vector3.Lerp(currentPosition.position, newPosition, (1-smooth));
    }
}
