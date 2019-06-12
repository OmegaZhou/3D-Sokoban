using System;
using System.Text;
using System.Threading;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;


public class Recognize : MonoBehaviour
{

    public static string init_audio(string my_appid, string session_begin_params, byte[] AudioData)
    {
        //Debug.Log(Application.internetReachability);
        int res = MSCDLL.MSPLogin(null, null, my_appid);//�û��������룬��½��Ϣ��ǰ������Ϊ��

        if (res != (int)Errors.MSP_SUCCESS)
        {//˵����½ʧ��
            Debug.Log("��½ʧ�ܣ�");
            Debug.Log(my_appid);
            Debug.Log("res:" + res);
            return null;
        }
        //Debug.Log("��½�ɹ���");
        return audio_iat(AudioData, session_begin_params);//��ʼ������Ƶ
    }

    private static string audio_iat(byte[] AudioData, string session_begin_params)
    {
        string hints = "hiahiahia";
        IntPtr session_id;
        StringBuilder result = new StringBuilder();//�洢����ʶ��Ľ��
        var aud_stat = AudioStatus.MSP_AUDIO_SAMPLE_CONTINUE;//��Ƶ״̬
        var ep_stat = EpStatus.MSP_EP_LOOKING_FOR_SPEECH;//�˵�״̬
        var rec_stat = RecogStatus.MSP_REC_STATUS_SUCCESS;//ʶ��״̬
        int errcode = (int)Errors.MSP_SUCCESS;
        byte[] audio_content = AudioData;  //�����洢��Ƶ�ļ��Ķ���������
        int totalLength = 0;//������¼�ܵ�ʶ���Ľ���ĳ��ȣ��ж��Ƿ񳬹��������ֵ
                            /* try
                             {
                                 audio_content = File.ReadAllBytes(audio_path);
                             }
                             catch (Exception e)
                             {
                                  Debug.Log(e);
                                 audio_content = null;
                             }*/
        if (audio_content == null)
        {
            Debug.Log("û�ж�ȡ���κ�����");
            MSCDLL.MSPLogout();//�˳���¼
            return null;
        }
        //Debug.Log("��ʼ����������д.......");

        /*
        * QISRSessionBegin������
        * ���ܣ���ʼһ������ʶ��
        * ����һ������ؼ���ʶ��||�﷨ʶ��||��������ʶ��null��
        * ����2������ʶ��Ĳ��������ԡ������������򡣡�����
        * ����3����������ʶ��Ľ�����ɹ�||�������
        * ����ֵintPtr����,������õ��������ֵ
        */
        session_id = MSCDLL.QISRSessionBegin(null, session_begin_params, ref errcode);
        if (errcode != (int)Errors.MSP_SUCCESS)
        {
            Debug.Log("��ʼһ������ʶ��ʧ�ܣ�");
            MSCDLL.MSPLogout();
            MSCDLL.QISRSessionEnd(session_id, hints);
            return null;
        }
        /*
          QISRAudioWrite������
          ���ܣ�д�뱾��ʶ�����Ƶ
          ����1��֮ǰ�Ѿ��õ���sessionID
          ����2����Ƶ���ݻ�������ʼ��ַ
          ����3����Ƶ���ݳ���,��λ�ֽڡ�
           ����4��������֪MSC��Ƶ�����Ƿ����     MSP_AUDIO_SAMPLE_FIRST = 1	��һ����Ƶ
                                                   MSP_AUDIO_SAMPLE_CONTINUE = 2	���к����Ƶ
                                                    MSP_AUDIO_SAMPLE_LAST = 4	���һ����Ƶ
          ����5���˵��⣨End-point detected����������״̬
                                                 MSP_EP_LOOKING_FOR_SPEECH = 0	��û�м�⵽��Ƶ��ǰ�˵㡣
                                                  MSP_EP_IN_SPEECH = 1	�Ѿ���⵽����Ƶǰ�˵㣬���ڽ�����������Ƶ����
                                                  MSP_EP_AFTER_SPEECH = 3	��⵽��Ƶ�ĺ�˵㣬��̵���Ƶ�ᱻMSC���ԡ�
                                                   MSP_EP_TIMEOUT = 4	��ʱ��
                                                  MSP_EP_ERROR = 5	���ִ���
                                                  MSP_EP_MAX_SPEECH = 6	��Ƶ����
          ����6��ʶ�������ص�״̬�������û���ʱ��ʼ\ֹͣ��ȡʶ����
                                        MSP_REC_STATUS_SUCCESS = 0	ʶ��ɹ�����ʱ�û����Ե���QISRGetResult����ȡ�����֣������
                                         MSP_REC_STATUS_NO_MATCH = 1	ʶ�������û��ʶ������
                                       MSP_REC_STATUS_INCOMPLETE = 2	����ʶ���С�
                                       MSP_REC_STATUS_COMPLETE = 5	ʶ�������
          ����ֵ���������óɹ�����ֵΪMSP_SUCCESS�����򷵻ش�����롣
            ���ӿ��費�ϵ��ã�ֱ����Ƶȫ��д��Ϊֹ���ϴ���Ƶʱ�������audioStatus��ֵ��������˵:
            ��д���׿���Ƶʱ,��audioStatus��ΪMSP_AUDIO_SAMPLE_FIRST
            ��д�����һ����Ƶʱ,��audioStatus��ΪMSP_AUDIO_SAMPLE_LAST
            ���������,��audioStatus��ΪMSP_AUDIO_SAMPLE_CONTINUE
            ͬʱ���趨ʱ�������������epStatus��rsltStatus��������˵:
            ��epStatus��ʾ�Ѽ�⵽��˵�ʱ��MSC�Ѳ��ٽ�����Ƶ��Ӧ��ʱֹͣ��Ƶд��
            ��rsltStatus��ʾ��ʶ��������ʱ�����ɴ�MSC�����л�ȡ���*/
        int res = MSCDLL.QISRAudioWrite(session_id, audio_content, (uint)audio_content.Length, aud_stat, ref ep_stat, ref rec_stat);
        if (res != (int)Errors.MSP_SUCCESS)
        {
            Debug.Log("д��ʶ�����Ƶʧ�ܣ�" + res);
            MSCDLL.MSPLogout();
            MSCDLL.QISRSessionEnd(session_id, hints);
            return null;
        }
        res = MSCDLL.QISRAudioWrite(session_id, null, 0, AudioStatus.MSP_AUDIO_SAMPLE_LAST, ref ep_stat, ref rec_stat);
        if (res != (int)Errors.MSP_SUCCESS)
        {
            Debug.Log("д����Ƶʧ�ܣ�" + res);
            MSCDLL.MSPLogout();
            MSCDLL.QISRSessionEnd(session_id, hints);
            return null;
        }
        while (RecogStatus.MSP_REC_STATUS_COMPLETE != rec_stat)
        {//���û����ɾ�һֱ������ȡ���
         /*
          QISRGetResult������
          ���ܣ���ȡʶ����
          ����1��session��֮ǰ�ѻ��
          ����2��ʶ������״̬
          ����3��waitTime[in]	�˲�����������
          ����4���������||�ɹ�
          ����ֵ������ִ�гɹ�����ʶ����ʱ�����ؽ���ַ���ָ�룻�������(ʧ�ܻ��޽��)����NULL��
          */
            IntPtr now_result = MSCDLL.QISRGetResult(session_id, ref rec_stat, 0, ref errcode);
            if (errcode != (int)Errors.MSP_SUCCESS)
            {
                Debug.Log("��ȡ���ʧ�ܣ�" + errcode);
                MSCDLL.MSPLogout();
                MSCDLL.QISRSessionEnd(session_id, hints);
                return null;
            }
            if (now_result != null)
            {
                int length = now_result.ToString().Length;
                totalLength += length;
                if (totalLength > 4096)
                {
                    Debug.Log("����ռ䲻��" + totalLength);
                    MSCDLL.MSPLogout();
                    MSCDLL.QISRSessionEnd(session_id, hints);
                    return null;
                }
                result.Append(Marshal.PtrToStringAnsi(now_result));
            }
            Thread.Sleep(150);//��ֹƵ��ռ��cpu
        }
        //Debug.Log("������д����");
        //Debug.Log("�����\n");
        Debug.Log(result.ToString());

        res = MSCDLL.QISRSessionEnd(session_id, hints);
        if (res != (int)Errors.MSP_SUCCESS)
        {
            Debug.Log("�Ự����ʧ�ܣ�");
            Debug.Log("������:" + res);

            MSCDLL.MSPLogout();
            return null;
        }
        Debug.Log("�ɹ������Ự��");

        res = MSCDLL.MSPLogout();
        if (res != (int)Errors.MSP_SUCCESS)
        {//˵����½ʧ��
            Debug.Log("�˳���¼ʧ�ܣ�");
            Debug.Log("������:" + res);
            return null;
        }
        //Debug.Log("�˳���¼�ɹ���");
        return result.ToString();

    }
}
