using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropRandomizer : MonoBehaviour
{
    public List<GameObject> eminemsSpawnPoints;
    public List<GameObject> eminemsPrefabs;
    public List<GameObject> propSpawnPoints;
    public List<GameObject> propPrefabs;

    void Start()
    {
        SpawnProps();
        SpawnEminems();
    }

    void SpawnProps()
    {
        foreach (GameObject sp in propSpawnPoints)
        {
            int rand = Random.Range(0, propPrefabs.Count);
            Instantiate(propPrefabs[rand], sp.transform.position, Quaternion.identity);
        }
    }

    void SpawnEminems()
    {
        foreach (GameObject sp in eminemsSpawnPoints)
        {
            int rand = Random.Range(0, eminemsPrefabs.Count);
            Instantiate(eminemsPrefabs[rand], sp.transform.position, Quaternion.identity);
        }
    }
}
