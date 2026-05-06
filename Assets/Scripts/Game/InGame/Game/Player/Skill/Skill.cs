using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.U2D; 


// Sprite 들은 GameObject 프리팹에 붙여서 사용.

public interface ISkill
{
	public void Excute(){}
	public void AddSlot(){}
	public void Update(){}
}

public abstract class Skill : MonoBehaviour, ISkill
{	
	[SerializeField] protected SpriteRenderer m_SpriteRenderer;
	[SerializeField] protected Vector2 m_startPos;
	protected SpriteAtlas m_Atlas;
	protected SkillRecord m_Record;
	protected float m_Time = 0f;
	protected Tweener m_Tweener = null;

	// 스킬 Table 들어간다.
	public virtual void Init(SkillRecord _record)
	{
		m_Record = _record;
		m_Atlas = Resources.Load<SpriteAtlas>(m_Record.Atlas);
		if(m_SpriteRenderer != null)
		{
			m_SpriteRenderer.sprite = m_Atlas.GetSprite(m_Record.Img);
			m_SpriteRenderer.gameObject.SetActive(false);
		}
	}

	public virtual void TimeReset()
	{
		if(m_Tweener != null)
		{
			m_Tweener.Kill(true);
			m_Tweener = null;
		}

		m_Time = 0f;
		if(m_SpriteRenderer != null)
		{
			m_SpriteRenderer.gameObject.SetActive(false);
			m_SpriteRenderer.transform.position = m_startPos;
		}
	}

    public virtual void Excute()
	{
		if(m_SpriteRenderer != null)
		{
			m_SpriteRenderer.transform.position = m_startPos;
			m_SpriteRenderer.gameObject.SetActive(true);
		}
	}

    protected virtual void OnDamage()
	{
		if(m_SpriteRenderer != null)
		{
			m_SpriteRenderer.gameObject.SetActive(false);
		}
		if(InGameMgr.instance.IsState(eInGameState.Battle) == false)
			return;
		
		UIMgr.instance.GetBottomUI.AddSlot($"스킬!! {m_Record.Name}");
		InGameMgr.instance.MonsterHpUpdate(m_Record.Power);
	}
	
	public virtual void Update()
	{
		if (m_SpriteRenderer.gameObject?.activeSelf == true || InGameMgr.instance.IsState(eInGameState.Battle) == false)
			return;

		m_Time += UnityEngine.Time.deltaTime;
		if (m_Time >= m_Record.CoolTime )
		{
			m_Time = 0f;
			Excute();
		}
	}
}
