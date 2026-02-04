import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { PolicyService } from '../../core/services/policy.service';
import { ClientService } from '../../core/services/client.service';

@Component({
  selector: 'app-policy-form',
  templateUrl: './policy-form.component.html'
})
export class PolicyFormComponent implements OnInit {

  policyId?: string;
  clients: any[] = [];

  form = this.fb.group({
    clientId: ['', Validators.required],
    type: ['', Validators.required],
    startDate: ['', Validators.required],
    endDate: ['', Validators.required],
    amount: ['', [Validators.required, Validators.min(1)]]
  });

  constructor(
    private fb: FormBuilder,
    private policyService: PolicyService,
    private clientService: ClientService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.policyId = this.route.snapshot.paramMap.get('id') ?? undefined;

    // cargar clientes para el combo
    this.clientService.getAll().subscribe(data => {
      this.clients = data;
    });

    // si es edición
    if (this.policyId) {
      this.policyService.getById(this.policyId).subscribe(policy => {
        this.form.patchValue({
          clientId: policy.clientId,
          type: policy.type,
          startDate: policy.startDate,
          endDate: policy.endDate,
          amount: policy.amount
        });
      });
    }
  }

  save(): void {
    if (this.form.invalid) return;

    const request = this.policyId
      ? this.policyService.update(this.policyId, this.form.value)
      : this.policyService.create(this.form.value);

    request.subscribe(() => {
      this.router.navigate(['/admin/policies']);
    });
  }
}

