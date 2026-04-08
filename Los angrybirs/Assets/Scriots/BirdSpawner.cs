using UnityEngine;

public class BirdSpawner : MonoBehaviour
{
    public GameObject birdPrefab;
    public Transform spawnPoint;

    [Header("Límite de pájaros")]
    public int maxBirds = 2;
    private int birdsSpawned = 0;

    public void SpawnBird()
    {
        if (birdsSpawned >= maxBirds)
        {
            Debug.Log("No hay más pájaros!");
            return;
        }

        Instantiate(birdPrefab, spawnPoint.position, Quaternion.identity);
        birdsSpawned++;
    }
}
