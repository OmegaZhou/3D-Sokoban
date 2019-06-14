using UnityEngine;
public class ButtonTrigger : MonoBehaviour
{
    void Start()
    {

    }
    void Update()
    {

    }
    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "box" || collider.tag == "Player")
        {
            var door = GameObject.Find("Door");
            door.transform.Rotate(new Vector3(-90, 0, 0));
            door.transform.position = new Vector3(4.5f, 1.5f, 2);
            door.GetComponent<MeshCollider>().enabled = false;
        }
    }
    void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "box" || collider.tag == "Player")
        {
            var door = GameObject.Find("Door");
            door.transform.Rotate(new Vector3(90, 0, 0));
            door.transform.position = new Vector3(4.5f, 2, 1.5f);
            door.GetComponent<MeshCollider>().enabled = true;
        }
    }
}