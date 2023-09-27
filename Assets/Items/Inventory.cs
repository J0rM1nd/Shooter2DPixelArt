using System.Linq;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class Inventory : MonoBehaviour
{
    [SerializeField] private ItemBar backpack;
    [SerializeField] private ItemBar inHeands;

    public Weapon EquippedWeapon;

    public int Count(Item item)
    {
        return backpack.Count(item) + inHeands.Count(item);
    }

    public void ClearAll()
    {
        inHeands.ClearAll();
        backpack.ClearAll();
    }

    public void DeleteInhandsItem(Item item, int count = 1)
    {
        inHeands.DeleteItems(item, count);
    }
    public void DeleteBackpackItem(Item item, int count = 1)
    {
        backpack.DeleteItems(item, count);
        count = backpack.Count(item);
        if (item is Bullet)
        {
            ItemSlot gunSlot = inHeands.FindItemOrNull((item as Bullet).Weapon);
            if (gunSlot != null)
                gunSlot.UpdateNumber(count);
        }
    }
    public void AddBackpackItem(Item item, int count = 1)
    {
        backpack.AddItems(item, count);
        count = backpack.Count(item);
        if (item is Bullet)
        {
            ItemSlot gunSlot = inHeands.FindItemOrNull((item as Bullet).Weapon);
            if (gunSlot != null)
                gunSlot.UpdateNumber(count);
        }
    }
    public void AddInhandsItem(Item item, int count = 1) => inHeands.AddItems(item, count);
}