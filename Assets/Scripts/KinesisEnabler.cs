using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class KinesisEnabler : MonoBehaviour
{
    [Header("Kinesis")]
    [SerializeField] bool aeroEnabled;
    [SerializeField] bool electroEnabled;
    [SerializeField] bool teleEnabled;
    [SerializeField] bool pyroEnabled;
    [SerializeField] bool cryoEnabled;

    public bool RetrieveLoop(int idx)
    {
        switch (idx)
        {
            case 0:
                return aeroEnabled;
            case 1:
                return electroEnabled;
            case 2:
                return teleEnabled;
            case 3:
                return pyroEnabled;
            case 4:
                return cryoEnabled;
            default:
                return false;
        }
    }


    public bool AeroEnabled()
    {
        return aeroEnabled;
    }

    public bool ElectroEnabled()
    {
        return electroEnabled;
    }

    public bool CryoEnabled()
    {
        return cryoEnabled;
    }

    public bool TeleEnabled()
    {
        return teleEnabled;
    }

    public bool PyroEnabled()
    {
        return pyroEnabled;
    }

    /// <summary>
    /// Gets if the Aerokinesis is active or not.
    /// </summary>
    /// <param name="active"></param>
    public void AeroSetActive(bool active)
    {
        aeroEnabled = active;
    }

    /// <summary>
    /// Gets if the Electrokinesis is active or not.
    /// </summary>
    /// <param name="active"></param>
    public void ElectroSetActive(bool active)
    {
        electroEnabled = active;
    }

    /// <summary>
    /// Gets if the Telekinesis is active or not.
    /// </summary>
    /// <param name="active"></param>
    public void TeleSetActive(bool active)
    {
        teleEnabled = active;
    }

    /// <summary>
    /// Gets if the Pyrokinesis is active or not.
    /// </summary>
    /// <param name="active"></param>
    public void PyroSetActive(bool active)
    {
        pyroEnabled = active;
    }

    /// <summary>
    /// Gets if the Cryokinesis is active or not.
    /// </summary>
    /// <param name="active"></param>
    public void CryoSetActive(bool active)
    {
        cryoEnabled = active;
    }
}