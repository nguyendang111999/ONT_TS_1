using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;
using UnityEngine;

[CreateAssetMenu(menuName = "State Machines/ThachSanh/Actions/Set Animator Ability Parameter")]
public class AnimatorAbilityActionSO : StateActionSO
{
    [SerializeField] AbilityContainerSO _abilities;
    public AbilityContainerSO Abilities => _abilities;
    protected override StateAction CreateAction() => new AnimatorAbilityAction();
}

public class AnimatorAbilityAction : StateAction
{
    private AnimatorAbilityActionSO _originSO => (AnimatorAbilityActionSO)base.OriginSO;
    private AbilityContainerSO _abilities;
    private AbilityBaseSO _a;
    private int _abilityIndex;
    string _aName;
    private Animator _anim;
    private int _parameterHash;
    public override void Awake(StateController stateController)
    {
        _abilities = _originSO.Abilities;
        _anim = stateController.GetComponent<Animator>();        
    }
    public override void OnStateEnter()
    {
        _abilityIndex = _abilities.Index;
        _a = _abilities.AbilityList[_abilityIndex];
        _aName = _a.AbilityName;
        _parameterHash = Animator.StringToHash(_aName);
        _anim.SetBool(_parameterHash, true);
    }
    public override void OnStateUpdate() { }
    public override void OnStateExit(){
        _a.CoolDownCounter = _a.CoolDownDuration;
        _anim.SetBool(_parameterHash, false);
    }
}
