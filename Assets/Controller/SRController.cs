using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SRController : MonoBehaviour
{
    private Toggle mToggle;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        mToggle = gameObject.GetComponent<Toggle>();
        mToggle.onValueChanged.AddListener(toggleSR);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toggleSR(bool value)
    {
        if (!value)
        {
            player.GetComponent<Sound>().Off();
        }
        else
        {
            player.GetComponent<Sound>().On();
        }
        player.GetComponent<Sound>().enabled = value;
        player.GetComponent<AudioSource>().enabled = value;
    }
}
