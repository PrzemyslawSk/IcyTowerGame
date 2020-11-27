using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] private GameObject platformPrefab;
    [SerializeField] private GameObject platformsParent;
    [SerializeField] private Transform lastSpawnedPlatformTransform;
    public float minXPos;
    public float maxXPos;
    public float minHeightDelta;
    public float maxHeightDelta;
    public float minPlatformWidth;
    public float maxPlatformWidth;
    public int startPlatformsAmount = 10;

    void Awake()
    {
        for (int i = 0; i < startPlatformsAmount; i++)
        {
            SpawnNewPlatform(); //we wanna have some platforms at the start of the game too :>
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //When some platform left screen we're disposing them and spawn new ones on top of screen
        if (collision.CompareTag("Platform"))
        {
            Destroy(collision.gameObject);
            SpawnNewPlatform();
        }
        else if(collision.CompareTag("Player")) // When player fell out of screen -> restart game
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void SpawnNewPlatform()
    {
        GameObject newPlatform = Instantiate(platformPrefab);

        newPlatform.transform.SetParent(platformsParent.transform); 
        newPlatform.GetComponent<SpriteRenderer>().size = new Vector2(
            Random.Range(minPlatformWidth, maxPlatformWidth), 0.5f);
        newPlatform.GetComponent<PlatformAdjuster>().CollidersAdjust();
        newPlatform.transform.position = new Vector2(Random.Range(minXPos, maxXPos),
            lastSpawnedPlatformTransform.position.y + Random.Range(minHeightDelta, maxHeightDelta));
        lastSpawnedPlatformTransform = newPlatform.transform; 
    }

}
