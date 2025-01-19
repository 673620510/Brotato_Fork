using System;

[Serializable]
public class PropData
{
    public int id;
    public string name;
    public float price;//�۸�
    public string describe;//����
    //����ֵ���
    public float maxHp = 15;
    public float revive = 0;//��������
    //�������
    public float short_damge = 1;//���ӽ�ս�����˺� �ٷֱ�
    public float long_damge = 1;//����Զ�������˺� �ٷֱ�
    public float short_range = 1;//���ӽ�ս������Χ �ٷֱ�
    public float long_range = 1;//����Զ��������Χ �ٷֱ�
    public float short_attackSpeed = 1;//���ӽ�ս�������� �ٷֱ�
    public float long_attackSpeed = 1;//����Զ��������Χ �ٷֱ�
    //�ƶ����
    public float speed = 5;//��������
    public float speedPer = 1;//�������� �ٷֱ�
    //��Ϸ�����
    public int harvest = 0;//�ջ�
    public int slot = 6;//���
    public float shopDiscount = 1;//���߼۸� �ٷֱ�
    public float expMuti = 1;//���鱶�� �ٷֱ�
    public float pickRange = 1;//ʰȡ��Χ  �ٷֱ�
    public int critical_strikes_probability = 1;//������ �ٷֱ�

}