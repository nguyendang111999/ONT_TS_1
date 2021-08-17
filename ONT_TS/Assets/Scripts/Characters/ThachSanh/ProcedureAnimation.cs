using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcedureAnimation : MonoBehaviour
{
    #region Variables
    public Animator anim;
    private float _speed;
    public CharacterController _controller;
    private bool _isGrounded;

    private Vector3 rightFootPos, leftFootPos, leftFootIKPos, rightFootIKPos;
    private Quaternion leftFootIKRotation, rightFootIKRotation;
    private float lastPelvisPositionY, lastRightFootPositionY, lastLeftFootPositionY;

    [Header("Feet grounder")]
    public bool enableFeetIK = true;
    [Range(0, 2)] [SerializeField] private float heightFromGroundRaycast = 1.14f;
    [Range(0, 2)] [SerializeField] private float raycastDownDistance = 1.5f;
    [SerializeField] private LayerMask environmentLayer;
    [SerializeField] private float pelvisOffset = 0f;
    [Range(0, 1)] [SerializeField] private float pelvisUpDownSpeed = .3f;
    [Range(0, 1)] [SerializeField] private float feetToIKPositionSpeed = .5f;

    public string leftFootAnimVariableName = "LeftFootCurve";
    public string rightFootAnimVariableName = "RightFootCurve";

    public bool useProIKFeature = false;
    public bool showSolverDebug = true;
    #endregion

    #region FeetGrounding
    /// <summary>
    /// Updating the AdjustFeetTarget and find the position of each foot inside our Solver Position
    /// </summary>
    private void FixedUpdate()
    {
        if (!enableFeetIK) return;

        if (anim == null) return;

        AdjustFeetTarget(ref rightFootPos, HumanBodyBones.RightFoot);
        AdjustFeetTarget(ref leftFootPos, HumanBodyBones.LeftFoot);

        //Find and raycast to the ground

        //Handle solver for right foot
        FeetPositionSolver(rightFootPos, ref rightFootIKPos, ref rightFootIKRotation);
        //Handle solver for left foot
        FeetPositionSolver(leftFootPos, ref leftFootIKPos, ref leftFootIKRotation);


    }
    private void OnAnimatorIK(int layerIndex)
    {
        if (!enableFeetIK) return;
        if (anim == null) return;

        MovePelvisHeight();

        //right foot ik position and rotaion -- use the pro features in here
        anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);

        if (useProIKFeature)
        {
            anim.SetIKRotationWeight(AvatarIKGoal.RightFoot, anim.GetFloat(rightFootAnimVariableName));
        }

        MoveFeetToIKPoint(AvatarIKGoal.RightFoot, rightFootIKPos, rightFootIKRotation, ref lastRightFootPositionY);

        //left foot ik position and rotaion -- use the pro features in here
        anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);

        if (useProIKFeature)
        {
            anim.SetIKRotationWeight(AvatarIKGoal.LeftFoot, anim.GetFloat(leftFootAnimVariableName));
        }

        MoveFeetToIKPoint(AvatarIKGoal.LeftFoot, leftFootIKPos, leftFootIKRotation, ref lastLeftFootPositionY);
    }
    #endregion

    #region FeetGroundingMethods
    void MoveFeetToIKPoint(
        AvatarIKGoal foot,
        Vector3 posIKHolder,
        Quaternion rotationIKHolder,
        ref float lastFootPostionY)
    {
        Vector3 targetIKPos = anim.GetIKPosition(foot);
        if (posIKHolder != Vector3.zero)
        {
            targetIKPos = transform.InverseTransformPoint(targetIKPos);
            posIKHolder = transform.InverseTransformPoint(posIKHolder);

            float yVariable = Mathf.Lerp(lastFootPostionY, posIKHolder.y, feetToIKPositionSpeed);
            targetIKPos.y += yVariable;

            lastFootPostionY = yVariable;

            targetIKPos = transform.TransformPoint(targetIKPos);

            anim.SetIKRotation(foot, rotationIKHolder);
        }
        anim.SetIKPosition(foot, targetIKPos);
    }

    /// <summary>
    /// Move the height of the pelvis
    /// </summary>
    private void MovePelvisHeight()
    {
        if (rightFootIKPos == Vector3.zero || leftFootIKPos == Vector3.zero || lastPelvisPositionY == 0)
        {
            lastPelvisPositionY = anim.bodyPosition.y;
            return;
        }
        float leftOffsetPos = leftFootIKPos.y - transform.position.y;
        float rightOffsetPos = rightFootIKPos.y - transform.position.y;

        float totalOffset = (leftOffsetPos < rightOffsetPos) ? leftOffsetPos : rightOffsetPos;

        Vector3 newPelvisPos = anim.bodyPosition + Vector3.up * totalOffset;

        newPelvisPos.y = Mathf.Lerp(lastPelvisPositionY, newPelvisPos.y, pelvisUpDownSpeed);

        anim.bodyPosition = newPelvisPos;
        lastPelvisPositionY = anim.bodyPosition.y;
    }

    /// <summary>
    /// Locating the feet position via a raycast and then solving
    /// </summary>
    private void FeetPositionSolver(
        Vector3 fromSkyPosition,
        ref Vector3 feetIKPos,
        ref Quaternion feetIKRotations)
    {
        //Raycast handling
        RaycastHit feetOutHit;

        if (showSolverDebug)
            Debug.DrawLine(fromSkyPosition, fromSkyPosition +
            Vector3.down * (raycastDownDistance + heightFromGroundRaycast),
            Color.red);
        if (Physics.Raycast(fromSkyPosition, Vector3.down, out feetOutHit,
        raycastDownDistance + heightFromGroundRaycast, environmentLayer))
        {
            //Finding feet ik position from the sky position
            feetIKPos = fromSkyPosition;
            feetIKPos.y = feetOutHit.point.y + pelvisOffset;
            feetIKRotations = Quaternion.FromToRotation(Vector3.up, feetOutHit.normal) * transform.rotation;
            return;
        }
        feetIKPos = Vector3.zero; //If it didn't work
    }

    /// <summary>
    /// Adjust the feet target
    /// </summary>
    private void AdjustFeetTarget(ref Vector3 feetPositions, HumanBodyBones foot)
    {
        feetPositions = anim.GetBoneTransform(foot).position;
        feetPositions.y = transform.position.y + heightFromGroundRaycast;
    }
    #endregion
}
