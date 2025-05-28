using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �����ӿ�
public interface IInteractable
{
    //�����ľ����߼�
    void Interact(Player player);
}
// ѡ�нӿڣ�һ��ֱ�Ӽ̳�����ӿھͿ�����
public interface ISelectable : IInteractable
{
    //ѡ�еľ����߼�
    void Select()
    {
        //��ʾѡ��Ч��
    }
    //ȡ��ѡ�еľ����߼�
    void Deselect();

}
