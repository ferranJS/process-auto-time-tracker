using System.Diagnostics;

public class ProcessesAutoTimeTracker {
    static Process process;
    static void Main(string[] args) {
        while (process == null) {
            Console.WriteLine(process);
            Thread.Sleep(3000);
            process = Process.GetProcessesByName("Notepad").FirstOrDefault();
        }
        process.EnableRaisingEvents = true;
        process.Exited += Process_Exited;
        process.WaitForExit();
    }
    private static void Process_Exited(object sender, EventArgs e) {
        Process process = (Process)sender;
        Console.WriteLine(process.ExitTime - process.StartTime);
    }
}
