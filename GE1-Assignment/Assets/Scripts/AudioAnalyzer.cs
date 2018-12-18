using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System;

public class AudioAnalyzer : MonoBehaviour
{
    public GameObject audioSource;
    AudioSource a;
    public GameObject trackNameText;
    public GameObject trackDuration;

    public static int frameSize = 512;
    public static float[] spectrum;
    public static float[] bands;

    public float binWidth;
    public float sampleRate;

    /*
     * 20-60 - Subbase
     * 60-250 - Bass
     * 250-500 - Low midrange
     * 500 - 2Khz - Midrange
     * 2Khz - 4Khz - Upper midrange
     * 4Khz - 6Khz - Presence
     * 6Khz - 20Khz - Brilliance
     */

    // Use this for initialization
    void Start()
    {
        sampleRate = AudioSettings.outputSampleRate;
        binWidth = AudioSettings.outputSampleRate / 2 / frameSize;
        a = audioSource.GetComponent<AudioSource>();
        spectrum = new float[frameSize];
        bands = new float[(int)Mathf.Log(frameSize, 2)];
        var trackName = a.clip.ToString();
        trackName = trackName.Replace("(UnityEngine.AudioClip)", "");
        trackNameText.GetComponent<Text>().text = "Now playing: " + trackName;
    }

    void GetFrequencyBands()
    {
        for (int i = 0; i < bands.Length; i++)
        {
            int start = (int)Mathf.Pow(2, i) - 1;
            int width = (int)Mathf.Pow(2, i);
            int end = start + width;
            float average = 0;
            for (int j = start; j < end; j++)
            {
                average += spectrum[j] * (j + 1);
            }
            average /= (float)width;
            bands[i] = average;
        }

    }

    // Update is called once per frame
    void Update()
    {
        a.GetSpectrumData(spectrum, 0, FFTWindow.Blackman);
        GetFrequencyBands();
        trackDuration.GetComponent<Text>().text = Math.Round(a.time, 2).ToString();
    }
}