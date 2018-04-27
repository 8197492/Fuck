using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    Avatar mHerp = null;
    // Use this for initialization
    void Start ()
    {
        //mHerp = new Avatar();
        //mHerp.Init("Assets/Res/C_Shushi/ShuShi.prefab");
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Debug.Log(hit.transform.gameObject);
            }
        }
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Run"))
        {
            mHerp.Play(AnimationData.Run);
        }
        else if (GUILayout.Button("Idle"))
        {
            mHerp.Play(AnimationData.Idle);
        }
        else if (GUILayout.Button("Attack"))
        {
            mHerp.Play(AnimationData.Attack01);
        }
    }
}
