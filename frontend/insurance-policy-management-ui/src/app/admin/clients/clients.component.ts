import { Component, OnInit } from '@angular/core';
import { ClientService } from '../../core/services/client.service';

@Component({
  selector: 'app-clients',
  styleUrls: ['./clients.component.scss'],
  templateUrl: './clients.component.html'
})
export class ClientsComponent implements OnInit {
  clients: any[] = [];

  constructor(private clientService: ClientService) {}

  ngOnInit(): void {
    this.loadClients();
  }

  loadClients() {
    this.clientService.getAll().subscribe(data => {
      this.clients = data;
    });
  }

  deleteClient(id: string) {
    if (!confirm('Are you sure you want to delete this client?')) return;

    this.clientService.delete(id).subscribe(() => {
      this.loadClients();
    });
  }
  filter = '';

  get filteredClients() {
    return this.clients.filter(c =>
      (c.fullName ?? '').toLowerCase().includes(this.filter.toLowerCase()) ||
      (c.email ?? '').toLowerCase().includes(this.filter.toLowerCase()) ||
      c.NumericId.includes(this.filter)
    );
  }

  trackById(_: number, client: any) {
    return client.id;
  }
}

