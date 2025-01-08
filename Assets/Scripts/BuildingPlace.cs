using UnityEngine;

public class BuildingPlace : MonoBehaviour
{
    public bool isOccupied = false;

    public void SetOccupied(bool occupied)
    {
        isOccupied = occupied;
        gameObject.SetActive(!occupied); // Deactivate if occupied
    }
}
