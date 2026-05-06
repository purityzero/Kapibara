using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FireBall : Skill
{
	public override void Excute()
	{
		base.Excute();
		m_Tweener = m_SpriteRenderer.transform.DOMoveX(InGameMgr.instance.Monster.transform.position.x - 0.2f, m_Record.AnimationSpeed).SetEase(Ease.Linear).OnComplete(OnDamage);
	}
}
