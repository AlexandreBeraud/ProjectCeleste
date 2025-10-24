using UnityEngine;

public class BombPowerUp : MonoBehaviour
{
    [SerializeField] private BombScriptableObject bombData;
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private ParticleSystem particleSystem;
    
    [Header("DEBUG")]
    [SerializeField] private int bombLevel;

    private void Start()
    {
        
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        Instantiate(particleSystem, transform.position, Quaternion.identity);
        
        DestroyAliments(collision);
    }

    private void DestroyAliments(Collision collision)
    {
        
    }
}
