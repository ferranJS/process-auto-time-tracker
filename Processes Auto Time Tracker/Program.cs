using System.Diagnostics;

public class ProcessesAutoTimeTracker {
    static Process process;
    static TimeSpan totalTime = new TimeSpan();
    static int sleepMs = 500;

    static void Main(string[] args) {
        Console.WriteLine("Runnin'!");
        using (FileStream fs = new FileStream("timeCounters.json", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read))
        using (StreamReader sr = new StreamReader(fs))
        using (StreamWriter sw = new StreamWriter(fs))
        {
            while (true) {
                if (process == null) {
                    Console.Write(".");
                    process = Process.GetProcessesByName("SnippingTool").FirstOrDefault();
                }
                else {
                    Console.Write("!");
                    process.EnableRaisingEvents = true;
                    process.Exited += Process_Exited;
                    totalTime += TimeSpan.FromSeconds(sleepMs/1000);
                    sw.Write("zelda: "+totalTime);
                    // sw.Close(); without Close(), text is never written in drive
                }
                Console.WriteLine(sr.ReadToEnd());
                Thread.Sleep(sleepMs);
            }
        }
    }
    private static void Process_Exited(object sender, EventArgs e) {
        Process process_ = (Process)sender;
        process = null;
    }
}
