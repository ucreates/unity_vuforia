﻿//======================================================================
// Project Name    : unity_foundation
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================
using UnityEngine;
using System;
using System.Xml;
using System.Collections.Generic;
using Core.Validator;
namespace Core.Validator.Factory {
public sealed class ValidatorFactory {
    public static BaseValidator FactoryMethod(string type) {
        BaseValidator validator = null;
        switch (type) {
        case "required":
            validator = new RequiredValidator();
            validator.compareOption = BaseValidator.CompareOption.And;
            break;
        case "range":
            validator = new RangeValidator();
            validator.compareOption = BaseValidator.CompareOption.And;
            break;
        case "compare":
            validator = new CompareValidator();
            validator.compareOption = BaseValidator.CompareOption.And;
            break;
        case "regex":
            validator = new RegexValidator();
            validator.compareOption = BaseValidator.CompareOption.And;
            break;
        case "mailandphone":
            validator = new RegexValidator();
            validator.compareOption = BaseValidator.CompareOption.Or;
            break;
        default:
            break;
        }
        return validator;
    }
}
}
