using BaoVietCore.Helpers;
using BaoVietCore.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

namespace BaoVietCore.Services
{
    public class ImageService : ServiceBase, IImageService
    {
        /// <summary>
        /// Fired when the image is downloaded.
        /// </summary>
        public event EventHandler<EventArgs> OnDownloadComplete
        {
            add { _OnDownloadComplete.Add(value); }
            remove { _OnDownloadComplete.Remove(value); }
        }
        internal SmartWeakEvent<EventHandler<EventArgs>> _OnDownloadComplete = new SmartWeakEvent<EventHandler<EventArgs>>();

        public ImageService(Manager man) : base(man)
        {

        }

        public async Task SaveImageToStorage(string url)
        {

            // If not we have to get the image
            Stream imgStream = await Manager.WebService.MakeRawGetRequest(url);

            // Get the photos library
            StorageLibrary myPictures = await Windows.Storage.StorageLibrary.GetLibraryAsync(Windows.Storage.KnownLibraryId.Pictures);

            // Get the save folder
            StorageFolder saveFolder = myPictures.SaveFolder;

            // Try to find the saved pictures folder
            StorageFolder savedPicturesFolder = null;
            IReadOnlyList<StorageFolder> folders = await saveFolder.GetFoldersAsync();
            foreach (StorageFolder folder in folders)
            {
                if (folder.DisplayName.Equals("Saved Pictures"))
                {
                    savedPicturesFolder = folder;
                }
            }

            // If not found create it.
            if (savedPicturesFolder == null)
            {
                savedPicturesFolder = await saveFolder.CreateFolderAsync("Saved Pictures");
            }

            // Write the file.
            StorageFile file = await savedPicturesFolder.CreateFileAsync($"Ảnh tải về từ Báo Việt {DateTime.Now.ToString("MM-dd-yy H.mm.ss")}.jpg", CreationCollisionOption.GenerateUniqueName);
            using (var fileStream = await file.OpenStreamForWriteAsync())
            {
                await imgStream.CopyToAsync(fileStream);
            }

            //Raise event when download completed
            _OnDownloadComplete.Raise(this, new EventArgs());
        }
    }
}
