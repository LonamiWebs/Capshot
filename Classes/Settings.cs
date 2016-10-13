/** Made by Lonami Exo
 * (C) LonamiWebs 2015
 * Created: 24/05/2015
 * LastMod: 03/06/2015
 * Requires: Serializer.cs
 */
using System;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// An improved (both on performance and error handling) Settings class
/// </summary>
public static class Settings
{
    #region Private variables and consts

    // The application name. It can be "Domain\\Name"
    static string appName;
    // Where the settings file is located
    static string settingsFile;

    // The stored values for the current settings
    static Dictionary<string, object> values = new Dictionary<string, object>();
    // The default values to be used
    static Dictionary<string, object> defaultValues;

    // Has been Settings initialized?
    static bool initialized;

    // Error messages
    const string errorNotInitialized = "You must call Settings.Init() before using any other method call";

    #endregion

    #region Public variables and consts

    /// <summary>
    /// Should the settings be saved automatically?
    /// </summary>
    public static bool Autosave { get; set; } = true;

    /// <summary>
    /// Determines whether the .SetValue<T>(...) should be disabled or not. 
    /// Useful when loading a settings window
    /// </summary>
    public static bool SetSettingDisabled { get; set; } = false;

    /// <summary>
    /// Retrieves the application folder
    /// </summary>
    public static string ApplicationFolder => Path.GetDirectoryName(settingsFile);

    #endregion

    #region Initialize

    /// <summary>
    /// Initializes the Settings instance. This must be called once
    /// </summary>
    /// <param name="appName">The name of the current application. You may use "Domain\\AppName"</param>
    public static void Init(string appName)
    { Init(appName, new Dictionary<string, object>()); }

    /// <summary>
    /// Initializes the Settings instance. This must be called once
    /// </summary>
    /// <param name="appName">The name of the current application. You may use "Domain\\AppName"</param>
    /// <param name="defaultValues">The value pairs for the default values if they're not found or corrupted</param>
    public static void Init(string appName, Dictionary<string, object> defaultValues)
    {
        if (initialized)
            return;
        initialized = true;

        Settings.appName = appName;
        Settings.defaultValues = defaultValues;

        var dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Settings.appName);
        settingsFile = Path.Combine(dir, "settings");

        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);

        load(false);
    }

    #endregion

    #region Load save and reset

    /// <summary>
    /// Loads the settings. Default values will be used if they're corrupted, and the method will return false. 
    /// This is automatically called once after calling <seealso cref="Init">Settings.Init()</seealso>
    /// </summary>
    /// <returns>False if the settings file was corrupted</returns>
    public static bool Load() => load(false);

    /// <summary>
    /// Saves the settings. 
    /// This is automatically called if Autosave is enabled
    /// </summary>
    public static void Save()
    {
        checkInit();
        Serializer.Serialize(values, settingsFile);
    }

    /// <summary>
    /// Resets the current settings to it's default values, and saves them if Autosave is true
    /// </summary>
    public static void Reset()
    {
        checkInit();

        load(true);
        if (Autosave)
            File.Delete(settingsFile);
    }

    #endregion

    #region Get and set value

    /// <summary>
    /// Gets a value for the given setting name. If it doesn't exist, an exception will be thrown
    /// </summary>
    /// <param name="settingName">The name of the setting</param>
    /// <returns>The value of the setting</returns>
    public static T GetValue<T>(string settingName) => (T)values[settingName];

    /// <summary>
    /// Tries to get a value for the given setting name. No exception will be thrown if the setting doesn't exist
    /// </summary>
    /// <param name="settingName">The name of the setting</param>
    /// <param name="value">Where the result value will be stored</param>
    /// <returns>True if the setting exists</returns>
    public static bool TryGetValue<T>(string settingName, out T value)
    {
        object result;
        if (values.TryGetValue(settingName, out result))
        {
            value = (T)result;
            return true;
        }
        value = default(T);
        return false;
    }

    /// <summary>
    /// Sets a value for the given setting name
    /// </summary>
    /// <param name="settingName">The name of the setting</param>
    /// <param name="value">The value of the setting</param>
    public static void SetValue<T>(string settingName, T value)
    {
        if (SetSettingDisabled)
            return;
        
        values[settingName] = value;
        if (Autosave)
            Save();
    }

    #endregion

    #region Private methods

    // Load
    static bool load(bool onlyDefault)
    {
        checkInit();

        values = new Dictionary<string, object>(defaultValues);
        if (onlyDefault || !File.Exists(settingsFile))
            return true;

        try
        {
            var result = Serializer.Deserialize<Dictionary<string, object>>(settingsFile);

            // don't set the dictionary directly, as there might be some
            // default values which don't exist in the loaded settings file
            foreach (var kvp in result)
                values[kvp.Key] = kvp.Value;

            return true;
        }
        catch { return false; }
    }
    
    // Checks
    static void checkInit()
    {
        if (!initialized)
            throw new NullReferenceException(errorNotInitialized);
    }

    #endregion
}
