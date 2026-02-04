import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AdminRoutingModule } from './admin-routing.module';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ClientsComponent } from './clients/clients.component';
import { ClientFormComponent } from './client-form/client-form.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PoliciesComponent } from './policies/policies.component';
import { PolicyFormComponent } from './policy-form/policy-form.component';


@NgModule({
  declarations: [
    DashboardComponent,
    ClientsComponent,
    ClientFormComponent,
    PoliciesComponent,
    PolicyFormComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    AdminRoutingModule
  ]
})
export class AdminModule { }
