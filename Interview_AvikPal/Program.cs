using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using static Interview_AvikPal.CircularLinkedList;

namespace Interview_AvikPal
{
    class Program
    {
        static void Main(string[] args)
        {
            //GIT API connection
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.github.com/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //GET Method
                HttpResponseMessage response = client.GetAsync("repositories/2126244/commits?per_page=100&sha=7a8d6b19767a92b1c4ea45d88d4eedc2b29bf1fa").Result;
                if (response.IsSuccessStatusCode)
                {
                }
                else
                {

                }
            }


            String str = string.Empty;
            Console.WriteLine("GIT API Connection issue. Please Enter a comment as input : ");
            str=Console.ReadLine();
            string[] strarr = str.Split(' ');

            //Circular Linked List 
            CircularLinkedList list = new CircularLinkedList();

            //Circular Linked List  insertion
            for (int i = 0; i < strarr.Length; i++)
            {
                Node temp = new Node(strarr[i]);
                list.sortedInsert(temp);
            }

            //Circular Linked List  insertion
            Console.WriteLine("\n\nPrinting Words present in comment in decending order are : \n");
            list.printList();

            //Calling Bubble sort method
            sortStrings(strarr);
            Console.WriteLine("\n\nPrinting Words present in comment in accesding order are : ");
            foreach (var item in strarr)
            {
                Console.WriteLine(item);
            }

            //CSV Export function call
            string filename=CsvExport(CountWord(strarr));
            Console.WriteLine("\n\nCsv File "+filename+" exported to Folder : ~\\bin\\Debug\\netcoreapp3.1 with count for each words in comment.");
        }



        public static void sortStrings(string[] arr)
        {
            string temp;

            // Sorting strings using bubble sort
            for (int j = 0; j < arr.Length - 1; j++)
            {
                for (int i = j + 1; i < arr.Length; i++)
                {
                    if (arr[j].CompareTo(arr[i]) > 0)
                    {
                        temp = arr[j];
                        arr[j] = arr[i];
                        arr[i] = temp;
                    }
                }
            }
        }


        public static Dictionary<string, int> CountWord(string[] strarr)
        {
            int count = 1;
            Dictionary<string, int> wordCount = new Dictionary<string, int>();
            for (int i = 0, j = i + 1; i < strarr.Length; i++, j++)
            {
                if (j < strarr.Length)
                {
                    if (strarr[i].Equals(strarr[j]))
                    {
                        if (i == strarr.Length - 2)
                        {
                            count++;
                            wordCount.Add(strarr[i], count);
                        }
                        count++;
                        continue;
                    }
                    else
                    {
                        wordCount.Add(strarr[i], count);
                        count = 1;
                    }
                }
                else
                {
                    if (!wordCount.ContainsKey(strarr[i]))
                        wordCount.Add(strarr[i], count);
                }
            }
            Console.WriteLine("\n\nPrinting each Word count : ");
            foreach(var item in wordCount)
            {
                Console.WriteLine(item.Key+" : "+item.Value);
            }
            return (wordCount);
        }

        public static string CsvExport(Dictionary<string, int> wordCount)
        {
            var file = @"WordCountFile_"
                            + DateTime.Now.Year + "_"
                            + DateTime.Now.Month + "_"
                            + DateTime.Now.Day + "_"
                            + DateTime.Now.Hour + "_"
                            + DateTime.Now.Minute + "_"
                            + DateTime.Now.Second + "_"
                            + DateTime.Now.Millisecond
                            + ".csv";
            using (var stream = File.AppendText(file))
            {
                string csvHeader = string.Format("{0},{1}", "Word", "Count");
                stream.WriteLine(csvHeader);

                foreach (var item in wordCount)
                {
                    string csvRow = string.Format("{0},{1}", item.Key, item.Value);
                    stream.WriteLine(csvRow);
                }

            }
            return (file);
        }
    }
}
