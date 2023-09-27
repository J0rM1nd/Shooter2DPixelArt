using UnityEngine;

public class Item
{
    public string Name { get; protected set; }
    public int ID { get; protected set; }
    public Sprite Sprite;

    public Item(Sprite sprite, string name)
    {
        ID = NextID;
        Sprite = sprite;
        Name = name;
    }
    public Item() { }

    public static Item Empty => new Item(_emptySprite, "");

    private static int _nextID;
    public static int NextID { get { _nextID++; return _nextID; } }
    private static Sprite _emptySprite;
    static Item()
    {
        _nextID = -1;
        _emptySprite = null;
    }
}