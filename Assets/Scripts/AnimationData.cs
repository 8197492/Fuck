using System;
using System.Collections.Generic;
using UnityEngine;

public enum AnimationLayerId
{
	DEFAULT,
	MOVE_BREAK,
	STATE,
    ACTION,
    HITED,
    DIE,
	ROTATE_H,
	ROTATE_V,
	TOTAL,
}

public class AnimInfo
{
	public AnimationLayerId layer;
	public WrapMode wrapMode;
	public AnimationBlendMode blendMod = AnimationBlendMode.Blend;
	public int upperWeight;
	public int lowerWeight;
}

public static class AnimationData
{
    public static readonly string Idle = "stand";
    public static readonly string Run = "run";

    public static readonly string Attack01 = "attack01";
    public static readonly string Hit = "suffer";
    public static readonly string Die = "die";

    static AnimationData()
	{
        Set(Idle, AnimationLayerId.DEFAULT, WrapMode.Loop);
        Set(Run, AnimationLayerId.DEFAULT, WrapMode.Loop);
        Set(Hit, AnimationLayerId.HITED, WrapMode.Once);
        Set(Attack01, AnimationLayerId.ACTION, WrapMode.Once);
        //AddMoveMixAnim(Attack01);
    }

    public static Dictionary<string, AnimInfo> Datas { get { return mDatas; } }
	public static List<string> MoveMixAnims { get { return mMoveMixAnims; } }

	public static bool Get(string name, out AnimInfo info)
	{
		return mDatas.TryGetValue(name, out info);
	}

	public static void Set(string name, AnimInfo info)
	{
		if (!mDatas.ContainsKey(name))
		{
            mDatas[name] = info;
		}
	}

	public static void Set(string name, AnimationLayerId layer, WrapMode wrap, AnimationBlendMode blend)
	{
		AnimInfo info = new AnimInfo();
		info.layer = layer;
		info.wrapMode = wrap;
		info.blendMod = blend;
		Set(name, info);
	}

	public static void Set(string name, AnimationLayerId layer, WrapMode wrap)
	{
		Set(name, layer, wrap, AnimationBlendMode.Blend);
	}

	public static void AddMoveMixAnim(string animNam)
	{
        mMoveMixAnims.Add(animNam);
	}

	public static void Format(Animation anim, float speed = 1.0f)
	{
		anim.playAutomatically = false;
		anim.Stop();
		anim.cullingType = AnimationCullingType.AlwaysAnimate;
		foreach (KeyValuePair<string, AnimInfo> pair in AnimationData.Datas)
		{
			SetAnimInfo(anim, pair.Key, pair.Value, speed);
		}
	}

	public static void SetAnimInfo(Animation animation, string name, AnimInfo info, float speed)
	{
		AnimationState animState = animation[name];
		if (animState == null)
		{
			return;
		}
		animState.layer = (int)info.layer;
		animState.wrapMode = info.wrapMode;
		animState.blendMode = info.blendMod;
		animState.speed = speed;
	}

	public static bool IsMoveBreakAnim(string name)
	{
		AnimInfo info;
		if (mDatas.TryGetValue(name, out info))
		{
			return info.layer == AnimationLayerId.MOVE_BREAK;
		}
		else
		{
			return false;
		}
	}

	public static bool IsLoopAnim(string name)
	{
		AnimInfo info;
		if (mDatas.TryGetValue(name, out info))
		{
			return info.wrapMode == WrapMode.Loop;
		}
		else
		{
			return false;
		}
	}


	static List<string> mMoveMixAnims = new List<string>();
	static Dictionary<string, AnimInfo> mDatas = new Dictionary<string, AnimInfo>();
}

