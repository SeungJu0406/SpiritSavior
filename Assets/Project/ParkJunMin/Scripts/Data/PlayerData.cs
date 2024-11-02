using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Data", menuName = "Scriptable Object/Player Data", order = int.MaxValue)]
public class PlayerData : ScriptableObject
{
    [SerializeField] public float moveSpeed;
    [SerializeField] public float dashForce;
    [SerializeField] public float jumpForce;
    [SerializeField] public float doubleJumpForce; 
    [SerializeField] public float knockbackForce;
    [SerializeField] public float speedAdjustmentOffsetInAir; // 공중에서의 속도 = 땅에서의 속도 * 해당 변수
    [SerializeField] public float maxAngle; // 이동 가능한 최대 각도
    [SerializeField] public float wallJumpPower;
}
