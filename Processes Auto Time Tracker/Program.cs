using System.Diagnostics;

public class ProcessesAutoTimeTracker {
    static Process process;
    static TimeSpan totalTime = new TimeSpan();
    static int sleepMs = 1000;
    static StreamWriter sw;
    static void Main(string[] args) {
        Console.WriteLine("Runnin'!");
        using (FileStream fs = new FileStream("timeCounters.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read))
        using (StreamReader sr = new StreamReader(fs))
        using (sw = new StreamWriter(fs))
        {
            sw.AutoFlush = true;
            Console.WriteLine(sr.ReadToEnd());
            if (sr.ReadToEnd() == "") {
                sw.Write("zelda: " + totalTime);
                //fs.Position = 0;
                //fs.Seek(0, SeekOrigin.Begin);
            }
            Console.WriteLine(sr.ReadToEnd());
            //Console.Write(fs.Seek(0, SeekOrigin.Begin));
            //string time = sr.ReadToEnd().Split(' ')[0];
            while (true) {
                fs.Seek(0, SeekOrigin.Begin);
                if (process == null) {
                    Console.Write(".");
                    process = Process.GetProcessesByName("yuzu").FirstOrDefault();
                }
                else {
                    Console.Write("!");
                    process.EnableRaisingEvents = true;
                    process.Exited += Process_Exited;
                    totalTime += TimeSpan.FromSeconds(sleepMs/1000);
                    sw.Write("zelda: "+totalTime);
                    // sw.Close(); without Close(), text is never written in drive
                }
                Thread.Sleep(sleepMs);
            }
        }
    }
    private static void Process_Exited(object sender, EventArgs e) {
        Process process_ = (Process)sender;
        process = null;
    }
}
