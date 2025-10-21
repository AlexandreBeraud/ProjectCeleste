using UnityEngine;
using DG.Tweening;

public class AlimentsSpawnerController : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private AlimentsScriptableObject aliments;
    [SerializeField] private KeyCode spawnKey = KeyCode.Space;

    [Header("Movement Settings")]
    [SerializeField] private KeyCode moveLeftKey = KeyCode.LeftArrow;
    [SerializeField] private KeyCode moveRightKey = KeyCode.RightArrow;
    [SerializeField] private float moveSpeed = 5f;         // Vitesse de déplacement
    [SerializeField] private bool limitMovement = true;
    [SerializeField] private float minX = -5f;
    [SerializeField] private float maxX = 5f;
    [SerializeField] private Ease movementEase = Ease.Linear;

    private Tween moveTween;

    void Update()
    {
        SpawnAliments();
        MoveSpawner();
    }

    private void SpawnAliments()
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

    private void MoveSpawner()
    {
        float horizontalInput = 0f;

        if (Input.GetKey(moveLeftKey))
            horizontalInput -= 1f;
        if (Input.GetKey(moveRightKey))
            horizontalInput += 1f;

        if (Mathf.Approximately(horizontalInput, 0f))
            return;

        float targetX = transform.position.x + horizontalInput * moveSpeed * Time.deltaTime;

        if (limitMovement)
            targetX = Mathf.Clamp(targetX, minX, maxX);

        // Kill l'ancien tween si actif pour éviter accumulation
        if (moveTween != null && moveTween.IsActive())
            moveTween.Kill();

        moveTween = transform.DOMoveX(targetX, 0.1f).SetEase(movementEase);
    }
}
