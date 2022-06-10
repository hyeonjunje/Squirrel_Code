using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI ��Ҹ� ���簢������ ������ݴϴ�.
/// �䱸 ����: ��Ŀ�� ���� ���̴� ȭ�� ������ �°� �����־�� �մϴ�.
/// </summary>
[RequireComponent(typeof(RectTransform))]
public class RectNormalizer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RectTransform rect = GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(1f, rect.rect.width);
    }
}
