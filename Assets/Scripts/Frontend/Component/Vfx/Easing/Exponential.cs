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
namespace Frontend.Component.Vfx.Easing {
public sealed class Exponential {
    public static float EaseIn(float currentTime, float start, float end, float totalTime) {
        float rate = currentTime / totalTime;
        float diff = end - start;
        if (1.0f <= rate) {
            rate = 1.0f;
        }
        return diff * Mathf.Pow(2, 10.0f * (rate - 1.0f)) + start;
    }
    public static float EaseOut(float currentTime, float start, float end, float totalTime) {
        float rate = currentTime / totalTime;
        float diff = end - start;
        if (1.0f <= rate) {
            rate = 1.0f;
        }
        return diff * (-1.0f * Mathf.Pow(2.0f, -10.0f * rate) + 1.0f) + start;
    }
    public static float EaseInOut(float currentTime, float start, float end, float totalTime) {
        bool switchType = currentTime / totalTime >= 0.5f;
        if (false == switchType) {
            return Exponential.EaseIn(currentTime, start, end, totalTime);
        } else {
            return Exponential.EaseOut(currentTime, start, end, totalTime);
        }
    }
}
}
