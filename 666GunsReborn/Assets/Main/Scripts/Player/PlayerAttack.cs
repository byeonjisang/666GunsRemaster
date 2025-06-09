using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerAttack : MonoBehaviour
{
    private Player player;
    private bool attackRequested = false;

    public bool IsAttacking { get; private set; } = false;

    public Rig attackRig;
    private float attackDuration = 0.3f;

    public void Initialize(Player player)
    {
        this.player = player;
        attackRequested = false;
        IsAttacking = false;
    }

        // 버튼을 누른 순간 호출
    public void RequestAttack()
    {
        attackRequested = true;
    }

    // 버튼에서 손 뗀 순간 호출
    public void CancelAttackRequest()
    {
        Debug.Log("Attack request cancelled");
        attackRequested = false;
        if (IsAttacking)
            StopAttack();
    }

    // 공격 시작
    public void StartAttack()
    {
        if (IsAttacking)
            return;

        if(!WeaponManager.Instance.CanAttack()){
            player.StopAttack();
            return;
        }

        IsAttacking = true;
        player.StartAttack();  
    }

    // 공격 중지
    public void StopAttack()
    {
        IsAttacking = false;
        player.StopAttack();
    }


    private void Update()
    {
        // 1) 공격 요청이 켜져 있고, 아직 공격 중이 아니면서 발사 가능하면 발사 시작
        if (attackRequested && !IsAttacking && WeaponManager.Instance.CanAttack())
        {
            StartAttack();
        }

        // 2) 공격 중인데 탄약이 떨어졌으면 멈추기
        if (IsAttacking && !WeaponManager.Instance.CanAttack())
        {
            StopAttack();
        }


        // 공격 자세 취하기
        attackRig.weight += (Time.deltaTime / attackDuration) * (IsAttacking ? 1 : -1);
    }  
}