import { Routes } from '@angular/router';
import { UserComponent } from './Components/user.component/user.component';
import { RoleComponent } from './Components/role.component/role.component';
import { UserRoleComponent } from './Components/user-role.component/user-role.component';
import { AppPageComponent } from './Components/app-page.component/app-page.component';
import { PermissionComponent } from './Components/permission.component/permission.component';

export const routes: Routes = [
  { path: 'users', component: UserComponent },
  { path: 'roles', component: RoleComponent },
  { path: 'userroles', component: UserRoleComponent   },
  { path: 'apppages', component: AppPageComponent },
  { path: 'permissions', component: PermissionComponent }
];
