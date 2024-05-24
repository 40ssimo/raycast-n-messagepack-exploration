using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public Vector3 startPosition;
    public bool walkLeft = true;
    public bool walkRight = false;
    public float turnSpeed;
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (walkLeft)
        {
            Vector3 direction = new Vector3(-1, 0, 0);
            Quaternion toRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, turnSpeed * Time.deltaTime);

            transform.position += (Vector3.left * speed * Time.deltaTime);
        }

        if (transform.position.x <= startPosition.x - 5) {
            walkLeft = false;
           
            walkRight = true;
        }

        if (transform.position.x >= startPosition.x + 5)
        {
            walkRight = false;
            
            walkLeft = true;
        }

        if (walkRight)
        {
            Vector3 direction = new Vector3(1, 0, 0);
            Quaternion toRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, turnSpeed * Time.deltaTime);

            transform.position += (Vector3.right * speed * Time.deltaTime);
        }
        
        
    }
}
