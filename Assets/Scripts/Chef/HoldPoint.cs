using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerHoldDirection
{
    None = -1,
    Right = 1,
	Left = 2,
}

public class HoldPoint : MonoBehaviour
{
    [SerializeField]
    private PlayerHoldDirection playerDirection;
	public PlayerHoldDirection PlayerDirection { get { return playerDirection; } }
}
