using Flurl;
using Flurl.Http;
using System.Threading.Tasks;
using Zuozishi.Common.Libs.QingTui.Models;
using Zuozishi.Common.Libs.QingTui.Response;

namespace Zuozishi.Common.Libs.QingTui
{
    public class QtMemberAPI
    {
        private readonly QingTuiAPI _qtAPI;

        public QtMemberAPI(QingTuiAPI qtAPI)
        {
            _qtAPI = qtAPI;
        }

        /// <summary>
        /// 获取企业内所有成员
        /// </summary>
        /// <param name="page">请求的页数，从1开始</param>
        /// <param name="pageSize">分页返回时每页数据量，最大100</param>
        /// <returns></returns>
        public async Task<MembersResponse> GetAllMembersAsync(int page, int pageSize = 100)
        {
            var url = QingTuiAPI.QtHost.AppendPathSegment("team/member/all/paged")
                .SetQueryParam("page_size", pageSize)
                .SetQueryParam("request_page", page);
            return await _qtAPI.GetJsonAsync<MembersResponse>(url);
        }

        /// <summary>
        /// 获取组织机构内成员
        /// </summary>
        /// <param name="orgId">组织机构Id，值为root时，为第一级组织机构</param>
        /// <param name="page">请求的页数，从1开始</param>
        /// <param name="pageSize">分页返回时每页数据量，最大100</param>
        /// <returns></returns>
        public async Task<MembersResponse> GetOrgMembersAsync(string orgId, int page, int pageSize = 100)
        {
            var url = QingTuiAPI.QtHost.AppendPathSegment("team/member/org/paged")
                .SetQueryParam("org_id", orgId)
                .SetQueryParam("page_size", pageSize)
                .SetQueryParam("request_page", page);
            return await _qtAPI.GetJsonAsync<MembersResponse>(url);
        }

        /// <summary>
        /// 创建成员
        /// </summary>
        /// <param name="member">成员</param>
        /// <param name="password">初始用户密码</param>
        /// <returns></returns>
        public async Task<CreateMemberResponse> CreateMemberAsync(QtMember member, string password)
        {
            var url = QingTuiAPI.QtHost.AppendPathSegment("team/member/create/single");
            return await _qtAPI.PostUrlEncodedAsync<CreateMemberResponse>(url, new
            {
                name = member.Name,
                comment = member.Comment,
                mail = member.Mail,
                mobile = member.Mobile,
                org_list = member.OrgList,
                password = password,
                employee_id = member.EmployeeId
            });
        }

        /// <summary>
        /// 删除成员
        /// </summary>
        /// <param name="userId">企业内用户Id</param>
        /// <returns></returns>
        public async Task<ErrorResponse> DeleteMemberAsync(string userId)
        {
            var url = QingTuiAPI.QtHost.AppendPathSegment("team/member/delete/single");
            return await _qtAPI.PostUrlEncodedAsync<ErrorResponse>(url, new {
                user_id = userId
            });
        }

        /// <summary>
        /// 更新成员
        /// </summary>
        /// <param name="member">成员</param>
        /// <returns></returns>
        public async Task<ErrorResponse> UpdateMemberAsync(QtMember member)
        {
            var url = QingTuiAPI.QtHost.AppendPathSegment("team/member/update/single");
            return await _qtAPI.PostUrlEncodedAsync<ErrorResponse>(url, new
            {
                user_id = member.UserId,
                name = member.Name,
                comment = member.Comment,
                org_id = member.OrgId,
                employee_id = member.EmployeeId
            });
        }

        /// <summary>
        /// 获取用户详情
        /// </summary>
        /// <param name="userId">企业内用户Id</param>
        /// <returns></returns>
        public async Task<MemberResponse> GetMemberDetailAsync(string userId)
        {
            var url = QingTuiAPI.QtHost.AppendPathSegment("team/member/detail")
                .SetQueryParam("user_id", userId);
            return await _qtAPI.GetJsonAsync<MemberResponse>(url);
        }
    }
}
