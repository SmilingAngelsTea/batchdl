using System;
using System.IO;
using System.Net;

namespace batchdl {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("batchdl\n\n" +
                "Treats each line from a text file as a link and downloads them in order.\n" +
                "USAGE: batchdl (text file location) [folder to output to]\n" +
                "Do note that links will not be recognized without the 'http://' or 'https://'\n");
            if (args.Length < 1) {
                Console.WriteLine("No text file specified. Exiting now.");
                Environment.Exit(0);
            }
            string list = args[0];
            string folder;
            if (args.Length < 2) {
                string listname = Path.GetFileNameWithoutExtension(list);
                Console.WriteLine("No folder name specified. Using the text file name (" + listname + ") as the folder name.\n" +
                "Do note that the folder will be created next to the text file.");
                folder = listname;
            } else {
                folder = args[1];
            }
            string[] urls = null;
            try {
                Directory.CreateDirectory(folder);
                urls = File.ReadAllLines(list);

                for(int i = 0; i < urls.Length - 1; i++) {
                    var uri = new Uri(urls[i]);
                    var extension = Path.GetExtension(uri.AbsolutePath);
                    Console.WriteLine("Downloading: " + urls[i] + " as " + i + extension);
                    WebClient webClient = new WebClient();
                    Console.WriteLine(Path.GetDirectoryName(Path.GetFullPath(list)) + "\\" + folder + "\\" + i + extension);
                    webClient.DownloadFile(urls[i], Path.GetDirectoryName(Path.GetFullPath(list)) + "\\" + folder + "\\" + i + extension);
                }

            } catch (Exception e){
                Console.WriteLine("\nEXCEPTION:");
                Console.WriteLine(e.ToString());
                Environment.Exit(0);
            }
        }
    }
}
