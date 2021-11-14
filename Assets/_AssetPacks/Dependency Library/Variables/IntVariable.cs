﻿// ----------------------------------------------------------------------------
// IntVariable.cs
// 
// Author: Jonathan Carter (A.K.A. J)
// Date: 25/10/2021
// ----------------------------------------------------------------------------

using UnityEngine;

namespace DependencyLibrary
{
    [CreateAssetMenu(fileName = "Int Variable", menuName = "Dependency Library/Variables/Int Variable", order = 0)]
    public class IntVariable : ScriptableObject, IDependencyLibVariable<int>, IDependencyResetOnBuild
    {
        [field: SerializeField, TextArea] public string DevDescription { get; set; }
        [field: SerializeField] public int Value { get; set; }
        [field: SerializeField] public int DefaultValue { get; set; }

        public void SetValue(int value) => Value = value;
        public void IncrementValue(int value) => Value += value;
        public void ResetValue() => Value = DefaultValue;
    }
}