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
        if (bombInfo == null)
        {
            Debug.LogWarning($"No bomb info found for level {bombLevel}");
            return;
        }

        // Effets visuels et sonores
        Instantiate(explosionParticleSystem, transform.position, Quaternion.identity);

        AudioManager.instance.PlaySFX(bombInfo.DestroySound, transform.position);

        DestroyAliments(bombInfo);
        Destroy(gameObject);
    }

    private void DestroyAliments(BombScriptableObject.Bomb bombInfo)
    {
        int alimentsToDestroy = Mathf.Clamp(bombLevel, 1, 3);

        Vector3 halfExtents = boxCollider.size * 1f;
        halfExtents.Scale(transform.localScale);

        Collider[] colliders = Physics.OverlapBox(transform.position, halfExtents, Quaternion.identity);

        List<Collider> nearbyAliments = new List<Collider>();
        foreach (var col in colliders)
        {
            if (col.CompareTag("Aliments"))
                nearbyAliments.Add(col);
        }

        nearbyAliments.Sort((a, b) =>
            Vector3.Distance(a.transform.position, transform.position)
                .CompareTo(Vector3.Distance(b.transform.position, transform.position))
        );

        int destroyedCount = 0;
        for (int i = 0; i < Mathf.Min(alimentsToDestroy, nearbyAliments.Count); i++)
        {
            GameObject aliment = nearbyAliments[i].gameObject;
            if (aliment != null)
            {
                Destroy(aliment);
                destroyedCount++;
            }
        }

        Debug.Log($"Bomb Level {bombInfo.Level} destroyed {destroyedCount}/{alimentsToDestroy} aliments");
    }

    public void SetBombLevel(int level) => bombLevel = Mathf.Clamp(level, 1, 3);
    public void SetBombData(BombScriptableObject data) => bombData = data;
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.3f);
        Vector3 halfExtents = boxCollider.size * 0.5f;
        halfExtents.Scale(transform.localScale);
        Gizmos.DrawCube(transform.position, halfExtents * 2);
    }
}
