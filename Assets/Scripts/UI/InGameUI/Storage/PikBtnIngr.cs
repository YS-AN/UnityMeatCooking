using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;



public class PikBtnIngr : MonoBehaviour
{
	[SerializeField]
	private Image imgIngr;

	[SerializeField]
	private Image imgCount;

	public int BtnId { get; private set; }
	public HavingIngrInfo Info { get; private set; }

	private void Awake()
	{
	}

	public void InitData()
	{
		BtnId = -1;
	}


	public void SetBtnInfo(int btnId)
	{
		var ingrInfo = StorageManager.GetInstance().Ingredients[btnId];

		BtnId = btnId;
		Info = new HavingIngrInfo(1, ingrInfo.Data.Icon);

		StorageManager.GetInstance().HavingList.Add(btnId, Info);

		imgIngr.sprite = ingrInfo.Data.Icon;
	}

	public void ClearBtnInfo()
	{
		StorageManager.GetInstance().HavingList.Remove(BtnId);

		BtnId = -1;
		Info = null;

		imgIngr.sprite = StorageManager.GetInstance().EmptyIngrData.Icon;
	}

	public void AddCount(int count)
	{
		Info.Count += count;

		bool isActive = Info.Count >= 2;
		imgCount.gameObject.SetActive(isActive);
		imgCount.GetComponentInChildren<TextMeshProUGUI>().text = Info.Count.ToString();
	}

	public void Click()
	{
		if(Info != null && Info.Count > 0)
		{
			int btnId = BtnId;

			AddCount(-1);
			if (Info.Count <= 0)
			{
				ClearBtnInfo();
			}
			StorageManager.GetInstance().OnDeselectIngr?.Invoke(btnId);
		}
	}
}