using System;
using Unity.VisualScripting;
using UnityEngine;

public class AlimentsController : MonoBehaviour
{
    [SerializeField] private BoxCollider boxCollider;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.name);
        Merge(other);
    }

    private void Merge(Collision collision)
    {
        if (this.gameObject.CompareTag(collision.gameObject.tag))
        {
            Debug.Log("Two same elements interact");
        }
    }
}
