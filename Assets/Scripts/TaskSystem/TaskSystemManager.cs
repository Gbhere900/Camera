using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

// ��������ࣺ����ģʽ
public class TaskSystemManager : SingletonMonoBase<TaskSystemManager>
{
    [FormerlySerializedAs("taskQueue")]
    [Header("�������")]
    [SerializeField] private List<Task> taskList;
    
    [Header("��ǰ����")]
    private int currentTaskIndex;
    [SerializeField] private Task currentTask;

    protected override void Awake()
    {
        base.Awake();
        Initialized();
    }
    
    /// <summary>
    /// ��ʼ������
    /// </summary>
    private void Initialized()
    {
        if (taskList.Count == 0)
        {
            return;
        }
        currentTaskIndex = 0;
        currentTask = taskList[currentTaskIndex];
        // ����������״̬����Ϊ����ȡ
        foreach (Task task in taskList)
        {
            task.SetTaskType(Task.TaskStatus.Pending);
        }
    }

    //--------------------------�����ӿ�--------------------------
    /// <summary>
    /// ��������Ϊ����ȡ״̬
    /// </summary>
    public void ResetCurrentTask()
    {
        if (currentTask == null || currentTaskIndex >= taskList.Count)
        {
            Debug.LogError("Current task is null");
            return;
        }
        currentTask.SetTaskType(Task.TaskStatus.Pending);
    }
    
    /// <summary>
    /// ��ȡ��ǰ����
    /// </summary>
    public void AcceptCurrentTask()
    {
        if (currentTask == null || currentTaskIndex >= taskList.Count)
        {
            Debug.LogError("Task index out of range");
            return;
        }
        currentTask.SetTaskType(Task.TaskStatus.InProgress);
    }
    
    /// <summary>
    /// ����ǰ������Ϊ��ɣ����ۼ���������
    /// </summary>
    public void SetCurrentTaskCompleted()
    {
        if (currentTask == null || currentTaskIndex >= taskList.Count)
        {
            Debug.LogError("Current task is null");
            return;
        }
        currentTask.SetTaskType(Task.TaskStatus.Completed);
        Debug.Log(string.Format("����:{0} ���!", currentTask.GetTaskName()));
        currentTask = null;
        
        // �ۼ�����������ָ����һ������
        currentTaskIndex++;
        if (currentTaskIndex >= taskList.Count)
        {
            Debug.LogError("Task index is out of range");
            return;
        }
        currentTask = taskList[currentTaskIndex];
    }

    public int GetCurrentTaskIndex()
    {
        return currentTaskIndex;
    }

    public Task GetCurrentTask()
    {
        return currentTask;
    }

    public bool IsAllTaskCompleted()
    {
        foreach (Task task in taskList)
        {
            if (task.GetTaskStatus() != Task.TaskStatus.Completed)
            {
                return false;
            }
        }
        return true;
    }
}
