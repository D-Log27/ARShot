using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NativeWifi;
/// <summary>
/// AP 정보 헬퍼
/// </summary>
public class NetworkAPHelper : MonoBehaviour
{
    /// <summary>
    /// AP 정보 헬퍼 인스턴스
    /// </summary>
    private static NetworkAPHelper Instance;

    NetworkAPHelper() { }

    /// <summary>
    /// AP 정보 헬퍼 인스턴스 반환
    /// </summary>
    /// <returns>AP 정보 헬퍼 인스턴스</returns>
    public static NetworkAPHelper GetInstance()
    {
        if (Instance == null) Instance = new NetworkAPHelper();
        return Instance;
    }

    /// <summary>
    /// AP 이름
    /// </summary>
    public string ApName { get; set; }

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        this.GetAPInfo();
    }

    void Update()
    {
        
    }

    /// <summary>
    /// AP 정보를 가져온다
    /// </summary>
    public void GetAPInfo()
    {
        var wlanClient = new WlanClient();

        foreach (WlanClient.WlanInterface wlanInterface in wlanClient.Interfaces)
        {
            Wlan.Dot11Ssid ssid = wlanInterface.CurrentConnection.wlanAssociationAttributes.dot11Ssid;
            ApName = System.Text.ASCIIEncoding.ASCII.GetString(ssid.SSID);
            print($"### CHECK : {System.Text.ASCIIEncoding.ASCII.GetString(ssid.SSID)}");
            //Wlan.WlanBssEntry[] wlanBssEntries = wlanInterface.GetNetworkBssList();
            //foreach (Wlan.WlanBssEntry wlanBssEntry in wlanBssEntries)
            //{
            //    int rss = wlanBssEntry.rssi;
            //    ApName = System.Text.ASCIIEncoding.ASCII.GetString(wlanBssEntry.dot11Ssid.SSID);
            //    byte[] macAddr = wlanBssEntry.dot11Bssid;
            //    var macAddrLen = (uint)macAddr.Length;
            //    var str = new string[(int)macAddrLen];
            //    var strOrigin = new string[(int)macAddrLen];
            //    for (int i = 0; i < macAddrLen; i++)
            //    {
            //        if (i == macAddrLen - 1)
            //        {
            //            str[i] = macAddr[i].ToString("X2");
            //            strOrigin[i] = macAddr[i].ToString();
            //        }
            //        else
            //        {
            //            str[i] = macAddr[i].ToString("X2") + ":";
            //            strOrigin[i] = macAddr[i].ToString() + ":";
            //        }
            //    }
            //    string mac = string.Join("", str);
                
                //Debug.Log($"1. Mad Address : {mac}");
                //Debug.Log($"2. SSID : {ApName}");
                //Debug.Log($"3. Signal : {wlanBssEntry.linkQuality}");
                //Debug.Log($"4. BSS Type : {wlanBssEntry.dot11BssType}");
                //Debug.Log($"5. wlanBssEntry : {wlanBssEntry.phyId}");
                //Debug.Log($"7. RSSID : {rss}");
                //Debug.Log($"======================================");
                
            }
        }
    }

}
