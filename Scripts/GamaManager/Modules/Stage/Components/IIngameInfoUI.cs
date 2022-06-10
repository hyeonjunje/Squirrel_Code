using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IIngameInfoUI
{
    void HandleStageText(int chapter, int stage);    // �������� �̸�
    void HandleHealthUI(int health);   // �����
    void HandleAcornUI(int acorn);     // ������������ ���� ���丮 ����
    void HandleEndingItemUI(int endingItem);  // ������������ ���� ���������� ����
}