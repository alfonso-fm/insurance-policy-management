import { Component } from '@angular/core';
import { PolicyService } from '../../core/services/policy.service';

@Component({
  selector: 'app-my-policies',
  templateUrl: './my-policies.component.html',
  styleUrls: ['./my-policies.component.scss']
})
export class MyPoliciesComponent {
  policies: any[] = [];

  constructor(private policyService: PolicyService) {}

  ngOnInit(): void {
    this.policyService.getMyPolicies().subscribe(data => {
      this.policies = data;
    });
  }

  cancelPolicy(id: string) {
    if (!confirm('Cancel this policy?')) return;

    this.policyService.cancel(id).subscribe(() => {
      this.policies = this.policies.map(p =>
        p.id === id ? { ...p, status: 'Cancelled' } : p
      );
    });
  }
}
