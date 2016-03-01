using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace BaoVietCore.Services
{
    public class StorageService : ServiceBase
    {
        StorageFolder tempFolder = ApplicationData.Current.TemporaryFolder;
        StorageFolder localFolder = ApplicationData.Current.LocalFolder;
        StorageFolder sharedLocalFolder = ApplicationData.Current.SharedLocalFolder;
        public StorageService(Manager man) : base(man)
        {

        }

        public async Task<StorageFile> CreateFile(StorageFolder folder, string filename, CreationCollisionOption option)
        {
            return await folder.CreateFileAsync(filename, option);
        }

        /// <summary>
        /// May throw exception on error
        /// </summary>
        /// <param name="file"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task WriteStringToFile(StorageFile file, string content)
        {
            try
            {
                await FileIO.WriteTextAsync(file, content);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<StorageFile> PickFileToOpen(params string[] extensions)
        {
            var picker = new FileOpenPicker();
            picker.ViewMode = PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;

            foreach (var ext in extensions)
            {
                picker.FileTypeFilter.Add(ext);
            }

            StorageFile file = await picker.PickSingleFileAsync();
            return file;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="defaultExt">limiteExt must not include it</param>
        /// <param name="limitExt"> e.g: ".txt" -> "Plain text"</param>
        /// <returns></returns>
        public async Task<StorageFile> PickFileToSave(string defaultFilename, string defaultExt, params KeyValuePair<string, string>[] limitExt)
        {
            var picker = new FileSavePicker();
            picker.DefaultFileExtension = defaultExt;
            picker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            picker.SuggestedFileName = defaultFilename;
            foreach (var ext in limitExt)
            {
                picker.FileTypeChoices.Add(ext.Value, new List<string>() { ext.Key });
            }
            StorageFile file = await picker.PickSaveFileAsync();
            return file;
        }
    }
}
