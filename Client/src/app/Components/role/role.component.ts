import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DxDataGridModule } from 'devextreme-angular';

import { ApiService } from '../../api.service';
import { APIPaths } from '../../api.service';
import { Role } from './role.model';

@Component({
  selector: 'app-role.component',
  imports: [CommonModule,
            DxDataGridModule],
  templateUrl: './role.component.html',
  styleUrl: './role.component.css'
})
export class RoleComponent implements OnInit {
  roles: Role[] = [];

  constructor(private api: ApiService) {}

  ngOnInit(): void {
    this.loadRoles();
  }

  loadRoles() {
    this.api.getAll<Role>(APIPaths.roles).subscribe(data => this.roles = data);
  }

  onRoleInserted(e: any) {
    this.api.create<Role>(APIPaths.roles, e.data).subscribe(() => this.loadRoles());
  }

  onRoleUpdated(e: any) {
    this.api.update<Role>(APIPaths.roles, e.key.roleId, e.data).subscribe(() => this.loadRoles());
  }

  onRoleRemoved(e: any) {
    this.api.delete<Role>(APIPaths.roles, e.key.roleID).subscribe(() => this.loadRoles());
  }
}
