import { Component, OnInit } from '@angular/core';
import { PolicyService } from '../../core/services/policy.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  policies: any[] = [];
  loading = true;

  constructor(private policyService: PolicyService) {}

  ngOnInit(): void {
    this.policyService.getMyPolicies().subscribe({
      next: data => {
        this.policies = data;
        this.loading = false;
      },
      error: () => {
        this.loading = false;
      }
    });
  }
}
