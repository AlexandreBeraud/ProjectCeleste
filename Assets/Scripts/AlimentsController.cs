using UnityEngine;

public class AlimentsController : MonoBehaviour
{
    [SerializeField] private AlimentsScriptableObject alimentsData;
    [SerializeField] private int alimentIndex;
    [SerializeField] private BoxCollider boxCollider;

    private bool hasMerged = false;

    private void OnCollisionEnter(Collision other)
    {
        if (hasMerged) return;

        AlimentsController otherAliment = other.gameObject.GetComponent<AlimentsController>();
        if (otherAliment == null || otherAliment.hasMerged) return;

        TryMerge(otherAliment);
    }

    private void TryMerge(AlimentsController otherAliment)
    {
        if (alimentIndex == otherAliment.alimentIndex)
        {
            hasMerged = true;
            otherAliment.hasMerged = true;

            Debug.Log($"Merge detected : index {alimentIndex}");

            int nextIndex = alimentIndex + 1;
            var nextAliment = alimentsData.GetAlimentByIndex(nextIndex);

            if (nextAliment == null)
            {
                Debug.Log($"No next item {nextIndex}.");
                return;
            }

            var currentAliment = alimentsData.GetAlimentByIndex(alimentIndex);
            if (currentAliment?.DestroySound != null)
                AudioSource.PlayClipAtPoint(currentAliment.DestroySound, transform.position);

            Vector3 spawnPos = (transform.position + otherAliment.transform.position) / 2f;

            GameObject newObj = Instantiate(nextAliment.Prefab, spawnPos, Quaternion.identity);

            AlimentsController newController = newObj.GetComponent<AlimentsController>();
            if (newController != null)
            {
                newController.SetIndex(nextAliment.Index);
                newController.SetAlimentsData(alimentsData);
            }

            if (nextAliment.SpawnSound != null)
                AudioSource.PlayClipAtPoint(nextAliment.SpawnSound, spawnPos);

            Destroy(gameObject);
            Destroy(otherAliment.gameObject);

            Debug.Log($"Merged : {nextAliment.Name} créé !");
        }
    }

    public void SetIndex(int index)
    {
        alimentIndex = index;
    }

    public void SetAlimentsData(AlimentsScriptableObject data)
    {
        alimentsData = data;
    }
}
