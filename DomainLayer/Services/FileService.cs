using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TheMailClient.Domain.Model;

using TheMailClient.Storage;
using TheMailClient.Storage.DTO;

namespace TheMailClient.Domain.Services
{
    public class FileService
    {
        private static FileService instance = new FileService();

        private FileService()
        {
        }

        public static FileService getInstance()
        {
            return instance;
        }

        public List<File> GetFiles(User u)
        {
            List<File> files = new List<File>();

            foreach (SyncAccount acc in u.accounts)
                foreach (FileDTO file in MailDataStore.getInstance().GetFiles(acc.Namespace))
                    files.Add(Utils.Utils.getInstance().toDomain(file));

            return files;
        }

        public File getFile(string file, User u)
        {
            foreach (SyncAccount acc in u.accounts)
            {
                File f = Utils.Utils.getInstance().toDomain(MailDataStore.getInstance().GetFile(file, acc.Namespace));
                if (f != null)
                    return f;
            }

            return null;
        }

        public File getFile(string file, SyncAccount acc)
        {
            return Utils.Utils.getInstance().toDomain(MailDataStore.getInstance().GetFile(file, acc.Namespace));
        }

        public File UploadFile(string path, SyncAccount acc)
        {
            return Utils.Utils.getInstance().toDomain(MailDataStore.getInstance().UploadFile(path, acc.Namespace));
        }
    }
}