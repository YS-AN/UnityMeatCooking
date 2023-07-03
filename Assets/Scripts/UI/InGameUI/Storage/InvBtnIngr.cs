using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InvBtnIngr : MonoBehaviour
{
	[SerializeField]
	private Image imgIngr;

	[SerializeField]
	private Image imgCount;

	public int BtnId { get; private set; }
	public IngrInfo Info { get; private set; }

	private void Awake()
	{
	}

	public void InitData()
	{
		BtnId = -1;
	}

	public void SetBtnInfo(int btnId, IngrInfo data, int count)
	{
		BtnId = btnId;
		Info = data;

		imgIngr.sprite = Info.Data.Icon;
		Info.OnChangedCnt += AddCount;

		Info.Count = count;
	}

	private void AddCount(int count)
	{
		bool isActive = count >= 2;
		imgCount.gameObject.SetActive(isActive);
		imgCount.GetComponentInChildren<TextMeshProUGUI>().text = count.ToString();
	}

	public void ClearBtnInfo()
	{
		BtnId = -1;
		Info = null;

		imgIngr.sprite = StorageManager.GetInstance().EmptyIngrData.Icon;
	}

	public void Click()
	{
		if(Info != null && Info.Count > 0)
		{
			int btnId = BtnId;

			Info.Count -= 1;
			if (Info.Count <= 0)
			{
				ClearBtnInfo();
			}

			StorageManager.GetInstance().OnSelectIngr?.Invoke(btnId);
		}
	}
}