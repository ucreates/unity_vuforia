//======================================================================
// Project Name    : ar
//
// Copyright © 2017 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================
using Vuforia;
using UnityEngine;
using Frontend.Notify;
using Core.Device.Touch;
public class VuforiaBehaviour : MonoBehaviour, ITrackableEventHandler {
    void Start() {
        TrackableBehaviour trackableBehaviour = GetComponent<TrackableBehaviour>();
        trackableBehaviour.RegisterTrackableEventHandler(this);
        return;
    }
    void Update() {
        TouchEntity entity = TouchHandler.Pop();
        if (TouchPhase.Began == entity.touchPhase) {
            this.OnRaycast();
        }
    }
    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus) {
        Notifier notifier = Notifier.GetInstance();
        if (newStatus == TrackableBehaviour.Status.DETECTED || newStatus == TrackableBehaviour.Status.TRACKED || newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED) {
            notifier.Notify(NotifyMessage.OnTrackingFound);
        } else if (previousStatus == TrackableBehaviour.Status.TRACKED && newStatus == TrackableBehaviour.Status.NOT_FOUND) {
            notifier.Notify(NotifyMessage.OnTrackingLost);
        } else {
            notifier.Notify(NotifyMessage.OnTrackingLost);
        }
    }
    public void OnRaycast() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10000)) {
            if (false != hit.collider.gameObject.name.Equals("Icon")) {
                Notifier notifier = Notifier.GetInstance();
                notifier.Notify(NotifyMessage.OnRaycastHit);
            }
        }
    }
}
