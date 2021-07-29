using Flurl;
using Flurl.Http;
using System.Threading.Tasks;
using Zuozishi.Common.Libs.QingTui.Models;
using Zuozishi.Common.Libs.QingTui.Response;

namespace Zuozishi.Common.Libs.QingTui
{
    public class QtOrgAPI
    {
        private readonly QingTuiAPI _qtAPI;

        public QtOrgAPI(QingTuiAPI qtAPI)
        {
            _qtAPI = qtAPI;
        }

        /// <summary>
        /// 获取企业Id
        /// </summary>
        /// <param name="number">企业号，可在管理后台中企业管理模块中查看</param>
        /// <returns></returns>
        public async Task<DomainIdResponse> GetDomainIdAsync(string number)
        {
            var url = QingTuiAPI.QtHost.AppendPathSegment("team/domain/id/get")
                .SetQueryParam("number", number);
            return await _qtAPI.GetJsonAsync<DomainIdResponse>(url);
        }

        /// <summary>
        /// 创建组织机构
        /// </summary>
        /// <param name="parentId">创建的组织机构的父机构id，顶级为root</param>
        /// <param name="name">组织机构名字</param>
        /// <returns></returns>
        public async Task<OrgIdResponse> CreateOrgAsync(string parentId, string name)
        {
            var url = QingTuiAPI.QtHost.AppendPathSegment("team/org/create");
            return await _qtAPI.PostUrlEncodedAsync<OrgIdResponse>(url, new
            {
                parent_id = parentId,
                name = name
            });
        }

        /// <summary>
        /// 删除组织机构
        /// </summary>
        /// <param name="orgId">创建的组织机构的父机构id，顶级为root</param>
        /// <returns></returns>
        public async Task<ErrorResponse> DeleteOrgAsync(string orgId)
        {
            var url = QingTuiAPI.QtHost.AppendPathSegment("team/org/delete");
            return await _qtAPI.PostUrlEncodedAsync<ErrorResponse>(url, new { 
                org_id = orgId
            });
        }

        /// <summary>
        /// 修改组织机构
        /// </summary>
        /// <param name="org">组织</param>
        /// <param name="autoSequence">true或者false，是否自动排序，默认为false不开启，如果开启此值，原组织中若已有待插入的sequence值，则会将原有值进行变更，以避免sequence出现重复</param>
        /// <returns></returns>
        public async Task<ErrorResponse> UpdateOrgAsync(QtOrg org, bool autoSequence = false)
        {
            var url = QingTuiAPI.QtHost.AppendPathSegment("team/org/update");
            return await _qtAPI.PostUrlEncodedAsync<ErrorResponse>(url, new
            {
                org_id = org.Id,
                name = org.Name,
                sequence = org.Sequence,
                autoSequence = autoSequence
            });
        }

        /// <summary>
        /// 分页获取组织机构列表
        /// </summary>
        /// <param name="orgId">组织机构id，顶级为root。获取此id的子组织机构信息；如果不填，则拉取整个组织机构</param>
        /// <param name="page">请求的页码，从1开始</param>
        /// <param name="pageSize">请求的页码数据量，最大100</param>
        /// <returns></returns>
        public async Task<OrgsResponse> GetOrgsAsync(string orgId, int page, int pageSize = 100)
        {
            var url = QingTuiAPI.QtHost.AppendPathSegment("team/org/paged")
                .SetQueryParam("org_id", orgId)
                .SetQueryParam("page_size", pageSize)
                .SetQueryParam("request_page", page);
            return await _qtAPI.GetJsonAsync<OrgsResponse>(url);
        }

        /// <summary>
        /// 获取组织机构详情
        /// </summary>
        /// <param name="orgId">组织机构id，root在此处不可用</param>
        /// <returns></returns>
        public async Task<OrgResponse> GetOrgDetail(string orgId)
        {
            var url = QingTuiAPI.QtHost.AppendPathSegment("team/member/detail")
                .SetQueryParam("org_id", orgId);
            return await _qtAPI.GetJsonAsync<OrgResponse>(url);
        }
    }
}
