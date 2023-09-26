using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootTextInfo
{
    public string content;
    public TextAnimationType animationType;
    public TextMoveType moveType;
    public float delayMoveTime;
    public int size;
    public Func<Vector3> getShootTextTopPoint;
    public Func<Vector3> getShootTextButtomPoint;
    public float initializedVerticalPositionOffset;
    public float initializedHorizontalPositionOffset;
    public float xIncrement;
    public float yIncrement;
}

