using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        spawnFood();        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void spawnFood()
    {
        transform.position = new Vector3(Random.Range(-7, 6), Random.Range(-3, 4), 2);
        gameObject.SetActive(true);
    }
}
