//http://geekswithblogs.net/tonyt/archive/2006/07/29/86608.aspx

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using WIA;

namespace VietOCR.NET.WIA
{
    public enum WiaScannerError : uint
    {
        LibraryNotInstalled = 0x80040154,
        OutputFileExists = 0x80070050,
        ScannerNotAvailable = 0x80210015,
        OperationCancelled = 0x80210064
    }

    public sealed class WiaScannerAdapter : IDisposable
    {
         private CommonDialogClass _wiaManager;
         private bool _disposed; // indicates if Dispose has been called

         public WiaScannerAdapter()
         {
         }

         ~WiaScannerAdapter()
         {
              Dispose(false);
         }

         private CommonDialogClass WiaManager
         {
              get { return _wiaManager; }
              set { _wiaManager = value; }
         }

         [System.Diagnostics.DebuggerNonUserCodeAttribute()]
         [SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
         public Image ScanImage(ImageFormat outputFormat, string fileName)
         {
              if (outputFormat == null)
                   throw new ArgumentNullException("outputFormat");

              FileIOPermission filePerm = new FileIOPermission(FileIOPermissionAccess.AllAccess, fileName);
              filePerm.Demand();

              ImageFile imageObject = null;

              try
              {
                   if (WiaManager == null)
                        WiaManager = new CommonDialogClass();

                   imageObject =
                        WiaManager.ShowAcquireImage(WiaDeviceType.ScannerDeviceType,
                             WiaImageIntent.GrayscaleIntent, WiaImageBias.MaximizeQuality, 
                             outputFormat.Guid.ToString("B"), false, true, true);

                   imageObject.SaveFile(fileName);
                   return Image.FromFile(fileName);
              }
              catch (COMException ex)
              {
                   string message = "Error scanning image";
                   throw new WiaOperationException(message, ex);
              }
              finally
              {
                   if (imageObject != null)
                        Marshal.ReleaseComObject(imageObject);
              }
         }

         public void Dispose()
         {
              Dispose(true);
              GC.SuppressFinalize(this);
         }

         private void Dispose(bool disposing)
         {
              if (!_disposed)
              {
                   if (disposing)
                   {
                        // no managed resources to cleanup
                   }

                  // cleanup unmanaged resources
                  if (_wiaManager != null)
                       Marshal.ReleaseComObject(_wiaManager);

                  _disposed = true;
              }
         }
    }
}
