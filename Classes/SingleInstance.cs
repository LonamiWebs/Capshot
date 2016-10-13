/*
 * Made by Lonami, the 06/12/2014 during the morning
 * (C) LonamiWebs
 */

using System;
using System.Reflection;
using System.Threading;

public static class SingleInstance {
	
	/// <summary>
	/// Check wheter is there or not another instance running already
	/// </summary>
	public static bool IsRunning { get { return !mutex.WaitOne(TimeSpan.Zero, true); } }
    
	// The correct way to do this is by using AssemblyGuid, not Name...
	static readonly Mutex mutex = new Mutex(true, AssemblyName);
	static string AssemblyName { get { return Assembly.GetExecutingAssembly().FullName; } }
        
	// There's no need for this! :D
    // public static void StopInstance() { mutex.ReleaseMutex(); }
	
}