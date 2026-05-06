using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

// 예정 State 패턴으로 변경한다.

public class Player : MonoBehaviour
{
	[SerializeField] SpriteRenderer sprPlayer;
	[SerializeField] SpriteAtlas atlas;
	[SerializeField] float fImageUpdateTime = 1f;
	private float m_Time;
	private float m_BattleTime;
	private int m_Idx = 1;
	private float m_Speed = 0;
	
	private void Start()
	{
		m_Idx = 1;
		m_Speed = DataMgr.Instance.GetData<PlayerData>().Speed;
		sprPlayer.sprite = atlas.GetSprite("capybara_walk_0");
	}

	public void PlayerShow(bool _isShow)
	{
		sprPlayer.gameObject.SetActive(_isShow);
	}

	public void UpdateLogic()
	{
		if( true == InGameMgr.instance.IsState(eInGameState.Move) )
		{
			if(false == sprPlayer.sprite.name.Contains("capybara_walk"))
			{
				sprPlayer.sprite = atlas.GetSprite("capybara_walk_0");
			}
			UpdateMoveMotion();
		}
		else if ( true == InGameMgr.instance.IsState(eInGameState.Battle) )
		{
			sprPlayer.sprite = atlas.GetSprite("capybara_idle_0");
			UpdateBattle();
		}
		else if( !InGameMgr.instance.IsState(eInGameState.Stop) )
		{
			m_Idx = 0;
			m_Time = 0;
			sprPlayer.sprite = atlas.GetSprite("capybara_idle_0");
		}
	}

	private void UpdateBattle()
	{
		m_BattleTime += Time.deltaTime;
		if( m_BattleTime >= m_Speed)
		{
			m_BattleTime = 0;
			UIMgr.instance.GetBottomUI.AddSlot($"적군을 공격했다!");
			InGameMgr.instance.MonsterHpUpdate(DataMgr.Instance.GetData<PlayerData>().Power);
		}
	}

	private void UpdateMoveMotion()
	{
		m_Time += Time.deltaTime;
		if( m_Time >= fImageUpdateTime)
		{
			m_Time = 0;
			sprPlayer.sprite = atlas.GetSprite($"capybara_walk_{m_Idx}");
			m_Idx += 1;
			if( m_Idx > 1)
				m_Idx = 0;
		}
	}
}
