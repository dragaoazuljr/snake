using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    [SerializeField] private Rigidbody SnakeRigedBody = null;
 
    private float snakeSpeed = 0.05f;
    private Vector3 snakeMoviment = new Vector2(0, 0);

    // Start is called before the first frame update
    void Start()
    {
       snakeMoviment = new Vector2(0, snakeSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Horizontal") > 0)
        {
            snakeMoviment = new Vector2(snakeSpeed, 0);
        } else if (Input.GetAxis("Horizontal") < 0)
        {
            snakeMoviment = new Vector2(-snakeSpeed, 0);
        } else if (Input.GetAxis("Vertical") > 0)
        {
            snakeMoviment = new Vector2(0, snakeSpeed);
        } else if (Input.GetAxis("Vertical") < 0)
        {
            snakeMoviment = new Vector2(0, -snakeSpeed);
        }
    }

    void FixedUpdate()
    {
        SnakeRigedBody.MovePosition(SnakeRigedBody.position + snakeMoviment);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Food")
        {
            Destroy(other.gameObject);
            snakeSpeed += 0.05f;
            transform.localScale += new Vector3(0, 0.05f, 0);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Border")
        {
            restartGame();
        }
    }

    private void restartGame()
    {
        snakeSpeed = 0.05f;
        snakeMoviment = new Vector2(0, snakeSpeed);
        transform.position = new Vector3(0, 0, 0);
    }
}
