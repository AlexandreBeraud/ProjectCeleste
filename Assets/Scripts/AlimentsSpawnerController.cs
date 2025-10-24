using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AlimentsSpawnerController : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private AlimentsScriptableObject aliments;
    [SerializeField] private KeyCode spawnKey = KeyCode.Space;

    [Header("Movement Settings")]
    [SerializeField] private KeyCode moveLeftKey = KeyCode.LeftArrow;
    [SerializeField] private KeyCode moveRightKey = KeyCode.RightArrow;
    [SerializeField] private float moveDistance = 2f;
    [SerializeField] private float moveDuration = 0.2f;
    [SerializeField] private bool limitMovement = true;
    [SerializeField] private Ease movementEase = Ease.Linear;
    [SerializeField] private float minX = -5f;
    [SerializeField] private float maxX = 5f;

    [Header("UI")]
    [SerializeField] private Image nextAlimentImage;

    private Tween moveTween;
    private AlimentsScriptableObject.Aliment nextAliment;
    
    public static AlimentsSpawnerController Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        SelectNextAliment();
    }

    void Update()
    {
        HandleMovement();
        HandleSpawn();
    }

    private void HandleMovement()
    {
        if (moveTween != null && moveTween.IsActive() && moveTween.IsPlaying())
            return;

        float targetX = transform.position.x;

        if (Input.GetKey(moveLeftKey))
            targetX -= moveDistance * Time.deltaTime * 5f;
        else if (Input.GetKey(moveRightKey))
            targetX += moveDistance * Time.deltaTime * 5f;

        if (limitMovement)
            targetX = Mathf.Clamp(targetX, minX, maxX);

        transform.position = Vector3.Lerp(transform.position, new Vector3(targetX, transform.position.y, transform.position.z), moveDuration);
    }
    
    private void HandleSpawn()
    {
        if (Input.GetKeyDown(spawnKey) && nextAliment != null)
        {
            GameObject spawned = Instantiate(nextAliment.Prefab, transform.position, transform.rotation);

            var controller = spawned.GetComponent<AlimentsController>();
            if (controller != null)
            {
                controller.SetIndex(nextAliment.Index);
                controller.SetAlimentsData(aliments);
            }

            Debug.Log($"{nextAliment.Name} spawned");

            SelectNextAliment();
        }
    }

    public void SelectNextAliment()
    {
        int randomIndex = Random.Range(0, aliments.Aliments.Count);
        nextAliment = aliments.GetAlimentByIndex(randomIndex);

        if (nextAlimentImage != null && nextAliment != null)
        {
            nextAlimentImage.sprite = nextAliment.Sprite;
            nextAlimentImage.color = Color.white;
        }

        Debug.Log($"Next aliment: {nextAliment.Name}");
    }
}
