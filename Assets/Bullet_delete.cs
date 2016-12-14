using UnityEngine;
using System.Collections;
public class Bullet_delete : MonoBehaviour {
    public GameObject blood1;
    public GameObject blood2;
    public GameObject blood3;
    public GameObject blood4;
    public GameObject blood5;
    public GameObject blood6;
    public GameObject blood7;
    public GameObject blood8;
    public GameObject blood9;
    public GameObject blood10;
    public GameObject blood11;
    public GameObject[] blood = new GameObject[11];
    public GameObject globalVar;
    void Start () {
        blood[0] = blood1;
        blood[1] = blood2;
        blood[2] = blood3;
        blood[3] = blood4;
        blood[4] = blood5;
        blood[5] = blood6;
        blood[6] = blood7;
        blood[7] = blood8;
        blood[8] = blood9;
        blood[9] = blood10;
        blood[10] = blood11;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.tag == "StaticObjectWithCollider")
        {
            Destroy(gameObject);
        }
        if(other.transform.name == "Soul")
        {
            Debug.Log('1');
            if (globalVar.GetComponent<GlobalVariabels>().spawned_blood.Count == 100)
            {
                globalVar.GetComponent<GlobalVariabels>().spawned_blood.RemoveAt(0);
            }
            GameObject blood_temp;
            float sprite_num = Random.Range(0f, 10f);
            blood_temp = blood[(int)sprite_num];
            GameObject bloodS = Instantiate(blood_temp, other.transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
            globalVar.GetComponent<GlobalVariabels>().spawned_blood.Add(bloodS);
        }
    }
}
