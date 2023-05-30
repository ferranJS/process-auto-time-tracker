using System.Diagnostics;
using System.Text.Json;
using Newtonsoft.Json;

public class ProcessesAutoTimeTracker {
    static Process process;
    static TimeSpan totalTime = new TimeSpan();

    static void Main(string[] args) {
        Console.WriteLine("Runnin'!");
        try {
            new StreamReader("time.json");
        } catch (Exception ex) {
            File.WriteAllText("time.json", "{}");
        }
        using (StreamReader r = new StreamReader("time.json")) {
            string jsonString = r.ReadToEnd();
            dynamic json = JsonConverter.DeserializeObject(jsonString);
            Console.WriteLine(json);
            while (true) {
                if (process == null) {
                    Console.Write(".");
                    process = Process.GetProcessesByName("SnippingTool").FirstOrDefault();
                } else {
                    Console.WriteLine(process);
                    process.EnableRaisingEvents = true;
                    process.Exited += Process_Exited;
                    totalTime += TimeSpan.FromSeconds(3);
                    json.
                    Console.WriteLine(json);
                }
                Thread.Sleep(500);
            }
        }
    }
    private static void Process_Exited(object sender, EventArgs e) {
        Process process_ = (Process)sender;
        process = null;
    }
}
