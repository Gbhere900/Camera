using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(QuickOutline))]
public abstract class InteractiveObjectBase : MonoBehaviour
{
    [Header("��������")]
    //[SerializeField] protected float interactiveDistance;
    [SerializeField] protected float cooldownTime;
    [SerializeField] protected bool isHighlight = false;
    [SerializeField] protected float outlineWidth = 10f;

    private bool isIntersectingWithDetector = false;
    protected float lastInteractTime;
    protected GameObject player;

    private QuickOutline outline;
    
    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
        Initialized();
    }
    
    protected virtual void Update()
    {
        // �����߼�
        isHighlight = CanInteract;
        if (outline)
        {
            if (isHighlight)
            {
                outline.enabled = true;
            }
            else
            {
                outline.enabled = false;
            }
        }
        
        // ����������
        CheckPlayerInput();
    }
    
    public bool CanInteract
    {
        get
        {
            // ������ȴʱ���ж�
            if (cooldownTime > 0 && Time.time < lastInteractTime + cooldownTime)
            {
                return false;
            }
            // ���������ж�
            if (!player || !isIntersectingWithDetector)
            {
                return false;
            }
            return IsInteractionPossible();
        }
    }

    /// <summary>
    /// �жϽ����Ƿ����
    /// </summary>
    /// <returns></returns>
    protected abstract bool IsInteractionPossible();

    /// <summary>
    /// ��ʼ��
    /// </summary>
    protected virtual void Initialized()
    {
        lastInteractTime = -cooldownTime;
        
        outline = GetComponent<QuickOutline>();
        if (outline)
        {
            outline.OutlineColor = Color.white;
            outline.OutlineWidth = outlineWidth;
            outline.OutlineMode = QuickOutline.Mode.OutlineVisible;
        }
    }

    /// <summary>
    /// ����������
    /// </summary>
    private void CheckPlayerInput()
    {
        // F������
        if (Input.GetKeyDown(KeyCode.I) && CanInteract)
        {
            Interact();
        }
    }

    /// <summary>
    /// ��������
    /// </summary>
    private void Interact()
    {
        if (!CanInteract)
        {
            Debug.LogError("Can't interact with this object");
            return;
        }
        
        // Todo: ����UI��ʾ�߼�
        
        // ִ�о��彻���߼�
        PerformInteraction();
        // ��¼����ʱ��
        lastInteractTime = Time.time;
    }
    
    /// <summary>
    /// ���彻���߼�
    /// </summary>
    protected abstract void PerformInteraction();

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "InteractiveDetect")
        {
            isIntersectingWithDetector = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "InteractiveDetect")
        {
            isIntersectingWithDetector = false;
        }
    }
}
