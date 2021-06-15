using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Instance
    {
        get { return s_Instance; }
    }

    [HideInInspector]
    public bool playerControllerInputBlocked;

    protected static PlayerInput s_Instance;
    protected Vector2 m_Movement;
    protected Vector2 m_Camera;
    protected bool m_Jump;
    protected bool m_Attack;
    protected bool m_Aim;
    protected bool m_Skill1;
    protected bool m_Skill2;
    protected bool m_ExternalInputBlocked;

    [SerializeField]
    private float mouseSensitivity = 1;

    public Vector2 MoveInput
    {
        get
        {
            if (playerControllerInputBlocked || m_ExternalInputBlocked)
                return Vector2.zero;
            return m_Movement;
        }
    }

    public bool Attack
    {
        get { return m_Attack && !playerControllerInputBlocked && !m_ExternalInputBlocked; }
    }

    WaitForSeconds m_AttackInputWait;
    Coroutine m_AttackWaitCoroutine;
    const float k_AttackInputDuration = 0.03f;
    void Awake()
    {
        m_AttackInputWait = new WaitForSeconds(k_AttackInputDuration);

        if (s_Instance == null)
            s_Instance = this;
        else if (s_Instance != this)
            throw new UnityException("There cannot be more than one PlayerInput script. The instance are " + s_Instance.name + " and " + name + ".");
    }

    void OnAttack(){
        if(m_AttackWaitCoroutine != null){
            StopCoroutine(m_AttackWaitCoroutine);
        m_AttackWaitCoroutine = StartCoroutine(AttackWait());
        }
    }

    IEnumerator AttackWait(){
        m_Attack = true;
        yield return m_AttackInputWait;
        m_Attack = false;
    }

    public bool HaveControl(){
        return !m_ExternalInputBlocked;
    }

    public void ReleaseControl(){
        m_ExternalInputBlocked = true;
    }

    public void GainControl(){
        m_ExternalInputBlocked = false;
    }
}
