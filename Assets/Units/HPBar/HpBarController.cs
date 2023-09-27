using UnityEngine;

public class HpBarController : MonoBehaviour
{
    private GameObject box;
    private GameObject green;
    private GameObject yellow;
    private GameObject red;
    private GameObject white;
    void Awake()
    {
        box = transform.Find("Box").gameObject;
        green = box.transform.Find("HPGreen").gameObject;
        yellow = box.transform.Find("HPYellow").gameObject;
        red = box.transform.Find("HPRed").gameObject;
        white = box.transform.Find("HPWhite").gameObject;
    }
    public void ChangeHP(float persent)
    {
        GetComponent<SpriteRenderer>().enabled = false;
        green.GetComponent<SpriteRenderer>().enabled = false;
        yellow.GetComponent<SpriteRenderer>().enabled = false;
        red.GetComponent<SpriteRenderer>().enabled = false;
        white.GetComponent<SpriteRenderer>().enabled = false;
        
        green.transform.position = new Vector3(green.transform.position.x, green.transform.position.y, green.transform.position.z);
        if ( persent == 1 || persent <= 0 )
            return;
        GetComponent<SpriteRenderer>().enabled = true;
        if (persent > 1f)
        {
            persent = 1f / persent;
            white.GetComponent<SpriteRenderer>().enabled = true;
        }
        else if (persent > 2f / 3f)
            green.GetComponent<SpriteRenderer>().enabled = true;
        else if (persent > 1f / 3f)
            yellow.GetComponent<SpriteRenderer>().enabled = true;
        else
            red.GetComponent<SpriteRenderer>().enabled = true;

        float pix = Mathf.Round(persent * 20f) / 20f;
        box.transform.localScale = new Vector3(pix, 1, 1);
        box.transform.localPosition = new Vector3(5f * (pix - 1) / 8f, 0, 0);
    }
}