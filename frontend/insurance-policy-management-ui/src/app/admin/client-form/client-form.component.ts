import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ClientService } from '../../core/services/client.service';

@Component({
  selector: 'app-client-form',
  templateUrl: './client-form.component.html'
})
export class ClientFormComponent implements OnInit {
  clientId?: string;

  form = this.fb.group({
    NumericId: ['', [Validators.required, Validators.pattern(/^\d{10}$/)]],
    Name: ['', [Validators.required, Validators.pattern(/^[a-zA-Z\s]+$/)]],
    email: ['', [Validators.required, Validators.email]],
    phone: ['', Validators.required]
  });

  constructor(
    private fb: FormBuilder,
    private service: ClientService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.clientId = this.route.snapshot.paramMap.get('id') ?? undefined;

    if (this.clientId) {
      this.service.getAll().subscribe(clients => {
        const client = clients.find(c => c.id === this.clientId);
        if (client) {
          this.form.patchValue(client);
          this.form.controls.NumericId.disable();
        }
      });
    }
  }

  save() {
    if (this.form.invalid) return;

    const request = this.clientId
      ? this.service.update(this.clientId, this.form.getRawValue())
      : this.service.create(this.form.value);

    request.subscribe(() => {
      this.router.navigate(['/admin/clients']);
    });
  }
}

