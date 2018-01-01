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
using Core.Entity;
using Frontend.Component.Asset.Sound;
using Frontend.Component.Property;
using Frontend.Notify;
namespace Frontend.Behaviour.Audio {
public sealed class AudioBehaviour : BaseBehaviour, INotify {
    public void Start() {
        this.property = new BaseProperty(this);
        Notifier notifier = Notifier.GetInstance();
        notifier.Add(this, this.property);
    }
    public void OnNotify(NotifyMessage notifyMessage, Parameter parameter = null) {
        return;
    }
}
}
