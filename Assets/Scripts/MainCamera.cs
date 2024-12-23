using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] Transform target;
    Vector3 targetPosition;
    [SerializeField] float smoothness = 1f;
    [SerializeField] float minX = -5f;
    [SerializeField] float maxX = 5f;
    void Update()
    {
        targetPosition = new Vector3(target.position.x, transform.position.y, transform.position.z);
        targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothness * Time.deltaTime);
    }
}
