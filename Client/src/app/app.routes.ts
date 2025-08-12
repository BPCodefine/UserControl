import { Routes } from '@angular/router';
import { UserComponent } from './Components/user/user.component';
import { RoleComponent } from './Components/role/role.component';
import { UserRoleComponent } from './Components/user-role/user-role.component';
import { AppPageComponent } from './Components/app-page/app-page.component';
import { PermissionComponent } from './Components/permission/permission.component';

export const routes: Routes = [
  { path: 'users', component: UserComponent },
  { path: 'roles', component: RoleComponent },
  { path: 'userroles', component: UserRoleComponent   },
  { path: 'apppages', component: AppPageComponent },
  { path: 'permissions', component: PermissionComponent }
];
