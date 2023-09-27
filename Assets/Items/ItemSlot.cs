using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] private GameObject num1;
    [SerializeField] private GameObject num2;
    [SerializeField] private GameObject num3;

    private bool _selected;

    public Sprite inactiveSlot;
    public Sprite activeSlot;
    public Sprite empty;

    public Item Item {  get; private set; }
    public int Count;
    public Vector2 size;

    public ItemBar ItemBar;

    private void Awake() => GetComponent<Button>().onClick.AddListener(OnClick);
    private void OnClick() => ItemBar.ActivateSlot(this);

    public bool Selected
    {
        get
        {
            return _selected;
        }
        set
        {
            _selected = value;
            transform.GetChild(0).GetComponent<Image>().sprite = Selected ? activeSlot : inactiveSlot;
        }
    }
    public void UpdateNumber(int n)
    {
        num1.GetComponent<Image>().enabled = false;
        num2.GetComponent<Image>().enabled = false;
        num3.GetComponent<Image>().enabled = false;
        bool b = false;
        if (n >= 100)
        {
            b = true;
            num3.GetComponent<Image>().sprite = Number.Get(n / 100);
            n %= 100;
            num3.GetComponent<Image>().enabled = true;
        }
        if (n >= 10 || b)
        {
            b = true;
            num2.GetComponent<Image>().sprite = Number.Get(n / 10);
            n %= 10;
            num2.GetComponent<Image>().enabled = true;
        }
        if (n > 0 || b)
        {
            b = true;
            num1.GetComponent<Image>().sprite = Number.Get(n);
            num1.GetComponent<Image>().enabled = true;
        }
    }

    public void UpdateItem(Item item, int count)
    {
        Item = item;
        Count = count;
        GetComponent<Image>().sprite = item.Sprite == null ? empty : item.Sprite;
        UpdateNumber(count);
    }
}