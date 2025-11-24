using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class RigController : MonoBehaviour
{
    [Header("Rig Builder")]
    [SerializeField]
    private RigBuilder rigBuilder;

    [Header("Rigs")]
    // 번호는 WeaponType과 동일하게 맞춰야 함
    [SerializeField]
    private List<Rig> weaponRigs = new List<Rig>();
    [SerializeField]
    private List<Rig> attackRigs = new List<Rig>();

    [Header("Constraints")]

    [SerializeField]
    private List<MultiParentConstraint> weaponConstraints = new List<MultiParentConstraint>();
    [SerializeField]
    private List<MultiParentConstraint> attackConstraints = new List<MultiParentConstraint>();

    [Header("IK Constraints")]
    [SerializeField]
    private TwoBoneIKConstraint rightHandIK;
    [SerializeField]
    private TwoBoneIKConstraint leftHandIK;

    public void ApplyRigTargets(Transform rightTarget, Transform leftTarget)
    {
        rightHandIK.data.target = rightTarget;
        leftHandIK.data.target = leftTarget;
    }

    public void ApplyConstrained(int weaponIndex, Transform weapon)
    {
        weaponConstraints[weaponIndex].data.constrainedObject = weapon;
        attackConstraints[weaponIndex].data.constrainedObject = weapon;
    }

    public void SetRigActive(int weaponIndex, bool isActive)
    {
        if (weaponIndex < 0 || weaponIndex >= weaponRigs.Count)
        {
            Debug.LogError("Invalid weapon index for rig activation.");
            return;
        }
        weaponRigs[weaponIndex].weight = isActive ? 1.0f : 0.0f;
    }

    /// <summary>
    /// 총기 애니메이션 변경 시 RigBuilder 리빌드해야 적용 됨.
    /// </summary>
    public void RigBuilderRebuild()
    {
        rigBuilder.enabled = false;
        rigBuilder.enabled = true;
    }

    public void SetAttackRigActive(int weaponIndex, bool isActive)
    {
        if (weaponIndex < 0 || weaponIndex >= attackRigs.Count)
        {
            Debug.LogError("Invalid weapon index for attack rig activation.");
            return;
        }
        StartCoroutine(SetAttackRigWeight(weaponIndex, isActive));
        
    }

    private IEnumerator SetAttackRigWeight(int weaponIndex, bool isActive)
    {
        attackRigs[weaponIndex].weight = 0.01f;
        yield return null;
        attackRigs[weaponIndex].weight = isActive ? 1.0f : 0.0f;
    }
}