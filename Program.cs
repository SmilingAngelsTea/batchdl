using System;
using System.IO;
using System.Net;

namespace batchdl {
    class Program {
        static void Main(string[] args) {
            if (args.Length < 1) {
                Console.WriteLine("No text file specified. Exiting now.");
                Environment.Exit(0);
            }
            string list = args[0];
            string folder = "";
            if (args.Length < 2) {
                string listname = Path.GetFileNameWithoutExtension(list);
                Console.WriteLine("No folder name specified. Using the text file name (" + listname + ") as the folder name.");
                folder = listname;
                Environment.Exit(0);
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
                    webClient.DownloadFile(urls[i], folder + "//" + i + extension);
                }

            } catch {
                Console.WriteLine("Something went wrong.");
                Environment.Exit(0);
            }
        }
    }
}
