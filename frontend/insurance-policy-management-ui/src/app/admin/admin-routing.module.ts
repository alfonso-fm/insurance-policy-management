import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ClientsComponent } from './clients/clients.component';
import { ClientFormComponent } from './client-form/client-form.component';
import { PoliciesComponent } from './policies/policies.component';
import { PolicyFormComponent } from './policy-form/policy-form.component';

const routes: Routes = [
  { path: '', component: DashboardComponent },
  { path: 'clients', component: ClientsComponent },
  { path: 'clients/new', component: ClientFormComponent },
  { path: 'clients/edit/:id', component: ClientFormComponent },
  { path: 'policies', component: PoliciesComponent },
  { path: 'policies/new', component: PolicyFormComponent },
  { path: 'policies/edit/:id', component: PolicyFormComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
