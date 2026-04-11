using System;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public class KeyPressCondition : ConditionBase
{
    public Key key;
    public override bool CheckCondition() => Keyboard.current[key].wasPressedThisFrame;
}