using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(newGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void newGame()
    {
        SceneManager.LoadScene("PushBox");
    }
}
