using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    public List<GameObject> pickupPrefabs = new List<GameObject>();
    public GameObject currentPickup;
    public float spawnTime;
    private float spawnTimer;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.pickupSpawns.Add(this);
        spawnTimer = Time.time + spawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentPickup == null)
        {
            if(spawnTimer < Time.time)
            {
                SpawnPowerUp();
                spawnTimer = Time.time + spawnTime;
            }
        }
        else
        {
            spawnTimer = Time.time + spawnTime;
        }
    }

    public void SpawnPowerUp()
    {
        
        currentPickup = Instantiate(pickupPrefabs[UnityEngine.Random.Range(0, pickupPrefabs.Count)], new Vector3((this.transform.position.x + 6.7f), 4.43f, (this.transform.position.z - 11.1f)), this.transform.rotation);
    }
}
