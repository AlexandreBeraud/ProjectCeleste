using UnityEngine;

public class SpawnObjecsScript : MonoBehaviour
{
    [SerializeField] private AlimentsScriptableObject aliments;
    [SerializeField] private KeyCode spawnKey = KeyCode.Space;

    void Update()
    {
        if (Input.GetKeyDown(spawnKey))
        {
            int randomIndex = Random.Range(0, aliments.Aliments.Count);
            var alimentToSpawn = aliments.GetAlimentByIndex(randomIndex);

            GameObject spawned = Instantiate(alimentToSpawn.Prefab, transform.position, transform.rotation);

            var controller = spawned.GetComponent<AlimentsController>();
            if (controller != null)
            {
                controller.SetIndex(alimentToSpawn.Index);
                controller.SetAlimentsData(aliments);
            }

            Debug.Log($"{alimentToSpawn.Name} spawned");
        }
    }
}