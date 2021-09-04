using UnityEngine;
using System.Net;
using System.Linq;

public class IPShow : MonoBehaviour {

    public TMPro.TMP_Text inputField;

    // Update is called once per frame
    void Start() {
        string externalip = new WebClient().DownloadString("http://icanhazip.com");
        inputField.text = "Your Local IP: " + GetLocalIPv4() + " Your Public IP: " + externalip;
    }

    public string GetLocalIPv4()
    {
        return Dns.GetHostEntry(Dns.GetHostName())
            .AddressList.First(
                f => f.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            .ToString();
    }
}
