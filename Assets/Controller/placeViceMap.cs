using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class placeViceMap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RectTransform viceMap = GetComponent<RectTransform>();
        viceMap.anchoredPosition3D = new Vector3(Screen.width / 2 - viceMap.rect.width / 2, Screen.height / 2 - viceMap.rect.height / 2, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
