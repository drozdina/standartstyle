using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Standartstyle.App_Code.Util
{
    public enum ActionTypes
    {
        Info = 1,
        Error = 2,
        AccountExpired = 3
    }
    public class Logger
    {
        public const int MaxDescriptionSize = 4000;

        private static readonly ILog log4net = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region SQL
        private const string WRITE_DB_LOG = @"INSERT INTO [TOTALLOG] (ACTIONCODE, USERCODE, ACTIONDATE, DESCRIPTION)
                                             VALUES (?, ?, ?, ?)";
        #endregion

        #region Private
        //static void WriteToDataBase(string message, Exception ex, ActionTypes type, int? userCode = null, int? custCode = null)
        //{
        //    try
        //    {
        //        switch (type)
        //        {
        //            case ActionTypes.Info:
        //                log4net.Info(message, ex);
        //                break;
        //            case ActionTypes.Error:
        //                log4net.Error(message, ex);
        //                break;
        //        }
        //        int? userCodeForLog = (userCode > 0) ? userCode :
        //            (UserSession.Current != null && UserSession.Current.User != null) ? (int?)UserSession.Current.User.Code : null;
        //        int? custCodeForLog = (custCode > 0) ? custCode :
        //            (UserSession.Current != null && UserSession.Current.User != null) ? (int?)UserSession.Current.User.CustomerCode : null;

        //        string userCustomer = "";
        //        if (userCodeForLog != null)
        //        {

        //            if (custCodeForLog != null)
        //                userCustomer = string.Format("User: {0}, Customer: {1}.", userCodeForLog, (Constants.CustomerCode)custCodeForLog);
        //            else
        //                userCustomer = string.Format("User: {0}.", userCodeForLog);
        //        }
        //        else
        //            if (custCodeForLog != null)
        //            userCustomer = string.Format("Customer: {0}.", (Constants.CustomerCode)custCodeForLog);

        //        string stackTrace = "";
        //        GetFullStackTrace(ex, ref stackTrace);

        //        string m = "";
        //        if (ex != null)
        //        {
        //            CreateMailAboutException(ex.Message, stackTrace);
        //            if (message != null)
        //                m = string.Format("{0} {1}\n {2}\n {3}", message, userCustomer, ex.Message, stackTrace);
        //            else
        //                m = string.Format("{0} {1}\n {2}", ex.Message, userCustomer, stackTrace);
        //        }
        //        else
        //            m = string.Format("{0} {1}", message, userCustomer); ;

        //        if (m.Length > MaxDescriptionSize)
        //            m = m.Substring(0, MaxDescriptionSize - 1);
        //        using (DbConnection conn = Connection.GetConnection(Constants.MAIN_CONNECTION))
        //        {
        //            DbCommand cmd = conn.CreateCommand(WRITE_DB_LOG);
        //            cmd.SetIntParam(0, (int)type);
        //            if (userCodeForLog == null)
        //                cmd.SetParam(1, DBNull.Value);
        //            else
        //                cmd.SetIntParam(1, userCodeForLog.Value);
        //            cmd.SetParam(2, DateTime.UtcNow);
        //            cmd.SetStringParam(3, m);
        //            if (custCodeForLog == null)
        //                cmd.SetParam(4, DBNull.Value);
        //            else
        //                cmd.SetIntParam(4, custCodeForLog.Value);
        //            cmd.Execute();
        //        }

        //        //if (ex.InnerException != null)
        //        //{
        //        //   WriteToDataBase(ex.InnerException.Message, ex.InnerException, type, isCalledByService);
        //        //}
        //    }
        //    catch (Exception exInternal)
        //    {
        //        log4net.Error(exInternal);
        //    }
        //}

        static void CreateMailAboutException(string message, string stackTrace)
        {
            try
            {
                /* SynMailTask mail = new SynMailTask(0);
                 mail.MailTaskTypeCode = (int)Constants.MailTaskTypeCode.NotSent;

                 string mailSubject = System.Configuration.ConfigurationManager.AppSettings.Get("faultMailSubject");
                 if (mailSubject != null)
                     mail.Subject = mailSubject;
                 else
                     mail.Subject = "An exception occured";

                 mail.Message = string.Format("Message:\n{0}\nStackTrace:{1}", message, stackTrace).Replace("\n", "<br/>");
                 mail.SendDate = DateTime.UtcNow;

                 int mailTaskCode = MailTaskRepository.AddMailTasks(mail);

                 var emails = System.Configuration.ConfigurationManager.AppSettings["emailFault"];
                 if (!String.IsNullOrEmpty(emails))
                 {
                     foreach (var email in emails.Split(new char[] { ',' }))
                     {
                         SynMailRecipient recip = new SynMailRecipient(0);
                         recip.MailTaskCode = mailTaskCode;
                         recip.Mail = email;

                         MailRecipientRepository.AddMailRecipient(recip);
                     }
                 }*/
            }
            catch (Exception ex)
            {
                log4net.Error(ex);
            }
        }

        static void GetFullStackTrace(Exception ex, ref string stackTrace)
        {
            if (ex != null)
            {
                GetFullStackTrace(ex.InnerException, ref stackTrace);
                stackTrace += ex.StackTrace;
            }
        }
        #endregion


        public static void DebugError(string message)
        {
            log4net.Error(message);
            Debug.WriteLine(message);
        }

        public static void DebugError(string message, Exception ex)
        {
            log4net.Error(message, ex);
            string logText = string.Format("{0}: {1}\n {2}", message, ex.Message, ex.StackTrace);
            DebugError(logText);
        }

        public static void DebugError(Exception ex)
        {
            string logText = string.Format("{0}\n {1}", ex.Message, ex.StackTrace);
            DebugError(logText);
        }

        public static void WriteError(string message, int? userCode = null, int? custCode = null)
        {
            WriteError(message, null, userCode, custCode);
        }

        public static void WriteError(Exception ex, int? userCode = null, int? custCode = null)
        {
            WriteError(ex.Message, ex, userCode, custCode);
        }

        public static void WriteAccountExpiredLog(string message, int userCode)
        {
            //WriteToDataBase(message, null, ActionTypes.AccountExpired, userCode);
        }

        public static void WriteError(string message, Exception ex, int? userCode = null, int? custCode = null)
        {
            //WriteToDataBase(message, ex, ActionTypes.Error, userCode, custCode);
        }

        public static void WriteInfo(string message, int? userCode = null, int? custCode = null)
        {
            //WriteToDataBase(message, null, ActionTypes.Info, userCode, custCode);
        }

        public static void WriteInfo(string message, ActionTypes actionType, int? userCode = null, int? custCode = null)
        {
            // WriteToDataBase(message, null, actionType, userCode, custCode);
        }

        public static void DebugMessage(object message)
        {
            log4net.Debug(message);
        }

        public static void SafelySaveLogger(string message, UserData user, ActionTypes typeForLog, int? typeForMessage = null)
        {
            try
            {
                if (user != null)
                {
                    //switch (typeForLog)
                    //{
                    //   case ActionTypes.Info:
                    //    case ActionTypes.ApprovalWorkflow:
                    //        if (typeForMessage != null)
                    //            WriteInfo(message, (ActionTypes)typeForMessage, user.Code, user.CustomerCode);
                    //        else
                    //            WriteInfo(message, user.Code, user.CustomerCode);
                    //        break;
                    //    case ActionTypes.Error:
                    //        WriteError(message, user.Code, user.CustomerCode);
                    //        break;
                    //}
                }
                else
                    WriteError(string.Format("User is null, but message for Logger could be: {0}", message));
            }
            catch (Exception ex)
            {
                WriteError(ex);
            }
        }
    }
}