using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlasterUI : MonoBehaviour
{
    [SerializeField] Blaster[] _blasters;
    [SerializeField] Image[] _blasterBar, _coolDownBar;

    private void LateUpdate()
    {
        for(int i =0; i<_blasters.Length; ++i)
        {
            _blasterBar[i].fillAmount = Mathf.Lerp(_blasterBar[i].fillAmount, _blasters[i].CapacitorChargePercentage, 10f * Time.deltaTime);
            _coolDownBar[i].fillAmount = Mathf.Lerp(_coolDownBar[i].fillAmount, _blasters[i].CoolDownPercent, 10f * Time.deltaTime);
        }
    }
}
