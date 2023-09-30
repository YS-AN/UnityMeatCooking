using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
	private static PlayerManager instance;

	public static PlayerManager GetInstance()
	{
		if (instance == null)
			instance = new PlayerManager();

		return instance;
	}

	private Player _player;
	public Player Player { get { return _player; } }

	private void Awake()
	{
		instance = this;
		_player = transform.GetComponent<Player>();
	}

	public void SetStartPosition()
	{
		_player.transform.position = new Vector3(13, 0, 5);
		_player.transform.localRotation = Quaternion.identity;
	}
}
