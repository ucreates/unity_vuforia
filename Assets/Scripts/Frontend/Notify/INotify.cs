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
using Core.Entity;
namespace Frontend.Notify {
public interface INotify {
    void OnNotify(NotifyMessage notifyMessage, Parameter parameter = null);
}
}
