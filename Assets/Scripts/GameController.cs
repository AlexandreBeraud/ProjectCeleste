using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Importations")]
    [SerializeField] private AlimentsSpawnerController _alimentsSpawnerController;
    [SerializeField] private BombController _bombController;
    [SerializeField] private HandAlimentController _handAlimentController;

    [Header("Keycodes")] 
    [SerializeField] private KeyCode spawnKeyCode;
    [SerializeField] private KeyCode moveleftkeycode;
    [SerializeField] private KeyCode moverightkeycode;
    [SerializeField] private KeyCode handPowerUpkeycode;
    
    [SerializeField] private List<KeyCode> bombKeyCodes = new List<KeyCode>();

    [ContextMenu("Update Keycodes")]
    public void UpdateKeycodes()
    {
        Debug.Log("Update keycodes from GameController");

        if (_alimentsSpawnerController != null)
        {
            _alimentsSpawnerController.SpawnKey = spawnKeyCode;
            _alimentsSpawnerController.MoveLeftKey = moveleftkeycode;
            _alimentsSpawnerController.MoveRightKey = moverightkeycode;
        }

        if (_bombController != null)
        {
            var bombKeys = _bombController.BombKeys;

            int count = Mathf.Min(bombKeys.Count, bombKeyCodes.Count);
            Debug.Log($"Bombkeys : {bombKeys.Count}");

            for (int i = 0; i < count; i++)
            {
                bombKeys[i] = bombKeyCodes[i];
            }

            if (bombKeyCodes.Count > bombKeys.Count)
            {
                for (int i = bombKeys.Count; i < bombKeyCodes.Count; i++)
                {
                    bombKeys.Add(bombKeyCodes[i]);
                    Debug.Log($"BombKey added : {bombKeyCodes[i]}");
                }
            }

            _bombController.BombKeys = bombKeys;
        }

        if (_handAlimentController != null)
        {
            _handAlimentController.HandPowerUpKey = handPowerUpkeycode;
        }
    }
}
