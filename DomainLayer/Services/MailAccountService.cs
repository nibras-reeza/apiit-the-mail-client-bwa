/*****************************************************************************
 * This file contains a C# class that provides storage service to the presentation
 * layer.
 *
 * Author: Nibras Ahamed Reeza (CB004641)
 * E-Mail: nibras.ahamed@gmail.com
 *
 * Last Modified: 28/08/2014
 *
 *
 * ---------------
 * Version History
 * ---------------
 *
 * 28/08/2014  Created file
 *
******************************************************************************/

using System.Collections.Generic;

using TheMailClient.Domain.Model;
using TheMailClient.Storage;
using TheMailClient.Storage.DTO;

namespace TheMailClient.Domain.Services
{
    public class MailAccountService
    {
        private static MailAccountService instance = new MailAccountService();

        private MailAccountService()
        {
        }

        public static MailAccountService getInstance()
        {
            return instance;
        }

        public SyncAccount getAccountByNameSpace(string p)
        {
            AccountDTO dto = MailDataStore.getInstance().GetMailAccount(p);

            return Utils.Utils.getInstance().toDomain(dto);
        }

        public List<DraftMessage> GetAllDrafts(SyncAccount acc)
        {
            List<DraftMessage> drafts = new List<DraftMessage>();

            foreach (DraftMessageDTO d in MailDataStore.getInstance().GetAllDrafts(acc.Namespace))
                drafts.Add(Utils.Utils.getInstance().toDomain(d));

            return drafts;
        }

        public List<Thread> getAllThreads(SyncAccount acc, int limit, int set)
        {
            List<Thread> threads = new List<Thread>();

            foreach (ThreadDTO d in MailDataStore.getInstance().getAllThreads(acc.Namespace, limit, set))
                threads.Add(Utils.Utils.getInstance().toDomain(d));

            return threads;
        }

        public List<Thread> getAllThreads(SyncAccount acc, int limit, int set, string query, string[] fields)
        {
            List<Thread> threads = new List<Thread>();

            foreach (ThreadDTO d in MailDataStore.getInstance().getAllThreads(acc.Namespace, limit, set, query, fields))
                threads.Add(Utils.Utils.getInstance().toDomain(d));

            return threads;
        }

        public DraftMessage GetDraft(SyncAccount acc, string draftid)
        {
            return Utils.Utils.getInstance().toDomain(MailDataStore.getInstance().GetDraft(acc.Namespace, draftid));
        }

        public SyncAccount GetMailAccount(string p)
        {
            return Utils.Utils.getInstance().toDomain(MailDataStore.getInstance().GetMailAccount(p));
        }

        public Message GetMessage(string message, SyncAccount acc)
        {
            return Utils.Utils.getInstance().toDomain(MailDataStore.getInstance().GetMessage(message, acc.Namespace));
        }

        public Thread getThread(string thread, SyncAccount acc)
        {
            return Utils.Utils.getInstance().toDomain(MailDataStore.getInstance().getThread(thread, acc.Namespace));
        }

        public void MarkRead(SyncAccount acc, Message message)
        {
            MailDataStore.getInstance().MarkRead(acc.Namespace, Utils.Utils.getInstance().toDTO(message));
        }

        public List<Message> SearchMessages(SyncAccount acc, int limit, int set, string query, string[] fields)
        {
            List<Message> msgs = new List<Message>();

            foreach (MessageDTO m in MailDataStore.getInstance().SearchMessages(acc.Namespace, limit, set, query, fields))
                msgs.Add(Utils.Utils.getInstance().toDomain(m));

            return msgs;
        }

        public DraftMessage Send(DraftMessage draft)
        {
            return Utils.Utils.getInstance().toDomain(MailDataStore.getInstance().Send(Utils.Utils.getInstance().toDTO(draft), draft.account.Namespace));
        }

        public DraftMessage SendDraft(SyncAccount acc, string draftid, string version)
        {
            return Utils.Utils.getInstance().toDomain(MailDataStore.getInstance().SendDraft(acc.Namespace, draftid, version));
        }

        public DraftMessage Update(DraftMessage draft)
        {
            return Utils.Utils.getInstance().toDomain(MailDataStore.getInstance().Update(Utils.Utils.getInstance().toDTO(draft), draft.account.Namespace));
        }

        public Thread UpdateThread(Thread thread)
        {
            return Utils.Utils.getInstance().toDomain(MailDataStore.getInstance().UpdateThread(Utils.Utils.getInstance().toDTO(thread)));
        }
    }
}