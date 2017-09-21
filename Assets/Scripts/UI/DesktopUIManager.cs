using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DesktopUIManager : BaseUIManager {

    [SerializeField] private Text ammoText;
    [SerializeField] private Text healthText;

    public override void SetShownHealth(float value)
    {
        healthText.text = string.Format("{0}", value);
    }

    public override void SetShownAmmo(int value)
    {
        ammoText.text = string.Format("{0}", value);
    }
}
