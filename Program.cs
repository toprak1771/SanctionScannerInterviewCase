using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using HtmlAgilityPack;

namespace SanctionScannerInterviewCase
{
    class Program
    {
        //Global variables are defined for use in methods and outputs.
        public static string html;
        public static Uri url;
        public static int sum = 0;

        static void Main(string[] args)
        {
            //Lists were created to store data from methods in lists.
            List<string> urlList = new List<string>();
            List<string> priceList = new List<string>();
            List<string> titleList = new List<string>();

            //All urls are stored in urlList in order to be able to enter the urls on the showcase screen on the home page.
            //The reason why the variable goes from 1 to 57 in the for loop is because the li parameter of the showcase advertisements starts from 1 and ends at 56.
            for (int i = 1; i < 57; i++)
            {
                GetUrl("https://www.sahibinden.com.tr/", "//*[@id='container']/div[3]/div/div[3]/div[3]/ul/li[" + i +"]/a", "href", urlList);
                            
            }

            //The urls coming from the urlList with the help of Foreach are written in the method to get the ad title and price.
            foreach (var item in urlList)
            {
                GetData(" " + item + " ", "//*[@id='classifiedDetail']/div/div[2]/div[2]/h3", priceList);
                GetData("" + item + "", "//*[@id='classifiedDetail']/div/div[1]/h1", titleList);
            }

            //Titles are writed to the console screen with the help of foreach.
            foreach (var title in titleList)
            {
                Console.WriteLine(title);
            }

            //Prices are writed to the console screen with the help of foreach.
            foreach (var price in priceList)
            {

                Console.WriteLine(price);
                sum += Convert.ToInt32(price);
                double average = sum / (priceList.Count());
                Console.WriteLine("Average of all prices:" + average);
            }

            //Writing titles and prices to text file
            try
            {
                //Pass the filepath and filename to the StreamWriter Constructor
                StreamWriter streamWriter = new StreamWriter("C:\\Test.txt");

                //Write title to file 
                foreach (var titleTxt in titleList)
                {
                    streamWriter.WriteLine(titleTxt);
                }

                //Write price to file
                foreach (var priceTxt in priceList)
                {
                    streamWriter.WriteLine(priceTxt);
                }
                streamWriter.Close();
            }
            //catch method to catch the error made
            catch (Exception e)
            {
                Console.WriteLine("Exception:" + e.Message);
            }
            finally 
            {
                Console.WriteLine("Executing finally block.");
            }


        }


        //A method for pulling price and title from incoming urls
        public static void GetData(String Url, String Xpath, List<string> list)
        {
            //It is equalized to the url we defined in the global and the url we sent as a parameter to our method. Errors are caught with the cath methods.
            try
            {
                url = new Uri(Url);
            }
            catch (UriFormatException e)
            {
                Console.WriteLine("Exception:" + e.Message);
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("Exception:" + e.Message);
            }

            //Create a client from the server.
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;

            //Download and synchronize the string expressions in the html which is created globally, in the url we sent to the client.
            try
            {
                html = client.DownloadString(url);
            }
            catch (WebException e)
            {
                Console.WriteLine("Exception:" + e.Message);
            }

            //Thanks to the HtmlAgilityPack library, we download the string expressions downloaded from the url as html to the document that is created.
            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
            document.LoadHtml(html);

            //Send the xpath information of the data, we want to the document with the html file in our url, capture the text in it and throw it into the list we sent from the parameter.
            try
            {
                list.Add(document.DocumentNode.SelectSingleNode(Xpath).InnerText);
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("Exception:" + e.Message);
            }

        }

        //A method that allows us to retrieve the url of the showcase ads coming from the Home tab.
        public static void GetUrl(String Url, String Xpath, String getUrl ,List<string> list)
        {
            //It is equalized to the url we defined in the global and the url we sent as a parameter to our method. Errors are caught with the cath methods.
            try
            {
                url = new Uri(Url);
            }
            catch (UriFormatException e)
            {
                Console.WriteLine("Exception:" + e.Message);
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("Exception:" + e.Message);
            }

            //Create a client from the server.
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;

            //Download and synchronize the string expressions in the html which is created globally, in the url we sent to the client.
            try
            {
                html = client.DownloadString(url);
            }
            catch (WebException e)
            {
                Console.WriteLine("Exception:" + e.Message);
            }

            //Thanks to the HtmlAgilityPack library, we download the string expressions downloaded from the url as html to the document that is created.
            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
            document.LoadHtml(html);

            //Send the xpath information of the data, we want to the document with the html file in our url, capture the text in it and throw it into the list we sent from the parameter.
            try
            {
                list.Add(document.DocumentNode.SelectSingleNode(Xpath).Attributes[getUrl].Value);
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("Exception:" + e.Message);
            }

        }

    }
}
