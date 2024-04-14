using UnityEngine;
using System.Collections;

public class ResourceSpawner : MonoBehaviour
{
    [SerializeField] private Resource _resourcePrefab;
    [SerializeField] private int _maxNumberOfResources = 20;
    [SerializeField] private float _spawnInterval = 5f;

    private int _currentNumberOfResources = 0;
    private Terrain _terrain;

    private void Start()
    {
        _terrain = Terrain.activeTerrain;
        StartCoroutine(SpawnResources());
    }

    private IEnumerator SpawnResources()
    {
        while (_currentNumberOfResources < _maxNumberOfResources)
        {
            SpawnResource();
            yield return new WaitForSeconds(_spawnInterval);
        }
    }

    private void SpawnResource()
    {
        Vector3 spawnPosition = GetValidSpawnPosition();
        Instantiate(_resourcePrefab, spawnPosition, Quaternion.identity);
        _currentNumberOfResources++;
    }

    private Vector3 GetValidSpawnPosition()
    {
        float terrainWidth = _terrain.terrainData.size.x;
        float terrainLength = _terrain.terrainData.size.z;

        bool validSpawn = false;
        Vector3 spawnPosition = Vector3.zero;

        while (!validSpawn)
        {
            float randomX = Random.Range(0f, terrainWidth);
            float randomZ = Random.Range(0f, terrainLength);
            spawnPosition = new Vector3(randomX, 0f, randomZ);

            float terrainHeight = _terrain.SampleHeight(spawnPosition);
            float heightAboveTerrain = 5f;
            spawnPosition.y = terrainHeight + heightAboveTerrain;

            if (spawnPosition.y >= terrainHeight)
            {
                validSpawn = true;
            }
        }

        return spawnPosition;
    }
}
