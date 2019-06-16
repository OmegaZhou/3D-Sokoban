using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Sound : MonoBehaviour
{
    public int limit = 3000;

    //中文：zh_cn 英文：en_us
    private string audio_path = null;
    private const string app_id = "appid = 5cffa937";
    private const string session_begin_params = "sub = iat, domain = iat, language = en_us, accent = mandarin, sample_rate = 16000, result_type = plain, result_encoding = utf-8";

    private AudioSource audio;//存储录制的音频
    private int frequency = 16000;//采样率
    private bool[] dir = new bool[6];
    private bool flag = false;
    private byte[] AudioData;
    void Start()
    {
        audio = gameObject.GetComponent<AudioSource>();
        audio.clip = Microphone.Start(null, true, 1, frequency);
        //On();
    }
    public void Rec()
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
        int vol = GetVolume(AudioData);
        print("vol is " + vol);
        if (vol < limit)
        {
            return;
        }
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
        if (re.Contains("forward") || re.Contains("Forward") || re.Contains("Front") || re.Contains("front"))
        {
            dir[0] = true;
        }
        if (re.Contains("back") || re.Contains("Back"))
        {
            dir[1] = true;
        }
        if (re.Contains("left") || re.Contains("Left"))
        {
            dir[2] = true;
        }
        if (re.Contains("right") || re.Contains("Right"))
        {
            dir[3] = true;
        }
        if (re.Contains("up") || re.Contains("Up"))
        {
            dir[4] = true;
        }
        if (re.Contains("down") || re.Contains("Down") || re.Contains("Done") || re.Contains("done"))
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
        var max = 0f;
        for (int i = 0; i < samples.Length; i++)
        {
            intData[i] = (short)(samples[i] * rescaleFactor);

            byte[] byteArr = new byte[2];
            if (max < Math.Abs(intData[i]))
            {
                max = Math.Abs(intData[i]);
            }
            byteArr = BitConverter.GetBytes(intData[i]);
            byteArr.CopyTo(bytesData, i * 2);

        }
        //print(max);
        return bytesData;

    }

    int GetVolume(byte[] source)
    {
        int vol = 0;
        for (int i = 0; i < source.Length; i += 2)
        {
            int temp = 0;
            if (source[i + 1]>0x80)
            {
                temp = (source[i + 1] ^ 0xff);
                temp = (temp << 8) + (source[i] ^ 0xff)+1;
            }
            else
            {
                temp = source[i + 1];
                temp = (temp << 8) + source[i];
                
            }
            
            //temp = source[i];
            if (vol < temp)
            {
                vol = temp;
            }
        }
        
        return vol;
    }
    public void Off()
    {
        while (flag) ;
        CancelInvoke("Rec");
    }
    public void On()
    {
        InvokeRepeating("Rec", 0, 1);
    }
}
