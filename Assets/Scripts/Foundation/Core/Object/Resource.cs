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
namespace Core.Object {
public sealed class Resource {
    public static void Instanciate(string prefabName, Vector3 position, Quaternion rotation) {
        UnityEngine.GameObject prefab = UnityEngine.Resources.Load(prefabName) as UnityEngine.GameObject;
        UnityEngine.GameObject.Instantiate(prefab, position, rotation);
    }
}
}
