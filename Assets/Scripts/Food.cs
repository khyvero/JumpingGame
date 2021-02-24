using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{

    public GameObject prefab;
    public Transform food;

    // Start is called before the first frame update
    void Start()
    {
        //int randomPrefabAmount = Random.Range(5, 10);

        for (int i = 0; i < 10; i++)
        {
            Vector3 position = new Vector3(Random.Range(-5.8f, 6.0f), Random.Range(1.0f, 4f),0);
            Instantiate(prefab, position, Quaternion.identity, food);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 position = new Vector3(Random.Range(-10.0f, 10.0f), 0, Random.Range(-10.0f, 10.0f));
        //Instantiate(prefab, position, Quaternion.identity,food);
    }
}
