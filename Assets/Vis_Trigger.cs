using UnityEngine;
using System.Collections;

public class Vis_Trigger : MonoBehaviour {
    void Start () {
    }
    void OnTriggerEnter2D(BoxCollider2D other)
    {
        Debug.Log(other);
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("entered");
            Debug.Log(1);
        }
    }
    void OnTriggerExit2D(BoxCollider2D other)
    {
        Debug.Log(other);
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Exited");
            Debug.Log(1);
        }
    }
}
