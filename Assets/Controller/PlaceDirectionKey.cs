using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceDirectionKey : MonoBehaviour
{
    public float width = 96;
    public float height = 96;

    // Start is called before the first frame update
    void Start()
    {
        RectTransform group = GetComponent<RectTransform>();
        group.sizeDelta = new Vector2(width, height);

        for (int i = 0; i < transform.childCount; i++)
        {
            var bt = transform.GetChild(i);
            if (bt.name == "right")
            {
                bt.GetComponent<RectTransform>().anchoredPosition3D = new Vector2(0, 0);
            }
            if (bt.name == "left")
            {
                bt.GetComponent<RectTransform>().anchoredPosition3D = new Vector2(-2 * bt.GetComponent<RectTransform>().rect.x, 0);
            }
            if (bt.name == "forward")
            {
                bt.GetComponent<RectTransform>().anchoredPosition3D = new Vector2(-bt.GetComponent<RectTransform>().rect.x, bt.GetComponent<RectTransform>().anchoredPosition3D.y);
            }
            if (bt.name == "back")
            {
                bt.GetComponent<RectTransform>().anchoredPosition3D = new Vector2(-bt.GetComponent<RectTransform>().rect.x, -bt.GetComponent<RectTransform>().anchoredPosition3D.y);
            }
        }
        group.anchoredPosition3D = new Vector3(2.5f * width - Screen.width / 2, 1.5f * height - Screen.height / 2, 0);
    }

        
        

    // Update is called once per frame
    void Update()
    {
        
    }
}
