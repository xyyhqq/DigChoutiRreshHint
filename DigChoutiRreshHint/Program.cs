using System;
using System.Net.NetworkInformation;
using System.Threading;

namespace DigChoutiRreshHint
{
    internal class Program
    {
        private static async System.Threading.Tasks.Task Main(string[] args)
        {
            if (new Ping().Send("api.chouti.com").Status != IPStatus.Success)
            {
                Console.WriteLine("无法连接 api.chouti.com，请检查网络稍后重试");
                Thread.Sleep(3000);
                return;
            }
            await CommonHelper.HttpClientHelper();
        }
    }
}