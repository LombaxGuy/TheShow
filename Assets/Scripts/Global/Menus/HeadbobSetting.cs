﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HeadbobSetting : MonoBehaviour
{
    private Slider headbobSlider;

    [SerializeField]
    private float defaultValue = 100;

    private float currentValue = 0;

    public float CurrentValue
    {
        get { return currentValue; }
        set { currentValue = value; }
    }

    private void OnEnable()
    {
        EventManager.OnCheckForSettingChanges += OnCheckForSettingChanges;
        EventManager.OnResetSettings += OnResetSettings;
        EventManager.OnResetToDefaultSettings += OnResetToDefaultSettings;
        EventManager.OnApplySettingChanges += OnApplySettingChanges;
        EventManager.OnSavePref += OnSavePref;
        EventManager.OnLoadPref += OnLoadPref;
    }

    /// <summary>
    /// Runs when the object is disabled
    /// </summary>
    private void OnDisable()
    {
        EventManager.OnCheckForSettingChanges -= OnCheckForSettingChanges;
        EventManager.OnResetSettings -= OnResetSettings;
        EventManager.OnResetToDefaultSettings -= OnResetToDefaultSettings;
        EventManager.OnApplySettingChanges -= OnApplySettingChanges;
        EventManager.OnSavePref -= OnSavePref;
        EventManager.OnLoadPref -= OnLoadPref;
    }

    private void Awake()
    {
        headbobSlider = GetComponentInChildren<Slider>();
    }

    private void OnCheckForSettingChanges()
    {
        if (currentValue != headbobSlider.value)
        {
            EventManager.RaiseOnSettingsChanged();
        }
    }

    private void OnResetSettings()
    {
        headbobSlider.value = currentValue;
    }

    private void OnResetToDefaultSettings()
    {
        headbobSlider.value = defaultValue;
        currentValue = defaultValue;
    }

    private void OnApplySettingChanges()
    {
        currentValue = headbobSlider.value;
    }

    private void OnSavePref()
    {
        // Save currentValue
    }

    private void OnLoadPref()
    {
        // Load currentValue
    }
}
