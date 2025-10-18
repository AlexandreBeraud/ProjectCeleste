using UnityEngine;

public class SpawnObjecsScript : MonoBehaviour
{
    [SerializeField] private AlimentsScriptableObject aliments;
    [SerializeField] private KeyCode spawnKey = KeyCode.Space;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(spawnKey))
        {
            AlimentsScriptableObject.Aliment alimentToSpawn = aliments.GetAlimentByIndex(Random.Range(0, aliments.Aliments.Count));
            //AlimentsScriptableObject.Aliment alimentToSpawn = aliments.GetAlimentByIndex(5);

            
            Instantiate(alimentToSpawn.Prefab, transform.position, transform.rotation);
            Debug.Log(alimentToSpawn.Name + " spawned");
        }
    }
}
