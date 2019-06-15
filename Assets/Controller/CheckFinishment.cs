using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckFinishment : MonoBehaviour
{
    Vector3[] desPos;
    Transform[] box;
    int boxLen = 0, desLen = 0;

    // Start is called before the first frame update

    void Start()
    {
        desPos = new Vector3[transform.childCount];
        box = new Transform[transform.childCount];
        for(int i = 0; i < transform.childCount; i++)
        {
            string tag = transform.GetChild(i).tag;
            if (tag == "box")
            {
                box[boxLen++] = transform.GetChild(i).transform;
            }
            else if(tag == "des")
            {
                desPos[desLen++] = transform.GetChild(i).transform.position;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool checkMatch()
    {
        print("box len is " + boxLen);
        print("des len is " + desLen);

        bool matched = false;
        for(int i = 0; i < boxLen; i++)
        {
            for(int j = 0; j < desLen; j++)
            {
                print("the difference is " + (box[i].position - desPos[i]));
                if ((box[i].position - desPos[j]).magnitude < 0.0001)
                {
                    matched = true;
                    break;
                }
            }
            if (!matched)
            {
                return false;
            }
            matched = false;
        }
        return true;
    }
}
