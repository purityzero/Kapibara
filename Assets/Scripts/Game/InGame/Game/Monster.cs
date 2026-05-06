using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] SpriteRenderer SprMonster;
	MonsterRecord m_MonsterRecord;
	float m_BattleTime;

	public ObservableVariable<int> ovMonsterHp = new ObservableVariable<int>(0);

	public void Show(bool _isShow, bool _isDummy = false)
	{
		if(gameObject.activeSelf == _isShow)
			return;
	    m_BattleTime = 0;

		// 임시
		m_MonsterRecord = TableManager.Instance.GetTable<MonsterTable>().Get(1);
		ovMonsterHp.Value = m_MonsterRecord.Hp;

		if( true == _isShow && false == _isDummy)
			UIMgr.instance.SetMonsterHp(m_MonsterRecord);

		gameObject.SetActive(_isShow);
	}

	public void SetImage(Sprite _spr)
	{
		SprMonster.sprite = _spr;
	}

	public void UpdateLogic()
	{
		if(gameObject.activeSelf == true && InGameMgr.instance.IsState(eInGameState.Battle) == true)
		{
			m_BattleTime += Time.deltaTime;
			if(m_MonsterRecord.Speed <= m_BattleTime)
			{
				m_BattleTime = 0;
				//공격
				InGameMgr.instance.PlayerHpUpdate(m_MonsterRecord.Power);
			}
		}
		else
		{
			m_BattleTime = 0;
		}
	}

	public void HpUpdate(int _minus)
	{
		ovMonsterHp.Value -= _minus;
		if(ovMonsterHp.Value <= 0)
		{
			InGameMgr.instance.SetState(eInGameState.Win);
			UIMgr.instance.GetBottomUI.AddSlot($"Monster는 {_minus} 데미지를 입고 장렬히 전사하였다...");
		}
		else
		{
			UIMgr.instance.GetBottomUI.AddSlot($"Monster는 {_minus}의 데미지를 입었다!");
		}
	}
}
