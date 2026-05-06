using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameBottomUI : MonoBehaviour
{
   [SerializeField] List<UIBottomSlot> listSlot = new List<UIBottomSlot>();
   [SerializeField] Transform[] listPos = new Transform[4];
   [SerializeField] Transform slotParent;

   [SerializeField] Text textHp;
   [SerializeField] Image HpBar;

   public void AddSlot(string _msg)
   {
		var slot = listSlot.Find(x => x.gameObject.activeSelf == false);
		if(slot == null)
		{
			slot = Instantiate(listSlot[0]);
			slot.transform.SetParent(slotParent);
			slot.transform.localScale = Vector3.one;
			slot.transform.SetAsLastSibling();

			listSlot.Add(slot);
		}
		slot.Show(_msg);

		listSlot.ForEach(x => x.PosUpdate());
   }

	public Transform GetPos(int _idx)
	{
		if(listPos.GetLength(0) <= _idx)
			return null;
			
		return listPos[_idx];
	}
}
