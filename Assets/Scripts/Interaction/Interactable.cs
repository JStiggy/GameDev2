﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour {
    abstract public IEnumerator Interact();
}
