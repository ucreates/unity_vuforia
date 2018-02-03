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
namespace Service.Integration.Query.Expression {
public sealed class OrderByExpression : BaseExpression {
    public enum OrderType {
        Asc = 0x00,
        Desc = 0x01,
    }
    public OrderType orderType {
        get;
        set;
    }
    public string orderFieldName {
        get;
        set;
    }
    public OrderByExpression() : this(string.Empty, OrderType.Asc) {
    }
    public OrderByExpression(string orderFieldName, OrderType orderType) {
        this.orderFieldName = orderFieldName;
        this.orderType = orderType;
    }
}
}
