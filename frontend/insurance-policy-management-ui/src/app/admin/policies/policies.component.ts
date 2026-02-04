import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { PolicyService } from 'src/app/core/services/policy.service';

@Component({
  selector: 'app-policies',
  templateUrl: './policies.component.html',
  styleUrls: ['./policies.component.scss']
})
export class PoliciesComponent implements OnInit {

  policies: any[] = [];

  // filtros
  selectedType: string = '';
  selectedStatus: string = '';

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

  // filtro calculado
  get filteredPolicies(): any[] {
    return this.policies.filter(p =>
      (!this.selectedType || p.type === this.selectedType) &&
      (!this.selectedStatus || p.status === this.selectedStatus)
    );
  }
}
