using System;
using UnityEngine;

[Serializable]
public class KeyPressCondition : ConditionBase
{
    public KeyCode key;
    public override bool CheckCondition() => Input.GetKeyDown(key);
}