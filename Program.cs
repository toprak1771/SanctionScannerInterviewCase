using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using HtmlAgilityPack;

namespace SanctionScannerInterviewCase
{
    class Program
    {
        //Global değişkenler metodlarda ve çıktılarda kullanmak için tanımlandı.
        public static string html;
        public static Uri url;
        public static int sum = 0;

        static void Main(string[] args)
        {
            //Metodlardan gelen verileri listelerde saklamak için listeler oluşturuldu.
            List<string> urlList = new List<string>();
            List<string> priceList = new List<string>();
            List<string> titleList = new List<string>();

            //Anasayfa sayfasındaki vitrin ekranındaki url'lerin içine girebilmek için bütün url'ler urlList içine depolandı. 
            for (int i = 1; i < 57; i++)
            {
                GetUrl("https://www.sahibinden.com.tr/", "//*[@id='container']/div[3]/div/div[3]/div[3]/ul/li[" + i +"]/a", "href", urlList);
                            
            }

            //Foreach yardımı ile gelen urlList'den gelen url'ler ilan başlığı ve fiyatı alabilmek için metodun içinbe yazıldı.
            foreach (var item in urlList)
            {
                GetData(" " + item + " ", "//*[@id='classifiedDetail']/div/div[2]/div[2]/h3", priceList);
                GetData("" + item + "", "//*[@id='classifiedDetail']/div/div[1]/h1", titleList);
            }

            //Foreach yardımı ile başlıklar console ekranına yazdırıldı.
            foreach (var title in titleList)
            {
                Console.WriteLine(title);
            }

            //Foreach yardımı ile fiyatlar başlık ekranına yazdırıldı.
            foreach (var price in priceList)
            {

                Console.WriteLine(price);
                sum += Convert.ToInt32(price);
                double average = sum / (priceList.Count());
                Console.WriteLine("All of the average of prices: {0} ", average);
            }




        }


        //Gelen url'lerden fiyat ve başlığı çekmemize yarayan metot
        public static void GetData(String Url, String Xpath, List<string> list)
        {
            //Globalde tanımladığımız url'ye metodumuza parametre olarak yolladığımız url'ye eşitlendi. Hatalar cath metotlarıyla yakalandı.
            try
            {
                url = new Uri(Url);
            }
            catch (UriFormatException)
            {
                Console.WriteLine("Fail url");
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("Fail url");
            }

            //Sunucudan bir istemci oluştururuz.
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;

            //Globalde oluşturduğumuz html'ye, istemci içine yollayadığımız url'nin içindeki string ifadeleri indirip eşitleriz.
            try
            {
                html = client.DownloadString(url);
            }
            catch (WebException)
            {
                Console.WriteLine("Fail url");
            }

            //HtmlAgilityPack kütüphanesi sayesinde oluşturduğumuz dökümana, url içinden indirdiğimiz string ifadeleri html olarak indiririz.
            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
            document.LoadHtml(html);

            //İçinde url'mizdeki html dosyası olan dökümana istediğimiz verinin xpath bilgisini yollayarak içindeki text'i yakalayıp parametreden yolladığımız listenin içine atarız.
            try
            {
                list.Add(document.DocumentNode.SelectSingleNode(Xpath).InnerText);
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("Fail Xpath");
            }

        }

        //Anasayfa sekmesinden gelen vitrin ilanların url'sini çekmemize saülayan metot
        public static void GetUrl(String Url, String Xpath, String getUrl ,List<string> list)
        {
            //Globalde tanımladığımız url'ye metodumuza parametre olarak yolladığımız url'ye eşitlendi. Hatalar cath metotlarıyla yakalandı.
            try
            {
                url = new Uri(Url);
            }
            catch (UriFormatException)
            {
                Console.WriteLine("Fail url");
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("Fail url");
            }

            //Sunucudan bir istemci oluştururuz.
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;

            //Globalde oluşturduğumuz html'ye, istemci içine yollayadığımız url'nin içindeki string ifadeleri indirip eşitleriz.
            try
            {
                html = client.DownloadString(url);
            }
            catch (WebException)
            {
                Console.WriteLine("Fail url");
            }

            //HtmlAgilityPack kütüphanesi sayesinde oluşturduğumuz dökümana, url içinden indirdiğimiz string ifadeleri html olarak indiririz.
            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
            document.LoadHtml(html);

            //İçinde url'mizdeki html dosyası olan dökümana istediğimiz verinin xpath bilgisini yollayarak içindeki text'i yakalayıp parametreden yolladığımız listenin içine atarız.
            try
            {
                list.Add(document.DocumentNode.SelectSingleNode(Xpath).Attributes[getUrl].Value);
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("Fail Xpath");
            }

        }



    }
}
