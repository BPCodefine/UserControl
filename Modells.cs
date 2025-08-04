namespace UserControl
{
    
    public class UserRole
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
    public class AppPage
    {
        public int PageId { get; set; }
        public required string PageName { get; set; }
        public required string AppName { get; set; }
        public required string Path { get; set; }
    }
    public class Permission
    {
        public int PermissionId { get; set; }
        public int PageId { get; set; }
        public int RoleId { get; set; }
        public bool CanView { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
    }
}
