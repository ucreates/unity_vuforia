﻿//======================================================================
// Project Name    : unity_foundation
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================
using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using Core.Validator.Unit;
using Core.Validator.Entity;
using Core.Validator.Factory;
using Core.Validator.Config;
using Core.Validator.Message;
namespace Core.Validator {
public sealed class ScreenValidator : BaseValidator {
    public override ValidatorResponse IsValid(object validateValue) {
        Vector3 position = (Vector3)validateValue;
        Vector3 viewportPoint = Camera.main.WorldToViewportPoint(position);
        ValidatorResponse response = new ValidatorResponse();
        ValidatorResponseEntity entity = new ValidatorResponseEntity();
        entity.result = !(0.0f < viewportPoint.x && viewportPoint.x < 1.0f && 0.0f < viewportPoint.y && viewportPoint.y < 1.0f);
        response.responseList.Add(entity);
        return response;
    }
}
}
