using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyItemSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] items;

    private Health health;


    private void OnEnable()
    {
        health = GetComponent<Health>();

        health.OnDie += SpawnItem;
    }

    private void OnDisable()
    {
        health.OnDie -= SpawnItem;
    }

    // When an enemy dies, spawn a random item among its item list
    public void SpawnItem()
    {
        int randomIndex = Random.Range(0, items.Length);
        GameObject itemToSpawn = items[randomIndex];

        // Item prefab's position will also affect to the spawning point
        Instantiate(itemToSpawn, this.transform.position, itemToSpawn.transform.rotation, this.transform);
    }
}