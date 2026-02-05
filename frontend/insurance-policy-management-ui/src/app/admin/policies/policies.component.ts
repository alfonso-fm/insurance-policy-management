import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { PolicyService } from 'src/app/core/services/policy.service';

@Component({
  selector: 'app-policies',
  templateUrl: './policies.component.html',
  styleUrls: ['./policies.component.scss']
})
export class PoliciesComponent implements OnInit {
  policyTypes: Record<number, string> = {
    1: 'Life',
    2: 'Auto',
    3: 'Health',
    4: 'Home'
  };

  policyStatus: Record<number, string> = {
    1: 'Active',
    2: 'Cancelled'
  };
  policies: any[] = [];

  // valores del UI
  selectedType: string = '';
  selectedStatus: string = '';

  // valores aplicados
  appliedType: string = '';
  appliedStatus: string = '';

  constructor(
    private policyService: PolicyService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadPolicies();
  }

  loadPolicies(): void {
    this.policyService.getAll().subscribe({
      next: (data) => this.policies = data,
      error: () => alert('Error loading policies')
    });
  }


  applyFilters(): void {
    this.appliedType = this.selectedType;
    this.appliedStatus = this.selectedStatus;
  }

  clearFilters(): void {
    this.selectedType = '';
    this.selectedStatus = '';
    this.appliedType = '';
    this.appliedStatus = '';
  }

  // navegación
  createPolicy(): void {
    this.router.navigate(['/admin/policies/new']);
  }

  editPolicy(id: string): void {
    this.router.navigate(['/admin/policies/edit', id]);
  }

  cancelPolicy(id: string): void {
    if (!confirm('Are you sure you want to cancel this policy?')) return;

    this.policyService.cancel(id).subscribe({
      next: () => this.loadPolicies(),
      error: () => alert('Error cancelling policy')
    });
  }


  get filteredPolicies(): any[] {
    return this.policies.filter(p =>
      (!this.appliedType || p.type === Number(this.appliedType)) &&
      (!this.appliedStatus || p.status === Number(this.appliedStatus))
    );
  }
}
