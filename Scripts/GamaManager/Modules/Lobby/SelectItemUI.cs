using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectItemUI : MonoBehaviour
{
    [SerializeField] GameObject selectItemTab;

    private IDataHandler dataHandler;
    private IGameFlowHandler flowHandler;
    private IFileHandler fileHandler;
    private Inventory inventory;

    [SerializeField] GameObject[] effectUI;
    [SerializeField] Text[] countText;

    [SerializeField] Image endingItemImage;
    [SerializeField] Sprite[] endingItemSprites;

    [SerializeField] Text myItemCount;

    [SerializeField] Text stageText;

    [SerializeField] WarningPopUp warningPopUp;

    [SerializeField] Text ItemResetPopUpText;

    [SerializeField] GameObject Stage0;
    [SerializeField] Text Stage0Text;

    private void Awake()
    { 
        GameManager gm = FindObjectOfType<GameManager>();
        dataHandler = gm;
        flowHandler = gm;
        fileHandler = gm;
        inventory = gm.Inventory;
    }

    public void ShowSelectItemTab()
    {
        SetUIValues();
        InitItems();
        foreach (GameObject o in effectUI) o.SetActive(false);

        selectItemTab.SetActive(true);
    }
    public void CloseSelectItemTab() => selectItemTab.SetActive(false);

    /// <summary>
    /// â�� ���� �� �ʱ�ȭ ���ִ� �κ�
    /// �������� �����ߴٰ� �ڷΰ��� ��ư ������ �ٽ� ���� �������� ����� �� �ʱ�ȭ ����
    /// </summary>
    private void InitItems()
    {
        for(int i = 0; i < 4; i++)
        {
            if(flowHandler.ShopItemContainer[i] == true)  // �������� ���õǾ� ������
            {
                inventory.UnuseItem(i);
                //fileHandler.Save();
                if(i != 3)  // �ʱ�ȭ���� �ƴϸ� ���� �ҵ� ���� ������ �������
                {
                    effectUI[i].SetActive(false);
                    countText[i].text = fileHandler.Data.MyItemCount[i].ToString();
                }
            }

            // ���� ������ 0���ε� �����ؼ� ���� ���� ��� ������ ������ �Ҹ� ����
            if(i != 3 && fileHandler.Data.MyItemCount[i] == 0 && effectUI[i].activeSelf == true)
            {
                effectUI[i].SetActive(false);
            }
        }
    }

    private void SetUIValues()
    {
        for (int i = 0; i < countText.Length; ++i)
        {
            Debug.Log(i + " " + dataHandler);
            countText[i].text = dataHandler.Data.MyItemCount[i].ToString();
        }

        endingItemImage.sprite = endingItemSprites[flowHandler.ChapterNum - 1];

        int currentStage = (flowHandler.ChapterNum - 1) * 5 + flowHandler.StageNum;
        myItemCount.text = dataHandler.Data.MyItemPerStage[currentStage].ToString();

        stageText.text = "STAGE " + flowHandler.ChapterNum + "-" + flowHandler.StageNum;

        string itemName = flowHandler.ChapterNum == 1 ? "����" : flowHandler.ChapterNum == 2 ? "�����" : flowHandler.ChapterNum == 3 ? "��ǳ" : "����";
        ItemResetPopUpText.text = flowHandler.ChapterNum + "-" + flowHandler.StageNum + "���������� " + itemName + "��\n�ٽ� �����ðڽ��ϱ�?";

        /// 0���������� ��� ������������ ���ٴ� ǥ�ø� �ش�.
        Stage0.SetActive(flowHandler.StageNum == 0);
        Stage0Text.text = "����� " + itemName + "�� ���ٶ�!";

    }

    public void SelectItem(int index)
    {
        // inventory.SelectItem(index);
        // effectUI[index].SetActive(!effectUI[index].activeSelf);

        effectUI[index].SetActive(inventory.SelectItem(index));

        countText[index].text = fileHandler.Data.MyItemCount[index].ToString();
    }

    /// <summary>
    /// �ʱ�ȭ �������� �ٸ� �������̶� ���� �ٸ��� �۵��ؼ� �Լ� �ϳ� �׳� ��������ϴ�...
    /// </summary>
    /// <param name="check">�ʱ�ȭ ������ ���ý� ������ â���� �� ������ check 1, �ƴϿ� ������ check 0</param>
    public void SelectInitStageItem(bool check)
    {
        // �� ������
        if(check)
        {
            /// �������� �ʱ�ȭ �������� ����ߴµ� �ش� ���������� Ŭ���� ���� �ʾҴٸ� ��� �Ұ�
            if (dataHandler.Data.ClearStatus[flowHandler.ChapterNum - 1].intArr[flowHandler.StageNum] == 1)
            {
                warningPopUp.gameObject.SetActive(true);
                warningPopUp.setText("�ش� ���������� Ŭ�����ؾ�\n��밡�� �մϴ�.");
                return;
            }
            else
            {
                // ���� �����µ� �̹� ���õǾ��ٸ� �̹� ���õƴٰ� �˷��ֱ�
                if(flowHandler.ShopItemContainer[3])
                {
                    warningPopUp.gameObject.SetActive(true);
                    warningPopUp.setText("�������� �ʱ�ȭ���� �̹�\n����ϰ� �ֽ��ϴ�.");
                    return;
                }
                else
                {
                    if (dataHandler.Data.MyItemCount[3] == 0)
                    {
                        warningPopUp.gameObject.SetActive(true);
                        warningPopUp.setText("�������� �ʱ�ȭ���� �����ϴ�.");
                        return;
                    }
                    inventory.UseItem(3);
                    //fileHandler.Save();
                }
            }
        }
        // �ƴϿ� ������
        else
        {
            // ���õǾ��� ��� ����ϱ�
            if (flowHandler.ShopItemContainer[3])
            {
                inventory.UnuseItem(3);
                //fileHandler.Save();
            }
        }
    }
}
