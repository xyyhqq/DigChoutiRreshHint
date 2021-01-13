using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace DigChoutiRreshHint
{
    public class CommonHelper
    {
        public static async System.Threading.Tasks.Task HttpClientHelper()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.105 Safari/537.36");

            var response = await httpClient.PostAsync("https://api.chouti.com/api/refreshHintsList.json",null);
            string realMessage = await response.Content.ReadAsStringAsync();
            ChoutTiMessageBase choutTiMessageBase = JsonConvert.DeserializeObject<ChoutTiMessageBase>(realMessage);
            StringBuilder stringBuilder = new StringBuilder();
            StringBuilder stringBuilderNoidNoTime = new StringBuilder();
            choutTiMessageBase.resp.ToList().ForEach(item =>
            {
                string str = $"内容:{item.content} 创建时间:{GetTime(item.createTime, true)}\r\n";
                string strNoidNotime = $"{item.content}\r\n";
                stringBuilder.Append(str);
                stringBuilderNoidNoTime.Append(strNoidNotime);
                Console.WriteLine(str);
            });

            if (string.IsNullOrEmpty(realMessage))
            {
                return;
            }
            byte[] buffer = Encoding.UTF8.GetBytes(stringBuilder.ToString());
            var path = $"{AppDomain.CurrentDomain.BaseDirectory}抽屉下拉提示RreshHint.txt";
            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                fs.Write(buffer);
            }

            byte[] bufferPure = Encoding.UTF8.GetBytes(stringBuilderNoidNoTime.ToString());
            var path2 = $"{AppDomain.CurrentDomain.BaseDirectory}抽屉下拉提示无id无时间.txt";
            using (FileStream fs = new FileStream(path2, FileMode.Create, FileAccess.Write))
            {
                fs.Write(bufferPure);
            }
            Console.WriteLine($"生成的数据已保存为文件!!!，请查看路径{path}");
        }

        private static DateTime GetTime(long TimeStamp, bool AccurateToMilliseconds = false)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            DateTime dateTime = AccurateToMilliseconds == true ? startTime.AddTicks(TimeStamp * 10000) : startTime.AddTicks(TimeStamp * 10000000);
            return dateTime;
        }
    }
}