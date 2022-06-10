using System;

public enum eReward
{ 
    ACORN,
    BUBBLE,
    BALLOON,
    MAGNET,
    INITSTAGE,
}

[System.Serializable]
public class Mail : IComparable<Mail>
{
    /// <summary>
    /// ������ ���Կ� ��� ���ڿ�
    /// </summary>
    public string sentence;

    /// <summary>
    /// ���� ����. ��å �� ���丮�� �شٸ� ���� �ʿ���� �ʵ�
    /// </summary>
    public eReward reward;

    /// <summary>
    /// ������ ��.
    /// </summary>
    public int rewardCount;

    /// <summary>
    /// ������ ���Ƶ�� ����
    /// </summary>
    public DateTime time;

    /// <summary>
    /// ������ �޾Ҵ��� ����.
    /// </summary>
    public bool available;

    public Mail(string sentence, eReward reward, int rewardCount)
    {
        // ������ ���� �Ҵ�
        this.sentence = sentence;
        this.reward = reward;
        this.rewardCount = rewardCount;

        this.time = DateTime.Now; // ������ ���Ƶ�� ������ �ڵ����� �Ҵ�.
        this.available = true;
    }
    public int CompareTo(Mail other) => time.CompareTo(other.time);
}
