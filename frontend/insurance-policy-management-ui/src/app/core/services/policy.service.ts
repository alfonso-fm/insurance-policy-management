import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environments';

@Injectable({
  providedIn: 'root'
})
export class PolicyService {
  private api = `${environment.apiUrl}/api/policies`;

  constructor(private http: HttpClient) {}

  getMyPolicies() {
    return this.http.get<any[]>(`${this.api}/my`);
  }
}
