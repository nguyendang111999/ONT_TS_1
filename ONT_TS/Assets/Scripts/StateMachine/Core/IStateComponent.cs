using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IStateComponent
{
    void OnStateEnter();
    void OnStateExit();
}
