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
    public class TagService
    {
        private static TagService instance = new TagService();

        private TagService()
        {
        }

        public static TagService getInstance()
        {
            return instance;
        }

        public Tag getTagByName(string name, SyncAccount a)
        {
            Tag tg = Utils.Utils.getInstance().toDomain(MailDataStore.getInstance().getTag(name, a.Namespace));
            if (!tg.Name.EndsWith("deletedwithtmc"))
                return tg;
            else
                return null;
        }

        public Tag getTagById(string name, SyncAccount a)
        {
            Tag tg = Utils.Utils.getInstance().toDomain(MailDataStore.getInstance().getTag(name, a.Namespace));
            if (!tg.Name.EndsWith("deletedwithtmc"))
                return tg;
            else
                return null;
        }

        public List<Tag> getTags(SyncAccount a)
        {
            List<Tag> tags = new List<Tag>();

            foreach (TagDTO tag in MailDataStore.getInstance().getAllTags(a.Namespace))
            {
                Tag tg = Utils.Utils.getInstance().toDomain(tag);
                if (!tg.Name.EndsWith("deletedwithtmc"))
                    tags.Add(tg);
            }
            return tags;
        }

        public Tag Add(Tag tag, SyncAccount acc)
        {
            Tag tg = Utils.Utils.getInstance().toDomain(MailDataStore.getInstance().add(Utils.Utils.getInstance().toDTO(tag), acc.Namespace));

            return tg;
        }

        public Tag Update(Tag tag, SyncAccount acc)
        {
            return Utils.Utils.getInstance().toDomain(MailDataStore.getInstance().update(Utils.Utils.getInstance().toDTO(tag), acc.Namespace));
        }

        public Tag Delete(Tag tag, SyncAccount acc)
        {
            tag.Name = tag.Name + "deletedwithtmc";
            return Update(tag, acc);
        }
    }
}