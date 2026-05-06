using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public enum eType
{
	None,
	Msg,
	Event
}

public class UIBottomSlot : MonoBehaviour
{
	[SerializeField] Text text;
	[SerializeField] Transform ShowPos;
	private int idx = 0;

	public void Show(string _msg, eType _type = eType.Msg)
	{
		gameObject.SetActive(true);
		idx = -1;
		transform.position = ShowPos.position;
		text.text = _msg;
	}

	public void PosUpdate()
	{
		if( false == gameObject.activeSelf )
			return;

		idx++;
		var pos = UIMgr.instance.GetBottomUI.GetPos(idx);
		if(pos == null)
			pos = this.transform;
		transform.DOMoveY(pos.position.y, 0.2f).OnComplete(()=> {
			if (idx >= 4)
				gameObject.SetActive(false);
		});

	}
}
