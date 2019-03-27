// 1710601040, fhs41350
// Julia Wiesbauer
// =============================================================================
using System;
using System.IO;
using System.Collections.Generic;

class Program
{
    public static void InvertedIndex()
    {
        // Build the index
        // Optional: Dateien direkt im Ordner mit Name, ohne Pfad
        //String[] documents = { "1.txt", "2.txt", "3.txt" };
         String[] documents = Directory.GetFiles(@"D:\Users\Jules-PC\Desktop\SemanticWeb\Abgabe1\Text", "*.txt"); 
        Dictionary<String, List<String>> index = new Dictionary<String, List<String>>();

        foreach (String document in documents)
        {
            String documentStr = File.ReadAllText(document);
            // Entfernt Satzzeichen, Bindestriche & Beistriche
            string trimmedDocumentStr = documentStr.Replace(".", string.Empty).Replace(",", string.Empty).Replace("!", string.Empty).Replace("?", string.Empty).Replace("-", string.Empty).ToLower();
            String[] words = trimmedDocumentStr.Split();

            foreach (String word in words)
            {
                if (!index.ContainsKey(word))
                    index[word] = new List<String>();

                index[word].Add(document);
            }
        }

        // Query the index
        String input = "Start";

        while (input !="")
        {
            Console.WriteLine();
            Console.WriteLine("Enter 1 or 2 keywords to search for, otherwise enter nothing to quit:");
            Console.WriteLine();
            input = Console.ReadLine();
            if (input == "") continue;
            // Entfernt Satzzeichen, Bindestriche & Beistriche im Input
            String keyword = input.Replace(".", string.Empty).Replace(",", string.Empty).Replace("!", string.Empty).Replace("?", string.Empty).Replace("-", string.Empty).ToLower();

            String[] keywords = keyword.Split();

            // 1 Keyword
            if (keywords.Length == 1)
            {
                if (index.ContainsKey(keywords[0]))
                {
                    foreach (String document in index[keywords[0]])
                    {
                        Console.WriteLine("Found in: " + document);
                    }
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Not found!");
                    break;
                }
            }
            // 2 Keywords
            else if(keywords.Length == 2)
            {
                if (index.ContainsKey(keywords[0]) && index.ContainsKey(keywords[1]))
                {
                    List<String> result1 = new List<String>();
                    foreach (String document in index[keywords[0]])
                    {
                        result1.Add (document);
                        
                    }
                    foreach (String document in index[keywords[1]])
                    {
                        if (result1.Contains(document))
                            Console.WriteLine("Found in: "+ document);
                    }
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Not found!");
                    break;
                }
            }
            // Neither 1 nor 2
            else
            {
                Console.WriteLine();
                Console.WriteLine("Error: Only 1 or 2 keywords allowed!");
                continue;
            }
        }
    }

    static void Main()
    {
        Program.InvertedIndex();
    }
}
