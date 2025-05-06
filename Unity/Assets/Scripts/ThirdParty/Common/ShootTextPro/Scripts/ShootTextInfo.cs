using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ShootTextInfo
{
    public bool isNeedShowPreOperator;
    public int showValue;
    public TextMoveType moveType;
    public float delayMoveTime;
    public float moveLifeTime;
    public int size;
    public Func<Vector3> getShootTextTopPoint;
    public Func<Vector3> getShootTextButtomPoint;
    public float initializedVerticalPositionOffset;
    public float initializedHorizontalPositionOffset;
    public float xIncrement;
    public float yIncrement;
}

