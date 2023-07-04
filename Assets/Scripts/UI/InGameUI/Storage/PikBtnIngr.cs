using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class PikBtnIngr : BtnIngr<HavingIngrInfo>
{
	public void SetBtnInfo(IngredientName btnId, HavingIngrInfo ingrInfo)
	{
		base.SetBtnInfo(btnId, ingrInfo, ingrInfo.Count);

		imgIngr.sprite = ingrInfo.IngrImg;
		ChangeCount();
	}

	public void SetBtnInfo(IngredientName btnId, IngrInfo ingrInfo, int count)
	{
		base.SetBtnInfo(btnId, new HavingIngrInfo(1, ingrInfo.Data.Icon), count);

		StorageManager.GetInstance().HavingList.Add(btnId, Info);

		imgIngr.sprite = ingrInfo.Data.Icon;
	}

	public void ClearBtnInfo(bool isRemoveInfo = true)
	{
		if(isRemoveInfo)
			StorageManager.GetInstance().HavingList.Remove(BtnId);

		base.ClearBtnInfo();
	}

	public void AddPickIngrCount(int count)
	{
		Info.Count += count;
		ChangeCount();
	}

	private void ChangeCount()
	{
		base.AddCount(Info.Count);
	}

	public override void Click()
	{
		if(Info != null && Info.Count > 0)
		{
			StorageManager.GetInstance().OnDeselectIngr?.Invoke(BtnId);

			AddPickIngrCount(-1);
			
			if (Info.Count <= 0)
			{
				ClearBtnInfo();
			}
			
		}
	}
}