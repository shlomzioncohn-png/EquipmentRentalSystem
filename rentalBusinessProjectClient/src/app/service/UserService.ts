import { inject, Injectable, signal, Signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../../models/User';
import { UserCreate } from '../../models/UserCreate';
import { LoginRequest } from '../../models/LoginRequest';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private apiUrl = 'https://localhost:7020/api/user';
  private pendingLoginData: LoginRequest | null = null;
  http: HttpClient = inject(HttpClient);
  currentUser = signal<User | null>(null);
  constructor() {
    this.loadUserFromStorage();
  }
  setPendingLogin(data: LoginRequest) {
    this.pendingLoginData = data;
  }

  getPendingLogin(): LoginRequest | null {
    const data = this.pendingLoginData;
    this.pendingLoginData = null; 
    return data;
  }

  setCurrentUser(user: User) {
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUser.set(user);
  }

  private loadUserFromStorage() {
    const savedUser = localStorage.getItem('user');
    if (savedUser) {
      try {
        this.currentUser.set(JSON.parse(savedUser));
      } catch (e) {
        localStorage.removeItem('user'); 
      }
    }
  }

  logout() {
    localStorage.removeItem('user');
    this.currentUser.set(null);
  }



  getAllUsers(): Observable<User[]> {
    return this.http.get<User[]>(this.apiUrl);
  }

  getUserById(id: string): Observable<User> {
    return this.http.get<User>(`${this.apiUrl}/${id}`);
  }

  createUser(user: UserCreate): Observable<User> {
    return this.http.post<User>(this.apiUrl, user);
  }

  updateUser(id: string, user: User): Observable<User> {
    return this.http.put<User>(`${this.apiUrl}/${id}`, user);
  }

  deleteUser(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  login(loginRequest: LoginRequest): Observable<User> {
    return this.http.post<User>(`${this.apiUrl}/login`, loginRequest);
  }
}