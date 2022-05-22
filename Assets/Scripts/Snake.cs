using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Snake : MonoBehaviour
{
    [SerializeField] private Rigidbody SnakeRigedBody = null;
    [SerializeField] private GameObject foodSpawn;
    [SerializeField] private GameObject body;
    [SerializeField] private GameObject scoreText;
 
    private GameObject food;
    private GameObject[] tail = new GameObject[100];
    private int tailLength = 0;
    public float snakeSpeed = 0.5f;
    private Vector3 snakeMoviment = new Vector2(0, 0);
    private float elapsed = 0f;

    // Start is called before the first frame update
    void Start()
    {
       snakeMoviment = new Vector2(0, snakeSpeed);
       spawnFood();
    }

    // Update is called once per frame
    void Update()
    {
        elapsed += Time.deltaTime;

        if (elapsed > snakeSpeed)
        {
            elapsed-= snakeSpeed;
            moveSnake();
        }

        if(Input.GetAxis("Horizontal") > 0)
        {
            if(snakeMoviment.x != -0.5f) {
                snakeMoviment = new Vector2(0.5f, 0);
            }
        } else if (Input.GetAxis("Horizontal") < 0)
        {
            if(snakeMoviment.x != 0.5f) {
                snakeMoviment = new Vector2(-0.5f, 0);
            }
        } else if (Input.GetAxis("Vertical") > 0)
        {
            if(snakeMoviment.y != -0.5f) {
                snakeMoviment = new Vector2(0, 0.5f);
            }
        } else if (Input.GetAxis("Vertical") < 0)
        {
            if(snakeMoviment.y != 0.5f) {
                snakeMoviment = new Vector2(0, -0.5f);
            }
        }
    }

    private void moveSnake()
    {
        SnakeRigedBody.MovePosition(SnakeRigedBody.position + snakeMoviment);
        moveTail();
    }

    private void moveTail()
    {
        for (int i = 0; i < tailLength; i++)
        {
            Vector2 pos = i > 0 ? tail[i - 1].transform.position : transform.localPosition;
            Rigidbody rb = tail[i].GetComponent<Rigidbody>();
            rb.MovePosition(pos);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Food")
        {
            Destroy(other.gameObject);
            if(snakeSpeed >= 0.06f) {
                snakeSpeed -= 0.01f;
            }          
            if (tailLength < 99) {
                addTail(); 
            } 
            spawnFood();
            updateScore();
        }

        if(other.gameObject.tag == "Body" && other.gameObject.GetComponent<SnakeBody>().index != 0)
        {
            restartGame();
        }
    }

    private void updateScore()
    {
        scoreText.GetComponent<TMP_Text>().text = "Score: " + tailLength;
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
        snakeSpeed = 0.5f;
        snakeMoviment = new Vector2(0, 0.5f);
        transform.position = new Vector3(0, 0, 2);
        for(int i = 0; i < tail.Length; i++)
        {
            if(tail[i] != null)
            {
                Destroy(tail[i]);
            }
        }
        tailLength = 0;
        
        if(food != null)
        {
            Destroy(food);
        }
        
        spawnFood();
    }

    private void spawnFood()
    {
        food = Instantiate(foodSpawn, new Vector3(10, 10, 2), Quaternion.identity);
    }

    private void addTail()
    {
        Vector3[] lastTwoTailsPostitions = new Vector3[2];
        for(int i = 0; i < 2; i++)
        {
            if(tail[i] != null)
            {
                lastTwoTailsPostitions[i] = tail[i].transform.position;
            } else {
                lastTwoTailsPostitions[i] = transform.position;
            }
        }

        Vector3 lastTwoTailsVector = lastTwoTailsPostitions[1] - lastTwoTailsPostitions[0];
        Vector3 newTailPosition = lastTwoTailsPostitions[1] + lastTwoTailsVector; 

        // GameObject lastTaill = tailLength > 0 ? tail[tailLength - 1]: null;
        // Vector3 lastTailPos = lastTaill != null ? lastTaill.transform.position + new Vector3(0, -0.5f) : transform.localPosition - snakeMoviment;
        
        GameObject newTail = Instantiate(body, newTailPosition, Quaternion.identity);
        
        newTail.GetComponent<SnakeBody>().index = tailLength;
        tail[tailLength] = newTail;
        tailLength++;
    }
}
