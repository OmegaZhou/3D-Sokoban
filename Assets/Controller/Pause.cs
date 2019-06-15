using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject player;
    private bool isActived = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.GetComponent<FPMovement>().isPaused = true;
            print("space pressed");
            if (!isActived)
            { 
                pauseMenu.SetActive(true);
                isActived = true;
            }
            else
            {
                pauseMenu.GetComponent<visibility>().appear();
            }
        }
    }
}
