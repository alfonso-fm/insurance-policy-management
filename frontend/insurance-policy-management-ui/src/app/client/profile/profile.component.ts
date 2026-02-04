import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { UserService } from '../../core/services/user.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html'
})
export class ProfileComponent implements OnInit {
  form = this.fb.group({
    address: ['', Validators.required],
    phone: ['', Validators.required]
  });

  constructor(
    private fb: FormBuilder,
    private userService: UserService
  ) {}

  ngOnInit(): void {
    this.userService.getProfile().subscribe(profile => {
      this.form.patchValue(profile);
    });
  }

  save() {
    if (this.form.invalid) return;

    this.userService.updateProfile(this.form.value).subscribe(() => {
      alert('Profile updated');
    });
  }
}

