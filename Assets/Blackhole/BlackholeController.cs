using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackholeController : MonoBehaviour
{
    // Start is called before the first frame update
    public float zAxis;
    public float xAxis;

    public float speed;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        zAxis = Input.GetAxis("Vertical");
        xAxis = Input.GetAxis("Horizontal");

        transform.position += new Vector3(xAxis * speed * Time.deltaTime, 0, zAxis * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Cube")
        {
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
