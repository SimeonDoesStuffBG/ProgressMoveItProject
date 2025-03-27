using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgressTestApp.Models
{
    class DisplaySettings
    {
        public int fileListPageSize { get; set; }
        public int liveViewPageSize { get; set; }
        public int userListPageSize { get; set; }
    }

    class UserInfo
    {
        public string authMethod { get; set; }
        public int defaultFolderID { get; set; }
        public DisplaySettings displaySettings { get; set; }
        public string email { get; set; }
        public string emailFormat { get; set; }
        public int? expirationPolicyID { get; set; }
        public int folderQuota { get; set; }
        public bool? forceChangePassword { get; set; }
        public string fullName { get; set; }
        public int homeFolderID { get; set; }
        public string id { get; set; }
        public string language { get; set; }
        public string lastLoginStamp { get; set; }
        public string notes { get; set; }
        public int orgID { get; set; }
        public string passwordChangeStamp { get; set; }
        public string permission { get; set; }
        public string receivesNotification { get; set; }
        public string status { get; set; }
        public string statusNote { get; set; }
        public int totalFileSize { get; set; }
        public string username { get; set; }
    }
}
