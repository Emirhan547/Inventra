namespace Inventra.WebUI.Constants
{
    public static class RoleGroups
    {
        public const string AdminOnly =
            Roles.Admin;

        public const string AdminAndManager =
            Roles.Admin + "," + Roles.Manager;

        public const string AllUsers =
            Roles.Admin + "," +
            Roles.Manager + "," +
            Roles.Employee;
    }
}
