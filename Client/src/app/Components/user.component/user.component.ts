import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DxDataGridModule } from 'devextreme-angular';

import { ApiService } from '../../api.service';
import { APIPaths } from '../../api.service';
import { User } from './user.model';

@Component({
  selector: 'app-user.component',
  imports: [CommonModule,
            DxDataGridModule],
  templateUrl: './user.component.html',
  styleUrl: './user.component.css'
})
export class UserComponent implements OnInit{
  users: User[] = [];

  constructor(private api: ApiService) {}

  ngOnInit(): void {
    this.loadUsers();
  }

  loadUsers() {
    this.api.getAll<User>(APIPaths.users).subscribe(data => this.users = data);
  }

  onUserInserted(e: any) {
    this.api.create<User>(APIPaths.users, e.data).subscribe(() => this.loadUsers());
  }

  onUserUpdated(e: any) {
    this.api.update<User>(APIPaths.users, e.key.userId, e.data).subscribe(() => this.loadUsers());
  }

  onUserRemoved(e: any) {
    this.api.delete<User>(APIPaths.users, e.key.userId).subscribe(() => this.loadUsers());
  }
}
