using UnityEngine;
using System.Collections;

public class MainCameraMovingScript : MonoBehaviour {
    private float H = 0;
    private float V = 0;
    private float Scrool = 0;

    public float Speed = 1.0f;
    public float SpeedMouseMoving = 1;
    public float ScrollSpeed = 1.0f;
    public float MinCameraSize = 5;
    public float MaxCameraSize = 20;
    public float sizeofbone = 100.0f;

    private Camera cam;
    private Rigidbody2D rb;

    // Use this for initialization
    void Awake ()
    {
        cam = GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {

        H = Input.GetAxis("Horizontal");
        V = Input.GetAxis("Vertical");
        Scrool = Input.GetAxis("Mouse ScrollWheel");

        rb.velocity = Vector2.zero;
        if (Scrool > 0)
            if (cam.orthographicSize - Scrool * ScrollSpeed > MinCameraSize)
                cam.orthographicSize -= Scrool * ScrollSpeed;
            else
                cam.orthographicSize = MinCameraSize;
        else
            if (cam.orthographicSize - Scrool * ScrollSpeed < MaxCameraSize)
            cam.orthographicSize -= Scrool * ScrollSpeed;
        else
            cam.orthographicSize = MaxCameraSize;

        if ((Input.mousePosition.x >= Screen.width - sizeofbone) && (Input.mousePosition.x <= Screen.width))            
            rb.velocity = Vector2.right * SpeedMouseMoving;
        else
       if ((Input.mousePosition.x <= sizeofbone) && (Input.mousePosition.x >= 0))
            rb.velocity = Vector2.left * SpeedMouseMoving;

        if ((Input.mousePosition.y >= Screen.height - sizeofbone) && (Input.mousePosition.y <= Screen.height))
                rb.velocity = Vector2.up * SpeedMouseMoving;
        else
            if ((Input.mousePosition.y <= sizeofbone)&&(Input.mousePosition.y >= 0))
            rb.velocity = Vector2.down * SpeedMouseMoving;
        rb.velocity += new Vector2(H, V) * Speed;
    }

}
