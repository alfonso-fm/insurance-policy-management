import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class PolicyService {
  private api = 'http://localhost:5090/api/policies';

  constructor(private http: HttpClient) {}

  getMyPolicies() {
    return this.http.get<any[]>(`${this.api}/my`);
  }
}
