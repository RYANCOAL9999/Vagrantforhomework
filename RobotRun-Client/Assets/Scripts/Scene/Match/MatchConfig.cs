using UnityEngine;
using System.Collections;

public class MatchConfig {
	public const int layerMaskBackground = 1 << 8;
	public const int layerMaskGround = 1 << 9;
	public const int layerMaskItem = 1 << 10;
	public const int layerMaskPlayer = 1 << 11;
	public const int layerMaskHurtable = 1 << 12;
	public const int layerMaskObstacle = 1 << 13;
	public const int layerMaskThrowItem = 1 << 14;
	public const int layerMaskPlayerTrigger = 1 << 15;
}
