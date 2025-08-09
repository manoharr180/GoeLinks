using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

public abstract class AppControllerBase : ControllerBase
{
    protected int? CurrentUserId
    {
        get
        {
            var userIdClaim = User?.FindFirst("ProfileId");
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
                return userId;
            return null;
        }
    }
}