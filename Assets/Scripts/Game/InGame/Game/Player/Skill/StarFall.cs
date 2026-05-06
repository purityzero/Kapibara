using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class StarFall : Skill
{
	[SerializeField] Transform m_TweenObj;
	[SerializeField] SpriteRenderer[] m_listSpr;
	public override void Init(SkillRecord _record)
	{
		base.Init(_record);
		for(int i=0;i<m_listSpr.Length; ++i)
		{
			m_listSpr[i].sprite = m_Atlas.GetSprite(m_Record.Img.ToString());
		}
		m_TweenObj.gameObject.SetActive(false);
	}

	public override void TimeReset()
	{
		base.TimeReset();
		m_TweenObj.transform.position = m_startPos;
		m_TweenObj.gameObject.SetActive(false);
	}

	public override void Excute()
	{
		base.Excute();
		m_TweenObj.transform.position = m_startPos;
		m_TweenObj.gameObject.SetActive(true);
		m_Tweener = m_TweenObj.DOMove(new Vector2(InGameMgr.instance.Monster.transform.position.x -0.2f, InGameMgr.instance.Monster.transform.position.y - 0.2f), m_Record.AnimationSpeed).SetEase(Ease.Linear).OnComplete(OnDamage);
	}

	
    protected override void OnDamage()
	{
		m_TweenObj.gameObject.SetActive(false);
		if(InGameMgr.instance.IsState(eInGameState.Battle) == false)
			return;
		InGameMgr.instance.MonsterHpUpdate(m_Record.Power);
		UIMgr.instance.GetBottomUI.AddSlot($"스킬!! {m_Record.Name} 사용!");
	}
	
	public override void Update()
	{
		if (m_TweenObj.gameObject.activeSelf == true || InGameMgr.instance.IsState(eInGameState.Battle) == false)
			return;

		m_Time += UnityEngine.Time.deltaTime;
		if (m_Time >= m_Record.CoolTime )
		{
			m_Time = 0f;
			Excute();
		}
	}
}
