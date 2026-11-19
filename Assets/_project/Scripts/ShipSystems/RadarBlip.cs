using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadarBlip : MonoBehaviour
{
    [SerializeField] Image _belowArrow, _aboveArrow;

    public Image BelowArrow => _belowArrow;
    public Image AboveArrow => _aboveArrow;
}
