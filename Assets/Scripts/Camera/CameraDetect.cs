using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraDetect : MonoBehaviour
{

    [SerializeField] private AudioClip detectAudio;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Transform detectUIPrefab;

    private Transform currentUI;


    private void OnEnable()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        TaskItem taskItem;
        if (other.TryGetComponent<TaskItem>(out taskItem))
        {
            Vector2 screenPos = Camera.main.WorldToScreenPoint(other.transform.position);

            // ��ʾUI
            ShowPositionUI(screenPos);
            //TODO: ������Ч��UI
        }

    }

    private void OnTriggerExit(Collider other)
    {
        TaskItem taskItem;
        if (other.TryGetComponent<TaskItem>(out taskItem))
        {
            if (currentUI != null)
            {
                Destroy(currentUI.gameObject);
                currentUI = null;
            }
            //TODO: ������Ч��UI
        }
    }

    private void ShowPositionUI(Vector2 screenPos)
    {
        // �����Ѵ��ڵ�UI�������ظ���ʾ��
        if (currentUI != null)
            Destroy(currentUI.gameObject);

        // ʵ����UIԤ���嵽Canvas��
        currentUI = Instantiate(detectUIPrefab, canvas.transform);

        // ��ȡUI��RectTransform�������������λ��
        RectTransform uiRect = currentUI.GetComponent<RectTransform>();

        // ����UI����Ļ�ϵ�λ�ã�ע�⣺ScreenPoint��ԭ������Ļ���½ǣ�UIê�����Ӧ��
        // ��UIê��Ϊ���ģ���ƫ��UI����ߴ��һ�루����򻯴�������ê�������½ǣ�
        uiRect.anchoredPosition = screenPos;

    }
}
