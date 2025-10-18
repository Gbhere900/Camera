using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class AlbumManager : SingletonMonoBase<AlbumManager>
{
    [Header("��Ƭ�б�")]
    // [SerializeField] private List<AlbumPageProperty> pageProperties = new List<AlbumPageProperty>();
    [SerializeField] private List<AlbumPage> pages;
    [SerializeField] private AlbumUI albumUI;


    private int currentPageIndex = 0;

    protected void Awake()
    {
        base.Awake();
        Initialized();
        
        
    }


    private void Initialized()
    {
        pages = new List<AlbumPage>(10);
        //ShowPage();
        ChangePage(currentPageIndex);
    }

    private AlbumPage GetPageByIndex(int taskIndex)
    {
        if (taskIndex >= pages.Count)
        {
            Debug.LogWarning("Index�����������������null");
            return null;

        }

        return pages[taskIndex];

    }

    public AlbumPage GetPageByTaskId(string taskId)
    {
        foreach (AlbumPage page in pages)
        {
            if (page.GetAlbumPageProperty().GetTaskId() == taskId)
            {
                return page;
            }
        }
        Debug.LogWarning("δ�ҵ�idΪ" + taskId + ",����null");
        return null;
    }

    private void InsertPage(string taskId, string pageDescription, Material photoMaterial, int index)
    {
        AlbumPageProperty albumPageProperty = new AlbumPageProperty(taskId, pageDescription);
        AlbumPage page = new AlbumPage(albumPageProperty, photoMaterial);
        pages[index] = page;
    }
    public void AddPage(string taskId, string pageDescription, Material photoMaterial)
    {
        AlbumPageProperty albumPageProperty = new AlbumPageProperty(taskId, pageDescription);
        AlbumPage page = new AlbumPage(albumPageProperty, photoMaterial);
        pages.Add(page);
    }
    public void ShowPage()
    {

        albumUI.gameObject.SetActive(true);
    }
    public void ChangePage(int index)
    {
        if (index >= pages.Count)
        {
            Debug.LogWarning("����������Χ���޷����ĵ�Ŀ��ҳ��");
            return ; 
        }
        albumUI.ChangePage(pages[index].GetAlbumPageProperty().GetPageDescription(), pages[index].GetCurrentPhotoMaterial());
    }

    public void HidePage()
    {
        albumUI.gameObject.SetActive(false);
    }

    public void OnNextPageButtonDown()
    {
        NextPage();

        //TODO:PlayAudio
    }

    public void OnLastPageButtonDown()
    {
        LastPage();
        //TODO:PlayAudio
    }

    private void NextPage()
    {
        currentPageIndex++;
        ChangePage(currentPageIndex);
    }

    private void LastPage()
    {
        currentPageIndex--;
        ChangePage(currentPageIndex);
    }
}
