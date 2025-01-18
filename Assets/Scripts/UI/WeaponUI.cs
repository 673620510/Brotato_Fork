using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public WeaponData weaponData;

    public Image _backImage;
    public Image _avatar;
    public Button _button;

    private void Awake()
    {
        _backImage = GetComponent<Image>();
        _avatar = transform.GetChild(0).GetComponent<Image>();
        _button = GetComponent<Button>();
    }

    public void SetData(WeaponData w)
    {
        this.weaponData = w;

        _avatar.sprite = Resources.Load<Sprite>(weaponData.avatar);
        _avatar.SetNativeSize();

        _button.onClick.AddListener(() =>
        {
            ButtonClick(weaponData);
        });
    }

    public void ButtonClick(WeaponData w)
    {
        GameManager.Instance.currentWeapon.Add(w);

        GameObject weapon_clone = Instantiate(WeaponSelectPanel.Instance._weaponDetails, DifficultySelectPanel.Instance._difficultyContent);
        weapon_clone.transform.SetSiblingIndex(0);

        GameObject role_clone = Instantiate(RoleSelectPanel.Instance._roleDetails, DifficultySelectPanel.Instance._difficultyContent);
        role_clone.transform.SetSiblingIndex(0);

        WeaponSelectPanel.Instance._canvasGroup.alpha = 0;
        WeaponSelectPanel.Instance._canvasGroup.interactable = false;
        WeaponSelectPanel.Instance._canvasGroup.blocksRaycasts = false;

        DifficultySelectPanel.Instance._canvasGroup.alpha = 1;
        DifficultySelectPanel.Instance._canvasGroup.interactable = true;
        DifficultySelectPanel.Instance._canvasGroup.blocksRaycasts = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _backImage.color = new Color(207 / 255f, 207 / 255f, 207 / 255f);

        if (WeaponSelectPanel.Instance._contentCanvasGroup.alpha != 1)
        {
            WeaponSelectPanel.Instance._contentCanvasGroup.alpha = 1;
        }

        RenewUI(weaponData);
    }

    public void RenewUI(WeaponData w)
    {
        WeaponSelectPanel.Instance._weaponAvatar.sprite = Resources.Load<Sprite>(w.avatar);
        WeaponSelectPanel.Instance._weaponAvatar.SetNativeSize();
        WeaponSelectPanel.Instance._weaponName.text = w.name;
        WeaponSelectPanel.Instance._weaponType.text = w.isLong == 0 ? "��ս" : "Զ��";
        WeaponSelectPanel.Instance._weaponDescribe.text = w.describe;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _backImage.color = new Color(34 / 255f, 34 / 255f, 34 / 255f);
    }
}
