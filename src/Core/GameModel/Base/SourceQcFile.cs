// using Core.General;
//
// namespace Core.GameModel.Base
// {
//     using System;
//     using System.Collections.Generic;
//     using System.IO;
//     using Microsoft.VisualBasic;
//
//     public class SourceQcFile
//     {
//         
//         private readonly App TheApp;
//
//         public SourceQcFile(App app)
//         {
//             TheApp = app;
//         }
//         
//         public string GetQcModelName(string qcPathFileName)
//         {
//             string qcModelName;
//
//             qcModelName = "";
//
//             using (StreamReader inputFileStream = new StreamReader(qcPathFileName))
//             {
//                 string inputLine;
//                 string modifiedLine;
//
//                 while ((!(inputFileStream.EndOfStream)))
//                 {
//                     inputLine = inputFileStream.ReadLine();
//
//                     modifiedLine = inputLine.ToLower().TrimStart();
//                     if (modifiedLine.StartsWith("\"$modelname\""))
//                         modifiedLine = modifiedLine.Replace("\"$modelname\"", "$modelname");
//                     if (modifiedLine.StartsWith("$modelname"))
//                     {
//                         modifiedLine = modifiedLine.Replace("$modelname", "");
//                         modifiedLine = modifiedLine.Trim();
//
//                         // Need to remove any comment after the file name token (which may or may not be double-quoted).
//                         int pos;
//                         if (modifiedLine.StartsWith("\""))
//                         {
//                             pos = modifiedLine.IndexOf("\"", 1);
//                             if (pos >= 0)
//                                 modifiedLine = modifiedLine.Substring(1, pos - 1);
//                         }
//                         else
//                         {
//                             pos = modifiedLine.IndexOf(" ");
//                             if (pos >= 0)
//                                 modifiedLine = modifiedLine.Substring(0, pos);
//                         }
//
//                         // temp = temp.Trim(Chr(34))
//                         qcModelName = modifiedLine.Replace("/", @"\");
//                         break;
//                     }
//                 }
//             }
//
//             return qcModelName;
//         }
//
//         public string InsertAnIncludeFileCommand(string qcPathFileName, string qciPathFileName)
//         {
//             string line = "";
//
//             using (StreamWriter outputFileStream = File.AppendText(qcPathFileName))
//             {
//                 outputFileStream.WriteLine();
//
//                 if (TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
//                     line += "$Include";
//                 else
//                     line += "$include";
//                 line += " ";
//                 line += "\"";
//                 line += FileManager.GetRelativePathFileName(FileManager.GetPath(qcPathFileName), qciPathFileName);
//                 line += "\"";
//                 outputFileStream.WriteLine(line);
//             }
//
//             return line;
//         }
//
//         protected List<List<short>> GetSkinFamiliesOfChangedMaterials(List<List<short>> iSkinFamilies)
//         {
//             List<List<short>> skinFamilies;
//             int skinReferenceCount;
//             List<short> firstSkinFamily;
//             List<short> aSkinFamily;
//             List<short> textureFileNameIndexes;
//
//             skinReferenceCount = iSkinFamilies[0].Count;
//             skinFamilies = new List<List<short>>(iSkinFamilies.Count);
//
//             try
//             {
//                 for (int skinFamilyIndex = 0; skinFamilyIndex <= iSkinFamilies.Count - 1; skinFamilyIndex++)
//                 {
//                     textureFileNameIndexes = new List<short>(skinReferenceCount);
//                     skinFamilies.Add(textureFileNameIndexes);
//                 }
//
//                 firstSkinFamily = iSkinFamilies[0];
//                 for (int j = 0; j <= skinReferenceCount - 1; j++)
//                 {
//                     // NOTE: Start at second skin family because comparing first with all others.
//                     for (int i = 1; i <= iSkinFamilies.Count - 1; i++)
//                     {
//                         aSkinFamily = iSkinFamilies[i];
//
//                         if (firstSkinFamily[j] != aSkinFamily[j])
//                         {
//                             for (int skinFamilyIndex = 0; skinFamilyIndex <= iSkinFamilies.Count - 1; skinFamilyIndex++)
//                             {
//                                 aSkinFamily = iSkinFamilies[skinFamilyIndex];
//
//                                 textureFileNameIndexes = skinFamilies[skinFamilyIndex];
//                                 textureFileNameIndexes.Add(aSkinFamily[j]);
//                             }
//
//                             break;
//                         }
//                     }
//                 }
//             }
//             catch (Exception ex)
//             {
//                 int debug = 4242;
//             }
//
//             return skinFamilies;
//         }
//
//         protected List<string> GetTextureGroupSkinFamilyLines(List<List<string>> skinFamilies)
//         {
//             List<string> lines = new List<string>();
//             List<string> aSkinFamily;
//             string aTextureFileName;
//             string line = "";
//
//             if (TheApp.Settings.DecompileQcSkinFamilyOnSingleLineIsChecked)
//             {
//                 List<int> textureFileNameMaxLengths = new List<int>();
//                 int length;
//
//                 aSkinFamily = skinFamilies[0];
//                 for (int textureFileNameIndex = 0; textureFileNameIndex <= aSkinFamily.Count - 1; textureFileNameIndex++)
//                 {
//                     aTextureFileName = aSkinFamily[textureFileNameIndex];
//                     length = aTextureFileName.Length;
//
//                     textureFileNameMaxLengths.Add(length);
//                 }
//
//                 for (int skinFamilyIndex = 1; skinFamilyIndex <= skinFamilies.Count - 1; skinFamilyIndex++)
//                 {
//                     aSkinFamily = skinFamilies[skinFamilyIndex];
//
//                     for (int textureFileNameIndex = 0; textureFileNameIndex <= aSkinFamily.Count - 1; textureFileNameIndex++)
//                     {
//                         aTextureFileName = aSkinFamily[textureFileNameIndex];
//                         length = aTextureFileName.Length;
//
//                         if (length > textureFileNameMaxLengths[textureFileNameIndex])
//                             textureFileNameMaxLengths[textureFileNameIndex] = length;
//                     }
//                 }
//
//                 for (int skinFamilyIndex = 0; skinFamilyIndex <= skinFamilies.Count - 1; skinFamilyIndex++)
//                 {
//                     aSkinFamily = skinFamilies[skinFamilyIndex];
//
//                     line = Constants.vbTab;
//                     line += "{";
//                     line += " ";
//
//                     for (int textureFileNameIndex = 0; textureFileNameIndex <= aSkinFamily.Count - 1; textureFileNameIndex++)
//                     {
//                         aTextureFileName = aSkinFamily[textureFileNameIndex];
//                         length = textureFileNameMaxLengths[textureFileNameIndex];
//
//                         // NOTE: Need at least "+ 2" to account for the double-quotes.
//                         line += Strings.LSet("\"" + aTextureFileName + "\"", length + 3);
//                     }
//
//                     // line += " "
//                     line += "}";
//                     lines.Add(line);
//                 }
//             }
//             else
//                 for (int skinFamilyIndex = 0; skinFamilyIndex <= skinFamilies.Count - 1; skinFamilyIndex++)
//                 {
//                     aSkinFamily = skinFamilies[skinFamilyIndex];
//
//                     line = Constants.vbTab;
//                     line += "{";
//                     lines.Add(line);
//
//                     for (int textureFileNameIndex = 0; textureFileNameIndex <= aSkinFamily.Count - 1; textureFileNameIndex++)
//                     {
//                         aTextureFileName = aSkinFamily[textureFileNameIndex];
//
//                         line = Constants.vbTab;
//                         line += Constants.vbTab;
//                         line += "\"";
//                         line += aTextureFileName;
//                         line += "\"";
//
//                         lines.Add(line);
//                     }
//
//                     line = Constants.vbTab;
//                     line += "}";
//                     lines.Add(line);
//                 }
//
//             return lines;
//         }
//     }
// }
