using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class visibility : MonoBehaviour
{
    CanvasGroup cg;

    // Start is called before the first frame update
    void Start()
    {
        cg = gameObject.GetComponent<CanvasGroup>();
        if(cg == null)
        {
            cg = gameObject.AddComponent<CanvasGroup>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void appear()
    {
        print("appeared");
        cg.alpha = Mathf.Lerp(0, 1.0f, 2.5f);
    }

    public void disappear()
    {
        print("disappeared");
        cg.alpha = Mathf.Lerp(1.0f, 0, 2.5f);
    }
}
