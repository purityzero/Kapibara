using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIMonsterHP : MonoBehaviour
{
    [SerializeField] SpriteRenderer TargetSprite;
    [SerializeField] Canvas TargetCanvas;
    [SerializeField] Vector3 Offset = new Vector3(0, 1f, 0);
	[SerializeField] Text HpText;
	[SerializeField] Image HpImage;
	MonsterRecord m_Record = null;
	int m_CurrentHp = 0;

	public void Show(MonsterRecord _record)
	{
		m_Record = _record;
		m_CurrentHp = m_Record.Hp;

		HpText.text = $"{m_CurrentHp}/{m_Record.Hp}";
		HpImage.fillAmount = 1;

		gameObject.SetActive(true);

		Vector3 worldPos = TargetSprite.transform.position + Offset;
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);

        RectTransformUtility.ScreenPointToLocalPointInRectangle( TargetCanvas.transform as RectTransform, screenPos, TargetCanvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : Camera.main,
            out Vector2 localPoint
        );

        this.transform.localPosition = localPoint;
		InGameMgr.instance.OnMonsterRegister(true, OnHpUpdate);
	}

	public void OnHpUpdate(int _old, int _new)
	{
		m_CurrentHp = Mathf.Clamp(m_CurrentHp - _new, 0, m_Record.Hp);
		HpText.text = $"{m_CurrentHp}/{m_Record.Hp}";

		HpImage.fillAmount = (m_Record.Hp > 0) ? Mathf.Clamp((float)m_CurrentHp / m_Record.Hp, 0f, 1f) : 0f;

		if (m_CurrentHp <= 0)
		{
			Hide();
		}
	}

	public void Hide()
	{
		HpText.text = $"0/{m_Record.Hp}";
		gameObject.SetActive(false);
		InGameMgr.instance.OnMonsterRegister(false, OnHpUpdate);
	}
}
