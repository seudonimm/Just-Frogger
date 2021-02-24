using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeSpawner : MonoBehaviour
{
    [SerializeField] int rand;

    [SerializeField] List<GameObject> shapes;

    [SerializeField] GameObject spawnPoint;

    [SerializeField] float timer, timerDefault;

    // Start is called before the first frame update
    void Start()
    {
        timer = timerDefault;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            Instantiate(shapes[rand], spawnPoint.transform.position, spawnPoint.transform.rotation);
            timer = timerDefault;

            RandomNumber();
            //rand = RandomNumber();
        }
    }

    void RandomNumber()
    {
        rand = Random.Range(0, 2/*shapes.Capacity*/);

        //return rand;
    }
}
