using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Sound : MonoBehaviour
{
    //中文：zh_cn 英文：en_us
    private string audio_path = null;
    private const string app_id = "appid = 5cffa937";
    private const string session_begin_params = "sub = iat, domain = iat, language = zh_cn, accent = mandarin, sample_rate = 16000, result_type = plain, result_encoding = utf-8";

    public AudioSource audio;//存储录制的音频
    private int frequency = 16000;//采样率
    private bool[] dir = new bool[6];
    private bool flag = false;
    private byte[] AudioData;
    void Start()
    {
        audio = gameObject.GetComponent<AudioSource>();
        audio.clip = Microphone.Start(null, true, 1, frequency);
    }
    void LateUpdate()
    {

        if (Microphone.GetPosition(null) <= 0)
        {
            return;
        }
        if (flag)
        {
            return;
        }
        AudioData = Float2Byte();
        System.Threading.Thread t = new System.Threading.Thread(Process);
        t.Start();
    }

    void Process()
    {
        flag = true;
        string re = Recognize.init_audio(app_id, session_begin_params, AudioData);

        if (re == null)
        {
            flag = false;
            return;
        }
        Debug.Log(re);
        if (re.Contains("前"))
        {
            dir[0] = true;
        }
        if (re.Contains("后"))
        {
            dir[1] = true;
        }
        if (re.Contains("左"))
        {
            dir[2] = true;
        }
        if (re.Contains("右"))
        {
            dir[3] = true;
        }
        if (re.Contains("上"))
        {
            dir[4] = true;
        }
        if (re.Contains("下"))
        {
            dir[5] = true;
        }
        flag = false;
    }

    public int GetDirection()
    {
        int re = -1;
        for (int i = 0; i < 6; ++i)
        {
            if (dir[i] == true)
            {
                re = i;
                dir[i] = false;
            }
        }
        return re;
    }



    //将clip中的float音频数据存储到byte数组中
    public byte[] Float2Byte()
    {

        if (audio.clip == null)
        {
            Debug.Log("clip is null!");
            return null;
        }
        float[] samples = new float[audio.clip.samples];

        audio.clip.GetData(samples, 0);

        short[] intData = new short[samples.Length];
        //converting in 2 float[] steps to Int16[], //then Int16[] to Byte[]

        byte[] bytesData = new byte[samples.Length * 2];
        //bytesData array is twice the size of
        //dataSource array because a float converted in Int16 is 2 bytes.

        int rescaleFactor = short.MaxValue; //to convert float to Int16

        for (int i = 0; i < samples.Length; i++)
        {
            intData[i] = (short)(samples[i] * rescaleFactor);
            byte[] byteArr = new byte[2];
            byteArr = BitConverter.GetBytes(intData[i]);
            byteArr.CopyTo(bytesData, i * 2);
        }

        return bytesData;

    }
}
