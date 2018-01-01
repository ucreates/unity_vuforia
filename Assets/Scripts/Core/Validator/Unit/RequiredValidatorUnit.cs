//======================================================================
// Project Name    : unity_foundation
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================
using System;
namespace Core.Validator.Unit {
public sealed class RequiredValidatorUnit<T> : BaseValidatorUnit<T>  where T : IComparable {
    public RequiredValidatorUnit() {
    }
    public override bool IsValid(T value) {
        if (null == value) {
            return false;
        }
        Type type = typeof(T);
        if (type.Name.ToLower().Equals("string")) {
            string strValue = value.ToString();
            if (string.IsNullOrEmpty(strValue)) {
                return false;
            }
        }
        return true;
    }
}
}
