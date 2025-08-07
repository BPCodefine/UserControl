import { Component } from '@angular/core';
import { Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { UserComponent } from './Components/user.component/user.component';
import { RoleComponent } from './Components/role.component/role.component';
import { AppPageComponent } from './Components/app-page.component/app-page.component';
import { PermissionComponent } from './Components/permission.component/permission.component';

export const routes: Routes = [
  { path: '', component: AppComponent },
  { path: 'users', component: UserComponent},
  { path: 'roles', component: RoleComponent },
  { path: 'app-pages', component: AppPageComponent },
  { path: 'permissions', component: PermissionComponent }
];
