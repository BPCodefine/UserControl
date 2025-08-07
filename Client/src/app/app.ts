import { Component, signal } from '@angular/core';
import { RouterModule, RouterOutlet } from '@angular/router';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet,RouterModule,
    MatSidenavModule,
    MatToolbarModule,
    MatListModule,
    MatIconModule,
    MatButtonModule,],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('Client');

  menuItems: any[] = [
    { icon: 'person', label: 'Users', route: 'users' },
    { icon: 'shield', label: 'Roles', route: 'roles' },
    { icon: 'admin_panel_settings', label: 'User-Roles', route: 'userroles' },
    { icon: 'apps', label: 'AppPages', route: 'apppages' },
    { icon: 'security', label: 'permissions', route: 'Permissions' },
  ];
}
