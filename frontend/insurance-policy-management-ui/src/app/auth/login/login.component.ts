import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/core/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  email = '';
  password = '';
  error = '';

  constructor(private auth: AuthService, private router: Router) {}

  login() {
    this.auth.login(this.email, this.password).subscribe({
    next: res => {
      this.auth.saveToken(res.token);
      const role = this.auth.role;

      if (role === 'ADMIN') {
      this.router.navigate(['/admin']);
      } else {
      this.router.navigate(['/client']);
      }

    },
    error: () => {
      this.error = 'Invalid credentials';
    }
  });
  }
}
