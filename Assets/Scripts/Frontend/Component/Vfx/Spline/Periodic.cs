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
using System.Collections;
namespace Frontend.Component.Vfx.Sprine {
public sealed class Periodic {
    public static float Sin(float currentFrame, float times = 1f) {
        return Mathf.Sin(currentFrame) * times;
    }
}
}
