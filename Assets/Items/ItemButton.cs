using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    [SerializeField] GameObject backpack;
    [SerializeField] GameObject trashButton;
    private void Awake() => GetComponent<Button>().onClick.AddListener(OnClick);

    public void OnClick()
    {
        if (backpack.transform.localPosition.magnitude < 200)
        {
            backpack.transform.localPosition += Vector3.up * 1000;
            trashButton.transform.localPosition += Vector3.up * 1000;
        }
        else
        {
            backpack.transform.localPosition -= Vector3.up * 1000;
            trashButton.transform.localPosition -= Vector3.up * 1000;
        }
    }
}
