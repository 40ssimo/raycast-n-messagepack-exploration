using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public Vector3 offset;
    public Vector3 velocity = Vector3.zero;
    public float smoothTime;

    void Awake()
    {
        offset = transform.position - player.gameObject.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 targetPosition = player.gameObject.transform.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
