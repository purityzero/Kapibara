using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMgr : MonoSingleton<UIMgr>
{
	[SerializeField] Text TextHp;
	[SerializeField] Image ImageHp;
	private InGameBottomUI inGameBottomUI;
	public UIMonsterHP UIMonsterHP;

	public InGameBottomUI GetBottomUI
	{
		// Scene 구분해서 안줘야할 곳도..필요

		get
		{
			if(inGameBottomUI == null)
				inGameBottomUI = FindObjectOfType<InGameBottomUI>();

			return inGameBottomUI;
		}
	}

	private void Start()
	{
		PlayerData playerData = DataMgr.Instance.GetData<PlayerData>();
		TextHp.text = $"{playerData.HP} / {playerData.HP}";
		ImageHp.fillAmount = 1;
		
		InGameMgr.instance.ovPlayerHp.RegisterObserver(OnHpUpdate);
	}


	public void OnHpUpdate(int _old, int _new)
	{
		PlayerData playerData = DataMgr.Instance.GetData<PlayerData>();
		TextHp.text = $"{_new} / {playerData.HP}";
		ImageHp.fillAmount = Mathf.Clamp((float)_new /playerData.HP, 0f, 1f);
	}

	public void SetMonsterHp(MonsterRecord _record)
	{
		UIMonsterHP.Show(_record);
	}

}
