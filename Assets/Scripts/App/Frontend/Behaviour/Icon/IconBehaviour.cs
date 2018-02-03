﻿//======================================================================
// Project Name    : ar
//
// Copyright © 2017 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================
using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Frontend.Notify;
using Frontend.Component.State;
using Frontend.Component.Property;
using Frontend.Component.Asset.Render;
using Frontend.Behaviour.Asset.State;
using Core.Entity;
public class IconBehaviour : BaseBehaviour, IObjAsset, IStateMachine<IconBehaviour>, INotify {
    private const float SCALE_RATE_IOS = 0.25f;
    private const float SCALE_RATE_ANDROID = 2f;
    public RuntimePlatform platform;
    public FiniteStateMachine<IconBehaviour> stateMachine {
        get;
        set;
    }
    public float scaleRate {
        get;
        private set;
    }
    public bool enableTouch {
        get;
        set;
    }
    public GameObject vortexVfx {
        get;
        set;
    }
    public GameObject hitVfx {
        get;
        set;
    }
    // Use this for initialization
    void Start() {
        this.enableTouch = false;
        this.property = new BaseProperty(this);
        this.stateMachine = new FiniteStateMachine<IconBehaviour>(this);
        this.stateMachine.Add("show", new IconShowState());
        this.stateMachine.Add("hide", new IconHideState());
        this.stateMachine.Add("beat", new IconBeatState());
        this.stateMachine.Add("jump", new IconJumpState());
        this.stateMachine.Stop();
        this.vortexVfx = GameObject.Find("VortexVfx");
        this.hitVfx = GameObject.Find("HitVfx");
        this.vortexVfx.SetActive(false);
        this.hitVfx.SetActive(false);
        this.StartCoroutine("OnLoad");
        Notifier notifier = Notifier.GetInstance();
        notifier.Add(this, this.property);
        return;
    }
    // Update is called once per frame
    void Update() {
        this.stateMachine.Update();
        return;
    }
    public void OnNotify(NotifyMessage notifyMessage, Parameter parameter = null) {
        if (NotifyMessage.OnTrackingFound == notifyMessage) {
            this.stateMachine.Change("show");
            this.stateMachine.Play();
        } else if (NotifyMessage.OnTrackingLost == notifyMessage) {
            this.stateMachine.Change("hide");
            this.stateMachine.Play();
        } else if (NotifyMessage.OnRaycastHit == notifyMessage && false != this.enableTouch) {
            this.enableTouch = false;
            int ret = Random.Range(0, 10);
            if (0 == ret % 2) {
                this.stateMachine.Change("beat");
            } else {
                this.stateMachine.Change("jump");
            }
            this.stateMachine.Play();
            this.hitVfx.SetActive(true);
        }
    }
    public IEnumerator OnLoad() {
        string assetRootPath = string.Empty;
        string assetDestRootPath = string.Empty;
        string objAssetPath = string.Empty;
        this.scaleRate = 1.0f;
        if (RuntimePlatform.Android == Application.platform) {
            string destPath = Core.IO.Path.Combine(Application.temporaryCachePath, "Android");
            Core.IO.Directory.Create(destPath);
            string[] files = new string[] {"android.obj", "android.mtl", "android.png"};
            foreach (string file in files) {
                string originPath = Core.IO.Path.Combine(Application.streamingAssetsPath, "Android", file);
                Core.IO.File.Copy(originPath, destPath);
            }
            assetRootPath = Core.IO.Path.Combine(Application.temporaryCachePath, "Android");
            objAssetPath = System.IO.Path.Combine(assetRootPath, "android.obj");
            this.scaleRate = IconBehaviour.SCALE_RATE_ANDROID;
        } else if (RuntimePlatform.IPhonePlayer == Application.platform) {
            assetRootPath = Core.IO.Path.Combine(Application.streamingAssetsPath, "iOS");
            objAssetPath = System.IO.Path.Combine(assetRootPath, "apple.obj");
            this.scaleRate = IconBehaviour.SCALE_RATE_IOS;
        } else {
            if (RuntimePlatform.IPhonePlayer == this.platform) {
                assetRootPath = Core.IO.Path.Combine(Application.streamingAssetsPath, "iOS");
                objAssetPath = System.IO.Path.Combine(assetRootPath, "apple.obj");
                this.scaleRate = IconBehaviour.SCALE_RATE_IOS;
            } else {
                assetRootPath = Core.IO.Path.Combine(Application.streamingAssetsPath, "Android");
                objAssetPath = System.IO.Path.Combine(assetRootPath, "android.obj");
                this.scaleRate = IconBehaviour.SCALE_RATE_ANDROID;
            }
        }
        if (false != string.IsNullOrEmpty(assetRootPath) || false != string.IsNullOrEmpty(objAssetPath)) {
            string message = string.Format("not found streaming assets::{0}/{1}", assetRootPath, objAssetPath);
            Debug.LogError(message);
        } else {
            ObjAsset objAsset = new ObjAsset();
            yield return objAsset.AsyncBuild(objAssetPath);
            while (null == objAsset.objList) {
                yield return null;
            }
            List<GameObject> meshObjectList = objAsset.objList;
            Dictionary<string, Material> mtlDictionary = new Dictionary<string, Material>();
            if (false != objAsset.hasMaterial) {
                string mtlAssetPath = System.IO.Path.Combine(assetRootPath, objAsset.mtlAssetName);
                MtlAsset mtlAsset = new MtlAsset();
                yield return mtlAsset.AsyncBuild(mtlAssetPath);
                mtlDictionary = mtlAsset.mtlList;
            }
            foreach (GameObject meshObject in meshObjectList) {
                meshObject.transform.parent = this.gameObject.transform;
                meshObject.transform.position = this.gameObject.transform.position;
                meshObject.transform.localScale = Vector3.one * this.scaleRate;
                Renderer[] renderers = meshObject.GetComponentsInChildren<Renderer>(true);
                if (0 == renderers.Length) {
                    continue;
                }
                foreach (Renderer renderer in renderers) {
                    string groupPath = Path.Combine(meshObject.name, renderer.gameObject.name);
                    string materialName = objAsset.GetMtlAssetName(groupPath);
                    if (false == string.IsNullOrEmpty(materialName) && false != mtlDictionary.ContainsKey(materialName)) {
                        renderer.material = mtlDictionary[materialName];
                    }
                    renderer.enabled = false;
                }
                yield return null;
            }
            Rigidbody rigidBody = this.gameObject.AddComponent<Rigidbody>();
            rigidBody.useGravity = false;
            SphereCollider collider = this.gameObject.AddComponent<SphereCollider>();
            collider.radius = 1f / this.scaleRate * 0.5f;
            collider.center += new Vector3(0f, 0.2f, 0f);
            Notifier notifier = Notifier.GetInstance();
            notifier.Notify(NotifyMessage.OnLoadingComplete);
        }
        yield return null;
    }
}
