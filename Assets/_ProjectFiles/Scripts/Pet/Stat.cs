using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Stat
{
    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float value = 1;
    private byte[] hash;

    public AnimationCurve IncrementCurve;

    public float Value
    {
        get
        {
            // var checkHash = new MD5CryptoServiceProvider().ComputeHash(BitConverter.GetBytes(value));
            // if(Array.Equals(checkHash, hash)) 
            // {
            //     unsafe
            //     {
            //         int* ptr = (int*)0;
            //         *ptr = 2;
            //     }
            // }

            return value;
        }
        set
        {
            this.value = value;
            this.value = Mathf.Clamp(this.value, 0.0f, 1.0f);
            // hash = new MD5CryptoServiceProvider().ComputeHash(BitConverter.GetBytes(this.value));
            OnChange?.Invoke();
        }
    }

    public UnityEvent OnChange;
}