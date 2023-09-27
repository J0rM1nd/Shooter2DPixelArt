using System.Drawing;
using UnityEngine;

public class ItemBar : MonoBehaviour
{
    [SerializeField] private GameObject playerGO;
    private Player player;

    public GameObject trashButton;

    private Sprite activeSlot;
    private Sprite activeSlot2;
    private Sprite inactiveSlot;
    private Sprite inactiveSlot2;
    private Sprite empty;
    private Sprite empty2;

    public ItemSlot[,] slots;

    private Point? _activeSlot;
    public Point? ActiveSlot
    {
        get
        {
            return _activeSlot;
        }
        set
        {
            _activeSlot = value;
            if (value.HasValue)
                for (int i = 0; i < slots.GetLength(0); i++)
                    for (int j = 0; j < slots.GetLength(1); j++)
                        slots[i, j].Selected = i == value.Value.X && j == value.Value.Y;
            else
                for (int i = 0; i < slots.GetLength(0); i++)
                    for (int j = 0; j < slots.GetLength(1); j++)
                        slots[i, j].Selected = false;
        }
    }

    public int Count(Item item)
    {
        for (int i = 0; i < slots.GetLength(0); i++)
            for (int j = 0; j < slots.GetLength(1); j++)
            {
                if (slots[i, j].Item.Name == item.Name)
                    return slots[i, j].Count;
            } 
        return 0;
    }

    public void ClearAll()
    {
        for (int i = 0; i < slots.GetLength(0); i++)
            for (int j = 0; j < slots.GetLength(1); j++)
                slots[i, j].UpdateItem(Item.Empty, 0);
    }

    public ItemSlot FindItemOrNull(Item item)
    {
        for (int i = 0; i < slots.GetLength(0); i++)
            for (int j = 0; j < slots.GetLength(1); j++)
                if (slots[i, j].Item.Name == item.Name)
                    return slots[i, j];
        return null;
    }

    public void AddItems(Item item, int count)
    {
        for (int i = 0; i < slots.GetLength(0); i++)
            for (int j = 0; j < slots.GetLength(1); j++)
            {
                if (slots[i, j].Item.Name == item.Name)
                {
                    slots[i, j].Count += count;
                    return;
                }
            }
        for (int i = 0; i < slots.GetLength(0); i++)
            for (int j = 0; j < slots.GetLength(1); j++)
                if (slots[i, j].Count == 0)
                {
                    slots[i, j].UpdateItem(item, count);
                    return;
                }
        throw new System.Exception();
    }
    public void DeleteItems(Item item, int count)
    {
        for (int i = 0; i < slots.GetLength(0); i++)
            for (int j = 0; j < slots.GetLength(1); j++)
                if (slots[i, j].Item.Name == item.Name)
                {
                    if (slots[i, j].Count == count)
                    {
                        slots[i, j].UpdateItem(Item.Empty, 0);
                        return;
                    }
                    slots[i, j].Count -= count;
                    slots[i, j].UpdateNumber(slots[i, j].Count);
                    if (slots[i, j].Count < 0)
                        throw new System.Exception();
                    return;
                }
        throw new System.Exception();
    }
    public void DeleteSelected()
    {
        if (!ActiveSlot.HasValue)
            throw new System.Exception();
        slots[ActiveSlot.Value.X, ActiveSlot.Value.Y].UpdateItem(Item.Empty, 0);
    }

    public void ActivateSlot(ItemSlot slot)
    {
        for (int i = 0; i < slots.GetLength(0); i++)
            for (int j = 0; j < slots.GetLength(1); j++)
                if (slots[i, j] == slot)
                    ActiveSlot = new Point(i, j);
    }
    private void Awake()
    {
        player = playerGO.GetComponent<Player>();

        slots = new ItemSlot[transform.childCount, transform.GetChild(0).childCount];

        inactiveSlot = Resources.Load<Sprite>("ItemSlot");
        inactiveSlot2 = Resources.Load<Sprite>("ItemSlot2");
        activeSlot = Resources.Load<Sprite>("ItemSelectedSlot");
        activeSlot2 = Resources.Load<Sprite>("ItemSelectedSlot2");
        empty = Resources.Load<Sprite>("EmptyItem");
        empty2 = Resources.Load<Sprite>("EmptyItem2");

        for (int i = 0; i < slots.GetLength(0); i++)
            for (int j = 0; j < slots.GetLength(1); j++)
            {
                slots[i, j] = transform.GetChild(i).GetChild(j).GetComponent<ItemSlot>();
                slots[i, j].ItemBar = this;
            }
        ClearAll();
    }
    private void UpdateSlot(int x, int y, Item item, int count = 1)
    {
        slots[x, y].UpdateItem(item, count);
    }
}