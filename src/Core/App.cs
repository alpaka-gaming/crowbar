﻿// using System;
// using System.Collections.Generic;
// using System.Collections.ObjectModel;
// using System.Globalization;
// using System.IO;
// using System.Net.Mime;
// using Core.GameSetup;
// using Core.General;
//
// namespace Core
// {
//     public class App : IDisposable
//     {
//         public App()
//         {
//             this.IsDisposed = false;
//
//             // NOTE: To use a particular culture's NumberFormat that doesn't change with user settings, 
//             // must use this constructor with False as second param.
//             this.theInternalCultureInfo = new CultureInfo("en-US", false);
//             this.theInternalNumberFormat = this.theInternalCultureInfo.NumberFormat;
//
//             this.theSmdFilesWritten = new List<string>();
//         }
//
//
//         public void Dispose()
//         {
//             // Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) below.
//             Dispose(true);
//             GC.SuppressFinalize(this);
//         }
//
//         protected virtual void Dispose(bool disposing)
//         {
//             if (!this.IsDisposed)
//             {
//                 if (disposing)
//                     this.Free();
//             }
//
//             this.IsDisposed = true;
//         }
//
//         // Protected Overrides Sub Finalize()
//         // ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
//         // Dispose(False)
//         // MyBase.Finalize()
//         // End Sub
//
//
//         public void Init()
//         {
//             this.theAppPath = MediaTypeNames.Application.StartupPath;
//             // NOTE: Needed for using DLLs placed in folder separate from main EXE file.
//             Environment.SetEnvironmentVariable("path", this.GetCustomDataPath(), EnvironmentVariableTarget.Process);
//             this.WriteRequiredFiles();
//             this.LoadAppSettings();
//
//             if (this.Settings.SteamLibraryPaths.Count == 0)
//             {
//                 SteamLibraryPath libraryPath = new SteamLibraryPath();
//                 this.Settings.SteamLibraryPaths.Add(libraryPath);
//             }
//
//             this.theUnpacker = new Unpacker();
//             this.theDecompiler = new Decompiler();
//             this.theCompiler = new Compiler(this);
//             this.thePacker = new Packer();
//             // Me.theModelViewer = New Viewer()
//
//             string documentsPath;
//             documentsPath = Path.Combine(this.theAppPath, "Documents");
//             Constants.HelpContentsLink = Path.Combine(documentsPath, Constants.HelpContentsLink);
//             Constants.HelpTutorialLink = Path.Combine(documentsPath, Constants.HelpTutorialLink);
//             Constants.HelpIndexLink = Path.Combine(documentsPath, Constants.HelpIndexLink);
//             Constants.HelpTipsLink = Path.Combine(documentsPath, Constants.HelpTipsLink);
//         }
//
//         private void Free()
//         {
//             if (this.theSettings != null)
//                 this.SaveAppSettings();
//         }
//
//
//         public Settings Settings
//         {
//             get
//             {
//                 return this.theSettings;
//             }
//         }
//
//         public bool CommandLineOption_Settings_IsEnabled
//         {
//             get
//             {
//                 return this.theCommandLineOption_Settings_IsEnabled;
//             }
//         }
//
//         public string ErrorPathFileName
//         {
//             get
//             {
//                 return Path.Combine(this.GetCustomDataPath(), this.ErrorFileName);
//             }
//         }
//
//         public Unpacker Unpacker
//         {
//             get
//             {
//                 return this.theUnpacker;
//             }
//         }
//
//         public Decompiler Decompiler
//         {
//             get
//             {
//                 return this.theDecompiler;
//             }
//         }
//
//         public Compiler Compiler
//         {
//             get
//             {
//                 return this.theCompiler;
//             }
//         }
//
//         public Packer Packer
//         {
//             get
//             {
//                 return this.thePacker;
//             }
//         }
//
//         // Public ReadOnly Property Viewer() As Viewer
//         // Get
//         // Return Me.theModelViewer
//         // End Get
//         // End Property
//
//         // Public Property ModelRelativePathFileName() As String
//         // Get
//         // Return Me.theModelRelativePathFileName
//         // End Get
//         // Set(ByVal value As String)
//         // Me.theModelRelativePathFileName = value
//         // End Set
//         // End Property
//
//         public CultureInfo InternalCultureInfo
//         {
//             get
//             {
//                 return this.theInternalCultureInfo;
//             }
//         }
//
//         public NumberFormatInfo InternalNumberFormat
//         {
//             get
//             {
//                 return this.theInternalNumberFormat;
//             }
//         }
//
//         public List<string> SmdFileNames
//         {
//             get
//             {
//                 return this.theSmdFilesWritten;
//             }
//             set
//             {
//                 this.theSmdFilesWritten = value;
//             }
//         }
//
//
//         public bool CommandLineValueIsAnAppSetting(string commandLineValue)
//         {
//             return commandLineValue.StartsWith(App.SettingsParameter);
//         }
//
//         public void WriteRequiredFiles()
//         {
//             string steamAPIDLLPathFileName = Path.Combine(this.GetCustomDataPath(), App.theSteamAPIDLLFileName);
//             this.WriteResourceToFileIfDifferent(My.Resources.steam_api, steamAPIDLLPathFileName);
//
//             // NOTE: Although Crowbar itself does not need the DLL file extracted, CrowbarSteamPipe needs it extracted.
//             string steamworksDotNetPathFileName = Path.Combine(this.GetCustomDataPath(), App.theSteamworksDotNetDLLFileName);
//             this.WriteResourceToFileIfDifferent(My.Resources.Steamworks_NET, steamworksDotNetPathFileName);
//
//             string crowbarSteamPipePathFileName = Path.Combine(this.GetCustomDataPath(), App.CrowbarSteamPipeFileName);
//             this.WriteResourceToFileIfDifferent(My.Resources.CrowbarSteamPipe, crowbarSteamPipePathFileName);
//
//             this.LzmaExePathFileName = Path.Combine(this.GetCustomDataPath(), App.theLzmaExeFileName);
//             this.WriteResourceToFileIfDifferent(My.Resources.lzma, this.LzmaExePathFileName);
//
//             // NOTE: Only write settings file if it does not exist.
//             string appSettingsPathFileName = Path.Combine(this.GetCustomDataPath(), App.theAppSettingsFileName);
//             try
//             {
//                 if (!File.Exists(appSettingsPathFileName))
//                     File.WriteAllText(appSettingsPathFileName, My.Resources.Crowbar_Settings);
//             }
//             catch (Exception ex)
//             {
//                 Console.WriteLine("EXCEPTION: " + ex.Message);
//                 // Throw New Exception(ex.Message, ex.InnerException)
//                 return;
//             }
//             finally
//             {
//             }
//         }
//
//         public void WriteUpdaterFiles()
//         {
//             this.SevenZrExePathFileName = Path.Combine(this.GetCustomDataPath(), App.theSevenZrEXEFileName);
//             this.WriteResourceToFileIfDifferent(My.Resources.SevenZr, this.SevenZrExePathFileName);
//
//             this.CrowbarLauncherExePathFileName = Path.Combine(this.GetCustomDataPath(), App.theCrowbarLauncherEXEFileName);
//             this.WriteResourceToFileIfDifferent(My.Resources.CrowbarLauncher, this.CrowbarLauncherExePathFileName);
//         }
//
//         public void DeleteUpdaterFiles()
//         {
//             this.SevenZrExePathFileName = Path.Combine(this.GetCustomDataPath(), App.theSevenZrEXEFileName);
//             try
//             {
//                 if (File.Exists(this.SevenZrExePathFileName))
//                     File.Delete(this.SevenZrExePathFileName);
//             }
//             catch (Exception ex)
//             {
//                 int debug = 4242;
//             }
//
//             this.CrowbarLauncherExePathFileName = Path.Combine(this.GetCustomDataPath(), App.theCrowbarLauncherEXEFileName);
//             try
//             {
//                 if (File.Exists(this.CrowbarLauncherExePathFileName))
//                     File.Delete(this.CrowbarLauncherExePathFileName);
//             }
//             catch (Exception ex)
//             {
//                 int debug = 4242;
//             }
//         }
//
//         public void WriteSteamAppIdFile(uint appID)
//         {
//             this.WriteSteamAppIdFile(appID.ToString());
//         }
//
//         public void WriteSteamAppIdFile(string appID_text)
//         {
//             string steamAppIDPathFileName = Path.Combine(this.GetCustomDataPath(), App.theSteamAppIDFileName);
//             using (StreamWriter sw = File.CreateText(steamAppIDPathFileName))
//             {
//                 sw.WriteLine(appID_text);
//             }
//         }
//
//         public string GetDebugPath(string outputPath, string modelName)
//         {
//             // Dim logsPath As String
//
//             
//             // logsPath = Path.Combine(outputPath, modelName + "_" + App.LogsSubFolderName)
//
//             // Return logsPath
//             return outputPath;
//         }
//
//         public void SaveAppSettings()
//         {
//             string appSettingsPath;
//             string appSettingsPathFileName;
//
//             appSettingsPathFileName = this.GetAppSettingsPathFileName();
//             appSettingsPath = FileManager.GetPath(appSettingsPathFileName);
//
//             if (FileManager.PathExistsAfterTryToCreate(appSettingsPath))
//                 FileManager.WriteXml(this.theSettings, appSettingsPathFileName);
//         }
//
//         public void InitAppInfo()
//         {
//             if (this.SteamAppInfos == null)
//                 this.SteamAppInfos = SteamAppInfoBase.GetSteamAppInfos();
//         }
//
//         // TODO: [GetCustomDataPath] Have location option where custom data and settings is saved.
//         public string GetCustomDataPath()
//         {
//             string customDataPath;
//             // Dim appDataPath As String
//
//             // ' If the settings file exists in the app's Data folder, then load it.
//             // appDataPath = Me.GetAppDataPath()
//             // If appDataPath <> "" Then
//             // customDataPath = appDataPath
//             // Else
//             // NOTE: Use "standard Windows location for app data".
//             // NOTE: Using Path.Combine in case theStartupFolder is a root folder, like "C:\".
//             customDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ZeqMacaw");
//             customDataPath += Path.DirectorySeparatorChar;
//             // customDataPath += "Crowbar"
//             customDataPath += My.Application.Info.ProductName;
//             customDataPath += " ";
//             customDataPath += My.Application.Info.Version.ToString(2);
//
//             FileManager.CreatePath(customDataPath);
//             // End If
//
//             return customDataPath;
//         }
//
//         public string GetPreviewsPath()
//         {
//             string customDataPath = TheApp.GetCustomDataPath();
//             string previewsPath = Path.Combine(customDataPath, App.PreviewsRelativePath);
//             if (FileManager.PathExistsAfterTryToCreate(previewsPath))
//                 return previewsPath;
//             else
//                 return "";
//         }
//
//         public string GetAppSettingsPathFileName()
//         {
//             return Path.Combine(this.GetCustomDataPath(), App.theAppSettingsFileName);
//         }
//
//
//         private void LoadAppSettings()
//         {
//             string appSettingsPathFileName;
//             appSettingsPathFileName = this.GetAppSettingsPathFileName();
//
//             bool commandLineOption_Settings_IsEnabled = false;
//             ReadOnlyCollection<string> commandLineValues = new ReadOnlyCollection<string>(System.Environment.GetCommandLineArgs());
//             if (commandLineValues.Count > 1 && commandLineValues[1] != "")
//             {
//                 string command = commandLineValues[1];
//                 if (command.StartsWith(App.SettingsParameter))
//                 {
//                     commandLineOption_Settings_IsEnabled = true;
//                     string oldAppSettingsPathFileName = command.Replace(App.SettingsParameter, "");
//                     oldAppSettingsPathFileName = oldAppSettingsPathFileName.Replace("\"", "");
//                     if (File.Exists(oldAppSettingsPathFileName))
//                         File.Copy(oldAppSettingsPathFileName, appSettingsPathFileName, true);
//                 }
//             }
//
//             if (File.Exists(appSettingsPathFileName))
//             {
//                 try
//                 {
//                     VersionModule.ConvertSettingsFile(appSettingsPathFileName);
//                     this.theSettings = (AppSettings)FileManager.ReadXml(typeof(AppSettings), appSettingsPathFileName);
//                 }
//                 catch
//                 {
//                     this.CreateAppSettings();
//                 }
//             }
//             else
//                 // File not found, so init default values.
//                 this.CreateAppSettings();
//         }
//
//         private void CreateAppSettings()
//         {
//             this.theSettings = new AppSettings();
//
//             GameSetup gameSetup = new GameSetup();
//             this.theSettings.GameSetups.Add(gameSetup);
//
//             SteamLibraryPath aPath = new SteamLibraryPath();
//             this.theSettings.SteamLibraryPaths.Add(aPath);
//
//             this.SaveAppSettings();
//         }
//
//         // Private Function GetAppDataPath() As String
//         // Dim appDataPath As String
//         // Dim appDataPathFileName As String
//
//         // appDataPath = Path.Combine(Me.theAppPath, App.theDataFolderName)
//         // appDataPathFileName = Path.Combine(appDataPath, App.theAppSettingsFileName)
//
//         // If File.Exists(appDataPathFileName) Then
//         // Return appDataPath
//         // Else
//         // Return ""
//         // End If
//         // End Function
//
//         private void WriteResourceToFileIfDifferent(byte[] dataResource, string pathFileName)
//         {
//             try
//             {
//                 bool isDifferentOrNotExist = true;
//                 if (File.Exists(pathFileName))
//                 {
//                     byte[] resourceHash;
//                     System.Security.Cryptography.SHA512Managed sha = new System.Security.Cryptography.SHA512Managed();
//                     resourceHash = sha.ComputeHash(dataResource);
//
//                     FileStream fileStream = File.Open(pathFileName, FileMode.Open);
//                     byte[] fileHash = sha.ComputeHash(fileStream);
//                     fileStream.Close();
//
//                     isDifferentOrNotExist = false;
//                     for (int x = 0; x <= resourceHash.Length - 1; x++)
//                     {
//                         if (resourceHash[x] != fileHash[x])
//                         {
//                             isDifferentOrNotExist = true;
//                             break;
//                         }
//                     }
//                 }
//
//                 if (isDifferentOrNotExist)
//                     File.WriteAllBytes(pathFileName, dataResource);
//             }
//             catch (Exception ex)
//             {
//                 Console.WriteLine("EXCEPTION: " + ex.Message);
//                 // Throw New Exception(ex.Message, ex.InnerException)
//                 return;
//             }
//             finally
//             {
//             }
//         }
//
//         public string GetHeaderComment()
//         {
//             string line;
//
//             line = "Created by ";
//             line += this.GetProductNameAndVersion();
//
//             return line;
//         }
//
//         public string GetProductNameAndVersion()
//         {
//             string result;
//
//             result = My.Application.Info.ProductName;
//             result += " ";
//             result += My.Application.Info.Version.ToString(2);
//
//             return result;
//         }
//
//         public string GetProcessedPathFileName(string pathFileName)
//         {
//             string result;
//             string aMacro;
//
//             result = pathFileName;
//
//             foreach (SteamLibraryPath aSteamLibraryPath in this.Settings.SteamLibraryPaths)
//             {
//                 aMacro = aSteamLibraryPath.Macro;
//                 if (pathFileName.StartsWith(aMacro))
//                 {
//                     pathFileName = pathFileName.Remove(0, aMacro.Length);
//                     if (pathFileName.StartsWith(@"\"))
//                         pathFileName = pathFileName.Remove(0, 1);
//                     result = Path.Combine(aSteamLibraryPath.LibraryPath, pathFileName);
//                 }
//             }
//
//             return result;
//         }
//
//
//         private bool IsDisposed;
//
//         private CultureInfo theInternalCultureInfo;
//         private NumberFormatInfo theInternalNumberFormat;
//
//         private Settings theSettings;
//
//         // NOTE: Use slash at start to avoid confusing with a pathFileName that Windows Explorer might use with auto-open.
//         public const string SettingsParameter = "/settings=";
//         private bool theCommandLineOption_Settings_IsEnabled;
//
//         // Location of the exe.
//         private string theAppPath;
//
//         private const string theSteamAPIDLLFileName = "steam_api.dll";
//         private const string theSteamworksDotNetDLLFileName = "Steamworks.NET.dll";
//         private const string theSevenZrEXEFileName = "7zr.exe";
//         private const string theCrowbarLauncherEXEFileName = "CrowbarLauncher.exe";
//         private const string theLzmaExeFileName = "lzma.exe";
//         public string SevenZrExePathFileName;
//         public string CrowbarLauncherExePathFileName;
//         public string LzmaExePathFileName;
//         public List<SteamAppInfoBase> SteamAppInfos;
//
//         private const string PreviewsRelativePath = "previews";
//         public const string CrowbarSteamPipeFileName = "CrowbarSteamPipe.exe";
//
//         private const string theSteamAppIDFileName = "steam_appid.txt";
//
//         // Private Const theDataFolderName As String = "Data"
//         private const string theAppSettingsFileName = "Crowbar Settings.xml";
//
//         public const string AnimsSubFolderName = "anims";
//         public const string LogsSubFolderName = "logs";
//
//         private string ErrorFileName = "unhandled_exception_error.txt";
//
//         private Unpacker theUnpacker;
//         private Decompiler theDecompiler;
//         private Compiler theCompiler;
//
//         private Packer thePacker;
//
//         // Private theModelViewer As Viewer
//         private string theModelRelativePathFileName;
//
//         private List<string> theSmdFilesWritten;
//     }
// }