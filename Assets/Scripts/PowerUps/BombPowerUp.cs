using System.Collections.Generic;
using UnityEngine;

public class BombPowerUp : MonoBehaviour
{
    [SerializeField] private BombScriptableObject bombData;
    
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private ParticleSystem explosionParticleSystem;
    
    [Header("DEBUG")]
    [SerializeField] private int bombLevel;
    
    private void OnCollisionEnter(Collision collision)
    {
        Explode();
    }

    private void Explode()
    {
        var bombInfo = bombData.GetBombByLevel(bombLevel);
        
        Instantiate(explosionParticleSystem, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(bombInfo.DestroySound, transform.position);

        DestroyAliments(bombInfo);
        Destroy(gameObject);
    }

    private void DestroyAliments(BombScriptableObject.Bomb bombInfo)
    {
        int alimentsToDestroy = Mathf.Clamp(bombLevel, 1, 3);
        
        Collider[] colliders = Physics.OverlapBox(transform.position, boxCollider.size);
        
        List<Collider> nearbyAliments = new List<Collider>(colliders);
        nearbyAliments.Sort((a,b) => 
            Vector3.Distance(a.transform.position, transform.position)
                .CompareTo(Vector3.Distance(transform.position, b.transform.position)
        ));

        for (int i = 0; i < Mathf.Min(alimentsToDestroy, nearbyAliments.Count); i++)
        {
            GameObject aliment = nearbyAliments[i].gameObject;
            Destroy(aliment);
        }
        
        Debug.Log($"Bomb Level : {bombInfo.Level} ; Destroyed " + alimentsToDestroy);
    }


    public void SetBombLevel(int level)
    {
        bombLevel = Mathf.Clamp(level, 1, 3);
    }

    public void SetBombData(BombScriptableObject data)
    {
        bombData = data;
    }
}
