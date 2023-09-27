using UnityEngine;
using UnityEngine.UI;

public class TrashButton : MonoBehaviour
{
    [SerializeField] GameObject itembar;
    private Button Button;
    private void Awake()
    {
        itembar.GetComponent<ItemBar>().trashButton = gameObject;
        Button = GetComponent<Button>();
        Button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        itembar.GetComponent<ItemBar>().DeleteSelected();
    }
}