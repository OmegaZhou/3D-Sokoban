
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;

    private Transform target; 
    public Vector3 Offset;  
    public float smoothing = 0.33f; 
                                 
    void Start()
    {
        target = player.transform;
        if(Offset.Equals(Vector3.zero))
            Offset = new Vector3(0, 0.5f, -1.444581f);
        Collision test = new Collision();
    }
    void LateUpdate()
    {
        //iTween.MoveTo(this.gameObject, target.position + target.TransformDirection(Offset), 1.0f);
        transform.position = Vector3.Lerp(transform.position, target.position + target.TransformDirection(Offset), Time.deltaTime * smoothing);
        //transform.position = target.position + target.TransformDirection(Offset);

        transform.LookAt(target);
    }
}