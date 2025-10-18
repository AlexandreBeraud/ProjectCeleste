using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AlimentsScriptableObject", menuName = "Scriptable Objects/AlimentsScriptableObject")]
public class AlimentsScriptableObject : ScriptableObject
{
    [System.Serializable]
    public class Aliment
    {
        [SerializeField] private int index;
        [SerializeField] private string name;
        [SerializeField] GameObject prefab;
        [SerializeField] private int points;
        [SerializeField] Sprite sprite;
        [SerializeField] AudioClip spawnSound;
        [SerializeField] AudioClip destroySound;
    
        public int Index { get => index; set => index = value; }
        public string Name { get => name; set => name = value; }
        public GameObject Prefab { get => prefab; set => prefab = value; }
        public int Points { get => points; set => points = value; }
        public Sprite Sprite { get => sprite; set => sprite = value; }
        public AudioClip SpawnSound { get => spawnSound; set => spawnSound = value; }
        public AudioClip DestroySound { get => destroySound; set => destroySound = value; }
    }
    
    [SerializeField] private List<Aliment> aliments = new List<Aliment>();
    
    public List<Aliment> Aliments { get => aliments; set => aliments = value; }

    public Aliment GetAlimentByIndex(int index)
    {
        return aliments.Find(aliment => aliment.Index == index);
    }
}
