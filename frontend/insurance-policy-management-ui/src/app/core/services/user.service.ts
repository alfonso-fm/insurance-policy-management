import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';


@Injectable({ providedIn: 'root' })
export class UserService {
  private api = `${environment.apiUrl}/api/users`;

  constructor(private http: HttpClient) {}

  getProfile() {
    return this.http.get<any>(`${this.api}/profile`);
  }

  updateProfile(data: any) {
    return this.http.put(`${this.api}/profile`, data);
  }
}
