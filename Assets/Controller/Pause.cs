using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject player;

    public GameObject light;
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
            print("space pressed");
            player.GetComponent<FPMovement>().isPaused = true;
            player.GetComponent<Animator>().speed = 0;
            light.GetComponent<Light>().color = new Color(0, 0, 0);
            
            if (!isActived)
            {
                print("activated");
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
