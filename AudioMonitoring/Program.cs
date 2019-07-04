using System;
using System.Threading;
using System.IO;
using System.Media;

namespace AudioMonitoring
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.Error.WriteLine("Usage: AudioMonitoring.exe <input_file_name>");
                return -1;
            }

            var fileName = args[0];

            return new Program().run(args[0]);
        }

        private SoundPlayer player = new SoundPlayer("standalone.wav");

        private int run(string fileName)
        {
            Console.WriteLine("Starting listening to changes in file " + fileName);
            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    while (true)
                    {
                        while (!sr.EndOfStream)
                            ProcessLinr(sr.ReadLine());
                        while (sr.EndOfStream)
                            Thread.Sleep(100);
                        ProcessLinr(sr.ReadLine());
                    }
                }
            }
        }

        private void ProcessLinr(string line)
        {
            Console.WriteLine(line);
            if (line.Contains("standalone"))
            {
                player.Play();
            }
        }
    }
}