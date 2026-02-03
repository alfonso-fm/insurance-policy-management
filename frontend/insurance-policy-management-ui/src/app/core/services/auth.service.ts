import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private api = 'http://localhost:5090/api/auth';

  constructor(private http: HttpClient) {}

  login(email: string, password: string) {
    console.log('ACA')
    return this.http.post<any>(`${this.api}/login`, {
      email,
      password
    });
  }

  saveToken(token: string) {
    localStorage.setItem('token', token);
  }

  get token(): string | null {
    return localStorage.getItem('token');
  }

  logout() {
    localStorage.removeItem('token');
  }

  get payload(): any | null {
    const token = this.token;
    if (!token) return null;

    const payload = token.split('.')[1];
    return JSON.parse(atob(payload));
  }

  get role(): string | null {
    const payload = this.payload;
    return payload?.role || payload?.['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
  }

  get isAuthenticated(): boolean {
    return !!this.token;
  }
}
