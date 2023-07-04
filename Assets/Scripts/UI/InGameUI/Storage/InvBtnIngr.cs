using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public abstract class BtnIngr<T> : MonoBehaviour 
{
	[SerializeField]
	protected Image imgIngr;

	[SerializeField]
	protected Image imgCount;

	public IngredientName BtnId { get; private set; }
	public T Info { get; private set; }

	public void InitData()
	{
		BtnId = IngredientName.None;
	}

	protected virtual void AddCount(int count)
	{
		bool isActive = count >= 2;
		imgCount.gameObject.SetActive(isActive);
		imgCount.GetComponentInChildren<TextMeshProUGUI>().text = count.ToString();
	}

	public virtual void SetBtnInfo(IngredientName btnId, T info, int count)
	{
		BtnId = btnId;
		Info = info;
	}

	public virtual void ClearBtnInfo()
	{
		BtnId = IngredientName.None;
		Info = default;

		imgIngr.sprite = StorageManager.GetInstance().EmptyIngrData.Icon;
		imgCount.gameObject.SetActive(false);
	}

	public abstract void Click();
}


public class InvBtnIngr : BtnIngr<IngrInfo>
{
	public override void SetBtnInfo(IngredientName btnId, IngrInfo info, int count)
	{
		base.SetBtnInfo(btnId, info, count);
		
		imgIngr.sprite = Info.Data.Icon;

		Info.OnChangedCnt += AddCount;

		Info.Count = count;
	}

	public override void ClearBtnInfo()
	{
		if(Info != null)
		{
			Info.OnChangedCnt -= AddCount;
		}
		base.ClearBtnInfo();
	}

	public override void Click()
	{
		if(Info != null && Info.Count > 0)
		{
			IngredientName id = BtnId;

			Info.Count -= 1;
			if (Info.Count <= 0)
			{
				ClearBtnInfo();
			}
			StorageManager.GetInstance().OnSelectIngr?.Invoke(id);
		}
	}
}