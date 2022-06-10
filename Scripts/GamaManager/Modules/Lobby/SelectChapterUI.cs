using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectChapterUI : MonoBehaviour
{
    [SerializeField] GameObject chapterTab;

    [SerializeField] GameObject chapterButtons;
    [SerializeField] Animator chapterAnimator;

    [SerializeField] Image[] chapter;
    [SerializeField] GameObject[] chapterInfoUI;
    [SerializeField] Text[] itemCountText;

    [SerializeField] GameObject endingButton;

    private IDataHandler dataHandler;

    private int[] seasonItemCount;

    private void Awake()
    {
        dataHandler = FindObjectOfType<GameManager>();
    }

    private void SetUIValues()
    {
        seasonItemCount = new int[4] { 0, 0, 0, 0 };

        for (int i = 0; i < 20; i++)
        {
            if (1 <= i && i < 5) seasonItemCount[0] += dataHandler.Data.MyItemPerStage[i];
            else if (6 <= i && i < 10) seasonItemCount[1] += dataHandler.Data.MyItemPerStage[i];
            else if (11 <= i && i < 15) seasonItemCount[2] += dataHandler.Data.MyItemPerStage[i];
            else if (16 <= i && i < 20) seasonItemCount[3] += dataHandler.Data.MyItemPerStage[i];
        }

        for(int i = 0; i < itemCountText.Length; i++)
        {
            itemCountText[i].text = "X " + seasonItemCount[i] + "��";
        }

        for (int i = 1; i < chapter.Length; i++)
        {
            if (dataHandler.Data.ClearStatus[i - 1].intArr[4] == 0) // ���� ���� 3�������� Ŭ�����(= 4�������� ����� �ƴҶ�)
                chapter[i].color = new Color(0.5f, 0.5f, 0.5f);
            else
                chapter[i].color = new Color(1f, 1f, 1f);
        }
    }

    public void CloseSelectChapterTab() => chapterTab.SetActive(false);

    public void InitSelectChapter()
    {
        chapterTab.SetActive(true);

        /// é�� ���� â�� é�� ���� Ȱ��ȭ
        foreach (GameObject o in chapterInfoUI) o.SetActive(true);

        /// é�� ���� â �� ����
        SetUIValues();

        /// �ܿ� 4 ���������� ������ ������ư�� Ȱ��ȭ(���� ��ȭ)
        endingButton.GetComponent<Image>().color = 
            dataHandler.Data.ClearStatus[3].intArr[4] != 0 ? 
            new Color(1, 1, 1) : new Color(0.5f, 0.5f, 0.5f);

        chapterAnimator.ResetTrigger("close");
        chapterAnimator.ResetTrigger("open1");
        chapterAnimator.ResetTrigger("open2");
        chapterAnimator.ResetTrigger("open3");
        chapterAnimator.ResetTrigger("open4");

        chapterAnimator.SetTrigger("close");

        chapterButtons.SetActive(true);
    }

    public void OpenChapter(int i)
    {
        foreach (GameObject o in chapterInfoUI) o.SetActive(false);

        /** (redmine #5382)
         * é�� ���� â �� ����
         * SelectChapterUI�� Ȱ��ȭ�ϴ� �� ���� �޼ҵ� ��, ���ʿ��� SetUI�� ������ �ʾҽ��ϴ�.
         * ���� ���� ��� ä�� �ִϸ��̼��� ��µǴ� ���μ��� ���� ��ó�� ���� �� �ۿ� ��������.
         */
        SetUIValues();

        chapterButtons.SetActive(false);

        chapterAnimator.ResetTrigger("close");
        chapterAnimator.ResetTrigger("open1");
        chapterAnimator.ResetTrigger("open2");
        chapterAnimator.ResetTrigger("open3");
        chapterAnimator.ResetTrigger("open4");

        chapterAnimator.SetTrigger("open" + i);
    }
}
