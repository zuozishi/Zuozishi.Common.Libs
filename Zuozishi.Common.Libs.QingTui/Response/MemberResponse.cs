using Newtonsoft.Json;
using Zuozishi.Common.Libs.QingTui.Models;

namespace Zuozishi.Common.Libs.QingTui.Response
{
    public class MemberResponse : ErrorResponse
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("mail")]
        public string Mail { get; set; }

        [JsonProperty("avatar")]
        public string Avatar { get; set; }

        [JsonProperty("comment")]
        public string Comment { get; set; }

        [JsonProperty("guest")]
        public string Guest { get; set; }

        [JsonProperty("user_id")]
        public string UserId { get; set; }

        [JsonProperty("mobile")]
        public string Mobile { get; set; }

        [JsonProperty("org_list")]
        public List<string> OrgList { get; set; }

        [JsonProperty("org_id")]
        public string OrgId { get; set; }

        [JsonProperty("employee_id")]
        public string EmployeeId { get; set; }

        public QtMember GetQtMember()
        {
            return (QtMember)MemberwiseClone();
        }
    }
}
