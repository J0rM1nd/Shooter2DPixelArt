using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject toad;

    public void Awake()
    {
        Vector3 r = 2f*(new Vector3 (Random.value*2-1, Random.value*2-1, 0));
        Instantiate(toad,(new Vector3(-34f,7.5f,0))+r, Quaternion.identity, gameObject.transform);
        r = 2f * (new Vector3(Random.value * 2 - 1, Random.value * 2 - 1, 0));
        Instantiate(toad, (new Vector3(16f, 10f, 0)) + r, Quaternion.identity, gameObject.transform);
        r = 2f * (new Vector3(Random.value * 2 - 1, Random.value * 2 - 1, 0));
        Instantiate(toad, (new Vector3(5f, -13f, 0)) + r, Quaternion.identity, gameObject.transform);
    }
}
