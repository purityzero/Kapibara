using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum eInGameState
{
	Stop,
	Move,
	Battle,
	Win,
	Lose,
}

public class InGameState : FsmState<eInGameState>
{
	protected InGameMgr m_Mgr;
	protected FlowCommand m_Command = new FlowCommand();
	public InGameState(eInGameState _stateType, InGameMgr _mgr) : base(_stateType)
	{
		m_Mgr = _mgr;
	}

	public override void Update()
	{
		base.Update();
		m_Command.UpdateLogic();
	}
}

public class InGameStop : InGameState
{
	public InGameStop(InGameMgr _mgr, eInGameState _stateType = eInGameState.Stop) : base(_stateType, _mgr)
	{
	}
}

public class InGameMove : InGameState
{
	public InGameMove(InGameMgr _mgr, eInGameState _stateType = eInGameState.Move) : base(_stateType, _mgr)
	{
	}

	public override void Enter()
	{
		base.Enter();
		m_Mgr.DummyMonsterShow(true);
		m_Command.Add(new Command_Delegate(m_Mgr.BG.Move));
	}

	public override void End()
	{
		base.End();
		m_Mgr.DummyMonsterShow(false);
	}
}

public class InGameBattle : InGameState
{
	public InGameBattle(InGameMgr _mgr, eInGameState _stateType = eInGameState.Battle) : base(_stateType, _mgr)
	{
	}

	public override void Enter()
	{
		base.Enter();
		UIMgr.instance.GetBottomUI.AddSlot("야생의 카피바라가 나타났다!!");
		m_Mgr.MonsterShow(true);

	}

	public override void End()
	{
		base.End();
		m_Mgr.MonsterShow(false);
		SkillMgr.instance.TimeReset();
	}
	
}


public class InGameWin : InGameState
{
	public InGameWin(InGameMgr _mgr, eInGameState _stateType = eInGameState.Win) : base(_stateType, _mgr)
	{
	}

	public override void Enter()
	{
		base.Enter();
		UIMgr.instance.GetBottomUI.AddSlot("승리!");
		m_Command.Add(new Command_DeltaTime(1f, ()=> {m_Mgr.SetState(eInGameState.Move);}));
	}
}

public class InGameLose : InGameState
{
	public InGameLose(InGameMgr _mgr, eInGameState _stateType = eInGameState.Lose) : base(_stateType, _mgr)
	{
	}

	public override void Enter()
	{
		base.Enter();
		UIMgr.instance.GetBottomUI.AddSlot("패배");
		InGameMgr.instance.MonsterShow(true);
		InGameMgr.instance.PlayerShow(false);
		m_Command.Add(new Command_DeltaTime(1f, ()=> {m_Mgr.SetState(eInGameState.Stop);}));
	}
}


