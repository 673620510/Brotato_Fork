   using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RoleUI : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
{
    public Image _backImage;
    public Image _avatar;
    public Button _button;

    public RoleData roleData;

    private void Awake()
    {
        _backImage = GetComponent<Image>();
        _avatar = transform.GetChild(0).GetComponent<Image>();
        _button =GetComponent<Button>();
    }
    public void SetData(RoleData r)
    {
        this.roleData = r;

        if (roleData.unlock == 0 && PlayerPrefs.GetInt(roleData.name, 1) == 0)
        {
            _avatar.sprite = Resources.Load<Sprite>("Image/UI/��");
            _avatar.SetNativeSize();
        }
        else
        {
            _avatar.sprite = Resources.Load<Sprite>(roleData.avatar);
            _avatar.SetNativeSize();

            _button.onClick.AddListener(() =>
            {
                ButtonClick(roleData);
            });
        }
    }
    public void ButtonClick(RoleData r)
    {
        GameManager.Instance.currentRole = r;

        RoleSelectPanel.Instance._canvasGroup.alpha = 0;
        RoleSelectPanel.Instance._canvasGroup.interactable = false;
        RoleSelectPanel.Instance._canvasGroup.blocksRaycasts = false;

        GameObject go = Instantiate(RoleSelectPanel.Instance._roleDetails, WeaponSelectPanel.Instance._weaponContent);
        go.transform.SetSiblingIndex(0);

        WeaponSelectPanel.Instance._canvasGroup.alpha = 1;
        WeaponSelectPanel.Instance._canvasGroup.interactable = true;
        WeaponSelectPanel.Instance._canvasGroup.blocksRaycasts = true;
    }
    public void RenewUI(RoleData r)
    {
        if (r.unlock == 0)
        {
            RoleSelectPanel.Instance._roleName.text = "???";
            RoleSelectPanel.Instance._avatar.sprite = Resources.Load<Sprite>("Image/UI/��");
            RoleSelectPanel.Instance._avatar.SetNativeSize();
            RoleSelectPanel.Instance._roleDescribe.text = r.unlockConditions;
            RoleSelectPanel.Instance._text3.text = "���޼�¼";
        }
        else
        {
            RoleSelectPanel.Instance._roleName.text = r.name;
            RoleSelectPanel.Instance._avatar.sprite = Resources.Load<Sprite>(r.avatar);
            RoleSelectPanel.Instance._avatar.SetNativeSize();
            RoleSelectPanel.Instance._roleDescribe.text = r.describe;
            RoleSelectPanel.Instance._text3.text = GetRecord(r.record);
        }
    }
    private string GetRecord(int rRecard)
    {
        string result = "";
        switch (rRecard)
        {
            case -1:
                result = "���޼�¼";
                break;
            case 0:
                result = "ͨ��Σ��0";
                break;
            case 1:
                result = "ͨ��Σ��1";
                break;
            case 2:
                result = "ͨ��Σ��2";
                break;
            case 3:
                result = "ͨ��Σ��3";
                break;
            case 4:
                result = "ͨ��Σ��4";
                break;
            case 5:
                result = "ͨ��Σ��5";
                break;
        }
        return result;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        _backImage.color = new Color(207 / 255f, 207 / 255f, 207 / 255f);

        if (RoleSelectPanel.Instance._contentCanvasGroup.alpha != 1)
        {
            RoleSelectPanel.Instance._contentCanvasGroup.alpha = 1;        
        }

        RenewUI(roleData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _backImage.color = new Color(34 / 255f, 34 / 255f, 34 / 255f);
    }
}
