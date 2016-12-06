using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour {
    private Renderer rend;
    public GameObject check;
    private string tag;
    // Use this for initialization
    void Start () {
        rend = GetComponent<Renderer>();
        tag = check.tag;
        if (tag == "Enemy")
        {
            rend.enabled = false;
            Debug.Log(1);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Vision Field") && tag == "Enemy")
        {
            Debug.Log(1);
            rend.enabled = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Vision Field") && gameObject.tag == "Enemy")
        {
            Debug.Log(1);
            rend.enabled = false;
        }
    }
}
