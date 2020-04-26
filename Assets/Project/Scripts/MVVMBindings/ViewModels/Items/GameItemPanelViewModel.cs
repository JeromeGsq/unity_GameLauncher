using System;
using System.Collections.Generic;
using UnityWeld.Binding;
using UnityEngine;

[Binding]
public class GameItemPanelViewModel : MainViewModel
{
    private GameItemData gameItemData;

    [Binding]
    public GameItemData GameItemData
    {
        get => this.gameItemData;
        set
        {
            this.gameItemData = value;
            this.RaiseAllPropertyChanged(typeof(GameItemPanelViewModel));
        }
    }

    [Binding]
    public string Title => this.GameItemData?.Title ?? string.Empty;

    [Binding]
    public string ImageLink => this.GameItemData?.ImageLink ?? string.Empty;

    [Binding]
    public string ExecutableLink => this.GameItemData?.ExecutableLink ?? string.Empty;

    [Binding]
    public void RunApp()
    {
        if (!string.IsNullOrEmpty(this.ExecutableLink) && this.ExecutableLink.Contains("steam://"))
        {
            var processInfo = new System.Diagnostics.ProcessStartInfo("explorer.exe", $"{this.ExecutableLink}");
            processInfo.CreateNoWindow = false;
            processInfo.UseShellExecute = false;

            var process = System.Diagnostics.Process.Start(processInfo);

            process.WaitForExit();
            process.Close();


            List<System.Diagnostics.Process> procs = new List<System.Diagnostics.Process>(System.Diagnostics.Process.GetProcesses());
            System.Diagnostics.Process steam = procs.Find(x => x.ProcessName == "Steam");
            int steamId = steam.Id;

            foreach (var proc in procs)
            {
                using (var mo = new System.Management.ManagementObject($"win32_process.handle='{proc.Id}'"))
                {
                    if (mo != null)
                    {
                        try
                        {
                            mo.Get();
                            int parentPid = Convert.ToInt32(mo["ParentProcessId"]);
                            if (parentPid == steamId)
                            {
                                Debug.Log($"{proc.ProcessName} is running as a child to {mo["ParentProcessId"]}");
                            }
                        }
                        catch (Exception ex)
                        {
                            // the process ended between polling all of the procs and requesting the management object
                        }
                    }
                }
            }
        }
    }
}
