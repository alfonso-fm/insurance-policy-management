import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PolicyService {
  private api = `${environment.apiUrl}/api/policies`;

  constructor(private http: HttpClient) {}

  getMyPolicies() {
    return this.http.get<any[]>(`${this.api}/my`);
  }

  getAll() {
    return this.http.get<any[]>(this.api);
  }

  getById(id: string) {
    return this.http.get<any>(`${this.api}/${id}`);
  }

  create(policy: any) {
    return this.http.post(this.api, policy);
  }

  update(id: string, policy: any) {
    return this.http.put(`${this.api}/${id}`, policy);
  }

  cancel(id: string) {
    return this.http.delete(`${this.api}/${id}`);
  }
}
