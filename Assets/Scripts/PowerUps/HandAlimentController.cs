using UnityEngine;

public class HandAlimentController : MonoBehaviour
{
    [SerializeField] private KeyCode handPowerUpKey;
    [SerializeField] private PointsSystemController pointsSystemController;
    [SerializeField] private int costPoints;

    private void Update()
    {
        if (Input.GetKeyDown(handPowerUpKey))
        {
            if (costPoints >= 0 && costPoints <= pointsSystemController.ActualScore)
            {
                Debug.Log($"POWER UP ! Cost you {costPoints} points");
                pointsSystemController.SubScore(costPoints);
                AlimentsSpawnerController.Instance?.SelectNextAliment();
            }
            else
            {
                Debug.Log($"Not enough points to change the next aliment. Need {costPoints} points");
            }
        }
    }
}
