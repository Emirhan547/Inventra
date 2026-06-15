using Inventra.WebUI.Constants;
using Inventra.WebUI.Services.AuditLogServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inventra.WebUI.Controllers
{
    [Authorize(Roles = RoleGroups.AdminOnly)]

    public sealed class AuditLogController
     : Controller
    {
        private readonly IAuditLogService
            _auditLogService;

        public AuditLogController(
            IAuditLogService auditLogService)
        {
            _auditLogService =
                auditLogService;
        }

        public async Task<IActionResult>
            Index()
        {
            var data =
                await _auditLogService
                    .GetAllAsync();

            return View(data);
        }
    }
}