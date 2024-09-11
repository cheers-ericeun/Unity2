using EditorTools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : AutoMonoBehaviour
{

    [SerializeField, AutoAssign(AutoAssignAttribute.AutoAssignMode.Parent)] private Health _health;
    [SerializeField] private Image _fillBar;
    private void Update()
    {
        if (_health == null) return;
        _fillBar.fillAmount = _health.percentage;

        transform.rotation = Camera.main.transform.rotation;
    }


}
