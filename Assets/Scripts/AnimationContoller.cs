using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationContoller
{
    private Animation mAnim = null;

    public void Init(Animation pAnim)
    {
        mAnim = pAnim;
        AnimationData.Format(mAnim);
        Play(AnimationData.Idle);
    }

    public void Play(string sAniName, bool bBreakCurrent = false)
    {
        if (mAnim == null)
        {
            return;
        }
        AnimationState pAnimState = mAnim[sAniName];
        if (pAnimState != null)
        {
            if (bBreakCurrent && mAnim.IsPlaying(sAniName))
            {
                mAnim.Rewind(sAniName);
            }
            mAnim.CrossFade(sAniName, 0.15f);
        }
    }
}
