using System;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;

public class MoveBG : MonoBehaviour
{
	public float backgroundWidth = 7.04f;
    public float moveSpeed = 5f;
	public bool isFinish  {get; private set;} = false;

	public void Move()
	{
		isFinish = false;
		transform.DOMoveX(-22.95f, moveSpeed).SetEase(Ease.Linear).OnComplete(()=> {
			transform.position = Vector2.zero;
			InGameMgr.instance.SetState(eInGameState.Battle);
			isFinish = true;
		});
	}
}
