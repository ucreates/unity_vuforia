//======================================================================
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
using Frontend.Component.Vfx;
using Frontend.Component.Property;
using Frontend.Component.Asset;
using Core.Entity;
namespace Frontend.Behaviour.Base {
public abstract class BaseBehaviour : MonoBehaviour {
    public AssetCollection assetCollection {
        get;
        set;
    }
    public TimeLine timeLine {
        get;
        set;
    }
    public BaseProperty property {
        get;
        protected set;
    }
    public BaseBehaviour() {
        this.assetCollection = new AssetCollection();
        this.timeLine = new TimeLine();
    }
}
}
