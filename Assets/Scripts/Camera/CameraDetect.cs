using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraDetect : MonoBehaviour
{
    
    [SerializeField] private AudioClip detectAudio;
    [SerializeField] private Transform detectUIPrefab;

    [SerializeField] private TaskItem requestTaskItem;
    private CapsuleCollider triggerCollider; // �����崥����




    private void OnEnable()
    {
        SceneManager.Instance().OnWorldStateChange += ResetDetectUI;
        CheckTrigger();


    }

    private void CheckTrigger()
    {
        triggerCollider = GetComponent<CapsuleCollider>();
        Vector3 center = transform.TransformPoint(triggerCollider.center); // ���������ģ��������꣩
        float height = triggerCollider.height; // ������߶ȣ�������������
        float radius = triggerCollider.radius; // ������뾶

        // �����������CapsuleColliderĬ��Y�ᣬ��ͨ��direction�޸ģ�0=X��1=Y��2=Z��
        Vector3 axis = Vector3.up; // Ĭ��Y��
        if (triggerCollider.direction == 0) axis = Vector3.right;
        else if (triggerCollider.direction == 2) axis = Vector3.forward;

        // ���㽺����������˵㣨�������꣩
        Vector3 point1 = center + axis * (height / 2 - radius); // �϶˵�
        Vector3 point2 = center - axis * (height / 2 - radius); // �¶˵�

        // �ֶ���⽺�����ڵ�������ײ��
        Collider[] overlappedColliders = Physics.OverlapCapsule(
            point1,       // �������϶˵�
            point2,       // �������¶˵�
            radius,       // ������뾶
            ~0            // ������в㼶�����Զ���㼶���룩
        );

        // ������⵽����ײ�壬ģ�ⴥ��OnTriggerEnter
        foreach (var col in overlappedColliders)
        {
            if (col != triggerCollider) // �ų�������ײ��
            {
                OnTriggerEnter(col); // ���ô����߼�
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        TaskItem taskItem;
        if (other.TryGetComponent<TaskItem>(out taskItem))
        {
            if (taskItem == requestTaskItem)
            {
                Debug.Log("Detected" + other.gameObject.name);
                // ��ʾUI


                detectUIPrefab.gameObject.SetActive(true);
                //TODO: ������Ч��UI
            }

        }

    }

    private void OnDisable()
    {
        detectUIPrefab.gameObject.SetActive(false);
    }

    private void OnTriggerExit(Collider other)
    {
        TaskItem taskItem;
        if (other.TryGetComponent<TaskItem>(out taskItem))
        {
            
        }
    }

    private void ResetDetectUI(WorldState worldState)
    {
        detectUIPrefab.gameObject.SetActive(false);
        CheckTrigger();
    }

    //private void ShowPositionUI(Vector2 screenPos)
    //{
    //    // �����Ѵ��ڵ�UI
    //    if (currentUI != null)
    //        Destroy(currentUI.gameObject);

    //    // ʵ����UIԤ����
    //    currentUI = Instantiate(detectUIPrefab, canvas.transform);
    //    RectTransform uiRect = currentUI.GetComponent<RectTransform>();

    //    // 1. ����CanvasΪScreen Space - Overlay������������
    //    // ת��Y�᣺��Ļ����Y��ԭ�����£��� UI����Y��ԭ�����ϣ�
    //    float uiY = Screen.height - screenPos.y;

    //    // 2. ����UIê�㣨����ê��Ϊ���ģ�������ƫ�ƣ�
    //    // ��UIê�������ģ����ȥ����һ��ߴ磬����λ��ƫ��
    //    Vector2 uiPivotOffset = new Vector2(
    //        uiRect.rect.width * uiRect.pivot.x,
    //        uiRect.rect.height * uiRect.pivot.y
    //    );

    //    // ����UIλ�� = ת�������Ļ���� - ê��ƫ��
    //    Vector2 uiPosition = new Vector2(screenPos.x, uiY) - uiPivotOffset;

    //    // ����UIλ��
    //    uiRect.anchoredPosition = uiPosition;
    //}
}
