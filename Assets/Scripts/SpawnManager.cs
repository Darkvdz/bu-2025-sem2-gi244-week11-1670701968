using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject enemyPrefab;

    private Coroutine byeRoutines;

    void Start()
    {
        //InvokeRepeating(nameof(RandomSpawn), 0, 3);
        //byeRoutines = StartCoroutine(Bye());
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            RandomSpawn();
            yield return new WaitForSeconds(3);
        }
    }
    
    void RandomSpawn()
    {
        var index = Random.Range(0, spawnPoints.Length);
        var spawnPoint = spawnPoints[index];
        
        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
    }

    IEnumerator Hello(float delay)
    {
        yield return new WaitForSeconds(delay);
        
        Debug.Log("Hello" + Time.frameCount);
        Debug.Log("Hello" + Time.frameCount);
        Debug.Log("Hello" + Time.frameCount);
        yield return null;
        Debug.Log("Hello" + Time.frameCount);
        yield return null;
        yield return null;
        Debug.Log("Hello" + Time.frameCount);

    }

    IEnumerator Bye()
    {
        while (true)
        {
            Debug.Log("Bye" + Time.frameCount + " " + Time.time);
            yield return new WaitForSeconds(1f);
            //yield return null;

            StartCoroutine(Hello(4));
            yield return new WaitForSeconds(1f);

            // if (Time.time > 5)
            // {
            //     yield break;
            // }

        }
    }

    void Update()
    {
        // if (Time.time > 3)
        // {
        //     //StopAllCoroutines();
        //     StopCoroutine(byeRoutines);
        // }
    }

}
