using UnityEngine;
using TowerBreaker.Data;
using TowerBreaker.Equipment;

public class TestInventory : MonoBehaviour
{
    [SerializeField] private ItemDataSO[] testItems;

    private void Start()
    {
        foreach (var item in testItems)
            PlayerInventory.Instance.AddItem(item);
    }
}