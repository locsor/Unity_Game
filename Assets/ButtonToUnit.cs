using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonToUnit : MonoBehaviour {
    public Camera Main_Camera;
    public Button yourButton;
    public GameObject AttachedUnit;
    void Start()
    {
        Debug.Log(AttachedUnit.name);
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }
    void Update()
    {
        AttachedUnit = GameObject.Find("Unit 1");
    }
    void TaskOnClick()
    {
        Debug.Log(AttachedUnit.transform.position.x);
        Main_Camera.transform.position = new Vector3(AttachedUnit.transform.position.x, AttachedUnit.transform.position.y, AttachedUnit.transform.position.z);
    }
}
