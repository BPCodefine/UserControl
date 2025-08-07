export interface Permission {
  permissionID: number;
  pageID: number;
  roleID: number;
  canView: boolean;
  canEdit: boolean;
  canDelete: boolean;
}
