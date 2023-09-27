using UnityEngine;
using UnityEngine.UI;

public class ShootButton : MonoBehaviour
{
    [SerializeField] GameObject player;
    private void Awake() => GetComponent<Button>().onClick.AddListener(OnClick);

    public void OnClick()
    {
        player.GetComponent<Player>().ShootTrigger = true;
    }
}
