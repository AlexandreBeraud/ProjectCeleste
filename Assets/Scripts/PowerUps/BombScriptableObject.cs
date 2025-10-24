using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BombsScriptableObject", menuName = "Scriptable Objects/BombsScriptableObject")]
public class BombScriptableObject : ScriptableObject
{
    [System.Serializable]
    public class Bomb
    {
        [SerializeField] private int level;
        [SerializeField] private string name;
        [SerializeField] GameObject prefab;
        [SerializeField] private int costPoints;
        [SerializeField] Sprite sprite;
        [SerializeField] AudioClip spawnSound;
        [SerializeField] AudioClip destroySound;
    
        public int Level { get => level; set => level = value; }
        public string Name { get => name; set => name = value; }
        public GameObject Prefab { get => prefab; set => prefab = value; }
        public int CostPoints { get => costPoints; set => costPoints = value; }
        public Sprite Sprite { get => sprite; set => sprite = value; }
        public AudioClip SpawnSound { get => spawnSound; set => spawnSound = value; }
        public AudioClip DestroySound { get => destroySound; set => destroySound = value; }
    }
    
    [SerializeField] private List<Bomb> bombs = new List<Bomb>();
    
    public List<Bomb> Bombs { get => bombs; set => bombs = value; }

    public Bomb GetBombByLevel(int level)
    {
        return bombs.Find(bombs => bombs.Level == level);
    }
}