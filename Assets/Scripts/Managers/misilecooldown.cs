using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class misilecooldown : MonoBehaviour
{
    [SerializeField] private Image misile;
    [SerializeField] private PlayerController playerController;


    private void Awake()
    {
        playerController.OnRocketCooldownChanged += OncooldownChangedHandler;
        Updatecooldown();
    }

    private void OncooldownChangedHandler(float currenthealth)
    {
        Updatecooldown();
    }
    private void Updatecooldown()
    {
        misile.fillAmount = playerController.GetCurrentMissileCooldown();
    }
}
