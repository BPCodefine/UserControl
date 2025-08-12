import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DxDataGridModule } from 'devextreme-angular';

import { ApiService } from '../../api.service';
import { APIPaths } from '../../api.service';
import { User } from '../user/user.model';
import { Role } from '../role/role.model';
import { UserRole } from './userrole.model';

@Component({
  selector: 'app-user-role.component',
  imports: [CommonModule,
            DxDataGridModule],
  templateUrl: './user-role.component.html',
  styleUrl: './user-role.component.css'
})
export class UserRoleComponent implements OnInit {
  userRoles: UserRole[] = [];
  user: User[] = [];
  role: Role[] = [];

  constructor(private api: ApiService) {}

  ngOnInit(): void {
    this.loadUserRoles();
  }

  setStateValue(this: DxDataGridTypes.Column, newData: Employee, value: number, currentRowData: Employee) {
    newData.CityID = null;
    this.defaultSetCellValue?.(newData, value, currentRowData);
  }
}

  loadUserRoles() {
    this.api.getAll<User>(APIPaths.users).subscribe(data => this.user = data);
    this.api.getAll<Role>(APIPaths.roles).subscribe(data => this.role = data);

    this.api.getAll<UserRole>(APIPaths.userroles).subscribe(data => this.userRoles = data);
  }

  onRoleInserted(e: any) {
    this.api.create<UserRole>(APIPaths.userroles, e.data).subscribe(() => this.loadUserRoles());
  }

  onRoleUpdated(e: any) {
    this.api.update<UserRole>(APIPaths.userroles, e.key.roleId, e.data).subscribe(() => this.loadUserRoles());
  }

  onRoleRemoved(e: any) {
    this.api.delete<UserRole>(APIPaths.userroles, e.key.roleID).subscribe(() => this.loadUserRoles());
  }

}
