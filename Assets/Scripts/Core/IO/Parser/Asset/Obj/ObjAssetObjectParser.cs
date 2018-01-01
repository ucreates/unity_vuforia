﻿//======================================================================
// Project Name    : unity_foundation
//
// Copyright © 2017 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================
using UnityEngine;
using System.Collections;
using System.IO;
using Core.IO.Format.Asset;
namespace Core.IO.Parser.Asset {
public class ObjAssetObjectParser : ObjAssetBaseParser<ObjAssetObjectFormat> {
    public override ObjAssetObjectFormat Read(string header, string body) {
        ObjAssetObjectFormat format = new ObjAssetObjectFormat();
        format.header = header;
        format.name = body;
        return format;
    }
}
}
