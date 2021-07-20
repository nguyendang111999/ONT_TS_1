using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Chanel/Input Chanel")]
public class InputChanelSO : ScriptableObject
{
    public float _velocity;
    public Vector2 _direction;
    private bool _run;
    private bool _crouch;
    private bool _jump;
    private bool _attack;
    private bool _heavyAttack;
    private bool _skill1;
    private bool _skill2;

    public float Velocity
    {
        get { return _velocity; }
        set { _velocity = value; }
    }
    public Vector2 Direction
    {
        get { return _direction; }
        set { _direction = value; }
    }
    public bool Run
    {
        get { return _run; }
        set { _run = value; }
    }
    public bool Crouch{
        get {return _crouch;}
        set {_crouch = value;}
    }
    public bool Jump{
        get {return _jump;}
        set {_jump = value;}
    }
    public bool Attack{
        get {return _attack;}
        set {_attack = value;}
    }
    public bool HeavyAttack{
        get {return _heavyAttack;}
        set {_heavyAttack = value;}
    }
    public bool Skill1{
        get {return _skill1;}
        set {_skill1 = value;}
    }
    public bool Skill2{
        get {return _skill2;}
        set {_skill2 = value;}
    }

}
