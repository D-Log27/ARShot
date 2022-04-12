using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net.NetworkInformation;

/// <summary>
/// AP ���� ����
/// </summary>
public class NetworkAPHelper : MonoBehaviour
{
#if UNITY_IPHONE
   
           // On iOS plugins are statically linked into
           // the executable, so we have to use __Internal as the
           // library name.
           [DllImport ("__Internal")]

#elif UNITY_ANDROID

#endif
    /// <summary>
    /// AP ���� ���� �ν��Ͻ�
    /// </summary>
    private static NetworkAPHelper Instance;

    NetworkAPHelper() { }

    AndroidRuntimePermissions.Permission[] permissions;
    const string pluginName = "com.example.wifigetter.WifiHelper";
    string[] permissionList = {
        "android.permission.INTERNET",
        "android.permission.ACCESS_NETWORK_STATE", 
        "android.permission.ACCESS_WIFI_STATE",
        "android.permission.ACCESS_COARSE_LOCATION",
        "android.permission.ACCESS_FINE_LOCATION",
        "android.permission.CHANGE_WIFI_STATE",
        "android.permission.CAMERA"
    };
    /// <summary>
    /// AP ���� ���� �ν��Ͻ� ��ȯ
    /// </summary>
    /// <returns>AP ���� ���� �ν��Ͻ�</returns>
    public static NetworkAPHelper GetInstance()
    {
        if (Instance == null) Instance = new NetworkAPHelper();
        return Instance;
    }

    /// <summary>
    /// AP �̸�
    /// </summary>
    public string ApName { get; set; }

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        //permissions = AndroidRuntimePermissions.RequestPermissions(permissionList);

        this.GetAPInfo();
    }

    void Update()
    {
        
    }

    /// <summary>
    /// AP ������ �����´�
    /// </summary>
    public void GetAPInfo()
    {
        try
        {
            bool isGranted = true;
            permissions = AndroidRuntimePermissions.RequestPermissions(permissionList);
            foreach (AndroidRuntimePermissions.Permission _permission in permissions)
            {
                if (_permission.Equals(AndroidRuntimePermissions.Permission.Granted)) isGranted = true;
                else isGranted = false;
            }

            if (isGranted)
            {
                // solution 1 : wlanapi.dll
                /*
                                using (var activity = new AndroidJavaClass(pluginName).GetStatic<AndroidJavaObject>("currentActivity"))
                                {
                                    using (var wifiManager = activity.Call<AndroidJavaObject>("getSystemService", "wifi"))
                                    {
                                        ApName = wifiManager.Call<AndroidJavaObject>("getConnectionInfo").Call<string>("getMacAddress");
                                    }
                                }
                */


                // Solution 2 : android native
                
                /*
                var helperClass = new AndroidJavaClass(pluginName);
                AndroidJavaObject helperInstance = helperClass.CallStatic<AndroidJavaObject>("getInstance");
                ApName = helperInstance.Call<string>("getApName");
                print($"### new test : {ApName}");
                */
                



                /*
                AndroidJavaObject activity = new AndroidJavaClass(pluginName).GetStatic<AndroidJavaObject>("currentActivity");
                print($"### activity is null ? {activity == null}");

                // under api level 31
                AndroidJavaObject supplicantState = activity.GetStatic<AndroidJavaObject>("SupplicantState");
                AndroidJavaObject wifiManager = activity.Call<AndroidJavaObject>("getSystemService", "wifi");
                AndroidJavaObject wifiInfo = wifiManager.Call<AndroidJavaObject>("getConnectionInfo");
                var supplicateState = wifiInfo.Call<AndroidJavaObject>("getSupplicantState");
                ApName = wifiManager.Call<AndroidJavaObject>("getConnectionInfo").Call<string>("getSSID");
                */
                // equal or over api level 31
                /*
                AndroidJavaObject connectivityManager = activity.Call<AndroidJavaObject>("getSystemService", "connectivity");
                print($"### connectivityManager ? {connectivityManager == null}");
                AndroidJavaObject test = connectivityManager.Call<AndroidJavaObject>("getNetworkCapabilities").Call<AndroidJavaObject>("getTransportInfo");
                print($"### test : {test == null}");
                */

                //print($"### device Namce Check : {SystemInfo.deviceName}");
                String strHostName = string.Empty;
                

                foreach (NetworkInterface adapter in NetworkInterface.GetAllNetworkInterfaces())
                {
                    foreach (var x in adapter.GetIPProperties().UnicastAddresses)
                    {
                        if (x.IPv4Mask.ToString().Equals("255.255.255.0"))
                        {
                            //print($"### IPAddress : {x.Address} / IPv4Mask : {x.IPv4Mask}");
                            ApName = x.Address.ToString();
                        }
                    }
                    foreach(var y in adapter.GetIPProperties().GatewayAddresses)
                    {
                        //print($"### GateWay : {y.Address}");
                    }
                }





            }

        }
        catch (Exception e)
        {
            print($"### GET API Info Exception :{e}");
        }
        
/*
        try
        {
            print("### create WlanClient");
            var wlanClient = gameObject.AddComponent<WlanClient>();
            print("### createed wlan client");
            foreach (WlanClient.WlanInterface wlanInterface in wlanClient.Interfaces)
            {
                print("### wlanInterface check start");
                Wlan.Dot11Ssid ssid = wlanInterface.CurrentConnection.wlanAssociationAttributes.dot11Ssid;
                ApName = System.Text.ASCIIEncoding.ASCII.GetString(ssid.SSID);
                print($"### CHECK : {System.Text.ASCIIEncoding.ASCII.GetString(ssid.SSID)}");
            }

            print($"### ApName : {ApName}");
        } 
        catch(Exception e)
        {
            print($"### GET API Info Exception :{e.StackTrace}");
        }
       */ 
        
    }
}


