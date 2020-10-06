using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeFly : MonoBehaviour
{
    //the fly's rigidbody
    private Rigidbody2D flyBody;

    //move speed
    private float moveSpeed = 500;

    // Start is called before the first frame update
    void Start()
    {
        flyBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //follow mouse position when holding it down
        if(Input.GetKey(KeyCode.Mouse0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 travelPath = mousePos - (Vector2)transform.position;
            if (Vector2.Distance(mousePos, (Vector2)transform.position) > 0.3)
            {
                flyBody.AddForce(travelPath.normalized * moveSpeed * Time.deltaTime);
            }
        }

        //WASD controls
        if(Input.GetKey(KeyCode.W))
        {
            flyBody.AddForce(Vector2.up * moveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            flyBody.AddForce(-Vector2.right * moveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            flyBody.AddForce(-Vector2.up * moveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            flyBody.AddForce(Vector2.right * moveSpeed * Time.deltaTime);
        }
    }
}
