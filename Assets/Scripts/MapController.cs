using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MapController : MonoBehaviour
{
    public List<GameObject> terrainChunks;
    public GameObject player;
    public float checkerRadius;
    Vector3 noTerrainPosition;
    public LayerMask terrainMask;
    public GameObject currentChunk;
    public PlayerMovement pm;

    [Header("Optimization")]
    public List<GameObject> spawnedChunks;
    GameObject latestChunk;
    public float maxOpDist;
    float opDist;
    float optimizerCooldown;
    public float optimizerCooldownDur;

    void Update()
    {
        if (player != null)
        {
            ChunkChecker();
            ChunkOptimizer();
        }
        else
        {
            player = GameObject.FindWithTag("Player");
            pm = FindObjectOfType<PlayerMovement>();
        }   
    }

    void ChunkChecker()
    {
        if (!currentChunk)
        {
            return;
        }

        if(pm.moveDir.x > 0.9 && pm.moveDir.y < 0) // right
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Right").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Right").position;
                SpawnChunk();
            }
        }
        else if (pm.moveDir.x < 0 && pm.moveDir.y > 0.9) // up
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Up").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Up").position;
                SpawnChunk();
            }
        }
        else if (pm.moveDir.x < -0.9 && pm.moveDir.y < 0) // left
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Left").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Left").position;
                SpawnChunk();
            }
        }
        else if (pm.moveDir.x > 0 && pm.moveDir.y < -0.9) // down
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Down").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Down").position;
                SpawnChunk();
            }
        }
        else if (pm.moveDir.x > 0 && pm.moveDir.y > 0) // right-up
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Right-Up").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Right-Up").position;
                SpawnChunk();
            }
        }
        else if (pm.moveDir.x > 0 && pm.moveDir.y < 0) // right-down
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Right-Down").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Right-Down").position;
                SpawnChunk();
            }
        }
        else if (pm.moveDir.x < 0 && pm.moveDir.y > 0) // left-up
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Left-Up").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Left-Up").position;
                SpawnChunk();
            }
        }
        else if (pm.moveDir.x < 0 && pm.moveDir.y < 0) // left-down
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Left-Down").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Left-Down").position;
                SpawnChunk();
            }
        }
    }

    void SpawnChunk()
    {
        int rand = Random.Range(0, terrainChunks.Count);
        latestChunk = Instantiate(terrainChunks[rand], noTerrainPosition, Quaternion.identity);
        spawnedChunks.Add(latestChunk);
    }

    void ChunkOptimizer()
    {
        optimizerCooldown -= Time.deltaTime;
        if (optimizerCooldown <= 0f)
        {
            optimizerCooldown = optimizerCooldownDur;
        }
        else
        {
            return;
        }

        foreach (GameObject chunk in spawnedChunks) 
        {
            opDist = Vector3.Distance(player.transform.position, chunk.transform.position);
            if(opDist > maxOpDist)
            {
                chunk.SetActive(false);           
            }
            else
            {
                chunk.SetActive(true);
            }
        }
    }
}
