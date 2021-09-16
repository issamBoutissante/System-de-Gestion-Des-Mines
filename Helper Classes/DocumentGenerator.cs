﻿using System;
using System.Windows;
using System.Windows.Xps.Packaging;
using System.IO;
using word = Microsoft.Office.Interop.Word;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Projet_Mines_Official
{
    class DocumentGenerator
    {
        internal static XpsDocument ConvertWordDocToXPSDoc(word.Document document, string xpsDocName)
        {
            try
            {
                document.SaveAs(xpsDocName, word.WdSaveFormat.wdFormatXPS);

                XpsDocument xpsDoc = new XpsDocument(xpsDocName, System.IO.FileAccess.Read);
                return xpsDoc;
            }
            catch (Exception exp)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    MessageBox.Show(exp.Message,"Error",MessageBoxButton.OK,MessageBoxImage.Error);
                });
            }
            return null;
        }
        internal static async void GenerateDocument(object filename, Action<word.Application> FindAndReplace, DocumentViewer documentViewer,Action OpenDocumentWindow=null)
        {
            loadingDocument loading = new loadingDocument();
            loading.Show();
            try
            {
                XpsDocument xpsDocument = await GetXpsGetXpsDocumentAndSaveAsync(filename,FindAndReplace);
                if (xpsDocument != null)
                {
                    documentViewer.Document = xpsDocument.GetFixedDocumentSequence();
                }
                else
                    //i'm not going to use this exception message for the instant
                    throw new Exception("there was an error related the file access call the Programmer cuase he had expected this error");
            }catch
            {
                return;
            }
            finally
            {
                loading.Close();
            }
            OpenDocumentWindow?.Invoke();
        }
        internal static void FindAndReplace(word.Application wordApp, object ToFindText, object replaceWithText)
        {
            object matchCase = true;
            object matchWholeWord = true;
            object matchWildCards = false;
            object matchSoundLike = false;
            object nmatchAllforms = false;
            object forward = true;
            object format = false;
            object matchKashida = false;
            object matchDiactitics = false;
            object matchAlefHamza = false;
            object matchControl = false;
            object read_only = false;
            object visible = true;
            object replace = 2;
            object wrap = 1;

            wordApp.Selection.Find.Execute(ref ToFindText,
                ref matchCase, ref matchWholeWord,
                ref matchWildCards, ref matchSoundLike,
                ref nmatchAllforms, ref forward,
                ref wrap, ref format, ref replaceWithText,
                ref replace, ref matchKashida,
                ref matchDiactitics, ref matchAlefHamza,
                ref matchControl);
        }

        internal static Task<XpsDocument> GetXpsGetXpsDocumentAndSaveAsync(object filename, Action<word.Application> FindAndReplace)
        {
            return Task.Factory.StartNew(() =>
            {
                word.Application wordApp = new word.Application();
                object missing = Missing.Value;
                word.Document myWordDoc = null;

                if (File.Exists((string)filename))
                {
                    object readOnly = false;
                    object isVisible = false;
                    wordApp.Visible = false;
                    wordApp.Documents.Add(filename);
                    myWordDoc = wordApp.ActiveDocument;

                    FindAndReplace(wordApp);
                }
                else
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        MessageBox.Show("the file name doesnt exists","Message");
                    });
                    return null;
                }
                string newXPSDocumentName = String.Concat(Path.GetDirectoryName(filename.ToString()), "\\",
                                System.IO.Path.GetFileNameWithoutExtension(filename.ToString()),new Random().Next(1,1000), ".xps");
                XpsDocument xpsDocument = null;
                try
                {
                    xpsDocument = ConvertWordDocToXPSDoc(myWordDoc, newXPSDocumentName);
                    myWordDoc.Close();
                }
                catch
                {
                    return null;
                }
                finally
                {
                    wordApp.Quit();
                }

                return xpsDocument;
            });
        }
    }
}
