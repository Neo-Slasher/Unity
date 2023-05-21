using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�ӽ÷� ������� �� ���� �־����
public enum EnemyType
{
    None, BlackSuitMan, WhiteSuitMan, MachineArmorSoldier
        , Red3LegRobot, Blue3LegRobot, Big4LegRobot
}

[System.Serializable]
public class EnemyTrashData
{
    public int monIdx;

    public double monWidth;     //�� ��Ʈ�ڽ� ����
    public double monLength;    //�� ��Ʈ�ڽ� ����

    //public int monImage;
    //public int monSound;

    public double hitPointMax;
    public double hitPoint;
    public double moveSpeed;
    public double attackPower;
    public bool dashAble;
    public double dashFreq;
    public double dashSpeed;
    public double dashDuration;

    public bool monElite;       //����Ʈ ���ΰ�?
    public bool monEnforce;     //��ȭ�Ǿ��°�?
    public bool monResist;      //������ �з����� �ʴ°�?
    public bool monKnockback;   //�÷��̾ �˹��ϴ°�?
    public bool monProj;        //����ü �߻翩��


    public EnemyTrashData(EnemyType getEnemyType = EnemyType.None)
    {
        SetEnemyTypeData(getEnemyType);
    }

    public void SetEnemyTypeData(EnemyType getEnemyType)
    {
        switch(getEnemyType)
        {
            case EnemyType.None:
                monIdx = -1;
                monWidth = 125;     //�� ��Ʈ�ڽ� ����
                monLength = 348;    //�� ��Ʈ�ڽ� ����

                //monImage;
                //monSound;

                hitPointMax = 0;
                hitPoint = 0;
                moveSpeed = 0;
                attackPower = 0;
                dashAble = false;
                dashFreq = 0;
                dashSpeed = 0;
                dashDuration = 0;

                monElite = false;       //����Ʈ ���ΰ�?
                monEnforce = false;     //��ȭ�Ǿ��°�?
                monResist = false;      //������ �з����� �ʴ°�?
                monKnockback = false;   //�÷��̾ �˹��ϴ°�?
                monProj = false;        //����ü �߻翩��
                break;

            case EnemyType.BlackSuitMan:
                monIdx = 1;
                monWidth = 125;     //�� ��Ʈ�ڽ� ����
                monLength = 348;    //�� ��Ʈ�ڽ� ����

                //monImage;
                //monSound;

                hitPointMax = 8;
                hitPoint = 8;
                moveSpeed = 5;
                attackPower = 2;
                dashAble = false;
                dashFreq = 0;
                dashSpeed = 0;
                dashDuration = 0;

                monElite = false;       //����Ʈ ���ΰ�?
                monEnforce = false;     //��ȭ�Ǿ��°�?
                monResist = false;      //������ �з����� �ʴ°�?
                monKnockback = false;   //�÷��̾ �˹��ϴ°�?
                monProj = false;        //����ü �߻翩��
                break;
            case EnemyType.WhiteSuitMan:
                monIdx = 2;
                monWidth = 125;     //�� ��Ʈ�ڽ� ����
                monLength = 348;    //�� ��Ʈ�ڽ� ����

                //monImage;
                //monSound;

                hitPointMax = 18;
                hitPoint = 18;
                moveSpeed = 7;
                attackPower = 4;
                dashAble = false;
                dashFreq = 0;
                dashSpeed = 0;
                dashDuration = 0;

                monElite = false;       //����Ʈ ���ΰ�?
                monEnforce = false;     //��ȭ�Ǿ��°�?
                monResist = false;      //������ �з����� �ʴ°�?
                monKnockback = false;   //�÷��̾ �˹��ϴ°�?
                monProj = false;        //����ü �߻翩��
                break;

            case EnemyType.MachineArmorSoldier:
                monIdx = 3;
                monWidth = 125;     //�� ��Ʈ�ڽ� ����
                monLength = 348;    //�� ��Ʈ�ڽ� ����

                //monImage;
                //monSound;

                hitPointMax = 40;
                hitPoint = 40;
                moveSpeed = 10;
                attackPower = 7;
                dashAble = false;
                dashFreq = 0;
                dashSpeed = 0;
                dashDuration = 0;

                monElite = false;       //����Ʈ ���ΰ�?
                monEnforce = false;     //��ȭ�Ǿ��°�?
                monResist = true;      //������ �з����� �ʴ°�?
                monKnockback = true;   //�÷��̾ �˹��ϴ°�?
                monProj = false;        //����ü �߻翩��
                break;

            case EnemyType.Red3LegRobot:
                monIdx = 4;
                monWidth = 125;     //�� ��Ʈ�ڽ� ����
                monLength = 348;    //�� ��Ʈ�ڽ� ����

                //monImage;
                //monSound;

                hitPointMax = 20;
                hitPoint = 20;
                moveSpeed = 8;
                attackPower = 5;
                dashAble = false;
                dashFreq = 0;
                dashSpeed = 0;
                dashDuration = 0;

                monElite = true;       //����Ʈ ���ΰ�?
                monEnforce = false;     //��ȭ�Ǿ��°�?
                monResist = false;      //������ �з����� �ʴ°�?
                monKnockback = false;   //�÷��̾ �˹��ϴ°�?
                monProj = false;        //����ü �߻翩��
                break;

            case EnemyType.Blue3LegRobot:
                monIdx = 5;
                monWidth = 125;     //�� ��Ʈ�ڽ� ����
                monLength = 348;    //�� ��Ʈ�ڽ� ����

                //monImage;
                //monSound;

                hitPointMax = 45;
                hitPoint = 45;
                moveSpeed = 10;
                attackPower = 9;
                dashAble = false;
                dashFreq = 0;
                dashSpeed = 0;
                dashDuration = 0;

                monElite = true;       //����Ʈ ���ΰ�?
                monEnforce = false;     //��ȭ�Ǿ��°�?
                monResist = false;      //������ �з����� �ʴ°�?
                monKnockback = false;   //�÷��̾ �˹��ϴ°�?
                monProj = true;        //����ü �߻翩��
                break;

            case EnemyType.Big4LegRobot:
                monIdx = 6;
                monWidth = 125;     //�� ��Ʈ�ڽ� ����
                monLength = 348;    //�� ��Ʈ�ڽ� ����

                //monImage;
                //monSound;

                hitPointMax = 100;
                hitPoint = 100;
                moveSpeed = 12;
                attackPower = 15;
                dashAble = false;
                dashFreq = 0;
                dashSpeed = 0;
                dashDuration = 0;

                monElite = true;       //����Ʈ ���ΰ�?
                monEnforce = false;     //��ȭ�Ǿ��°�?
                monResist = false;      //������ �з����� �ʴ°�?
                monKnockback = false;   //�÷��̾ �˹��ϴ°�?
                monProj = true;        //����ü �߻翩��
                break;
        }
    }
}
