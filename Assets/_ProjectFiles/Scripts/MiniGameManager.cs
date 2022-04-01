using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameManager : MonoBehaviour
{
    public int AccumulatedCoins;
    public Transform ObstaclesParent;
    public ObstacleEntry[] ObstaclesPrefabs;

    readonly System.Random rand = new System.Random();

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Play()
    {
        StartCoroutine(Spawner());
    }

    public IEnumerator Spawner()
    {
        while (true)
        {
            var obstacle = ObstaclesPrefabs[rand.Next(ObstaclesPrefabs.Length)];
            if ((float)rand.NextDouble() <= obstacle.SpawnRate)
            {
                var instance = Instantiate(obstacle.Prefab, ObstaclesParent);
                instance.name = instance.name.Replace("(Clone)", string.Empty);
                instance.transform.Translate(Vector3.up * ((float)rand.NextDouble() * 2.0f - 1.0f) * 2.0f);

                var secs = (float)rand.NextDouble() * 1.0f;
                yield return new WaitForSeconds(secs);
            }
        }
    }

    public void Quit(string target)
    {
        SceneManager.LoadScene(target);
    }
}

[Serializable]
public struct ObstacleEntry
{
    public GameObject Prefab;
    public float SpawnRate;
}
