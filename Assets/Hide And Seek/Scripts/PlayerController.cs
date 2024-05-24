using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float zAxis;
    public float xAxis;

    public float speed;
    public float turnSpeed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        zAxis = Input.GetAxis("Vertical");
        xAxis = Input.GetAxis("Horizontal");
        Vector3 direction = new Vector3(xAxis, 0, zAxis);

        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

        if (direction != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, turnSpeed * Time.deltaTime);
        }
    }
}
