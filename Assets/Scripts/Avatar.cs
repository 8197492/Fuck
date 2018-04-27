using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Avatar
{
    private GameObject mFacadeObj = null;
    private AnimationContoller mAnimContoller = null;
    public void Init(string sResPath)
    {
        mFacadeObj = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>(sResPath));
        mFacadeObj.transform.position = Vector3.zero;
        mAnimContoller = new AnimationContoller();

        mAnimContoller.Init(mFacadeObj.GetComponentInChildren<Animation>());
    }

    public void Play(string sName)
    {
        mAnimContoller.Play(sName);
    }
}
