using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    [Header("Bomb Data")]
    [SerializeField] private BombScriptableObject bombData;

    [Header("Bomb Prefabs & Inputs")]
    [SerializeField] private List<GameObject> bombPrefabs = new List<GameObject>();
    [SerializeField] private List<KeyCode> bombKeys = new List<KeyCode>();

    [Header("Spawn Settings")]
    [SerializeField] private Transform spawnPoint;

    [Header("Score System")]
    [SerializeField] private PointsSystemController pointsSystemController;

    private void Update()
    {
        for (int i = 0; i < bombKeys.Count && i < bombPrefabs.Count; i++)
        {
            if (Input.GetKeyDown(bombKeys[i]))
            {
                int bombLevel = i + 1;
                TrySpawnBomb(bombLevel);
            }
        }
    }

    private void TrySpawnBomb(int level)
    {
        var bombInfo = bombData.GetBombByLevel(level);

        int costPoints = bombInfo.CostPoints;

        // Vérifie le score
        if (pointsSystemController.ActualScore < costPoints)
        {
            Debug.Log($"Not enough points to change the next aliment. Need {costPoints} points");
            return;
        }

        // Déduit les points
        pointsSystemController.SubScore(costPoints);
        Debug.Log($"Bomb {bombInfo.Name} launched ! Cost : {costPoints} points");

        SpawnBomb(level, bombInfo);
    }

    private void SpawnBomb(int level, BombScriptableObject.Bomb bombInfo)
    {
        if (level - 1 >= bombPrefabs.Count)
        {
            Debug.LogWarning($"No prefab for this bomb's {level}");
            return;
        }

        GameObject prefabToSpawn = bombPrefabs[level - 1];

        GameObject bombObj = Instantiate(prefabToSpawn, spawnPoint.position, Quaternion.identity);

        BombPowerUp bombScript = bombObj.GetComponent<BombPowerUp>();
        if (bombScript != null)
        {
            bombScript.SetBombLevel(level);
            bombScript.SetBombData(bombData);
        }

        if (bombInfo.SpawnSound != null)
            AudioSource.PlayClipAtPoint(bombInfo.SpawnSound, spawnPoint.position);
    }
}
