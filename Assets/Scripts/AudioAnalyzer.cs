using System.Collections;
using UnityEngine;

public class AudioAnalyzer : MonoBehaviour
{
    public static AudioAnalyzer Instance { get; private set; }

    [SerializeField] float smoothSpeed = 10f;
    [SerializeField] float beatThreshold = 0.15f;
    [SerializeField] float beatCooldown = 0.2f;

    AudioSource audioSource;
    float[] spectrumData = new float[1024];
    float[] samples = new float[1024];

    public float Bass { get; private set; }
    public float Mid  { get; private set; }
    public float High { get; private set; }
    public float RMS  { get; private set; }
    public bool OnBeat { get; private set; }

    float lastBeatTime;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(InitMicrophone());
    }

    IEnumerator InitMicrophone()
    {
        if (Microphone.devices.Length == 0)
        {
            Debug.LogError("[AudioAnalyzer] No microphone found.");
            yield break;
        }
        string mic = Microphone.devices[0];
        Debug.Log($"[AudioAnalyzer] Using mic: {mic}");
        audioSource.clip = Microphone.Start(mic, true, 10, AudioSettings.outputSampleRate);
        audioSource.loop = true;
        yield return new WaitUntil(() => Microphone.GetPosition(mic) > 0);
        audioSource.Play();
    }

    void Update()
    {
        if (!audioSource.isPlaying) return;

        audioSource.GetSpectrumData(spectrumData, 0, FFTWindow.BlackmanHarris);
        audioSource.GetOutputData(samples, 0);

        float targetBass = GetRangeEnergy(20f,   200f);
        float targetMid  = GetRangeEnergy(200f,  2000f);
        float targetHigh = GetRangeEnergy(2000f, 20000f);

        float dt = Time.deltaTime * smoothSpeed;
        Bass = Mathf.Lerp(Bass, targetBass, dt);
        Mid  = Mathf.Lerp(Mid,  targetMid,  dt);
        High = Mathf.Lerp(High, targetHigh, dt);

        float sum = 0;
        foreach (var s in samples) sum += s * s;
        RMS = Mathf.Lerp(RMS, Mathf.Sqrt(sum / samples.Length), dt);

        OnBeat = false;
        if (Bass > beatThreshold && Time.time - lastBeatTime > beatCooldown)
        {
            OnBeat = true;
            lastBeatTime = Time.time;
        }
    }

    float GetRangeEnergy(float freqMin, float freqMax)
    {
        int nyquist = AudioSettings.outputSampleRate / 2;
        int indexMin = Mathf.Clamp(Mathf.FloorToInt(freqMin / nyquist * spectrumData.Length), 0, spectrumData.Length - 1);
        int indexMax = Mathf.Clamp(Mathf.FloorToInt(freqMax / nyquist * spectrumData.Length), 0, spectrumData.Length - 1);
        if (indexMax <= indexMin) return 0f;
        float sum = 0;
        for (int i = indexMin; i <= indexMax; i++) sum += spectrumData[i];
        return sum / (indexMax - indexMin + 1);
    }
}
