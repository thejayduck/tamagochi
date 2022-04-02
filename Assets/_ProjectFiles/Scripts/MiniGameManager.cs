using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MiniGameManager : MonoBehaviour
{
    public int AccumulatedCoins;

    public TMP_Text ScoreText;

    public Transform ObstaclesParent;
    public ObstacleEntry[] ObstaclesPrefabs;
    public UnityEvent OnDeathEvent;

    readonly System.Random rand = new System.Random();

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Play()
    {
        StartCoroutine(Spawner());
    }

    public void OnDeath()
    {
        StopAllCoroutines();

        foreach (Transform child in ObstaclesParent)
            Destroy(child.gameObject);

        OnDeathEvent.Invoke();
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
