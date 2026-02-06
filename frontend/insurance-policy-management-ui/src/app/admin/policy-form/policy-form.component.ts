import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { PolicyService } from '../../core/services/policy.service';
import { ClientService } from '../../core/services/client.service';
import { dateRangeValidator } from 'src/app/core/validators/date-range.validator';

@Component({
  selector: 'app-policy-form',
  styleUrls: ['./policy-form.component.scss'],
  templateUrl: './policy-form.component.html'
})
export class PolicyFormComponent implements OnInit {

  policyId?: string;
  isEditMode = false;

  clients: any[] = [];

  form = this.fb.group({
    id: [],
    status: [],
    clientId: ['', Validators.required],
    type: ['', Validators.required],
    validityStartDate: ['', Validators.required],
    validityEndDate: ['', Validators.required],
    amount: ['', [Validators.required, Validators.min(1)]]
  },
  {
    validators: dateRangeValidator('validityStartDate', 'validityEndDate')
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
    this.isEditMode = !!this.policyId;

    // cargar clientes
    this.clientService.getAll().subscribe(data => {
      this.clients = data ?? [];
    });

    // si es edición
    if (this.isEditMode && this.policyId) {
      this.policyService.getById(this.policyId).subscribe(policy => {
        this.form.patchValue({
          clientId: policy.clientId,
          id: policy.id,
          type: policy.type,
          validityStartDate: this.toDateInputValue(policy.validityStartDate),
          validityEndDate: this.toDateInputValue(policy.validityEndDate),
          amount: policy.amount,
          status: policy.status
        });

        // 🔒 Bloquear campos NO editables
        this.form.get('clientId')?.disable();
        this.form.get('type')?.disable();
        this.form.get('id')?.disable();
      });
    }
  }

  private toDateInputValue(date: string | Date): string {
    const d = new Date(date);
    return d.toISOString().split('T')[0];
  }

  isInvalid(controlName: string): boolean {
  const control = this.form.get(controlName);
  return !!(
    control &&
    control.invalid &&
    (control.touched || this.submitted)
  );
}
submitted = false;
  save(): void {
    this.submitted = true;
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    //const payload = this.form.getRawValue(); // incluye campos deshabilitados
    const raw = this.form.getRawValue();

    const payload = {
      clientId: raw.clientId,
      type: Number(raw.type),
      validityStartDate: raw.validityStartDate,
      validityEndDate: raw.validityEndDate,
      amount: raw.amount,
      status: raw.status
    };

    const request = this.isEditMode && this.policyId
      ? this.policyService.update(this.policyId, payload)
      : this.policyService.create(payload);

    request.subscribe(() => {
      this.router.navigate(['/admin/policies']);
    });
  }
  get isDateRangeInvalid(): boolean {
    return !!this.form.errors?.['dateRangeInvalid'];
  }
}
