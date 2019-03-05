﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmosPoint : MonoBehaviour {
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(transform.position, 0.25f);
    }
}
