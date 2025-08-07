import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from '../Environments/environment'

@Injectable({ providedIn: 'root' })
export class ApiService {

  constructor(private http: HttpClient) {}

   // Generic CRUD Methods
  getAll<T>(endpoint: string): Observable<T[]> {
    return this.http.get<T[]>(`${environment.baseUrl}/${endpoint}`);
  }

  getById<T>(endpoint: string, id: number): Observable<T> {
    return this.http.get<T>(`${environment.baseUrl}/${endpoint}/${id}`);
  }

  create<T>(endpoint: string, data: any): Observable<T> {
    return this.http.post<T>(`${environment.baseUrl}/${endpoint}`, data);
  }

  update<T>(endpoint: string, id: number, data: any): Observable<T> {
    return this.http.put<T>(`${environment.baseUrl}/${endpoint}/${id}`, data);
  }

  delete<T>(endpoint: string, id: number): Observable<T> {
    return this.http.delete<T>(`${environment.baseUrl}/${endpoint}/${id}`);
  }

  getUsers()        { return this.getAll('users'); }
  getRoles()        { return this.getAll('roles'); }
  getAppPages()     { return this.getAll('AppPages'); }
  getPermissions()  { return this.getAll('Permissions'); }

  addUser(data: any)        { return this.create('users', data); }
  addRole(data: any)        { return this.create('roles', data); }
  addAppPage(data: any)     { return this.create('AppPages', data); }
  addPermission(data: any)  { return this.create('Permissions', data); }

  updateUser(id: number, data: any)        { return this.update('users', id, data); }
  updateRole(id: number, data: any)        { return this.update('roles', id, data); }
  updateAppPage(id: number, data: any)     { return this.update('AppPages', id, data); }
  updatePermission(id: number, data: any)  { return this.update('Permissions', id, data); }

  deleteUser(id: number)        { return this.delete('users', id); }
  deleteRole(id: number)        { return this.delete('roles', id); }
  deleteAppPage(id: number)     { return this.delete('AppPages', id); }
  deletePermission(id: number)  { return this.delete('Permissions', id); }
}

