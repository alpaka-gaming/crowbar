// using Core.Enums;
//
// namespace Core.GameSetup
// {
//     using System;
//     using System.Collections.Generic;
//     using System.Diagnostics;
//     using System.Globalization;
//     using System.IO;
//     using System.Linq;
//     using System.Reflection;
//     using System.Runtime.CompilerServices;
//     using System.Security;
//     using System.Text;
//     using System.Threading.Tasks;
//     using Microsoft.VisualBasic;
//     using System.ComponentModel;
//     using System.Xml.Serialization;
//
//     public class GameSetup : ICloneable, INotifyPropertyChanged
//     {
//         private readonly App TheApp;
//
//         public GameSetup(App app)
//         {
//             TheApp = app;
//             // MyBase.New()
//
//             this.theGameName = "Left 4 Dead 2";
//             this.theGameEngine = Enums.GameEngine.Source;
//             this.theGamePathFileName = @"C:\Program Files (x86)\Steam\steamapps\common\left 4 dead 2\left4dead2\gameinfo.txt";
//             this.theGameAppPathFileName = @"C:\Program Files (x86)\Steam\steamapps\common\left 4 dead 2\left4dead2.exe";
//             this.theGameAppOptions = "";
//             this.theCompilerPathFileName = @"C:\Program Files (x86)\Steam\steamapps\common\left 4 dead 2\bin\studiomdl.exe";
//             this.theViewerPathFileName = @"C:\Program Files (x86)\Steam\steamapps\common\left 4 dead 2\bin\hlmv.exe";
//             this.theMappingToolPathFileName = @"C:\Program Files (x86)\Steam\steamapps\common\left 4 dead 2\bin\hammer.exe";
//             this.thePackerPathFileName = @"C:\Program Files (x86)\Steam\steamapps\common\left 4 dead 2\bin\vpk.exe";
//         }
//
//         protected GameSetup(App app, GameSetup originalObject)
//         {
//             TheApp = app;
//
//             this.theGameName = originalObject.GameName;
//             this.theGameEngine = originalObject.GameEngine;
//             // Me.theGamePathFileName = originalObject.GamePathFileName
//             // Me.theGameAppPathFileName = originalObject.GameAppPathFileName
//             this.theGamePathFileName = originalObject.GamePathFileNameUnprocessed;
//             this.theGameAppPathFileName = originalObject.GameAppPathFileNameUnprocessed;
//             this.theGameAppOptions = originalObject.GameAppOptions;
//             // Me.theCompilerPathFileName = originalObject.CompilerPathFileName
//             // Me.theViewerPathFileName = originalObject.ViewerPathFileName
//             // Me.theMappingToolPathFileName = originalObject.MappingToolPathFileName
//             // Me.theUnpackerPathFileName = originalObject.UnpackerPathFileName
//             this.theCompilerPathFileName = originalObject.CompilerPathFileNameUnprocessed;
//             this.theViewerPathFileName = originalObject.ViewerPathFileNameUnprocessed;
//             this.theMappingToolPathFileName = originalObject.MappingToolPathFileNameUnprocessed;
//             this.thePackerPathFileName = originalObject.PackerPathFileNameUnprocessed;
//         }
//
//         public object Clone()
//         {
//             return new GameSetup(TheApp, this);
//         }
//
//
//         public string GameName
//         {
//             get
//             {
//                 return this.theGameName;
//             }
//             set
//             {
//                 if (this.theGameName != value)
//                 {
//                     this.theGameName = value;
//                     NotifyPropertyChanged("GameName");
//                 }
//             }
//         }
//
//         public GameEngine GameEngine
//         {
//             get
//             {
//                 return this.theGameEngine;
//             }
//             set
//             {
//                 if (this.theGameEngine != value)
//                 {
//                     this.theGameEngine = value;
//                     NotifyPropertyChanged("GameEngine");
//                 }
//             }
//         }
//
//         [XmlIgnore()]
//         public string GamePathFileName
//         {
//             get
//             {
//                 return TheApp.GetProcessedPathFileName(this.theGamePathFileName);
//             }
//         }
//
//         [XmlElement("GamePathFileName")]
//         public string GamePathFileNameUnprocessed
//         {
//             get
//             {
//                 return this.theGamePathFileName;
//             }
//             set
//             {
//                 this.theGamePathFileName = value;
//                 NotifyPropertyChanged("GamePathFileName");
//                 NotifyPropertyChanged("GamePathFileNameUnprocessed");
//             }
//         }
//
//         [XmlIgnore()]
//         public string GameAppPathFileName
//         {
//             get
//             {
//                 return TheApp.GetProcessedPathFileName(this.theGameAppPathFileName);
//             }
//         }
//
//         [XmlElement("GameAppPathFileName")]
//         public string GameAppPathFileNameUnprocessed
//         {
//             get
//             {
//                 return this.theGameAppPathFileName;
//             }
//             set
//             {
//                 this.theGameAppPathFileName = value;
//                 NotifyPropertyChanged("GameAppPathFileName");
//                 NotifyPropertyChanged("GameAppPathFileNameUnprocessed");
//             }
//         }
//
//         public string GameAppOptions
//         {
//             get
//             {
//                 return this.theGameAppOptions;
//             }
//             set
//             {
//                 this.theGameAppOptions = value;
//                 NotifyPropertyChanged("GameAppOptions");
//             }
//         }
//
//         [XmlIgnore()]
//         public string CompilerPathFileName
//         {
//             get
//             {
//                 return TheApp.GetProcessedPathFileName(this.theCompilerPathFileName);
//             }
//         }
//
//         [XmlElement("CompilerPathFileName")]
//         public string CompilerPathFileNameUnprocessed
//         {
//             get
//             {
//                 return this.theCompilerPathFileName;
//             }
//             set
//             {
//                 this.theCompilerPathFileName = value;
//                 NotifyPropertyChanged("CompilerPathFileName");
//                 NotifyPropertyChanged("CompilerPathFileNameUnprocessed");
//             }
//         }
//
//         [XmlIgnore()]
//         public string ViewerPathFileName
//         {
//             get
//             {
//                 return TheApp.GetProcessedPathFileName(this.theViewerPathFileName);
//             }
//         }
//
//         [XmlElement("ViewerPathFileName")]
//         public string ViewerPathFileNameUnprocessed
//         {
//             get
//             {
//                 return this.theViewerPathFileName;
//             }
//             set
//             {
//                 this.theViewerPathFileName = value;
//                 NotifyPropertyChanged("ViewerPathFileName");
//                 NotifyPropertyChanged("ViewerPathFileNameUnprocessed");
//             }
//         }
//
//         [XmlIgnore()]
//         public string MappingToolPathFileName
//         {
//             get
//             {
//                 return TheApp.GetProcessedPathFileName(this.theMappingToolPathFileName);
//             }
//         }
//
//         [XmlElement("MappingToolPathFileName")]
//         public string MappingToolPathFileNameUnprocessed
//         {
//             get
//             {
//                 return this.theMappingToolPathFileName;
//             }
//             set
//             {
//                 this.theMappingToolPathFileName = value;
//                 NotifyPropertyChanged("MappingToolPathFileName");
//                 NotifyPropertyChanged("MappingToolPathFileNameUnprocessed");
//             }
//         }
//
//         [XmlIgnore()]
//         public string PackerPathFileName
//         {
//             get
//             {
//                 return TheApp.GetProcessedPathFileName(this.thePackerPathFileName);
//             }
//         }
//
//         [XmlElement("PackerPathFileName")]
//         public string PackerPathFileNameUnprocessed
//         {
//             get
//             {
//                 return this.thePackerPathFileName;
//             }
//             set
//             {
//                 this.thePackerPathFileName = value;
//                 NotifyPropertyChanged("PackerPathFileName");
//                 NotifyPropertyChanged("PackerPathFileNameUnprocessed");
//             }
//         }
//
//
//         public event PropertyChangedEventHandler PropertyChanged;
//
//
//         protected void NotifyPropertyChanged(string info)
//         {
//             PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
//         }
//
//
//         private string theGameName;
//         private GameEngine theGameEngine;
//         private string theGamePathFileName;
//         private string theGameAppPathFileName;
//         private string theGameAppId;
//         private string theGameAppOptions;
//         private string theCompilerPathFileName;
//         private string theViewerPathFileName;
//         private string theMappingToolPathFileName;
//         private string thePackerPathFileName;
//     }
// }
