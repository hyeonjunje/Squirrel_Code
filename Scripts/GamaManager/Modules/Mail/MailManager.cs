using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MailManager : MonoBehaviour, IMailHandler, IRewardHandler
{
    private List<Mail> Mails => dataHandler.Data.Mails;  // ���� ������ ���̺� �������� ���ϸ���Ʈ�� ����Ű��
    
    private IDataHandler dataHandler;
    ISEHandler se;

    [SerializeField] private MailBox mailBox;
    [SerializeField] private GameObject newPrompt;
    public bool HasNew => Mails.Count > 0;

    private const int maxMailCount = 50;
    public int CurrentMailCount => Mails.Count;

    public static int MAX_MAIL_COUNT => maxMailCount;

    public void Send(Mail mail)
    {
        if (Mails.Count == MAX_MAIL_COUNT)
        {
            Mails.RemoveAt(0); // ������ ���� ������
        }

        Mails.Add(mail);
        Mails.Sort();

        newPrompt.SetActive(HasNew);
    }

    public void GetReward(int index, MailSlot slot)
    {
        se.PlaySE(se.SE_CLICK);

        Mail m = Mails[index];

        if (!m.available) return; // �̹� ������ �޾Ҵٸ� ����������

        m.available = false;

        // reward �߰� ����
        switch (m.reward)
        {
            case eReward.ACORN:
                dataHandler.Acorn += m.rewardCount;
                break;
            case eReward.BUBBLE:
                dataHandler.Data.MyItemCount[1] += m.rewardCount;
                break;
            case eReward.BALLOON:
                dataHandler.Data.MyItemCount[2] += m.rewardCount;
                break;
            case eReward.MAGNET:
                dataHandler.Data.MyItemCount[0] += m.rewardCount;
                break;
            case eReward.INITSTAGE:
                dataHandler.Data.MyItemCount[3] += m.rewardCount;
                break;
        }

        slot.SetUnavailable();

        //fileHandler.Save();
    }

    // Start is called before the first frame update
    async void Start()
    {
        GameManager gm = FindObjectOfType<GameManager>();
        dataHandler = gm;
        se = gm;

        await UniTask.WaitUntil(() => dataHandler?.Data != null);

        newPrompt.SetActive(HasNew);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenMailBox()
    {
        se.PlaySE(se.SE_CLICK);
        mailBox.gameObject.SetActive(true);
        mailBox.SetMails(Mails);

        newPrompt.SetActive(false);
    }
    public void CloseMailBox()
    {
        mailBox.gameObject.SetActive(false);
        Mails.RemoveAll(mail => !mail.available);

        newPrompt.SetActive(HasNew);
    }

    public void SendTest()
    {
        Send(new Mail("��ȹ�� Ư������", eReward.ACORN, 10));
        Send(new Mail("���ٻ�", eReward.ACORN, 20));
        Send(new Mail("1�� ��� ����", eReward.ACORN, 30));
    }


    public void GetRewardsAtOnce()
    {
        se.PlaySE(se.SE_CLICK);
        foreach (MailSlot slot in mailBox.MailSlots)
        {
            GetReward(slot.Index, slot);
        }
    }
}
