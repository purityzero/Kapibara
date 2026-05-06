using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMgr : MonoSingleton<InGameMgr>
{
	[SerializeField] MoveBG m_BG;
	[SerializeField] Player m_player;
	[SerializeField] Monster m_DummyMonster;
	[SerializeField] Monster m_Monster;

	public PlayerData playerData {get; private set;}
	public PlayerData InGamePlayerData {get; private set;}
	private FsmClass<eInGameState> m_Fsm = new FsmClass<eInGameState>();
	public ObservableVariable<int> ovPlayerHp = new ObservableVariable<int>(0);
	public MoveBG BG { get { return m_BG;} }
	public Monster Monster { get { return m_Monster;} }

	
    void Start()
    {
		// 임시
		DataMgr.Instance.Init();
		TableManager.Instance.init();
		SkillMgr.instance.Init();

		playerData = DataMgr.Instance.GetData<PlayerData>();
		ovPlayerHp.Value = playerData.HP;

		m_Fsm.AddFsm(new InGameStop(this));
		m_Fsm.AddFsm(new InGameMove(this));
		m_Fsm.AddFsm(new InGameBattle(this));
		m_Fsm.AddFsm(new InGameWin(this));
		m_Fsm.AddFsm(new InGameLose(this));


		UIMgr.instance.GetBottomUI.AddSlot("카피바라는 위대한 여정을 떠나기로 하였다다!");
		SetState(eInGameState.Move);
    }

	public void PlayerShow(bool _isShow)
	{
		m_player.PlayerShow(_isShow);
	}

	public void PlayerHpUpdate(int _minus)
	{
		ovPlayerHp.Value -= _minus;
		if(ovPlayerHp.Value <= 0)
		{
			SetState(eInGameState.Lose);
			UIMgr.instance.GetBottomUI.AddSlot($"Player는 {_minus} 입고 장렬히 전사하였다...");
		}
		else
			UIMgr.instance.GetBottomUI.AddSlot($"Player는 {_minus}의 데미지를 입었다!");
	}

	public void MonsterHpUpdate(int _minus)
	{
		m_Monster.HpUpdate(_minus);
	}

	public void OnMonsterRegister(bool _isRegiser, Action<int, int> _action)
	{
		if(_isRegiser == false)
		{
			m_Monster.ovMonsterHp.UnregisterObserver(_action);
			return;
		}
		Monster.ovMonsterHp.RegisterObserver(_action);
	}

	void Update()
    {
        m_player.UpdateLogic();
		m_Monster.UpdateLogic();
		m_Fsm.Update();
    }

	public bool IsState(eInGameState _state)
	{
		return m_Fsm.IsState(_state);
	}

	public void SetState(eInGameState _state)
	{
		if(m_Fsm.IsState(_state) == true)
			return;

		m_Fsm.SetState(_state);
	}

	public void DummyMonsterShow(bool _isShow)
	{
		m_DummyMonster.Show(_isShow, true);
	}

	public void MonsterShow(bool _isShow)
	{
		m_Monster.Show(_isShow);
	}
}
