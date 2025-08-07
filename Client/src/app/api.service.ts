import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from './Environments/environment'

@Injectable({ providedIn: 'root' })
export class ApiService {

  constructor(private http: HttpClient) {}

  getAll<T>(endpoint: string): Observable<T[]> {
    return this.http.get<T[]>(`${environment.baseUrl}/${endpoint}`);
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
}

export const APIPaths = {
  users: 'users',
  roles: 'roles',
  userroles: 'userroles',
  appPages: 'apppages',
  Permissions: 'permissions'
};

