using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillMgr : MonoSingleton<SkillMgr>
{
	private List<Skill> m_listSkill = new List<Skill>();


	public void Init()
	{
		AddSkill(TableManager.Instance.GetTable<SkillTable>().Get(1));
		AddSkill(TableManager.Instance.GetTable<SkillTable>().Get(2));
	}

	public void TimeReset()
	{
		m_listSkill.ForEach(x => x.TimeReset());
	}

	public void AddSkill(SkillRecord _record)
	{
		Skill obj = Instantiate(Resources.Load<Skill>(_record.Prefab));
		Skill skill = obj.GetComponent<Skill>();
		skill.Init(_record);
		m_listSkill.Add(skill);
	}

	private void Update()
	{
		if( InGameMgr.instance.IsState(eInGameState.Battle) == false )
			return;

		m_listSkill.ForEach(skill => skill.Update());		
	}
}
