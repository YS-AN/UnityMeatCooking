using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldPoint : MonoBehaviour
{
    [SerializeField]
    private PlayerHoldDirection playerDirection;
	public PlayerHoldDirection PlayerDirection { get { return playerDirection; } }
}
